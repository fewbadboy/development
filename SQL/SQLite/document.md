# document

[sqlite](https://www.sqlite.org/index.html)

SQLite 不对字符串、BLOB 或数值的长度施加任何长度限制

## introduction

### 数据类型

- NULL
- INTEGER 有符号整数，根据值的大小存储在最大 8 字节中
- REAL 8 byte IEEE 浮点数
- TEXT 文本字符串，使用数据库编码
- BLOB 二进制数据

布尔值存储为整数 0(false) 和 1(true)
日期时间类型存储为 TEXT、REAL 或 INTEGER 值

### 聚合函数

- min/max
- avg
- sum/total
- count
- group_concat

### 内置函数

- coalesce(x,y,...) 返回第一个非 NULL 参数的副本。可以用来设置默认值用最后一个参数
- ifnull(x,y) coalesce函数俩参数的表现形式
- iif(x,y,z) 如果 X 为真，则值为 Y，否则为 Z
- like(x,y,z) Y LIKE X [ESCAPE Z]，like('_三%', name) `_` 标识一个字符
- trim
- lower/upper
- sign/round/random
