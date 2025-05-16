# cloud development

## 窗口信息

```js
const {
  pixelRatio,
  screenWidth,
  windowWidth,
  statusBarHeight,
  safeAre: { left, right, top, bottom, width },
  screenTop
} = wx.getWindowInfo()
```

## 数据库

### 安全规则

- auth 用户登录信息 auth.openid 是用户的 openid
- doc 记录内容(当前操作的数据文档内容)
- now 当前时间的时间戳

```json
{
  "read": true,
  "write": "doc.openid === auth.openid",
  "create": "",
  "update": "",
  "delate": "",
}
```

### Explain 查询分析

分析查询语句

### 定时触发器

单个云函数目录下创建 `config.json`

```json
{
  "triggers": [
    {
      "name": "triggerRemove",
      "type": "timer",
      "config": "* * * 1 * * *"
    }
  ]
}
```

## 云函数示例

```js
const cloud = require('wx-server-sdk');

cloud.init({
  env: cloud.DYNAMIC_CURRENT_ENV
});
const db = cloud.database();

// 数据库操作示例
exports.main = async (event, context) => {

  /**
   * 查询：
   * exists/mod
   * and/or/not/nor
   * eq/neq/le/lee/gt/gte/in/nin
   * all/elemMatch/size
   * geoNear/geoWithin/geoIntersects 地理位置操作
   * expr 表达式
   * 
   * 更新：
   * set/remove/inc/mul/min/max/rename
   * push/pop/unshift/shift/pull/pullAll/addToSet/size/slice
   * and/or/not
   * cmp/eq/neq
   * cond/ifNull/switch
   * dateFromString/dateToString/year/dayOfYear
   * literal 直接返回一个字面量
   * mergeObjects/objectToArray
   * setUnion/setDifference/setEquals/setIntersection
   * split/substr/toLower
   * avg/first/last/sum/max/min/mergeObjects
   * 
   */
  const _ = db.command

  /**
   * replaceRoot({ newRoot: $.mergeObjects([$.arrayElemAt('', 0), '$$ROOT']) }) 
   * sample 随机选取指定的数量
   * sortByCount
   * unwind 拆分数组，文档会从一个变成多个
   * 
   * 聚合操作符
   * mod/multiply/pow/add/subtract/trunc
   * arrayElemAt/arrayToObject/
   */
  const $ = db.command.aggregate

  const { OPENID, APPID } = cloud.getWXContext()

  /**
   * 0. 统计匹配条数
   * total
   */
  const res = await db.collection('user')
    .count()

  /**
   * 1. 添加数据
   * 成功返回
   * _id
   * errMsg
   * 
   * 添加索引冲突时 抛出 error
   */
  const res = await db.collection('user')
    .add({
      data: {
        // _id 系统自动设置 或 自己配置
        name: 'admin',
        createTime: db.serverDate() // 服务器时间
      }
    });

  /**
    * 2. 查询数据
    * 查询一个记录
    * success: false 查询值为 null
    * data: {}
    * errMsg
    */
  const res = await db.collection('user')
    .doc('_id')
    .get()

  /**
    * 2.1 查询数据 where
    * 查询多个记录
    * success: false 查询值为 null
    * data: [] 
    * errMsg
    */
  const res = await db.collection('user')
    .where(_.and([
      {
        age: _.gt(18).and(_.lt(55))
      },
      {
        name: /java/i // db.RegExp({ regexp: 'java', option: 'i' })
      }
    ]))
    /**
     * field 普通查询指定返回字段
     *  */ 
    .field({
      _id: true,
      name: true
    })
    .orderBy('name', 'desc')
    .skip(0)
    .limit(10) // 最大上限 20
    .get()

  /**
   * 3. 更新数据
   * 成功返回
   * stats: { updated: 1 }
   * errMsg
   */
  // 更新部分字段
  const res = await db.collection('user')
    .doc('test-1')
    .update({
      data: {
        age: 31
      }
    })
  // 整体更新-未指定的数据被清除
  const res = await db.collection('user')
    .doc('test-1')
    .set({
      data: {
        age: 31
      }
    })

  /**
   * 4. 删除数据
   * 成功返回
   * stats: { removed: 1 }
   * errMsg
   */
  const res = await db.collection('user')
    .doc('test-1')
    .remove()

  /**
   * 5. 聚合
   */ 
  const res = await db.collection('books')
    .aggregate()
    .group({
      /**
       * 按 category 字段分组
       *  */ 
      _id: "$category",
      total: $.sum('$sales')
    })
    .end()

  /**
   * 6. 事务
   *  */
  const result = await db.runTransaction(async transaction => {
    try {
      // todo 数据库多操作
      if () {
        return {} // runTransaction resolve 的结果
      } else {
        await transaction.rollback(-100) // runTransaction reject 的结果
      }

    } catch (e) {

    }
  })

  /**
   * 7. 索引
   * 云开发控制台 - 数据库 - 可设置索引字段的排序(查询只能使用的排序)
   *  */ 
  const res = db.collection('user')
    .where(
      _.or([
        {
          updated_at: _.gte(startTime).and(_.lte(endTime))
        },
        {
          updated_at: _.exists(false), // 仅判断不存在
          created_at: _.gte(startTime).and(_.lte(endTime))
        }
      ])
    )
    .orderBy('updated_at', 'desc')
    .orderBy('created_at', 'desc')
    .get()
  
  /**
   * 8. 联合查询
   * list: [],
   * errMsg
   */
  const res = db.collection('user').aggregate()
    .match({
      created_at: _.gte(new Date('2025-03-01')).lte('2025-03-31')
    })
    /**
     * 自定义连接条件(或 多相等条件)
     * lookup({
     *   from, 
     *   left: {  }, 
     *   pipeline: [], 
     *   as 
     * })
     */
    .lookup({
      from: 'product',    // 被关联 product 表
      localField: 'code', // 主表 user user.code
      foreignField: 'code', // 被关联 product 表 product.code
      as: 'product_info'
    })
    .addFields({
      unit_price: { $arrayElemAt: ["$product_info.price", 0] }, // 获取单价
      total_price: { 
        $multiply: ["$quantity", { $arrayElemAt: ["$product_info.price", 0] }] 
      } // 计算单项订单总价
    })
    .group({
      _id: null,                 // 统计所有订单
      total: { $sum: "$total_price" } // 计算所有订单的总价
    })
    .get() // [{ total: 111 }]
};
```

## 时区

默认时区 UTC+0, 设置 UTC+8 时，需要设置函数的环境变量 `TZ: Asia/Shanghai`

## 地理位置
