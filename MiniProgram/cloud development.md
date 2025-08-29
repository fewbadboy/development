# cloud development

## 存储

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

## 云后台

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
 * 数据库操作示例
 * @see {@link https://developers.weixin.qq.com/miniprogram/dev/wxcloudservice/wxcloud/reference-sdk-api/Cloud.database.html}
 **/

exports.main = async (event, context) => {
  const { pageIndex } = event;

  const { OPENID, APPID } = cloud.getWXContext();
};
```

## 时区

默认时区 UTC+0, 设置 UTC+8 时，需要设置函数的环境变量 `TZ: Asia/Shanghai`
