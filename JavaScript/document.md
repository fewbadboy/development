# document

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

/**
 * 返回新的数组，不影响原数组
 */
Array.property.toReversed()
Array.property.toSorted(compareFn)
Array.property.toSpliced()
```

## Object

每一个属性名是一个 string 或 Symbol
`[]` 获取属性名时，任何非字符串对象都会通过 `toString` 方法转换

```js
Object.assign(target, source1, ...) // copy 所有自身可枚举属性, 返回第一个参数
Object.create(proto, propertiesObject)
Object.groupBy(items, callbackFn)
Object.hasOwn(obj, prop)
Object.is(NaN, NaN) // 比较严格相等

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
