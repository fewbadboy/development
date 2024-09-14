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

### Prefer the object spread syntax over `Object.assign` to shallow-copy objects

```js
// bad
const original = { a: 1, b: 2 };
const copy = Object.assign({}, original, { c: 3 }); // copy => { a: 1, b: 2, c: 3 }

// good
const original = { a: 1, b: 2 };
const copy = { ...original, c: 3 }; // copy => { a: 1, b: 2, c: 3 }
```

## Array

### Use Array.from instead of spread ... for mapping over iterables

```js
// good
Array.from([], (x) => x ** 2)
```

## Function

### Never use `arguments`, opt to use rest syntax `...` instead

```js
// good
function concatenateAll(...args) {
  return args.join('');
}
```

### Avoid side effects with default parameters

## Properties

### Use bracket notation [] when accessing properties with a variable

```js
const luke = {
  jedi: true,
  age: 28,
};

function getProp(prop) {
  return luke[prop];
}
```
