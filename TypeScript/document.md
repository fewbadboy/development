# document

## automatically infer the types

大多数情况下，不需要类型注解 (Annotations)

## type assertions

`as` and `<T>`

```ts
document.getElementById("main_canvas") as HTMLCanvasElement

<HTMLCanvasElement>document.getElementById("main_canvas")
```

## literal types

```ts
// 对线要声明类型用于读写数据
type Person = { name: string }

const obj: Person = {
  name: 'person'
}
```

```ts
declare function handleRequest(url: string, method: "GET" | "POST"): void;

// 不然 method 值被当作 string 解析，无法通过编译
const req = { url: "https://example.com", method: "GET" as "GET" };
// 或 const req = { url: "https://example.com", method: "GET"} as const;
handleRequest(req.url, req.method);
```

## null and undefined

`!`后缀移除 null 和 undefined 类型检查

```ts
function test(x?: number | null | undefined) {

  console.log(x!.toFixed())

  if (x != null) { // or x != undefined
    console.log('looser equality checks')
  }
}

```

## type of

```ts
// typeof
symbol
bigint
```

## narrowing

缩小类型范围

- !=
- in
- instanceof
- is

## Function Overloads

```ts
function makeDate(timestamp: number): Date;
function makeDate(m: number, d: number, y: number): Date;
```

## tuple

确切地知道它包含多少元素，以及它在特定位置包含哪些类型

## type guards

```ts
function isFish(pet: Animal): pet is Fish {
  return (pet as Fish).swim !== undefined
}
```
