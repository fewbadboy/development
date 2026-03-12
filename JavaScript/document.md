# document

## Data URLs

[MIME_types](https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types)
`data:[<media_type>][;base64],<data>`

## shallow clone

复制一个对象的第一层属性，对于嵌套的对象或数组，浅拷贝并不会递归地复制它们的内部数据，而是复制它们的引用

```js
// 添加 css 样式
console.log(
  `%c 成功:%c 成功的消息`,
  "color:green",
  "border: 1px solid green;color: red"
);
```

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

[HTML DOM API] 中表示 HTML 元素的对象

## Array

```js
// 静态方法
from(arrayLike, mapFn, thisArg) 执行mapFn 时使用
fromAsync(arrayLike, mapFn, thisArg)
of(element1, element2,...)
isArray()

Array.from(['first','last'],
  function (s) { return `${s} ${this.suffix}`},
  { suffix: 'Th' }
)

// 实例方法
at(index) // index < 0，index + array.length
concat()
copyWithin(target, start, end)
find(callbackFn, thisArg)
findLast(callbackFn, thisArg)
findIndex(callbackFn, thisArg)
findLastIndex(callbackFn, thisArg)
indexOf(searchElement, fromIndex)
lastIndexOf(searchElement, fromIndex)
fill(value, start, end)
flat(depth=1)
reduceRight((accumulator, currentValue, currentIndex, array) => {
  return accumulator + currentValue
}, initialValue)
slice(start, end)
splice(start, deleteCount, a1,...)
with(index, value) // 返回一个 value 替换 index 处值的数组
/**
 * 返回新的数组，不影响原数组
 */
toReversed()
toSorted(compareFn)
toSpliced()
```

## Object

每一个属性名是一个 string 或 Symbol
`[]` 获取属性名时，任何非字符串对象都会通过 `toString` 方法转换

```js
// 静态方法
assign(target, source1, ...) // copy 所有自身可枚举属性, 返回第一个参数
create(proto, propertiesObject)
groupBy(items, callbackFn)
hasOwn(obj, prop)
is(v1, v2) // 比较严格相等

const obj = { name: 'js' };
Object.getOwnPropertyDescriptors(obj);
// {
//   name: {
//     configurable: true
//     enumerable: true
//     value: "js"
//     writable: true
//   }
// }
```

1. `for...in` 遍历对象自身和继承的可枚举属性
2. `Object.keys()` 遍历对象自身的所有可枚举的属性的键名
3. `JSON.stringify()` 序列化对象自身可枚举的属性
4. `Object.assign()` 只拷贝对象自身可枚举的属性
5. `Object.getOwnPropertyNames()` 返回对象自身所有属性(不含 Symbol)
6. `Object.getOwnPropertySymbols()` 返回对象自身所有 Symbol 属性
7. `Reflect.ownKeys()` 包含自身的所有键名
8. `Object.getOwnPropertyDescriptors` 获取对象的所以自身属性的描述符

`JSON.parse` 纯文本转换，就算解析出来的看起来是脚本也绝对不执行，是安全的 API, 然后设置 `textContent` 或者 `innerText`

## globalThis

## Infinity

## Number

```js

// 静态
EPSILON
MAX_SAFE_INTEGER
NEGATIVE_INFINITY
isFinite()
isInteger()
isNaN()
isSafeInteger()
parseFloat()
parseInt()
// 实例方法
toFixed(digits); // 指定的小数位数
toPrecision(precision); // 指定的有效数字位数
```

## String

字符串以 UTF-16 编码表示

```js
const str = '½A'
// 示例方法
at(index) // 负值就是 index + length
charAt(index) // index 超出范围 0 -str.length - 1 返回空字符串
match(regexp)
padEnd(targetLength, padString)
replace(pattern, replacement)

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

## Set

- size
- add(value)
- delete(value)
- clear()
- entries() 返回 `[value,value]`
- has(value)
- intersection(s)
- isDisjointFrom(s) 不存在交集
- isSubsetOf(s)
- isSupersetOf(s)
- symmetricDifference(s)
- union(s)

## Map

- size
- get(key)
- set(key,value)
- has(key)
- delete(key)
- clear()
- entries()
- keys()
- values()

## WeakRef

- deref()

## Date

```js
Date.now();
Date.parse();

// ISO 8601, YYYY-MM-DDTHH:mm:ss.sssZ 或 ±YYYYYY-MM-DDTHH:mm:ss.sssZ
const now = new Date();
console.log(now.toLocaleString("en-US", { timeZone: "UTC" }));
console.log(now.toLocaleString("en-US", { timeZone: "Asia/Shanghai" }));
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

## Promise

```js
Promise.all() // 返回所有 fulfill 或者第一个 reject 的原因
Promise.allSettled() // 返回记录各个 promise 结果的数组
Promise.any() // 返回第一个 fulfill 的 promise, 都拒绝时返回一个拒绝原因的数组
Promise.race() // 随着第一个 Promise 的最终状态而确定
Promise.try(func, arg1, ...) // 接受任何回调
Promise.withResolvers() // 返回 { promise, resolve, reject }
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
// '2026年2月4日星期三 GMT+8 09:42:34'
```

### in

指定属性在指定对象或原型链中

### 属性访问器

分别是点和方括号

属性名称必须是字符串或符号 Symbol

任何非字符串对象都会通过 toString 方法，被转换成一个字符串

## Proxy

```js
// 简易响应式实现
const deps = new Map();

let activeEffect = null;
function watchEffect(effect) {
  activeEffect = effect;
  effect();
  activeEffect = null;
}

function reactive(object) {
  return new Proxy(object, {
    get(target, key) {
      if (!deps.has(key)) {
        // 简易收集依赖
        deps.set(key, []);
      }
      if (activeEffect) deps.get(key).push(activeEffect);
      return target[key];
    },
    set(target, key, value) {
      target[key] = value;
      // 触发更新
      if (deps.has(key)) {
        deps.get(key).forEach((fn) => fn());
      }
      return true;
    },
  });
}

const state = reactive({
  message: "hello",
});

input.addEventListener("input", (e) => {
  // 触发 set
  state.message = e.target.value;
});

watchEffect(() => {
  // 触发 get
  text.textContent = state.message;
});
```

## Encode

- encode/decodeURI()： 除 `A–Z a–z 0–9 - _ . ! ~ * ' ( ) ; / ? : @ & = + $ , #`
- encode/decodeURIComponent(): 除 `A–Z a–z 0–9 - _ . ! ~ * ' ( )`
- TextEncode/Decode(): 返回 UTF-8 编码的 Uint8Array
- btoa/atob(): Base64 加密解密

## 动画

1. window.requestAnimationFrame(callback)
大多数浏览器在后台标签页或者隐藏的 iframe 标签里时暂停调用，为了提高性能和电池使用寿命

2. setTimeout(fun, delay, param1,...)/setInterval(func, delay, arg1,...)
根据 HTML 标准，setTimeout 嵌套调用超时 五次以上时， 浏览器强制执行 4ms 的最小超时

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

未被激活的 tabs 的定时最小延迟>=1000ms

## MouseEvent

- screenX/Y 屏幕
- clientX/Y 窗口(iframe 是一个单独的窗口)
- offsetX/Y 事件到目标节点 padding 边
- pageX/Y 存在滚动时，返回包括滚动到试图外的像素长度
- x/y clientX/Y 别名

## Function

- apply(thisArg, argsArray)
- call(thisArg, arg1, arg2, …, argN)
- bind(thisArg, arg1, arg2, …, argN)
