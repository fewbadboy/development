# document

## 索引

可以设为唯一(不重复)

## 示例

```js
const cloud = require("wx-server-sdk");

cloud.init({
  env: cloud.DYNAMIC_CURRENT_ENV,
});
const db = cloud.database();

// 数据库操作示例
exports.main = async (event, context) => {
  // _.set/remove/inc/mul/push/pop/shift/unshift

  const _ = db.command;
  const $ = db.command.aggregate;

  const { OPENID, APPID } = cloud.getWXContext();
  /**
   * 1. 添加数据
   * 成功返回
   * _id
   * errMsg
   *
   * 添加索引冲突时 抛出 error
   */
  const res = await db.collection("user").add({
    data: {
      // _id: 'test-1' // 可选自定义 _id
      name: "admin",
      role: 0,
      access: true,
      age: 30,
      createTime: db.serverDate(), // 服务器时间
    },
  });

  /**
   * 2. 查询数据
   * 成功返回
   * success: false 查询值为 null
   * data: [] 查询存在满足值
   * errMsg
   */
  const res = await db.collection("user").doc("test-1").get();

  const res = await db
    .collection("user")
    .where({
      access: true,
      // or
      age: _.gt(18).add(_.lt(55)), // in nin
    })
    .field({
      _id: true,
      name: true,
    })
    .orderBy("name", "desc")
    .skip(0)
    .limit(10)
    .get();

  /**
   * 3. 更新数据
   * 成功返回
   * stats: { updated: 1 }
   * errMsg
   */
  // 更新部分字段
  const res = await db
    .collection("user")
    .doc("test-1")
    .update({
      data: {
        age: 31,
      },
    });
  // 整体更新-未指定的数据被清除
  const res = await db
    .collection("user")
    .doc("test-1")
    .set({
      data: {
        age: 31,
      },
    });

  /**
   * 4. 删除数据
   * 成功返回
   * stats: { removed: 1 }
   * errMsg
   */
  const res = await db.collection("user").doc("test-1").remove();

  /**
   * 5. 聚合
   * $match 筛选，相当于 where
   * $group 分组
   * $sort
   * $project 选择输出字段
   * $limit
   * $lookup 关联查询
   * $unwind 拆分数组
   * $addFields
   * $set
   *  */

  // 根据 account 分组,统计 age
  const res = await db
    .collection("user")
    .aggregate()
    .group({
      _id: "$account",
      total: { $sum: "$age" },
    });

  // 6. 事务
  try {
    const result = await db.runTransaction(async (transaction) => {
      const aaaRes = await transaction.collection("user").doc("aaa").get();
      const bbbRes = await transaction.collection("user").doc("bbb").get();

      if (aaaRes.data && bbbRes.data) {
      }
    });
    return { success: true, result: result };
  } catch (error) {
    return {
      success: false,
      error: error,
    };
  }

  // 7. 索引 默认 _id 字段已经有索引，无需手动创建
  const res = db
    .collection("user")
    .where(
      _.or([
        {
          updated_at: _.gte(startTime).and(_.lte(endTime)),
        },
        {
          updated_at: _.exists(false), // 仅判断不存在
          created_at: _.gte(startTime).and(_.lte(endTime)),
        },
      ])
    )
    .orderBy("updated_at", "desc")
    .orderBy("created_at", "desc")
    .get();

  // 8. 联合查询
  const res = db
    .collection("user")
    .aggregate()
    .match({
      created_at: _.gte(new Date("2025-03-01")).lte("2025-03-31"),
    })
    .lookup({
      from: "product", // 关联 product 表
      localField: "code", // 关联字段 user.code -> product.code
      foreignField: "code",
      as: "product_info",
    })
    .addFields({
      unit_price: { $arrayElemAt: ["$product_info.price", 0] }, // 获取单价
      total_price: {
        $multiply: ["$quantity", { $arrayElemAt: ["$product_info.price", 0] }],
      }, // 计算单项订单总价
    })
    .group({
      _id: null, // 统计所有订单
      total: { $sum: "$total_price" }, // 计算所有订单的总价
    })
    .get(); // [{ total: 111 }]
};
```
