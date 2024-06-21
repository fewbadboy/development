# document

[MDN web Docs 词汇表](https://developer.mozilla.org/en-US/docs/Glossary)

## shallow clone

所有标准内置对象复制操作都创建浅复制（扩展运算符。。。）

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
 */
Array.property.with(index, value)
```

## Object

`[]` 获取属性名时，任何非字符串对象都会通过 `toString` 方法转换

```js
Object.prototype.toString.call([]).slice(8, -1)
Object.is(NaN, NaN)
```

1. `for...in` 遍历对象自身和继承的可枚举属性
2. `Object.keys()` 遍历对象自身的所有可枚举的属性的键名
3. `JSON.stringify()` 序列化对象自身可枚举的属性
4. `Object.assign()` 只拷贝对象自身可枚举的属性
5. `Object.getOwnPropertyNames()` 返回对象自身所有属性(不含Symbol)
6. `Object.getOwnPropertySymbols()` 返回对象自身所有Symbol属性
7. `Reflect.ownKeys()` 包含自身的所有键名
8. `Object.getOwnPropertyDescriptors` 获取对象的所以自身属性的描述符

## String

- match/matchAll 返回所有正则捕获组匹配值

## 动画

requestAnimationFrame(callback): requestAnimationFrame() 运行在后台标签页或者隐藏的 iframe标签里时，被暂停调用

setTimeout()/setInterval()
根据 HTML 标准，setTimeout 调用 0ms 超时 五次以上时， 浏览器强制执行 4ms 的最小超时

如果想在浏览器中实现 0ms 延时的定时器，你可以参考 window.postMessage()

未被激活的 tabs 的定时最小延迟>=1000ms

## 操作符

- leftExpr ?? rightExpr
- `obj.val?.prop` `obj.val?.[expr]` `obj.func?.(args)`

## click 事件

- screenX/Y 屏幕
- clientX/Y 从 content 计算（padding 及 border, margin 不包括）
- offsetX/Y 事件到目标节点 padding 边
- pageX/Y 存在滚动时，返回包括滚动到试图外的像素长度
- x/y clientX/Y 别名
- currentTarget(Event) 绑定事件处理程序的元素
- target(Event) 调度事件的元素
