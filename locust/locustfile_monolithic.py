# locustfile_monolithic.py
# Load test for the monolithic API (single host, all endpoints).
#
# Usage (headless, 3 runs automated via shell):
#   locust -f locustfile_monolithic.py --host https://<your-app>.azurewebsites.net \
#          --headless --run-time 8m --html reports/monolithic_50.html \
#          TARGET_USERS=50
#
# Or open the web UI:
#   locust -f locustfile_monolithic.py --host https://<your-app>.azurewebsites.net

import random
from locust import HttpUser, task, between, events
from shape import ThreePhaseShape
from tasks import create_user, create_product, create_order, get_all, get_by_id


class MonolithicUser(HttpUser):
    """
    Simulates a user hitting the monolithic API.
    Task weights mirror realistic traffic:
      - Reading (GET) is more frequent than writing (POST)
      - Orders reference real user/product IDs seeded during warmup
    """
    wait_time = between(0.5, 2.0)

    # Shared ID pools populated on first run to avoid 404s on GET-by-id
    _user_ids: list[str] = []
    _product_ids: list[str] = []
    _order_ids: list[str] = []

    def on_start(self):
        # Each new virtual user seeds one user and one product on start
        uid = create_user(self.client)
        if uid:
            MonolithicUser._user_ids.append(uid)
        pid = create_product(self.client)
        if pid:
            MonolithicUser._product_ids.append(pid)

    @task(5)
    def list_products(self):
        get_all(self.client, "/api/products", "products")

    @task(5)
    def list_users(self):
        get_all(self.client, "/api/users", "users")

    @task(5)
    def list_orders(self):
        get_all(self.client, "/api/orders", "orders")

    @task(3)
    def get_product_by_id(self):
        if MonolithicUser._product_ids:
            pid = random.choice(MonolithicUser._product_ids)
            get_by_id(self.client, "/api/products", pid, "products")

    @task(3)
    def get_user_by_id(self):
        if MonolithicUser._user_ids:
            uid = random.choice(MonolithicUser._user_ids)
            get_by_id(self.client, "/api/users", uid, "users")

    @task(3)
    def get_order_by_id(self):
        if MonolithicUser._order_ids:
            oid = random.choice(MonolithicUser._order_ids)
            get_by_id(self.client, "/api/orders", oid, "orders")

    @task(2)
    def create_order(self):
        if MonolithicUser._user_ids and MonolithicUser._product_ids:
            uid = random.choice(MonolithicUser._user_ids)
            pid = random.choice(MonolithicUser._product_ids)
            oid = create_order(self.client, uid, pid)
            if oid:
                MonolithicUser._order_ids.append(oid)

    @task(1)
    def create_product(self):
        pid = create_product(self.client)
        if pid:
            MonolithicUser._product_ids.append(pid)

    @task(1)
    def create_user(self):
        uid = create_user(self.client)
        if uid:
            MonolithicUser._user_ids.append(uid)
