# document

## Data URLs

[MIME_types](https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types)
`data:[<media_type>][;base64],<data>`

## shallow clone

复制一个对象的第一层属性，对于嵌套的对象或数组，浅拷贝并不会递归地复制它们的内部数据，而是复制它们的引用

## deep clone

在以下条件下调用 JSON.stringify() 来序列化会失效。

|          类型          |            序列化结果             |
| :--------------------: | :-------------------------------: |
|          函数          |              被忽略               |
|         Symbol         |              被忽略               |
|       undefined        | 被忽略(对象属性值)/null(数组元素) |
| 循环应用(obj.self=obj) |             抛出错误              |
|      DOM 节点对象      |                {}                 |
|         BigInt         |             抛出错误              |
|        Map/Set         |                {}                 |
|          Date          |            ISO 字符串             |
|         RegExp         |                {}                 |

## Object

每一个属性名是一个 string 或 Symbol
`[]` 获取属性名时，任何非字符串对象都会通过 `toString` 方法转换

```js
// 静态方法
assign(target, source1, ...) // copy 所有自身可枚举属性, 返回第一个参数
groupBy(items, callbackFn)
is(v1, v2) // 比较严格相等
```

1. `for...in` 遍历对象自身和继承的可枚举属性
2. `Object.keys()` 遍历对象自身的所有可枚举的属性的键名
3. `JSON.stringify()` 序列化对象自身可枚举的属性

`JSON.parse` 纯文本转换，就算解析出来的看起来是脚本也绝对不执行，是安全的 API, 然后设置 `textContent` 或者 `innerText`

## String

字符串以 UTF-16 编码表示

```js
const str = '½A'
// 示例方法
at(index) // 负值就是 index + length
charAt(index) // index 超出范围 0 -str.length - 1 返回空字符串
match(regexp)
padEnd(targetLength, padString)
padStart(targetLength, padString)

const DATE_REG = /(?<year>\d{4})\/(?<month>\d{2})\/(?<day>\d{2}) (?<hour>\d{2}):(?<minute>\d{2}):(?<second>\d{2})/

const format = ({ year, month, day, hour, minute, second }) => 
`${second} ${minute} ${hour} ${day} ${month} ? ${year}`

'2026/01/01 23:01:30'.replace(
  /(\d{4})\/(\d{2})\/(\d{2}) (\d{2}):(\d{2}):(\d{2})/,
  (_, groups) => format(groups)
)

search(regexp) // 返回 index
split(separator, limit)
charCodeAt(index) // 返回 0 -65535 的 UTF-16 code unit
codePointAt(index) // 返回完整的 Unicode code unit 0 - 0x10FFFF 
startsWith(searchString, endPosition)
endsWith(searchString, endPosition)
```

## Iterator

提供一个 `[Symbol.iterator]()` 方法，返回迭代器对象本身

- for...of
- 解构赋值(数组，Set)
- 扩展运算符
- yield\*
- Array.from
- Promise.all/race()

## RegExp

`(...)` 捕获组，会占用编号，存储匹配内容，供后续引用或替换， 如 `$1、\1、match[1]` 引用
`(?<name>)` 命名捕获组
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

```js
RegExp.prototype.exec(str);
RegExp.prototype.test(str);
```

## Encode

- encodeURI()： 转义字符，除 `A–Z a–z 0–9 - _ . ! ~ * ' ( ) ; / ? : @ & = + $ , #` 字符
- encodeURIComponent(): 除 `A–Z a–z 0–9 - _ . ! ~ * ' ( )`
- TextEncode: 返回 UTF-8 编码的 Uint8Array

## 动画

1. window.requestAnimationFrame(callback)
大多数浏览器在后台标签页或者隐藏的 iframe 标签里时暂停调用，为了提高性能和电池使用寿命

```js
/**
 * 避免堆积问题：setInterval 在上一次回调没完成时，下一次时间到达时继续执行，导致多个回调
 * 递归的 setTimeout 根据上次执行的时间决定下次的延迟(超过1000ms的就取消上次的setTimeout)
 */
function do() {
  setTimeout(() => {
    console.log('Task completed');
  }, 2000);
  setTimeout(do, 1000) // 当前任务执行完成后再触发
}
setTimeout(do, 1000)
```

如果想在浏览器中实现 0ms 延时的定时器，可以参考 window.postMessage()

## await using

变量值必须是 null 、undefined 、一个具有 `[Symbol.asyncDispose]()` 或 `[Symbol.dispose]()` 方法的对象

```js
class Resource {
  constructor() {
    // 初始化资源
  }
  [Symbol.dispose]() {
    // 释放资源
  }
}

async function example() {
  await using resource = new Resource();
  // 变量超出作用域，调用对象的 [Symbol.dispose]() 方法，以确保资源被释放
}
```
