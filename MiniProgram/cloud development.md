# cloud development

## 存储

```js
wx.cloud.uploadFile()
```

## 定时触发器

```js
{
  "triggers": [
    {
      // name: 触发器的名字，规则见下方说明
      "name": "myTrigger",
      // type: 触发器类型，目前仅支持 timer (即 定时触发器)
      "type": "timer",
      // 秒(0-59) 分钟 小时(0-23) 日(1-31) 月(1-12) 星期(0-6) 年
      "config": "0 0 2 1 * * *"
    }
  ]
}
```

## 数据库

服务端时间：`db.serverDate({ offset: 0 })`

```js
/**
 * 复合索引
 * 最左匹配原则：查询条件必须从复合索引的最左边字段开始，索引才能被有效利用
 * 
 * 针对俩索引顺序不一样的需要重新排序
 */
db.user.createIndex({
  id_card: 1,
  city: -1
})
```

### 表解构设计

- 主文档核心字段，子集合动态增加内容
- 适度冗余换性能
- 围绕查询设计(而不是围绕数据关系)

### 权限规则

示例：仅能修改自己的信息，(未登录 auth 为 空)

全局函数 `get()` 获取指定记录

- auth 用户登录信息
- doc 记录信息
- now 当前时间戳

```json
{
  "read": true,
  "write": "auth.openid === doc._openid",
  "create": true,
  "update": true,
  "delate": "get(`database.user.${id}`).isManager"
}
```

### 时区

默认时区 UTC+0, 设置 UTC+8 时，需要设置函数的环境变量 `TZ: Asia/Shanghai`

## 云函数示例

注意：
**排序的时候,为保证排序的稳定性和可靠性，必须加一个唯一属性属性字段，从而避免跳页、重复、丢失问题**
**数据库并不保证重复值之间的记录顺序**

```js
const cloud = require("wx-server-sdk");

cloud.init({
  env: cloud.DYNAMIC_CURRENT_ENV,
});
const db = cloud.database({
  throwOnNotFound: false, // 防止 doc.get 获取不到抛出异常
});

const _ = db.command;
const $ = db.command.aggregate;

const pageSize = 20;

/**
 * 执行计划查看
 * stage: COLLSCAN (Collection Scan 全表查询)
 *        IXSCAN (Index Scan 索引)
 *        FETCH (回表读取数据)
 *        SORT (内部排序)
 */
db.user.find({
  name: 'mongo'
}).explain()

/**
 * 数据库操作示例
 * @see {@link https://developers.weixin.qq.com/miniprogram/dev/wxcloudservice/wxcloud/reference-sdk-api/Cloud.database.html}
 **/

exports.main = async (event, context) => {
  const { pageIndex } = event;

  const { OPENID, APPID } = cloud.getWXContext();

  /**
   * 返回需要的字段提升查询速度
   */
};

/**
 * 小程序调用
 */
wx.cloud.callFunction({
  // 云函数函数名
  name: 'userList',
  // 传递云函数参数
  data: {
    pageIndex: 1
  }
})
```

### 查询

```js
const _ = db.command
const $ = db.command.aggregate // 聚合
const MAX_LIMIT = 100 // 云函数一次性获量最大数量，小程序端请求数据库时限制 20
db.collection('users').doc('id').get().then(({ data }) => {  })

db.collection('users').get().then(({ data }) => {  })
db.collection('users')
  .where({ age: _.gte(18).and(_.lt(45)) })
  .skip(0)
  .limit(MAX_LIMIT)
  .get()

/**
 * 优化: 
 * _id 索引默认升序
 * lastId 游标分页
 * 
 */

const { lastId, keyword } = event
const pageSize = 20
const condition = {}
if (keyword) {
  condition.title = db.RegExp({
    regexp: keyword,
    options: 'i'
  })
}
if (lastId) {
  condition._id = _.lt(lastId)
}
try {
  let query = db.collection('xxx')
    .where(condition)
    .orderBy('_id', 'desc')
    .limit(pageSize + 1)
  const res = await query.get()
  const list = res.data

  let hasMore = false
  if (list.length > pageSize) {
    hasMore = true
    list.pop()
  }

  return {
    hasMore,
    code: 200,
    data: list,
    lastId: list.length > 0 ? list[list.length - 1]._id : null
  }
} catch(e) {
  return {
    code: 500
    error: e
  }
}

/**
 * update 局部更新一个或多个
 * set 替换一个记录
 * 
 * 指令
 * inc 自增
 * mul 自乘
 * push\pop\shift\unshift
 */

db.collection('users').doc('id').remove() // 删除单个记录


/**
 * 聚合
 * 返回 { list： [{ _id, sales }] }
 *  */ 
await db.collection('users').aggregate()
  .match()
  .group({
    _id: '$group',
    avg: $.avg('$sales')
  })
  .project()
  .end()
```
