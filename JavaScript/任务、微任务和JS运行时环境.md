# 任务、微任务和 JS 运行时环境

## 任务（Tasks）

任务就是通过标准机制而被调度的任意 JavaScript 代码。这些都在任务队列（task queue）上被调度。

在以下时机，任务会被添加到任务队列：

- 一段新程序或子程序被直接执行时（比如从一个控制台，或在一个 `<script>` 元素中运行代码）。
- 触发了一个事件，将其回调函数添加到任务队列时。
- 达到由 setTimeout() 或 setInterval() 创建的超时或间隔时间，以致相应的回调函数被添加到任务队列时。

驱动代码的事件循环按照这些任务的排队顺序依次处理这些任务。在当前迭代轮次中，只有那些当事件循环过程开始时已经处于任务队列中的任务会被执行。其余的任务不得不等待到下一次迭代。

## 微任务（Microtasks）

首先，每当一个任务结束，事件循环都会检查该任务是否正把控制权交给其他 JavaScript 代码。如若不然，事件循环就会运行微任务队列中的所有微任务。接下来微任务循环会在事件循环的每次迭代中被处理多次，包括处理完事件和其他回调之后。

其次，如果一个微任务通过调用 queueMicrotask(), 向队列中加入了更多的微任务，则那些新加入的微任务 会早于下一个任务运行。这是因为事件循环会持续调用微任务直至队列中没有留存的，即使是在有更多微任务持续被加入的情况下。

### 入列微任务

入列微任务，新加入的 queueMicrotask() 方法（以定义的形式被暴露在 Window 或 Worker 接口上）增加了一种标准的方式。

避免使用 Promise 创建微任务

```js
queueMicrotask(() => {
  // 微任务中运行的代码
});
```

### 使用场景

捕捉或检查结果、执行清理等；其时机晚于一段 JavaScript 执行上下文主体的退出，但早于任何事件处理函数、timeouts 或 intervals 及其他回调被执行。

```js
element.prototype.getData = url => {
  if (this.cache[url]) {
    // 确保操作顺序的一致性，达到平衡两个子句的目的
    queueMicrotask(() => {
      this.data = this.cache[url];
      this.dispatchEvent(new Event("load"));
    });
  } else {
    fetch(url).then(result => result.arrayBuffer()).then(data => {
      this.cache[url] = data;
      this.data = data;
      this.dispatchEvent(new Event("load"));
    )};
  }
};

// 重复执行一下代码，结果一样
element.addEventListener("load", () => console.log("Loaded data"));
console.log("Fetching data...");
element.getData();
console.log("Data fetched");
```

以上代码如若未在 if 语句中加入微任务执行效果如下

| 数据未缓存    | 数据已缓存    |
| ------------- | ------------- |
| Fetching data | Fetching data |
| Data fetched  | Loaded data   |
| Loaded data   | Data fetched  |

### 来自函数的微任务

微任务不在其所处的函数退出时，而是在主程序退出时被执行

```js
let callback = () => log("Regular timeout callback has run");
let urgentCallback = () => log("*** Oh noes! An urgent callback has run!");
let doWork = () => {
  let result = 1;
  queueMicrotask(urgentCallback);
  for (let i = 2; i <= 10; i++) {
    result *= i;
  }
  return result;
};

log("Main program started");
setTimeout(callback, 0);
log(`10! equals ${doWork()}`);
log("Main program exiting");

// Main program started
// 10! equals 3628800
// Main program exiting
// *** Oh noes! An urgent callback has run!
// Regular timeout callback has run
```

## 执行上下文

下面 3 种类型的代码会创建一个新的执行上下文：

- 全局上下文是为运行代码主体而创建的执行上下文
- 每个函数会在执行的时候创建自己的执行上下文
- 使用 eval() 函数也会创建一个新的执行上下文

每个上下文创建的时候会被推入执行上下文栈。当退出的时候，它会从上下文栈中移除。

## JavaScript 运行时

JavaScript 运行时实际上维护了一组用于执行 JavaScript 代码的代理。
每个代理由一组执行上下文的集合、执行上下文栈、主线程、一组可能创建用于执行 worker 的额外的线程集合、一个任务队列以及一个微任务队列构成。

每个代理都是由事件循环驱动的，事件循环负责收集用事件（包括用户事件以及其他非用户事件等）、对任务进行排队以便在合适的时候执行回调。
然后它执行所有处于等待中的 JavaScript 任务（宏任务），然后是微任务，然后在开始下一次循环之前执行一些必要的渲染和绘制操作。
