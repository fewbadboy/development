# Mongo

## _id

自动索引，驱动生成

`_id` 是 ObjectId, 4 byte 时间戳、 5 byte 机器+ 进程、 3 byte 计数器

## 文档解构

扁平化设计

## 分页

游标分页(从索引位置开始扫): 用上一页的最后一条记录 ID 作为下一页的起点，排序必须稳定，不能跳页，适合无限滚动、时间线、日志、消息流、Feed类系统(不支持任意页码跳转，只支持按顺序跳转)

```text
GET /user?last_id=null&limit=20
```

第二页才开始使用，第一页无需条件

```js
const query = {}

if (last_id) {
  query._id = { $lt: ObjectId(lastId) }
}

db.user.find(query)
.sort({create_time: -1, _id: -1})
.limit(21)
.explain("executionStats") // 查看是否走索引 IXSCAN 索引 COLLSCAN 全表扫描

/**
 * 返回长度 21 则有下一页
 * 同时返回 
 * {
 *    next_cursor: {
 *      last_time: ,
 *      last_id: 
 *    }, 
 *    has_more: true
 * }
 */
```

`skip(n)`: 扫描 n 条,丢弃，然会再返回 `limit` 的条数(skip 越大越慢)

## 索引

占内存、写入慢、索引维护
