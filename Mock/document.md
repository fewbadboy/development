# document

## 安装及使用

```js
npm install mockjs

const Mock = require('mockjs')
const data = Mock.mock({
  'list|1-10': [{
    'id|+1': 1
  }]
})
console.log(JSON.stringify(data, null, 4))
```

## DTD 数据模板定义

```js
// 属性名 name
// 生成规则 rule
// 属性值 value
'name|rule': value
```

注意:

- 生成规则 是可选的
- 生成规则 有 7 种格式：
  1. 'name|min-max': value
  2. 'name|count': value
  3. 'name|min-max.dmin-dmax': value
  4. 'name|min-max.dcount': value
  5. 'name|count.dmin-dmax': value
  6. 'name|count.dcount': value
  7. 'name|+step': value
- 属性值 中可以含有 `@占位符`

## DPD 数据占位符定义

- 格式 `@占位符` `@占位符(参数[, 参数])`
- 占位符引用的是 `Mock.Random` 中的方法
- 通过 `Mock.Random.extend()` 来扩展

```js
Random.extend({
    constellations: ['白羊座', '金牛座', '双子座', '巨蟹座', '狮子座', '处女座', '天秤座', '天蝎座', '射手座', '摩羯座', '水瓶座', '双鱼座'],
    constellation: function(date){
        return this.pick(this.constellations)
    }
})
Random.constellation()
// => "水瓶座"
Mock.mock('@CONSTELLATION')
// => "天蝎座"
Mock.mock({ constellation: '@CONSTELLATION'})
// => { constellation: "射手座" }
```

## Basics

- Random.boolean(min, max, cur)

```js
// cru(true/false) 出现的概率 min / (min + max) 
```

- Random.natural(min, max) 返回一个随机的自然数（大于等于 0 的整数）
- Random.integer(min, max)
- Random.float(min, max, dmin, dmax) 返回一个随机的浮点数

```js
// dmin 小数部分最小位数
```

- Random.character(pool) 返回一个随机字符

参数 pool：可选。字符串。表示字符池，将从中选择一个字符返回。

```js
// 传入 'lower' 或 'upper'、'number'、'symbol'，表示从内置的字符池从选取
{
  lower: "abcdefghijklmnopqrstuvwxyz",
  upper: "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
  number: "0123456789",
  symbol: "!@#$%^&*()[]"
}
```

- Random.string(pool, min, max) 返回一个随机字符串， pool 同上

```js
Random.string()
Random.string( length )
Random.string( pool, length )
Random.string( min, max )
Random.string( pool, min, max )
```

- Random.range(start, stop, step) 返回一个整型数组

```js
Random.range(stop)
Random.range(start, stop)
Random.range(start, stop, step)
```

- Random.date(format) 随机的日期字符串

```js
// yyyy yy y 年 及是否有前导0
// MM M 月
// dd d 日
// HH H 24h制
// hh h 12h制
// mm m 分
// ss s 秒
// SS S 毫秒
// A a AM or PM (am or pm)
// T  从 1970-1-1 00:00:00 UTC,开始毫秒数
```

- Random.time(format)
- Random.datetime(format)
- Random.now(unit, format) unit 可选值 year、month、week、day、hour、minute、second、week

## Image

- Random.image(size, background, foreground, format, text)
- Random.dataImage(size, text) 生成一段随机的 Base64 图片编码

```js
// size 默认从下面数组中取值
[
  '300x250', '250x250', '240x400', '336x280', 
  '180x150', '720x300', '468x60', '234x60', 
  '88x31', '120x90', '120x60', '120x240', 
  '125x125', '728x90', '160x600', '120x600', 
  '300x600'
]

```

## Color

- Random.color()  随机生成一个颜色，格式为 '#RRGGBB'

## Helpers

- Random.capitalize(word) 字符串的第一个字母转换为大写
- Random.upper(str)
- Random.lower(str)
- Random.pick(arr) 从数组中随机选取一个元素，并返回
- Random.shuffle(arr)

## Text

- Random.paragraph(len) 随机生成一段文本

```js
Random.paragraph()
Random.paragraph(len)
Random.paragraph(min, max)
```

- Random.sentence(len) 随机生成一个句子，第一个的单词的首字母大写
- Random.word(len)
- Random.title(len) 随机生成一句标题，其中每个单词的首字母大写

## Name

- Random.first()
- Random.last()
- Random.name()
  
```js
Random.name()
// 参数 middle：可选。布尔值。指示是否生成中间名。
Random.name(middle)
```

- Random.cname(count)  参数 count：可选。数字。指示姓名的字数，默认为 2 个或 3 个字的随机姓名。 Mock 0.2

## Web

- Random.url()
- Random.domain()
- Random.email()
- Random.ip()
- Random.tld() 随机生成一个顶级域名

## Address

- Random.area() 随机生成一个（中国）大区
- Random.region() 随机生成一个（中国）省（或直辖市、自治区、特别行政区）

## Miscellaneous

- Random.guid()
- Random.id() 随机生成一个 18 位身份证
- Random.increment(step)
