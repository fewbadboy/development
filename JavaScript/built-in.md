# Built-in

## RegExp

`(...)` 捕获组，会占用编号，能被 `$1、\1、match[1]` 引用
`(?:pattern)` 非捕获组，只分组，不捕获，不占用编号

- 默认贪婪模式，加 `?` 变成懒惰
- `\d` `\w` `\s` `\c`
- `(?=pattern)` `(?!pattern)` 当前位置的后面必须紧跟着能匹配 pattern，才算成功
- `(?<=pattern)` `(?<!pattern)`
- `\b` `\B`
- `(pattern)` `(?<Name>pattern)` `(?:pattern)`

```js
'100元 200美元'.match(/\d+(?!美元)/g);  // ["100"]

let date = '2026-01-06'
date.replace(/(\d{4})-(\d{2})-(\d{2})/, '$3/$2/$1'); // "06/01/2026"

// 反引用(引用前面的捕获组)
/(\d)\1/; re.test('11'); // true
```

## Intl

### Intl.Collator()

语言敏感的字符串比较

### Intl.DataTimeFormat()

```js
new Intl.DateTimeFormat("zh-CN", {
  dateStyle: "full", // long medium short
  timeStyle: "long",
  timeZone: "Asia/Shanghai",
}).format(new Date())
```

### Intl.DurationFormat()

时长格式化

### Intl.ListFormat()

### Intl.NumberFormat()

### Intl.PluralRules()

### Intl.RelativeTimeFormat()

### Intl.Segmenter()
