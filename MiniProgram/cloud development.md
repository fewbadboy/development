# cloud development

## demonstration

云函数运行环境是 Node.js

### 图片处理

```js
// 云端上传图片及获取图片地址
const { fileID } = wx.cloud.uploadFile({
  cloudPath: 'demo.jpg',
  fileContent: fileStream
})
const { fileList } = wx.cloud.getTempFileURL({
  fileList: [fileID]
})
fileList[0].tempFileURL
```

## Explain 查询分析

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
   * _.all/elemMatch/size/push/pull/addToSet/...
   * _.geoNear/geoWithin/geoIntersects
   * _.expr
   * _.set/remove/inc/mul/min/max/rename
   */

  const _ = db.command
  const $ = db.command.aggregate

  const { OPENID, APPID } = cloud.getWXContext()
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
        // _id: 'test-1' // 可选自定义 _id
        name: 'admin',
        createTime: db.serverDate() // 服务器时间
      }
    });

  /**
    * 2. 查询数据
    * 成功返回
    * success: false 查询值为 null
    * data: [] 查询存在满足值
    * errMsg
    */
  const res = await db.collection('user')
    .doc('test-1')
    .get()

  const res = await db.collection('user')
    .where(_.and([
      {
        age: _.gt(18).and(_.lt(55))
      },
      {
        name: /java/i
      }
    ]))
    // 指定返回的字段
    .field({
      _id: true,
      name: true
    })
    .orderBy('name', 'desc')
    .skip(0)
    .limit(10)
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
   * match 筛选，相当于 where
   * group 分组
   * lookup 关联查询
   * project 把指定字段传递给下一个流水线
   * unwind 拆分数组(变成多个文档，值分别对应数组的每个元素)
   * addFields
   * limit
   * sort
   * sample
   * -----
   * 聚合表达式：$profile.name 
   * 算术：abs/add/ceil/floor/divide/mod/multiply/subtract/trunc
   * 数组：arrayElemAt/concatArrays/filter/...
   * 布尔：and/or/not
   * 比较：cmp/eq/...
   * 条件：cond/ifNull/switch
   * 日期：year/...
   * 常量：literal
   * 对象：mergeObjects/objectToArray
   * 集合：allElementsTrue/setUnion/...
   * 字符串：dateToString/split/substr/...
   * 累加：addToSet/avg/max/sum/...
   * 变量：let
   *  */ 

  // 按 .account 分组,统计 age
  const res = await db.collection('user')
    .aggregate()
    .group({
      _id: "$account",
      total: { $sum: '$age' }
    })
    .end()

  // 6. 事务

  // 7. 索引 默认 _id 字段已经有索引，无需手动创建
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
    .lookup({
      from: 'product',    // 关联 product 表
      localField: 'code', // 关联字段 user.code -> product.code
      foreignField: 'code',
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

## 云存储

## 高级日志

云开发 - 云函数 - 高级日志

## 定时触发器

## 时区

默认时区 UTC+0, 设置 UTC+8 时，需要设置函数的环境变量 `TZ: Asia/Shanghai`

## 消息推送

云开发 - 设置 - 其他设置

目前仅支持客服消息推送(服务器主动向用户发送消息)

```js
// 云函数入口函数
exports.main = async (event, context) => {
  const wxContext = cloud.getWXContext()
  
  await cloud.openapi.customerServiceMessage.send({
    touser: wxContext.OPENID,
    msgtype: 'text',
    text: {
      content: '收到',
    },
  })

  return 'success'
}

微信云
```

## 数据模型

提供多端 SDK 可供调用

在初始化 SDK 之后，会自动在 models 上挂载针对当前云开发环境下的数据模型的操作方法

## 内容安全

存放的数据进行内容安全审核，减少提审及运营过程中的违规问题
