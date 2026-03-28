# shape.py
# LoadTestShape implementing the three-phase test plan:
#   - Warmup  : 0 → target_users over RAMP_UP_SECONDS
#   - Load    : hold at target_users for HOLD_SECONDS
#   - Cooldown: target_users → 0 over RAMP_DOWN_SECONDS
#
# Configure TARGET_USERS via environment variable or edit the constant below.
# Run example:
#   TARGET_USERS=100 locust -f locustfile_microservices.py --headless --run-time 8m

import os
from locust import LoadTestShape

TARGET_USERS     = int(os.getenv("TARGET_USERS", 50))
SPAWN_RATE       = int(os.getenv("SPAWN_RATE",   10))   # users per second
RAMP_UP_SECONDS  = 120   # 2 minutes
HOLD_SECONDS     = 300   # 5 minutes
RAMP_DOWN_SECONDS = 60   # 1 minute

TOTAL_DURATION = RAMP_UP_SECONDS + HOLD_SECONDS + RAMP_DOWN_SECONDS  # 480 s = 8 min


class ThreePhaseShape(LoadTestShape):
    """
    Phase 1 – Warmup  (0 – 120 s)  : ramp from 0 to TARGET_USERS
    Phase 2 – Load    (120 – 420 s) : hold at TARGET_USERS
    Phase 3 – Cooldown(420 – 480 s) : ramp from TARGET_USERS to 0
    """

    def tick(self):
        elapsed = self.get_current_user_count() and self.runner.stats.total.start_time
        run_time = self.get_run_time()

        if run_time > TOTAL_DURATION:
            return None  # stop the test

        if run_time < RAMP_UP_SECONDS:
            # Warmup: linear ramp-up
            progress = run_time / RAMP_UP_SECONDS
            users = max(1, int(TARGET_USERS * progress))
            return (users, SPAWN_RATE)

        if run_time < RAMP_UP_SECONDS + HOLD_SECONDS:
            # Load: hold at target
            return (TARGET_USERS, SPAWN_RATE)

        # Cooldown: linear ramp-down
        cooldown_elapsed = run_time - RAMP_UP_SECONDS - HOLD_SECONDS
        progress = cooldown_elapsed / RAMP_DOWN_SECONDS
        users = max(0, int(TARGET_USERS * (1 - progress)))
        return (users, SPAWN_RATE)
