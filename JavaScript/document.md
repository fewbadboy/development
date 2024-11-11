# document

[ecma-262](https://ecma-international.org/publications-and-standards/standards/ecma-262/)

[MDN web Docs 词汇表](https://developer.mozilla.org/en-US/docs/Glossary)

## let const

暂时性死区：当前区域中存在 let const 声明的变量只要在声明之前使用就报错

## 函数绑定

`obj::fun(...arguments)` === `fun.bind(obj, arguments)`

## Data URLs

[MIME_types](https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types)
`data:[<media_type>][;base64],<data>`

## shallow clone

所有标准内置对象复制操作(返回新对象)都创建浅复制（扩展运算符...）

```js
// globalThis
const obj = { name: '张三', age: 18, skill: ['javascript', 'python'] }
const copy = Object.assign(obj, { sex: '男' })
obj.name = '李四'
// copy.name '李四'

const shallow = { ...obj }
obj.skill.push('java')
// shallow.skill ['javascript', 'python', 'java']

// 添加 css 样式
console.log(`%c 成功:%c 成功的消息`,'color:green','border: 1px solid green;color: red')
```

## deep clone

一个 JavaScript 对象可以被`序列化`(将一个对象或数据结构转换为适合网络传输或存储的格式（如数组缓冲区或文件格式）的过程)

许多JavaScript 对象根本不能序列化。
函数（带有闭包）、Symbol、在 [HTML DOM API](https://developer.mozilla.org/zh-CN/docs/Web/API/HTML_DOM_API) 中表示 HTML 元素的对象、递归数据以及许多其他情况。
在这些条件下调用 JSON.stringify() 来序列化会失效。

## Array

```js
/**
 * accumulator: 上一次调用 callbackFn 的结果。在第一次调用时，如果指定了 initialValue 则为指定的值，否则为 array[0] 的值。
 * currentValue: 当前元素的值。在第一次调用时，如果指定了 initialValue，则为 array[0] 的值，否则为 array[1]。
 */
Array.property.reduce((accumulator, currentValue,currentIndex, array) =>{}, initialValue)

/**
 * 返回新的数组，不影响原数组
 */
Array.property.toReversed()
Array.property.toSorted()
Array.property.toSpliced()
/**
 * 返回一个用给定 index 替换 value 的新数组
 * 同样是浅拷贝
 */
Array.property.with(index, value)

const arr = [1, 2, { name: 'test' }, 4, 5];
const tes = arr.with(1, 6);
arr.at(2).name = 'hello'
// tes.at(2).name
```

## Object

`[]` 获取属性名时，任何非字符串对象都会通过 `toString` 方法转换

```js
Object.groupBy(items, (item, index))
Object.hasOwn(obj, prop)

Object.is(NaN, NaN) // 比较严格相等

Object.prototype.toString.call([]).slice(8, -1)
```

1. `for...in` 遍历对象自身和继承的可枚举属性
2. `Object.keys()` 遍历对象自身的所有可枚举的属性的键名
3. `JSON.stringify()` 序列化对象自身可枚举的属性
4. `Object.assign()` 只拷贝对象自身可枚举的属性
5. `Object.getOwnPropertyNames()` 返回对象自身所有属性(不含Symbol)
6. `Object.getOwnPropertySymbols()` 返回对象自身所有Symbol属性
7. `Reflect.ownKeys()` 包含自身的所有键名
8. `Object.getOwnPropertyDescriptors` 获取对象的所以自身属性的描述符

## utf-16

Unicode的编码空间从 U+0000 到 U+10FFFF，共有1,112,064个码位（code point）可用来映射字符。
Unicode的编码空间可以划分为17个平面（plane），每个平面包含216（65,536）个码位。17个平面的码位可表示为从 U+xx0000 到 U+xxFFFF，其中xx表示十六进制值从0016到1016，共计17个平面。

## String

字符串 utf-16 编码单元序列，意思最大字符的表示值为65535

Unicode 字符集(U+0000 - U+10FFFF)远大于 65535个，额外的字符以`surrogate pairs`（代理对）的形式存储在 utf-16中
为了避免歧义，`0xD800` 到 `0xDFFF` 不会用来表示单个字符编码
`0xD800` 到 `0xDBFF` 前代理项(高代理项),它是字符串中的最后一个码元，或者下一个码元不是尾代理
`0xDC00` 到 `0xDFFF` 尾代理项(低代理项),它是字符串中的第一个码元，或者前一个码元不是前代理
每个 Unicode 字符由 1 或 2 个 uft-16 编码单元组成，字符串表示为 `\u{xxxxxx}`, x 表示 1-6 位 16 进制数

除 Unicode 字符外，某些字符序列应视为一个视觉单元如 emoji(许多具有多种变体的 emoji，实际上是由多个 emoji组成`U+200D`字符连接)

```js
[..."👨‍👦"]; // [ '👨', '‍', '👦' ]
```

- codePointAt(index) 从给定索引(基于 utf-16 编码)开始的 Unicode 码点值
- match/matchAll 返回所有正则捕获组匹配值

## Iterator

Symbol.iterator

- for...of
- 解构赋值(数组，Set)
- 扩展运算符
- yield*
- Array.from
- Promise.all/race()

```js
const arr = [3, 4]
arr.foo = 5
for(let i in arr){
  // '0' '1' 'foo'
}
for(let i of arr){
  // '3' '4'
}
```

## Generator

```js
function* gen () {
  yield 1;
  yield 2;
  return 3;
}
const g = gen()
g.next() // { value: 1, done: false }
g.next() // { value: 2, done: false }
g.next() // { value: 3, done: true }
g.next() // { value: undefined, done: true }

// next方法参数当作上一条 yield 语句的返回值
```

## Promise

- all 返回所有 fulfill 或者第一个 reject 的原因
- allSettled 返回记录各个 promise 结果的数组
- any 返回第一个 fulfill 的 promise
- race 随着第一个 Promise 的最终状态而确定

## 动画

1. window.requestAnimationFrame(callback)
大多数浏览器在后台标签页或者隐藏的 iframe标签里时暂停调用，为了提高性能和电池使用寿命

2. setTimeout(fun, delay, param1,...)/setInterval()
根据 HTML 标准，setTimeout 调用 0ms 超时 五次以上时， 浏览器强制执行 4ms 的最小超时

如果想在浏览器中实现 0ms 延时的定时器，你可以参考 window.postMessage()

未被激活的 tabs 的定时最小延迟>=1000ms

## 操作符

- leftExpr ?? rightExpr
- `obj.val?.prop` `obj.val?.[expr]` `obj.func?.(args)`

## canvas

左上坐标原点

```js
const canvas = document.createElement( 'canvas' );
const context = canvas.getContext( '2d' );

// createPattern

canvas.toDataURL('image/png') // 默认参数

// fillRect/strokeRect/clearRect(x,y,width,height)

// drawImage(image, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight)
context.drawImage( image, 100, 100 );
// createImageData(width, height, settings)
// context.getImageData(sx, sy, sw, sh, settings)

// createRadialGradient/createConicGradient
// const gradient = context.createLinearGradient()
// gradient.addColorStop(0, '#0f0')
// gradient.addColorStop(1, '#f00')

// context.beginPath()
// context.arc(...)
// context.strokeStyle = "rgb(255 0 0 / 0.5)"
// context.stroke()

// addPath/closePath/moveTo/lineTo/arc/arcTo/ellipse/rect
// bezierCurveTo/quadraticCurveTo/roundRect
const path = new Path2D()

// startAngle: 0 x正轴
// counterclockwise: false 顺时针
path.arc(x, y, radius, startAngle, endAngle, counterclockwise)
// lineJoin
// lineDashOffset
// getLineDash()
// setLineDash([4, 2])

// translate() 移动 canvas 坐标原点
// rotate((Math.PI / 180) * degree) // 基于当前原点旋转
// scale(x, y)

// clip() 剪切

// shadowOffsetX/shadowOffsetY/shadowBlur/shadowColor

// save()
// restore()
```

拖尾：每次更新动画时，半透明的覆盖上一层背景色，从而淡化之前轨迹

```js
context.fillStyle = 'rgb(0 0 0 / 5%)'
context.fillRect(0, 0, canvas.width, canvas.height)
```

## click 事件

- screenX/Y 屏幕
- clientX/Y 从 content 计算（padding 及 border, margin 不包括）
- offsetX/Y 事件到目标节点 padding 边
- pageX/Y 存在滚动时，返回包括滚动到试图外的像素长度
- x/y clientX/Y 别名
- currentTarget(Event) 绑定事件处理程序的元素
- target(Event) 调度事件的元素

## Date

```js
const now = new Date();
console.log(now.toLocaleString("en-US", { timeZone: "UTC" }));
console.log(now.toLocaleString("en-US", { timeZone: "Asia/Shanghai" }));
```
