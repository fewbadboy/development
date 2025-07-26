# document

```js
// let 不能重复声明，TDZ
```

## Data URLs

[MIME_types](https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types)
`data:[<media_type>][;base64],<data>`

## shallow clone

复制一个对象的第一层属性，对于嵌套的对象或数组，浅拷贝并不会递归地复制它们的内部数据，而是复制它们的引用

```js
const obj = { name: "张三", age: 18, skill: ["javascript", "python"] };
const copy = Object.assign({}, obj, { sex: "男" });
obj.name = "李四";
// copy.name '张三'

const shallow = { ...obj };
obj.name = "李四";
// shallow.name '张三'
obj.skill.push("java");
// shallow.skill ['javascript', 'python', 'java']

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
Array.from(arrayLike, mapFn, thisArg)
Array.fromAsync(arrayLike, mapFn, thisArg)
Array.of(element1, element2,...)
Array.isArray()

Array.property.at(index) // index < 0，index + array.length
Array.property.copyWithin(target, start, end)
Array.property.fill(value, start, end)
Array.property.findLast()

/**
 * accumulator: 上一次调用 callbackFn 的结果。
 * 在第一次调用时，如果指定了 initialValue 则为指定的值，否则为 array[0] 的值。
 * currentValue: 当前元素的值。在第一次调用时，如果指定了 initialValue，则为 array[0] 的值，否则为 array[1]。
 */
Array.property.reduce((accumulator, currentValue,currentIndex, array) =>{}, initialValue)

/**
 * 返回新的数组，不影响原数组
 */
Array.property.toReversed()
Array.property.toSorted(compareFn)
Array.property.toSpliced()
/**
 * 返回一个用给定 index 替换 value 的新数组
 * 同样是浅拷贝
 */
Array.property.with(index, value)

const arr = [1, 2, { name: 'test' }, 4, 5];
const tes = arr.with(1, 6); // 改变给定索引的值,返回新数组
arr.at(2).name = 'hello'
// tes.at(2).name
```

## Object

每一个属性名是一个 string 或 Symbol
`[]` 获取属性名时，任何非字符串对象都会通过 `toString` 方法转换

```js
Object.assign(target, source1, ...) // copy 所有自身可枚举属性, 返回第一个参数
Object.create(proto, propertiesObject)
Object.freeze(obj) // 阻止对象扩展
object.fromEntries(iterable) // reverse of Object.entries()
Object.groupBy(items, callbackFn)
Object.hasOwn(obj, prop)
Object.seal(obj) // 阻止对象扩展和现有属性不可配置

Object.is(NaN, NaN) // 比较严格相等

Object.prototype.toString.call([]).slice(8, -1)

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

## String

字符串 utf-16 编码单元序列，意思最大字符的表示值为 65535

Unicode 字符集(U+0000 - U+10FFFF)远大于 65535 个
额外的字符以`surrogate pairs`（代理对）的形式存储在 utf-16 中

为了避免歧义，`0xD800` 到 `0xDFFF` 不会用来表示单个字符编码
`0xD800` 到 `0xDBFF` 前代理项(高代理项),它是字符串中的最后一个码元，或者下一个码元不是尾代理
`0xDC00` 到 `0xDFFF` 尾代理项(低代理项),它是字符串中的第一个码元，或者前一个码元不是前代理
每个 Unicode 字符由 1 或 2 个 uft-16 编码单元组成，字符串表示为 `\u{xxxxxx}`, x 表示 1-6 位 16 进制数

除 Unicode 字符外，某些字符序列应视为一个视觉单元如 emoji(许多具有多种变体的 emoji，实际上是由多个 emoji 组成`U+200D`字符连接)

```js
[..."👨‍👦"]; // [ '👨', '‍', '👦' ]
```

```js
String.fromCharCode(num1,...) // 返回字符串,0 <= num <= 0xFFFF
String.fromCodePoint(num1,...) // 返回字符串,0 <= num <= 0x10FFFF
String.raw() // 模板文字的原始字符串
String.prototype.at()
String.prototype.charCodeAt()
String.prototype.codePointAt()
String.prototype.match(regexp) //
String.prototype.localeCompare(compareString, locales, options)
String.prototype.padStart(targetLength, padString)
String.prototype.padEnd(targetLength, padString)
String.prototype.replace(pattern, replacement)
String.prototype.search(regexp)
String.prototype.split(separator, limit)
String.prototype.trim()
```

## Math

```js
// 负数主要，判断奇数同样通过 0 去实现
function isEven(n) {
  return n % 2 === 0;
}

function isOdd(n) {
  return n % 2 !== 0;
}
```

## Number

```js
Number.prototype.toFixed(digits); // 指定的小数位数
Number.prototype.toPrecision(precision); // 指定的有效数字位数
```

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

```js
const arr = [3, 4];
arr.foo = 5;
for (let i in arr) {
  // '0' '1' 'foo'
}
for (let i of arr) {
  // '3' '4'
}
```

## Generator

```js
Generator.prototype.next(value);
Generator.prototype.return(value);
Generator.prototype.throw(exception);
function* gen() {
  yield 1;
  yield 2;
  return 3;
}
const g = gen();
g.next(); // { value: 1, done: false }
g.next(); // { value: 2, done: false }
g.next(); // { value: 3, done: true }
g.next(); // { value: undefined, done: true }

// next方法参数当作上一条 yield 语句的返回值
```

## RegExp

`()` 捕获组，存储匹配内容，供后续引用或替换， 如 `\1` 引用
`(?<name>)` 命名捕获组

`(?:)` 非捕获组，相当于匹配空字符串

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

## Modules

```js
// math.js
export const pi = 3.14;
export default class RandomNumber {}

// 别名调用
import * as math from "math.js";
math.default;

// 解构调用
import { default as RandomNumber } from "math.js";

// 重导出
export * from "math.js";
```

## 操作符

[优先级](https://developer.mozilla.org/zh-CN/docs/Web/JavaScript/Reference/Operators/Operator_precedence#%E6%B1%87%E6%80%BB%E8%A1%A8)

### 解构

取出数组或对象中的值，赋值给其他变量

### 幂

```js
2 ** 10; // 1024
```

### in

指定属性在指定对象或原型链中

### instanceof

### ??

左侧为 null 或 undefined 时返回右侧操作数

### 属性访问器

分别是点和方括号

属性名称必须是字符串或符号 Symbol

任何非字符串对象都会通过 toString 方法，被转换成一个字符串

## Proxy

```js
const target = {};
const proxy = new Proxy(target, {
  get(target, property, receiver) {},
  set(target, property, value, receiver) {
    return true; // 代表属性设置成功
  },
  apply(target, thisArg, argumentsList) {},
  defineProperty(target, property, descriptor) {},
  deleteProperty(target, property) {},
  getPrototypeOf(target) {},
  setPrototypeOf(target, prototype) {},
});
```

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

## Reflect

```js
Reflect.apply(target, thisArgument, argumentsList)
Reflect.construct(target, argumentsList[, newTarget])
Reflect.get(target, propertyKey[, receiver])
Reflect.set(target, propertyKey, value[, receiver])
Reflect.getPrototypeOf(target)
Reflect.setPrototypeOf(target, prototype)
Reflect.isExtensible(target)
Reflect.preventExtensions(target)
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
- currentTarget(Event) 绑定事件处理程序的元素
- target(Event) 调度事件的元素

## [uint8array-to-string](https://ourcodeworld.com/articles/read/164/how-to-convert-an-uint8array-to-string-in-javascript)

[HTML DOM API]: https://developer.mozilla.org/zh-CN/docs/Web/API/HTML_DOM_API
