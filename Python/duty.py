# Staff {
#     id                # 唯一标识
#     name
#     maxConsecutive    # 最多连续上班天数
#     maxNightPerMonth  # 每月夜班上限
#     totalDays         # 已排班总天数   ← 动态更新
#     totalNights       # 已排夜班总数   ← 动态更新
#     consecutiveDays   # 连续上班计数   ← 动态更新
#     lastShiftDate     # 上一次班日期   ← 动态更新
#     lastShiftType     # 上一次班类型   ← 动态更新
# }

# Assignment { date, shiftType, staffId }      # 排班结果行
# Leave      { staffId, startDate, endDate }   # 请假区间

# SHIFT_TYPES = ["DAY", "NIGHT"]
# REST_AFTER_NIGHT = 1        # 夜班后至少休息 1 天
# DAY_DEMAND   = 1            # 白班需要几人，可按需改
# NIGHT_DEMAND = 1            # 夜班需要几人，可按需改

# 一次遍历年份：按日期 → 班次双重循环生成排班
# 实时公平排序：每次选人时，优先挑“已排得最少的人”，从而自然逼近均衡
# 硬约束过滤：请假、夜班后休息、连上限等
# 软约束打分：若有多人同分，用“夜班少→连班短→随机”
# 这是“贪心 + 动态优先级”范式，能快速出一个可行且相对公平的排班；若要数学最优，请替换为 ILP 或 CP‑SAT 求解器

# 伪代码
# function generate_year_roster(staffList, year, leaves):
#     roster = []                              # Assignment[]
#     dateList = list_all_dates_of_year(year)  # ① 按时间轴遍历一年

#     for date in dateList:
#         for shift in SHIFT_TYPES:            # ② 每天两班
#             demand = DAY_DEMAND if shift=="DAY" else NIGHT_DEMAND
#             for k in 1 .. demand:            # ③ 若一班需多人
#                 pool = [p for p in staffList if is_eligible(p, date, shift, leaves)]
#                 if pool is empty:
#                     raise "No feasible staff for " + date + " " + shift
#                 sort(pool, key = fairness_key(shift))   # ④ 实时公平度排序
#                 chosen = pool[0]                        # ⑤ 贪心挑首位
#                 make_assignment(chosen, date, shift, roster)
#     return roster

# function is_eligible(staff, date, shift, leaves):
#     # A. 是否在请假
#     if in_leave(staff.id, date, leaves): return false

#     # B. 连班上限
#     if staff.consecDays >= staff.maxConsecutive: return false

#     # C. 夜班后休息
#     if staff.lastShiftType == "NIGHT" and days_between(staff.lastShiftDate, date) < REST_AFTER_NIGHT + 1:
#         return false

#     # D. 夜班上限（按月）
#     if shift == "NIGHT":
#         nightsThisMonth = count_night_in_month(staff, date.month)
#         if nightsThisMonth >= staff.maxNightPerMonth: return false

#     return true

# function fairness_key(shift):
#     # 返回一个比较键 (tuple) 用于 sort
#     # 1) 先按全年总班天数，从少到多
#     # 2) 若是夜班，再按全年夜班数，从少到多
#     # 3) 再按当前连续班数，从少到多
#     # 4) 最后随机打散，避免长尾同分
#     return lambda p: (
#         p.totalDays,
#         p.totalNights if shift == "NIGHT" else 0,
#         p.consecDays,
#         random()            # tie‑break
#     )

# function make_assignment(staff, date, shift, roster):
#     roster.append( Assignment(date, shift, staff.id) )
#     # 动态指标更新 ↓
#     staff.totalDays   += 1
#     if shift == "NIGHT": staff.totalNights += 1
#     if staff.lastShiftDate == date - 1_day:
#         staff.consecDays += 1
#     else:
#         staff.consecDays  = 1
#     staff.lastShiftDate  = date
#     staff.lastShiftType  = shift

