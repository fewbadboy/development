# TypeScript

## Union Type

```ts
type fruits = "apple" | "banana" | "pear";

type Animal = {
  name: string;
};

type Bear = Animal & {
  honey: boolean;
};
```

```ts
interface SomeObject {
  value: string;
  log(): void;
}

class MyClass implements SomeObject {
  value: string;

  constructor(s: string) {
    this.value = s;
  }

  log() {
    console.log("value is:", this.value);
  }
}

type CatConstructor = {
  new (name: string): SomeObject;
};
```

```ts
type CatFunction = {
  description: string;
  (name: string): void;
};

function MyCat(name: string) {
  console.log(name);
}

MyCat.description = "default description";
```

## Type Assertions

```ts
(document.getElementById("main_canvas") as HTMLCanvasElement) <
  HTMLCanvasElement >
  document.getElementById("main_canvas");

const a = expr as any as T;

// 会让值变成字面量类型, 是具体的 red 而不是 string 类型
const color = "red" as const;
```

## Non-null Assertion

移除 null 和 undefined

```ts
function doWhat(x?: number | null) {
  return x!.toFixed();
}
```

## Generic

```ts
function getProperty<Type, Key extends keyof Type>(obj： Type, key: Key) {
  return obj[key]
}
```

## Function

```ts
function someFunction<Type extends { length: number }>(
  obj: Type
): Type {
  return obj;
}

// Overloads
function makeDate(input: string): void;
function makeDate(input: number): void；
function makeDate(input: number | string): void {
  if (typeof input === 'string') {
    console.log(input)
  } else {
    console.log(input)
  }
}
```

## Object Types

```ts
interface Person {
  name: string;
  age?: number;
}

interface PersonArray {
  [index: number]: Person;
}

interface NumberOrStringDictionary {
  [index: string]: number | string;
  name: string;
  age: number;
}

/**
 * 不同属性直接合并
 * 相同属性但类型不同会报错
 */
interface Colorful {
  color: string;
}
interface Circle {
  radius: number;
}

type ColorfulCircle = Colorful & Circle;

/**
 * Tuple Type
 */
type StringNumberBooleans = [string, number, ...boolean[]];
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

- 索引访问类型

```ts
type Person = { age: number; name: string; alive: boolean };
type I1 = Person["age" | "name"]; // string | number
```

- 条件类型

```ts
interface Animal {
  live(): void;
}
interface Dog extends Animal {
  woof(): void;
}

type Example = Dog extends Animal ? number : string;
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

## Classes

```ts
interface Animal {
  name: string;
  age?: number;
}

interface Dog extends Animal {}

// 实现带有可选属性的接口并不会创建该属性
class AnimalHouse {
  /**
   * 类成员的修饰符
   * public 默认
   * protected 当前类和子类
   * private 仅当前类
   * readonly
   */
  resident: Animal;

  constructor(animal: Animal) {
    this.animal = animal;
  }

  say() {
    console.log("Hello, AnimalHouse");
  }
}

class DogHouse extends AnimalHouse {
  static {
    console.log("static block");
  }

  /**
   * 类字段在父类构造函数完成后初始化，覆盖父类设置的任何值
   * 当您只想为继承的字段重新声明更准确的类型时，这可能会有问题
   * 为了处理这些情况，您可以编写 declare
   *  */
  declare resident: Dog; // declare 字段声明不应产生运行时效果

  constructor(dog: Dog) {
    super(dog);
  }

  say(name?: string) {
    if (name === undefined) {
      super.say();
    } else {
      console.log(`Hello, ${name.toUpperCase()}`);
    }
  }
}
```

```ts
// 结构上比较
class Person {
  name: string;
  age: number;
}

class Employee {
  name: string;
  age: number;
  salary: number;
}

const p: Person = new Employee();
```

## Modules

```ts
/**
 * 文件中无 import 或 export
 * 但你想文件被视为一个模块(模块作用域)
 * 不然文件就是一个全局脚本
 *  */
export {};
```

```ts
export type Cat = { breed: string };

import type { Cat } from "someFile";
```

## Utility Types

```ts
/**
 * 1. Awaited<Type>
 * 2. Partial<Type>: 返回给定类型的所有子集的类型
 * 3. Required<Type>: 设置 required, 去除 optional
 * 4. Readonly<Type>
 * 5. Record<Keys, Type>: 一个对象类型，键为 Keys，值为 Type
 * 6. Pick<Type, Keys>: 从 Type 中选择 Keys(字符串字面量或联合的字符串字面量)
 * 7. Omit<Type, Keys>: 从 Type 中移除 Keys
 * 8. Exclude<UnionType, ExcludedMembers>
 * 9. Extract<Type, Union>: 提取相同的成员
 * 10. NonNullable<Type>: 排除 null 和 undefined
 * 11. Parameters<Type>: 返回函数的参数类型
 * 12. ConstructorParameters<Type>
 * 13. ReturnType<Type>
 * 14. InstanceType<Type>
 * 15. ThisType<Type>
 *  */

Awaited<Promise<string>>;
```

## 资源释放管理

`using`
