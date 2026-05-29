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

`.expression` 获取属性, expression 必须是一个有效的 JavaScript 标识符

`[expression]` 获取属性, expression 转换成属性名称的字符串或 Symbol

```js
// 静态方法
assign(target, source1, ...) // copy 所有自身可枚举属性, 返回第一个参数
groupBy(items, callbackFn)
is(v1, v2) // 比较严格相等
```

1. `for...in` 遍历对象自身和继承的可枚举属性
2. `Object.keys()` 遍历对象自身的所有可枚举的属性的键名
3. `JSON.stringify()` 序列化对象自身可枚举的属性

`JSON.parse` 纯文本转换，就算解析出来的看起来是脚本也绝对不执行，是安全的 API

## String

字符串基本上以 1 个 UTF-16 编码单元表示(有些字符占 2 个编码单元)

```js
const str = '½A'
charCodeAt(index) // 返回 0 -65535 的 UTF-16
codePointAt(index) // 返回编码 0 - 0x10FFFF 的 Unicode 码点

Array.from(str).length // 将字符串转换为数组，底层按 codePoint 分割, 类似 for...of 循环
// 多个 codePoint 组成的字符(emoji) 使用 Intl.Segmenter 分割
```

## Iterator

提供一个 `[Symbol.iterator]()` 方法，`next()` 返回迭代器结果对象

## RegExp

`(...)` 捕获组，会占用编号，存储匹配内容，供后续引用或替换， 如 `$1、\1、match[1]` 引用
`(?:pattern)` 非捕获组，只分组，不捕获，不占用编号
`(?<name>pattern)` 命名捕获组

- 默认贪婪模式，加 `?` 变成懒惰
- `\d` `\w` `\s` `\c`
- `x(?=pattern)` `x(?!pattern)` x 后面紧跟着能(不能)匹配 pattern
- `(?<=pattern)x` `(?<!pattern)x` x 前面紧跟着能(不能)匹配 pattern

```js
'100元 200美元'.match(/\d+(?=美元)/g);  // ["200"]

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
- TextEncode 默认使用 UTF-8 编码将字符串转换为字节流
  - encode(str): 将字符串转换为 Uint8Array 字节流
-TextDecoder: 将 Uint8Array 字节流转换为字符串
  - decode(arrayBuffer): 将 Uint8Array 字节流转换为字符串

## Blob

Blob(blobParts, options) blobParts 是一个可迭代对象

- arrayBuffer(): 二进制数据，用于存储文件内容
- bytes()
- slice
- stream(): 返回一个读取流内容的 ReadableStream 对象
- text(): 返回一个 Promise 对象，包含 Blob 内容的字符串

## FileReader

异步读取文件内容

- readAsArrayBuffer(file): 读取文件内容为 ArrayBuffer
- readAsDataURL(file): 读取文件内容为 Data URL，包含文件类型和编码
- readAsText(file, encoding): 读取文件内容为文本，默认 UTF-8 编码

```js
const file = event.target.files[0]; // FileList 对象

const reader = new FileReader();
reader.onload = function() {
  const arrayBuffer = reader.result
  const data = new Uint8Array(arrayBuffer)
  console.log('Received message:', data);
};
reader.readAsArrayBuffer(file);
```

## 动画

window.requestAnimationFrame(callback) 要求浏览器在下一次重绘前调用 callback
回调参数为时间戳 timestamp, 表示上一帧渲染结束的时间，单位为毫秒

大多数浏览器在后台标签页或者隐藏的 iframe 标签里时暂停调用，为了提高性能和电池使用寿命

```js
/**
 * 避免堆积问题：setInterval 在上一次回调没完成时，下一次时间到达时继续执行，导致多个回调
 * 递归的 setTimeout 根据上次执行的时间决定下次的延迟(超过1000ms的就取消上次的setTimeout)
 */
function do() {
  console.log('Task completed');
  setTimeout(do, 2000) // 当前任务执行完成后再触发
}
setTimeout(do, 2000)
```

如果想在浏览器中实现 0ms 延时的定时器，可以参考 window.postMessage()

```js
const rowHeight = tbody.querySelector('tr')!.offsetHeight

// 1. 动画
tbody.style.transition = 'transform 0.5s ease'
tbody.style.transform = `translateY(-${rowHeight}px)`

// 2. 等动画结束
await new Promise(resolve => setTimeout(resolve, 500))

// 3. 关闭动画 + 复位（第一帧）
tbody.style.transition = 'none'
tbody.style.transform = 'translateY(0)'

// ⚠️ 强制让浏览器吃掉这一帧
await new Promise(requestAnimationFrame)

// 4. 再改数据（第二帧）
const item = tableData.value.shift()
tableData.value.push(item!)
```

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

## websocket

```js
const ws = new WebSocket('ws://example.com/socket');
ws.binaryType = 'blob'; // 设置为二进制类型，  'blob' 或 'arraybuffer'

// 事件
ws.onopen = function(event) {
  console.log('WebSocket connection opened');
};
ws.onmessage = async(event) {
  // 1. 从 Blob 转换为文本
  const blob = event.data;
  const text = await blob.text();
  console.log('Received message:', JSON.parse(text));

  // 2. FileReader 读取 Blob
  const reader = new FileReader();
  reader.onload = function() {
    const arrayBuffer = reader.result
    const data = new Uint8Array(arrayBuffer)
    console.log('Received message:', data);
  };
  reader.readAsArrayBuffer(blob);

  // 3. 从 ArrayBuffer + TextDecoder 解析
  const textDecoder = new TextDecoder();
  const text = textDecoder.decode(arrayBuffer);
  console.log('Received message:', text);
};
ws.onclose = function(event) {
  console.log('WebSocket connection closed');
};
ws.onerror = function(error) {
  console.error('WebSocket error:', error);
};
ws.send(JSON.stringify({hello: 'world'}));
```
