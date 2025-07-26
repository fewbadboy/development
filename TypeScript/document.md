# TypeScript

## Type Assertions

```ts
// 会让值变成字面量类型, 是具体的 red 而不是 string 类型
const color = "red" as const;
```

## Type Manipulation

- keyof

```ts
type Point = { x: number; y: number };
type PointKey = keyof Point; // 'x' | 'y'

// JavaScript 对象键总是被强制转换为字符串, obj[0] 始终和 obj["0"] 相同
type StringObject = { [k: string]: boolean };
type StringObjectKey = keyof StringObject; // string | number
```

- typeof

```ts
function f() {
  return { x: 10, y: 3 };
}
type P = ReturnType<typeof f>; // { x: number, y: number }
```

- Mapped 类型

```ts
//从类型的属性中删除 ? ,同是改名
type Getters<Type> = {
  [Property in keyof Type as `get${Capitalize<
    string & Property
  >}`]-?: () => Type[Property];
};

type MaybeUser = {
  id: string;
  name?: string; // string | undefined
};

type User = Getters<MaybeUser>;
// { getId: () => string, name: () => string | undefined }
```

- 模板字面量类型

- `Uppercase<StringType>`
- `Lowercase<StringType>`
- `Capitalize<StringType>`

## Modules

```ts
/**
 * 文件中无 import 或 export
 * 但你想文件被视为一个模块(模块作用域)
 * 不然文件就是一个全局脚本
 *  */
export {};
```
