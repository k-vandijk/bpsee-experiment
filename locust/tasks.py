# tasks.py
# Shared Locust task logic for both monolithic and microservices tests.
# Each HttpUser subclass calls these helpers with its own client and base paths.

import random
from locust import task, between


def create_user(client, base="/api/users"):
    payload = {
        "name": f"User_{random.randint(1, 999999)}",
        "email": f"user_{random.randint(1, 999999)}@test.com"
    }
    with client.post(base, json=payload, catch_response=True) as r:
        if r.status_code == 201:
            r.success()
            return r.json().get("id")
        else:
            r.failure(f"POST {base} returned {r.status_code}")
    return None


def create_product(client, base="/api/products"):
    payload = {
        "name": f"Product_{random.randint(1, 999999)}",
        "description": "Load test product",
        "price": round(random.uniform(1.0, 999.99), 2),
        "stock": random.randint(1, 1000)
    }
    with client.post(base, json=payload, catch_response=True) as r:
        if r.status_code == 201:
            r.success()
            return r.json().get("id")
        else:
            r.failure(f"POST {base} returned {r.status_code}")
    return None


def create_order(client, user_id, product_id, base="/api/orders"):
    payload = {
        "userId": user_id,
        "lines": [
            {
                "productId": product_id,
                "quantity": random.randint(1, 5),
                "unitPrice": round(random.uniform(1.0, 99.99), 2)
            }
        ]
    }
    with client.post(base, json=payload, catch_response=True) as r:
        if r.status_code == 201:
            r.success()
            return r.json().get("id")
        else:
            r.failure(f"POST {base} returned {r.status_code}")
    return None


def get_all(client, base, name):
    with client.get(base, name=f"GET {name}", catch_response=True) as r:
        if r.status_code == 200:
            r.success()
            items = r.json()
            return [i.get("id") for i in items if i.get("id")] if items else []
        else:
            r.failure(f"GET {base} returned {r.status_code}")
    return []


def get_by_id(client, base, item_id, name):
    with client.get(f"{base}/{item_id}", name=f"GET {name}/{{id}}", catch_response=True) as r:
        if r.status_code in (200, 404):
            r.success()
        else:
            r.failure(f"GET {base}/{{id}} returned {r.status_code}")
