# locustfile_microservices.py
# Load test for the microservices architecture.
# Each service runs on its own host, so we use three separate HttpUser subclasses
# and distribute virtual users across them.
#
# Configure the three hosts via environment variables:
#   USERS_HOST    = https://<users-service>.azurecontainerapps.io
#   PRODUCTS_HOST = https://<products-service>.azurecontainerapps.io
#   ORDERS_HOST   = https://<orders-service>.azurecontainerapps.io
#
# Usage:
#   locust -f locustfile_microservices.py --headless --run-time 8m \
#          --html reports/microservices_50.html \
#          TARGET_USERS=50

import os
import random
from locust import HttpUser, task, between
from shape import ThreePhaseShape
from tasks import create_user, create_product, create_order, get_all, get_by_id

USERS_HOST    = os.getenv("USERS_HOST",    "http://localhost:5257")
PRODUCTS_HOST = os.getenv("PRODUCTS_HOST", "http://localhost:5229")
ORDERS_HOST   = os.getenv("ORDERS_HOST",   "http://localhost:5264")

# Shared cross-service ID pools (orders need user + product IDs)
_user_ids:    list[str] = []
_product_ids: list[str] = []
_order_ids:   list[str] = []


class UsersServiceUser(HttpUser):
    host      = USERS_HOST
    wait_time = between(0.5, 2.0)
    weight    = 2  # relative share of virtual users

    def on_start(self):
        uid = create_user(self.client)
        if uid:
            _user_ids.append(uid)

    @task(5)
    def list_users(self):
        get_all(self.client, "/api/users", "users")

    @task(3)
    def get_user_by_id(self):
        if _user_ids:
            get_by_id(self.client, "/api/users", random.choice(_user_ids), "users")

    @task(1)
    def create_user(self):
        uid = create_user(self.client)
        if uid:
            _user_ids.append(uid)


class ProductsServiceUser(HttpUser):
    host      = PRODUCTS_HOST
    wait_time = between(0.5, 2.0)
    weight    = 3  # products are read more often

    def on_start(self):
        pid = create_product(self.client)
        if pid:
            _product_ids.append(pid)

    @task(5)
    def list_products(self):
        get_all(self.client, "/api/products", "products")

    @task(3)
    def get_product_by_id(self):
        if _product_ids:
            get_by_id(self.client, "/api/products", random.choice(_product_ids), "products")

    @task(1)
    def create_product(self):
        pid = create_product(self.client)
        if pid:
            _product_ids.append(pid)


class OrdersServiceUser(HttpUser):
    host      = ORDERS_HOST
    wait_time = between(0.5, 2.0)
    weight    = 2

    def on_start(self):
        # Seed local pools if empty (orders depend on users + products)
        if not _user_ids:
            import requests
            r = requests.get(f"{USERS_HOST}/api/users")
            if r.ok:
                _user_ids.extend([u["id"] for u in r.json()])
        if not _product_ids:
            import requests
            r = requests.get(f"{PRODUCTS_HOST}/api/products")
            if r.ok:
                _product_ids.extend([p["id"] for p in r.json()])

    @task(5)
    def list_orders(self):
        get_all(self.client, "/api/orders", "orders")

    @task(3)
    def get_order_by_id(self):
        if _order_ids:
            get_by_id(self.client, "/api/orders", random.choice(_order_ids), "orders")

    @task(2)
    def create_order(self):
        if _user_ids and _product_ids:
            oid = create_order(
                self.client,
                random.choice(_user_ids),
                random.choice(_product_ids)
            )
            if oid:
                _order_ids.append(oid)
