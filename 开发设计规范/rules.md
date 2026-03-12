# 开发设计规范

## 接口属性

- 驼峰

**注意**  

> 接口字段返回驼峰，但排序字段要求 snake_case，本质是后端把数据库实现细节泄露给了前端
> 前端自己转时，转换规则未必 100% 正确。 如：userID  -> user_i_d ❌

## 返回字段与入参统一

**拒绝** 返回的 string，但是传递参数的时候用数值

## 枚举值

```json
{
  "status": "ENABLED"
}
```

或

```json
{
  "status": 1,
  "statusName": "启用"
}
```

##

- Controller 禁止 直接返回 Entity(数据库的样子)
- 每个接口必须有 VO(前端看到的样子) / DTO(业务流转的样子，例如：查询条件，新增告警)
- 列表接口字段 = 页面字段（1:1）

## 数据库表字段

Java `long` 范围 -2^63 ~ 2^63-1, 而前端 `Number.MAX_SAFE_INTEGER` 为 2^53 - 1
很多后端框架在 JSON 里将 `long` 转为 `string`,就是因为精度问题

主键不再使用自增 ID，而是用 UUID 或 Snowflake ID(分布式唯一 ID 生成算法)
