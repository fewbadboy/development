# document

## Interface 和 Type

```ts
// 不能重复声明
type Animal = {
  name: string;
}

type Bear = Animal & { 
  honey: boolean;
}

const bear = getBear();
bear.name;
bear.honey;
```

接口可以继承(extends)

## type assertions

`as` and `<T>`

```ts
document.getElementById("main_canvas") as HTMLCanvasElement

<HTMLCanvasElement>document.getElementById("main_canvas")
```

## 字面量 types

```ts
declare function handleRequest(url: string, method: "GET" | "POST"): void;

// 不然 method 值被当作 string 解析，无法通过编译
const req = { url: "https://example.com", method: "GET" as "GET" };
// 或 const req = { url: "https://example.com", method: "GET"} as const;
handleRequest(req.url, req.method);
```

## null 和 undefined

`!`后缀移除 null 和 undefined 类型检查

```ts
function test(x?: number | null | undefined) {

  console.log(x!.toFixed())

  if (x != null) { // or x != undefined
    console.log('looser equality checks')
  }
}

```

## narrowing

缩小类型范围

- !=
- in
- instanceof
- is

## Function 重载

```ts
function makeDate(timestamp: number): Date;
function makeDate(m: number, d: number, y: number): Date;
```

## type guards

```ts
function isFish(pet: Animal): pet is Fish {
  return (pet as Fish).swim !== undefined
}
```

## KeyOf Type 操作符

```ts
type Some = { [k: string]: boolean }
type S = keyof Some // string | number
// JS 对象的 key 总是转换成 string, 因此 obj[0] 等价于 obj['0']
```

## 映射 类型

```ts
// 移除可选属性后重命名
type C<Type> = {
  [Property in keyof Type as `get${Capitalize<string & Property>}`]-?: () => Type[Property]
}

type Person = {
  id: number
  name?: string
}

type Car = C<Person>
```

## 公共 Type

- Awaited
- Partial
- Record
- Exclude 针对 UnionType
- Extract 提取
- NonNullable 排除 null 和 undefined

## module

```ts
/**
 * 不想在使用模块前花时间去编写声明时
 * 采用如下简写方式
 * 模块里所有导出的类型将是 any
 */ 
declare module "module-name";
```

模块解析的时候：

1. 相对导入 `/`, `./`, `../` 开头的，不能解析为一个外部模块的声明
2. 非相对导入的可以相对与 `baseUrl` 或路径映射(ts 配置文件中 paths 来声明映射)

文件仅导出单个 class 或 function, 使用 export default

## tsc CLI

```ts
tsc --init // create tsconfig.json
```