# staffs = init_staff_list(x)          # 创建 x 名员工，填好约束
# leaves  = [Leave(2, '2025-05-01','2025-05-05'), ...]  # 假期
# roster  = generate_year_roster(staffs, 2026, leaves)  # 得到 1 年排班
# output_csv(roster, "roster_2026.csv")

from __future__ import annotations

"""Roster generator – array‑based staff list version (today → end‑of‑year).

* 直接在源代码顶部维护：
    NAMES  = ["周", "吴", "郑", "王"]   # 2‑20 名员工
    YEAR   = 2025                           # 目标年份

* **新特性** 🆕  若 `YEAR == date.today().year`，只排 **今天起到 12‑31**，
  否则排整年 1‑1 → 12‑31。

运行：
    python roster.py
生成 `roster_<YEAR>.csv` 并打印公平度统计。

Python ≥ 3.10，仅用标准库。
"""

from dataclasses import dataclass, field
from datetime import date, timedelta
from enum import Enum, auto
from collections import defaultdict
from typing import List, Dict, Set
import math
import csv
import random

# ───────────────────── 用户可编辑区域 ──────────────────────
NAMES = ["周", "吴", "郑", "王"]  # 员工姓名列表（2 – 20 人）
YEAR = 2025  # 目标年份

# 假期示例：[(姓名, "YYYY‑MM‑DD", "YYYY‑MM‑DD"), …]
LEAVE_RAW: list[tuple[str, str, str]] = [
    # ("吴", "2026-05-01", "2026-05-05"),
]
# ─────────────────────────────────────────────────────────


# ─────────────────────────── Domain model ────────────────────────────


class Shift(Enum):
    DAY = auto()
    NIGHT = auto()

    @property
    def demand(self) -> int:  # 可独立配置各班需求
        return 1  # 默认日夜各 1 人


@dataclass(slots=True)
class Staff:
    id: int
    name: str
    max_consecutive: int
    max_night_per_month: int
    # dynamic stats
    total_days: int = 0
    total_nights: int = 0
    consec_days: int = 0
    last_shift_date: date | None = None
    last_shift_type: Shift | None = None
    nights_by_month: Dict[int, int] = field(default_factory=lambda: defaultdict(int))


@dataclass(slots=True, frozen=True)
class Leave:
    staff_id: int
    start: date
    end: date  # inclusive


@dataclass(slots=True)
class Assignment:
    day: date
    shift: Shift
    staff_id: int


# ───────────────────────────── Helpers ───────────────────────────────


def date_range(start: date, end: date):
    current = start
    one = timedelta(days=1)
    while current <= end:
        yield current
        current += one


class LeaveIndex:
    def __init__(self, leaves: List[Leave]):
        self._idx: Dict[int, Set[date]] = defaultdict(set)
        for lv in leaves:
            for d in date_range(lv.start, lv.end):
                self._idx[lv.staff_id].add(d)

    def on_leave(self, staff_id: int, d: date) -> bool:
        return d in self._idx.get(staff_id, set())


# ───────────────────── Eligibility & scoring key ─────────────────────

REST_AFTER_NIGHT = 1  # 必休天数


def eligible(s: Staff, d: date, sh: Shift, leave_idx: LeaveIndex) -> bool:
    if leave_idx.on_leave(s.id, d):
        return False
    if s.consec_days >= s.max_consecutive:
        return False
    if (
        s.last_shift_type is Shift.NIGHT
        and s.last_shift_date
        and (d - s.last_shift_date).days <= REST_AFTER_NIGHT
    ):
        return False
    if sh is Shift.NIGHT and s.nights_by_month[d.month] >= s.max_night_per_month:
        return False
    return True


def score_fn(alpha: float, beta: float, gamma: float, sh: Shift):
    def _score(s: Staff):
        return (
            alpha * s.total_days
            + beta * (s.total_nights if sh is Shift.NIGHT else 0)
            + gamma * s.consec_days
            + random.random() * 1e-3  # tiny tie‑break noise
        )

    return _score


# ───────────────────────── Roster generator ──────────────────────────


