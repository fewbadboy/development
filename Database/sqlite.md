# SQLite

## 使用场景

Flutter
本地缓存
Electron
React Native

## 事务

批量写入一定使用事务

```sql
BEGIN TRANSACTION

COMMIT
```

## 类型

动态类型

## 分页

数据多 `OFFSET` 会很慢

更好的方式

```sql
WHERE id > last_id
```

## 索引

## 缓存策略

## 查询

只取需要的字段避免 `SELECT *`

## 临时表

几万到几十万数据

性能要求不高的场景，可以定时刷新
