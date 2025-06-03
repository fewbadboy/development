# document

## Environment Variable(实验性)

设置环境变量
多个环境变量文件时，后续的会重写之前存在的变量

```js
node --env-file=.env --env-file=.development.env test.js
```

## module

### 循环依赖

```js
// a.js
console.log('a starting');
exports.done = false;
const b = require('./b.js');
console.log('in a, b.done = %j', b.done);
exports.done = true;
console.log('a done'); 

// b.js
console.log('b starting');
exports.done = false;
const a = require('./a.js');
console.log('in b, a.done = %j', a.done);
exports.done = true;
console.log('b done'); 

// main.js
console.log('main starting');
const a = require('./a.js');
const b = require('./b.js');
console.log('in main, a.done = %j, b.done = %j', a.done, b.done); 

// main starting
// a starting
// b starting
// in b, a.done = false
// b done
// in a, b.done = true
// a done
// in main, a.done = true, b.done = true
```

为防止无限循环，将 a.js 导出对象的 未完成的副本 返回给 b.js 模块。 然后 b.js 完成加载，并将其 exports 对象提供给 a.js 模块。

## 事件循环

每个阶段都有一个要执行的回调的 FIFO 队列

- timers: 通过 setTimeout() 和 setInterval() 执行回调的阶段
- pending callbacks: 延迟到下一个循环迭代的 i/o 回调
- idle, prepare: 仅用于内部
- poll: 检索新的 I/O 事件；执行 I/O 相关回调（几乎所有回调，关闭回调、计时器调度的回调和 setImmediate() 除外）；节点会在适当的时候阻塞在这里
- check: setImmediate() 回调在这调用(当前轮询阶段完成后执行脚本)
- close callbacks: 一些关闭回调, e.g. socket.on('close', ...)

微任务：

- `process.nextTick()` 是一个异步API, 从技术上讲不属于事件循环
- `Promise.then/catch/finally` 回调

## 非阻塞 I/O

libuv 负责处理事件循环、异步 I/O、线程池、定时器等核心功能

主线程提交任务(Node) → 线程池执行(libuv) → 回调进入事件循环 → 主线程执行回调

## 环境变量

```shell
NODE_ENV=production node app.js
```
