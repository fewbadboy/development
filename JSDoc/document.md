# document

## Block Tags

- abstract 定义的成员在继承的时候必须要实现
- access `@access <package|private|protected|public>`
- alias
- async
- augments 标记指示继承自哪
- author `@author <name> [<emailAddress>]`
- borrows

```js
/**
 * @namespace
 * @borrows aliasTrim as trim
 */
var util = {
    trim: aliasTrim
};

/**
 * Remove whitespace from around a string.
 * @param {string} str
 */
function aliasTrim(str) {}
```

- callback

针对 class-specific 的 callback

```js
/**
 * @class
 */
function Requester() {}

/**
 * Send a request.
 * @callback Requester~requestCallback
 * @param {number} responseCode
 * @param {string} responseMessage
 */
Requester.prototype.send = function(cb) {}
```

全局的 callback

```js
/**
 * Send a request.
 * @param {requestCallback} cb - The callback that handles the response.
 */
Requester.prototype.send = function(cb) {}
```

- class `@class [<type> <name>]`
- classdesc 提供一个 class 的描述信息
- constant
- constructs
- copyright
- default
- deprecated
- description
- enum
- event `@event <className>#[event:]<eventName>`
- example
- exports
- external
- file
- fires `@fires <className>#[event:]<eventName>` 调用时触发指定类型的事件
- function
- generator
- global
- hideconstructor
- ignore
- implements 实现接口
- inheritdoc 继承文档
- inner 将标记为其父符号的内部成员
- instance
- interface
- kind 标识类型
- lends
- license
- listens
- member `@member [<type>] [<name>]`
- memberof 标识属于哪个
- mixes
- mixin
- module
- name
- namespace
- override
- package
- param
- private
- property
- protected
- public
- readonly
- requires 标识当前代码需要的模块
- returns
- see
- since
- static
- summary
- this
- throws
- todo 记录需要完成的任务
- tutorial
- type

```js
/**
 * @typedef person
 * @type {object}
 * @property {string} name - user name
 * @property {number} age - user age
 * @property {*} note - user note
 */


/**
 * @param {(number|string|boolean)} multiple 多类型
 * @param {Array.<number>} array 数组中元素是数值
 * @param {?Object.<string,number>} object 一个键是字符串，值是数值的对象 或 null
 * @param {{name: string, age: !number, note}} person note 是任何类型, age 不是 null
 * @param {number} [year=1] 可选参数 year, 默认值 1
 */
function custom (multiple, array, object, person, year) {}
```

- typedef
- variation
- version
- yields

## Inline Tags

- {@link} 创建一个 link
- {@tutorial}
