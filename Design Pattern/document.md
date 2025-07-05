# Design Pattern

JavaScript 实现简易的设计模式

## 单例模式

```js
class Singleton {
  static #instance;

  #data = {
    name: "single",
  };

  static {
    console.log("1. Class static initialization block called");
  }

  constructor() {
    console.log("2. constructor called");

    if (Singleton.#instance) {
      return Singleton.#instance;
    }

    Singleton.#instance = this;
  }

  getKey(key) {
    return this.#data[key];
  }

  setKey(key, value) {
    this.#data[key] = value;
  }
}

const config1 = new Singleton();
const config2 = new Singleton();

console.log(config1 === config2); // 输出: true
```

## 观察者模式(发布订阅模式)

Observer

```js
class Publisher {
  constructor() {
    this.subscribers = [];
  }

  subscribe(observer) {
    this.subscribers.push(observer);
  }

  unsubscribe(observer) {
    this.subscribers = this.subscribers.filter((sub) => sub !== observer);
  }

  notifyAll(message) {
    this.subscribers.forEach((sub) => sub.update(message));
  }
}

class Subscriber {
  constructor(name) {
    this.name = name;
  }

  update(message) {
    console.log(`${this.name} 接收到信息：${message}`);
  }
}

const publisher = new Publisher();
const sub1 = new Subscriber("订阅者A");
const sub2 = new Subscriber("订阅者B");

publisher.subscribe(sub1);
publisher.subscribe(sub2);
publisher.notifyAll("重大突发新闻！");
```

## 命令模式

```js
class Command {}
```
