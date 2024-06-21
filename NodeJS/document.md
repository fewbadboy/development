# document

## Environment Variable

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
