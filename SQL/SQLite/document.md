# document

SQLite 不对字符串、BLOB 或数值的长度施加任何长度限制

## 介绍

### 概念

Trigger: 一些自动执行的 SQL 语句，它会在某个特定的数据库事件发生时触发执行
View: 一个虚拟表，本身不存储只是一个 SQL 查询结果
Virtual Table: 是一种扩展功能的机制，允许用户创建能够访问外部数据源（如文件、外部数据库、网络等）的表

### 数据类型

- NULL
- INTEGER 有符号整数，根据值的大小存储在最大 8 字节中
- REAL 8 字节浮点数字
- TEXT 文本字符串，使用数据库编码
- BLOB 二进制数据

布尔值存储为整数 0(false) 和 1(true)
日期时间类型存储为 TEXT、REAL 或 INTEGER 值

### 运算符

- AND
- OR
- IN
- NOT IN
- BETWEEN
- EXISTS
- LIKE % 代表零个、一个或多个数字或字符 \_ 代表一个单一的数字或字符
- GLOB 大小写敏感，通配符遵循 UNIX 语法(? 代表一个单一的数字或字符)
- IS
- IS NULL
- IS NOT NULL
- UNIQUE

### 聚合函数

### 内置函数

## SQL Clause

### WHERE

- NOT
- BETWEEN ... AND
- IN
- IS NULL
- LIKE
- GLOB: 区分大小写，支持 UNIX 通配符，不支持转义
- MATCH: 全文搜索

### DISTINCT

- 保留一行 Null 值

### ORDER BY

- 排序默认 ASC
- NULLS FIRST/LAST: 可选， Null 值出现的顺序

### LIMIT OFFSET

### GROUP BY

### HAVING

指定 GROUP BY 的搜索条件

### INNER/LEFT/RIGHT/CROSS JOIN

## UNION/INTERSECT

## EXCEPT

## EXISTS

## INSERT/REPLACE INTO table (..) VALUES (..)

## RETURNING

## BEGIN TRANSACTION...COMMIT, ROLLBACK
