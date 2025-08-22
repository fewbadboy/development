# cloud development

## 数据库

### 安全规则

- auth 用户登录信息 auth.openid 是用户的 openid
- doc 记录内容(当前操作的数据文档内容)
- now 当前时间的时间戳

```json
{
  "read": true,
  "write": "doc._openid === auth.openid",
  "create": "",
  "update": "",
  "delate": ""
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

注意：
**排序的时候,为保证排序的稳定性和可靠性，需要加一个次排序的字段(保证唯一性)，从而避免跳页、重复、丢失问题**
**数据库并不保证重复值之间的记录顺序**

```js
const cloud = require("wx-server-sdk");

cloud.init({
  env: cloud.DYNAMIC_CURRENT_ENV,
});
const db = cloud.database();

const pageSize = 20;

// 数据库操作示例
exports.main = async (event, context) => {
  const { pageIndex } = event;

  const _ = db.command;

  const $ = db.command.aggregate;

  const { OPENID, APPID } = cloud.getWXContext();

  /**
   * 1. 统计匹配条数
   * { total, errMsg }
   */
  const res = await db.collection("user").count();

  /**
   * 2. 查询数据-单条(默认主键 _id 的快捷查询方式)
   * 查询条件不存在时返回 data:{}
   * { data: { _id, ...}, errMsg }
   */
  const res = await db.collection("user").doc("_id").get();

  /**
   * 2. 查询数据-文档/集合
   * 查询条件不存在时返回 data: []
   * { data: [], errMsg }
   */
  const res = await db
    .collection("user")
    .where(
      _.and([
        {
          age: _.gt(18).and(_.lt(55)),
        },
        {
          name: /java/i, // db.RegExp({ regexp: 'java', option: 'i' })
        },
      ])
    )
    /**
     * 普通查询指定返回字段
     * 不支持字段重命名，不支持表达式
     * 支持字段嵌套选择(不解构或提升字段，也不会消除嵌套解构)
     */
    .field({
      _id: true, // 1 或 -1
      name: true,
    })
    .orderBy("name", "desc")
    /**
     * 默认不写分页时，云开发平台默认 get() 返回 20 条记录，永远从第 0 条开始
     * 建议分页时 limit 参数效值 20
     * 获取大数据量数据：循环分页拉取
     *
     * 单独获取时可以 设置 limit 最大 100
     */
    .skip((pageIndex - 1) * pageSize)
    .limit(pageSize)
    .get();

  /**
   * 3. 添加数据
   * { _id, errMsg }
   */
  const res = await db.collection("user").add({
    data: {
      // _id 系统自动设置 或 自己配置
      name: "admin",
      createTime: db.serverDate(), // 服务器时间
    },
  });

  /**
   * @see https://developers.weixin.qq.com/miniprogram/dev/wxcloudservice/wxcloud/reference-sdk-api/database/collection/Collection.update.html
   * 4. 更新数据
   * where 条件在事务中可以
   * 更新部分字段
   *  { stats: { updated: 1 }, errMsg }
   */
  const res = await db
    .collection("user")
    .doc("_id")
    .update({
      data: {
        age: 31,
      },
    });
  /**
   * 整体更新
   * 创建 或 修改
   * { _id, stats: { created: 1, updated: 1 }, errMsg }
   */
  const res = await db
    .collection("user")
    .doc("_id")
    .set({
      data: {
        age: 31,
      },
    });

  /**
   * 5. 删除数据
   * { stats: { removed: 1 }, errMsg }
   */
  const res = await db.collection("user").doc("_id").remove();

  /**
   * 6. 聚合
   * @see https://developers.weixin.qq.com/miniprogram/dev/wxcloudservice/wxcloud/reference-sdk-api/database/Command.html
   * { list: [], errMsg }
   */
  const res = await db
    .collection("books")
    .aggregate()
    // 添加字段，也可嵌套添加字段
    .addFields({
      order: "1",
    })
    // 创建新文档解构
    .group({
      /**
       * _id: null, 全表作为一组
       * 按 category 字段分组
       *
       * 内存限制：该阶段有 100M 内存使用限制
       *  */
      _id: "$category",
      total: $.sum("$sales"),
    })
    /**
     * 过滤显示字段
     * 支持算数表达式 $.floor/multiply 等
     */
    .project({
      category: 1,
      total: 0, // 除去字段
    })
    .sort({ category: -1 }) // 降序
    .end();

  /**
   * 7. 事务
   */

  /**
   * 8. 索引
   * 云开发控制台 - 数据库 - 可设置索引字段的排序(查询只能使用的排序)
   *  */
  const res = db
    .collection("user")
    .where(
      _.or([
        {
          updated_at: _.gte(startTime).and(_.lte(endTime)),
        },
        {
          updated_at: _.exists(false),
          created_at: _.gte(startTime).and(_.lte(endTime)),
        },
      ])
    )
    .orderBy("updated_at", "desc")
    .orderBy("created_at", "desc")
    .get();

  /**
   * 9. 联合查询
   * list: [],
   * errMsg
   */
  const res = db
    .collection("user")
    .aggregate()
    .match({
      created_at: _.gte(new Date("2025-03-01")).lte("2025-03-31"),
    })
    .lookup({
      from: "product", // 被关联 product 表
      localField: "code", // 主表 user user.code
      foreignField: "code", // 被关联 product 表 product.code
      as: "product_info",
    })
    .addFields({
      unit_price: { $arrayElemAt: ["$product_info.price", 0] }, // 获取单价
      total_price: {
        $multiply: ["$quantity", { $arrayElemAt: ["$product_info.price", 0] }],
      },
    })
    .group({
      _id: null, // 统计所有订单
      total: { $sum: "$total_price" }, // 计算所有订单的总价
    })
    .get(); // [{ total: 111 }]
};
```

## 时区

默认时区 UTC+0, 设置 UTC+8 时，需要设置函数的环境变量 `TZ: Asia/Shanghai`

## 地理位置
