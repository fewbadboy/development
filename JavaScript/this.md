# this

有严格模式和非严格模式之分

当前执行上下文(global, function, eval)的一个属性

## 全局上下文

无论是否在严格模式下，在全局执行环境中（在任何函数体外部）this 都指向全局对象

globalThis 可以获取全局对象，无论你的代码是否在当前上下文

## 函数上下文

函数内部，this 取决于函数被调用的方式

```js
function add(c, d) {
  return this.a + this.b + c + d
}

const o = { a: 1, b: 2 }

add.call(o, 3, 4) // 10

add.apply(o, [3, 4]) // 10

function type() {
  return Object.prototype.toString.call(this)
}

type.call([]) // [object Array]
type.call(null) // [object Window]
type.call(undefined) // [object Window]
```

bind 方法：永久的被绑定到 bind 的第一个参数，无论这个函数是如何被调用的

在箭头函数中，this与封闭词法环境的this保持一致。在全局代码中，它将被设置为全局对象

```js
const globalObject = this
const foo = (() => this)

const obj = {foo: foo}
obj.foo() === globalObject
foo.call(obj) === globalObject
```

## 作为构造函数

虽然构造函数返回的默认值是 this 所指的那个对象，但它仍可以手动返回其他的对象

## 作为一个 DOM 事件处理函数

this 总是指向触发事件的元素 `this === e.currentTarget`

## 类

```js
class Car {
  constructor() {
    // Bind sayBye but not sayHi to show the difference
    this.sayBye = this.sayBye.bind(this)
  }
  sayBye() {
    console.log(`Bye from ${this.name}`)
  }
  get name() {
    return 'Ferrari'
  }
}

class Bird extends Car {
  get name() {
    return 'Tweety'
  }
}

const car = new Car();
const bird = new Bird();


bird.sayBye = car.sayBye;
bird.sayBye();  // Bye from Ferrari
```
