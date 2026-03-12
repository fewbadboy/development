# MySQL

## 分页

数据量大的系统都不用 `offset`, 都会改成 `cursor pagination`

下一页:(上一页时传递第一个数据相关信息，排序相反)

```sql
SELECT *
FROM post
WHERE id < last_id
ORDER BY id DESC
LIMIT 20
```

cursor pagination `O(limit)`、offset pagination `O(offset)` 差别非常大

针对那种前后几页的操作实现完全不是 offset 方式：
