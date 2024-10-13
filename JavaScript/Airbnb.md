# JavaScript Style Guide

## Objects

### Do not call `Object.prototype` methods directly

1. 这些方法可能会被相关对象的属性所掩盖 `{ hasOwnProperty: false }`
2. 对象的原型可能是空对象 `Object.create(null)`

```js
// good
const obj = { className: 'red' }
Object.hasOwn(obj, 'className') // 静态方法取代旨在取代Object.prototype.hasOwnProperty()
Object.prototype.hasOwnProperty.call(obj, 'className')
```
