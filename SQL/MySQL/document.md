# document

## introduction

## 登录

```shell
# 123456
mysql -u root -p
```

## 创建/删除 数据库

```shell
# SHOW DATABASES / TABLES

# 创建
CREATE DATABASE IF NOT EXISTS rookie
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

# 删除
DROP DATABASE IF EXISTS rookie;

# 选择数据库
use rookie;
```

## 数据类型

- 数值
- 日期和时间
- 字符串
- 枚举和集合
- 空间数据

## 创建 / 删除 表

```shell
# 创建
CREATE TABLE user (
  id INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(20) MOT NULL,
  birthday DATE,
  UNIQUE(name)
)

# 删除
DROP TABLE IF EXISTS rookie;
```

## 插入数据

```shell
INSERT INTO user(name) values ('html')
```

## 查询

```shell
# AND OR LIKE IN NOT 
# BETWEEN ... AND
# IS [NOT] NULL
# 排序默认 升序
SELECT column1, ...
FROM table_name
# REGEXP 'pattern'
[WHERE condition]
[ORDER BY column_name [ASC | DESC]]
[GROUP BY column_name]
[LIMIT number];
```

## 更新数据

```shell
UPDATE user SET column1 = value1, ... WHERE condition;
```

## 删除数据

```shell
DELETE FROM user WHERE condition;
```

## UNION / UNION ALL

UNION: 连接结果到一个集合，并去除重复的行
UNION ALL: 不会去除重复的行

## INNER JOIN / LEFT JOIN / RIGHT JOIN ... ON

## 事务

```shell
# BEGIN
# SAVEPOINT
# ROLLBACK TO SAVEPOINT
START TRANSACTION;

UPDATE accounts SET balance = balance - 100 WHERE user_id = 1;
UPDATE accounts SET balance = balance + 100 WHERE user_id = 2;

IF (条件) THEN
    COMMIT;
ELSE
    ROLLBACK;
END IF;
```

## 修改表

```shell
# 添加
ALERT TABLE user ADD COLUMN salary DECIMAL(10,2); 

# 修改值
ALERT TABLE user MODIFY COLUMN salary DECIMAL(10,2); 

# 修改列名
ALERT TABLE user CHANGE COLUMN salary all_salary DECIMAL(10,2); 

# 删除列
ALERT TABLE user DROP COLUMN salary; 

# 添加主键
ALERT TABLE user ADD PRIMARY KEY (id); 
```

## 索引

定期检查和重建索引: 写操作频繁的表，索引会碎片化，需要定期 `OPTIMIZE TABLE` 去维护
索引也会占用额外的存储空间

```shell
# CREATE / DROP 
CREATE INDEX index_name ON TABLE user (name, age DESC)
```

- 避免使用函数或表达式(索引会失效)
- LIKE 前缀通配符的操作，索引会失效， 如 LIKE '%abc'

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
