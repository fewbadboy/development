# document

## 临时表

只在当前连接可见，关闭连接时，自动删除并释放所有空间

```shell
CREATE TEMPORARY TABLE
```

## 修改密码

```sql
set password for root@localhost = password('admin')
```

## LEFT JOIN

返回左表的所有记录，即使右表中没有匹配的记录（右表没找到于左表匹配的记录时，右表列显示为 NULL）。
条件过滤最好都在 ON 条件中进行过滤，如在 WHERE 中过滤，会转换成 INNER JOIN。

## 排序

需要有序的输出记录**必须**使用 ORDER BY 子句

## 分析查询

```sql
EXPLAIN SELECT * FROM records WHERE id BETWEEN 80001 AND 90000;
```

## 优化

1. 为 WHERE 子句， JOIN 条件， ORDER BY 和 GROUP BY 子句中常用列创建索引
2. 覆盖索引(联合索引包含所有要查询的列)
3. 分页优化：大数据集分页 LIMIT 和 OFFSET 导致性能下降，可以使用索引列或主键列优化
    - `SELECT * FROM table WHERE id > ? ORDER BY id LIMIT 10`
4. 使用 EXISTS 代替 IN 检查存在性条件
5. 仅选择必要的列
6. 启用缓存减少频繁的相同查询
7. EXPLAIN 分析查询