def assign(staff: Staff, day: date, sh: Shift, roster: List[Assignment]):
    roster.append(Assignment(day, sh, staff.id))

    staff.total_days += 1
    if sh is Shift.NIGHT:
        staff.total_nights += 1
        staff.nights_by_month[day.month] += 1

    if staff.last_shift_date and (day - staff.last_shift_date).days == 1:
        staff.consec_days += 1
    else:
        staff.consec_days = 1

    staff.last_shift_date = day
    staff.last_shift_type = sh


def night_cap(staff_cnt: int, night_demand: int = 1, margin: int = 1) -> int:
    """Minimal per‑person night cap to cover any 31‑day month."""
    return min(31, math.ceil(31 * night_demand / staff_cnt) + margin)


def build_staff(names: List[str], max_consecutive: int = 5) -> List[Staff]:
    cap = night_cap(len(names))
    return [
        Staff(
            id=i + 1, name=n, max_consecutive=max_consecutive, max_night_per_month=cap
        )
        for i, n in enumerate(names)
    ]


def generate_roster_range(
    staff: List[Staff],
    start: date,
    end: date,
    leaves: List[Leave],
    *,
    alpha=1.0,
    beta=2.0,
    gamma=0.2,
) -> List[Assignment]:
    leave_idx = LeaveIndex(leaves)
    roster: List[Assignment] = []

    random.shuffle(staff)  # baseline randomness

    for d in date_range(start, end):
        for sh in Shift:
            for _ in range(sh.demand):
                pool = [p for p in staff if eligible(p, d, sh, leave_idx)]
                if not pool:
                    raise RuntimeError(f"No feasible staff for {d} {sh.name}")
                chosen = min(pool, key=score_fn(alpha, beta, gamma, sh))
                assign(chosen, d, sh, roster)
    return roster


# ──────────────────────────── Statistics ─────────────────────────────


def print_stats(staff: List[Staff]):
    days = [s.total_days for s in staff]
    nights = [s.total_nights for s in staff]
    var_days = sum((x - sum(days) / len(days)) ** 2 for x in days) / len(days)
    var_nights = sum((x - sum(nights) / len(nights)) ** 2 for x in nights) / len(nights)

    print("\n=== Fairness stats ===")
    for s in staff:
        print(f"{s.name}: days={s.total_days}, nights={s.total_nights}")
    print(f"Variance – days: {var_days:.2f}, nights: {var_nights:.2f}\n")


# ────────────────────────────── Main ────────────────────────────────


def parse_leaves(
    raw: list[tuple[str, str, str]], staff_map: dict[str, int]
) -> List[Leave]:
    out: list[Leave] = []
    for name, s, e in raw:
        if name not in staff_map:
            raise ValueError(f"Leave refers to unknown staff '{name}'")
        out.append(
            Leave(
                staff_id=staff_map[name],
                start=date.fromisoformat(s),
                end=date.fromisoformat(e),
            )
        )
    return out


def main():
    if not 2 <= len(NAMES) <= 20:
        raise SystemExit("NAMES length must be between 2 and 20")

    staff = build_staff(NAMES)
    name_to_id = {s.name: s.id for s in staff}
    leaves = parse_leaves(LEAVE_RAW, name_to_id)

    today = date.today()
    if YEAR == today.year:
        start_date = today
    else:
        start_date = date(YEAR, 1, 1)
    end_date = date(YEAR, 12, 31)

    roster = generate_roster_range(staff, start_date, end_date, leaves)

    out_file = f"roster_{YEAR}.csv"
    with open(out_file, "w", newline="", encoding="utf‑8") as fh:
        writer = csv.writer(fh)
        writer.writerow(["date", "shift", "staff_id"])
        for a in roster:
            writer.writerow([a.day.isoformat(), a.shift.name, a.staff_id])

    print(f"✅ {out_file} generated – rows: {len(roster)}")
    print_stats(staff)


if __name__ == "__main__":
    main()
