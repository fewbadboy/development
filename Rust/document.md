# rust

[doc.rust-lang](https://doc.rust-lang.org/std/index.html)
[rust-by-example](https://doc.rust-lang.org/rust-by-example/index.html)

## 基础语法

Rust 允许先声明，后初始化(保证使用前已经赋值)。

```rust
/**
 * 变量默认是不可变的
 * 1. mut : 变量可被修改
 */
let mut age = 12;
age = 18;

/**
 * 常量：
 * 1. 不允许使用 mut 和常量一起使用
 * 2. 任何范围声明
 * 3. 只能是常量表达式，不能是运行时计算的值
*/
const THREE_HOURS_IN_SECONDS: u32 = 60 * 60 * 3;

/**
 * Shadowing
 * 重新声明一个变量覆盖前一个
 */
let x = 5;
let x = x + 1;
{
  let x = x * 2;
}

/**
 * Rust 不提供原始类型之间的类型隐式转换
 * 需要借助 as 转换
 * 类型推断 Inference
 */

// 表达式加分号是一个声明，没分号表达式在块中成为了返回值
let x = { let y = 1; y + 1 }; // x = 2

/**
 * 函数
 */
fn plus_one(x: i32) -> i32 {
  // 返回值末尾不带分号
  x + 1
}

/**
 * 控制流
 */
let condition = true;
let age = if condition { 18 } else { 6 };

// 外部标签 'outer_loop
'outer_loop: loop {

  loop {
    // 跳出到外部标签
    break 'outer_loop;
  }
}

let mut number = 3;
while number != 0  {
  number -= 1;
}

/**
 * 1..6 不包含 6
 * 范围的另一个写法 1..=5 包含两端数据
 */
for number in (1..4).rev() {
  print("{number}");
}

/**
 * iter 每次迭代都会借用集合中的每个元素
 * into_iter 在循环内被 move
 * iter_mut 可变地借用了集合中的每个元素，从而允许对集合进行修改
 */
let names = vec!["Rust", "Java"];
for name in names.iter() {
  match name {
    &"Rust" => println!("Rust !!");
    _ => println!("{}", name);
  }
}
for name in names.into_iter() {
  match name {
    "Rust" => println!("Rust !!");
    _ => println!("{}", name);
  }
}
// names 为 mut
for name in names.iter_mut() {
  *name = match name {
    &mut "Rust" => "Mut Rust";
    _ => "rust";
  }
}

/**
 * match 用多种方式解构
 * - 元组
 * - 数组/切片
 * - 枚举: enum Color { RED }
 * - 指针/ref
 * - structs: struct Student { name: &str, .. }
 */
match item {
  i if i == 0 => , // 守卫
  i @ 2..=10 => , // @ 将值绑定到名称
  (1, .., 4) => , // First is `1`, last is `4`, and the rest doesn't matter
  [1, second, ..] => ,
  Color::RED => ,
  Student {
    name,
    ..
  } => ,
}

let reference = &4;
match reference {
  &val => ,
}

match *reference {
  val => ,
}

// ref 创建一个引用(解构时 borrow 数据，而不是转移 ownership)
let mut mut_value = 6;
match mut_value {
  ref mut m => {
    *m += 10;
  }
}

// 解决匹配枚举 或 while let
let number = Some(7);
if let Some(n) == number {
  // n
}
```

## 函数

- 闭包 (Closure)

语法： `|val| val + x`

优先是通过引用捕获变量(&T, &mut T 闭包只是 borrow 外部变量)，只有在需要时才通过值捕获变量(闭包获得变量所有权)

move 闭包：把闭包中用到的外部变量的所有权移进闭包内部

```rust
// 声明类型
let a = |x: i32| -> i32 { x };
```

## Modules

- `mod` 声明模块，模块内的代码对父模块默认是私有的
  - 公开模块使用 `pub`
  - 公开模块内部需要公开的部分也需要 `pub` 去声明
- `use` 创建快捷引用方式(`pub use` 重新导出)
- 绝对路径 `crete::` 开头(推荐在 main.rs / lib.rs) 中使用
- 相对路径：当前模块为基准，如 `super::` `self::`当前模块作用域

## 集合

- `Vet<T>`

```rust
let v: Vec<i32> = Vec::new();
// 宏实现
let v = vec![1, 2, 3]; // println!("{:#?}", v);
let third: &i32 = &v[2];
let third: Option<&i32> = v.get(2);
match third {
  Some(third) => println!("{}", third),
  None => println!("no third element")
}
```

- String 可增长，存储 utf-8 编码的文本

```rust
let s1 = String::from("Hello");
let s2 = String::from("Rust");
// format! 宏组合复杂字符串(不占所有权)
let s3 = format!("{s1}-{s2}");
```

字符串进行索引通常是一个不好的操作(无法确定返回值是字节值、字符、字素簇还是字符串切片)

- Hash Map

```rust
use std::collection::HashMap;
let mut scores = HashMap::new();
scores.insert(String::from("Blue"), 10);

let team_name = String::from("Blue");
/**
 * get(), 返回 Option<&i32>
 * copied(): Option<&i32> -> Option<i32>，复制 &i32 的值(i32 实现了 Copy trait)
 * unwrap_or(0): 有值返回值，None 返回 0
 */
let score = scores.get(&team_name).copied().unwrap_or(0);
for (key, value) in &scores {
  println!("{key}: {value}");
}

scores.entry(String::from("Yellow")).or_insert(50);
```

## 泛型、Traits、生命周期

### Ownership

内存：

- 栈存储的数据都是固定大小的
- 堆存储编译时大小未知或大小可能变化的数据(指向堆的指针大小固定存储在栈上)

Ownership 规则：

- Each value in Rust has an **owner**
- There can only be one owner at a time
- When the owner goes out of scope, the value will be dropped

存储栈上的数据，作用域结束时从栈顶弹出；可以快速的轻松的复制一个新的值；不同作用域使用相同的值是独立的实例

假如设计的时候(赋值时)两个变量同时指向一个堆地址，两次释放内存可能会导致内存损坏，从而可能导致安全漏洞

复制指针、长度和容量而不复制数据的概念听起来可能像是浅拷贝。
但由于 Rust 也会使第一个变量失效(Borrow Checker 作用)，所以它不被称为浅拷贝，而被称为 move。

## Reference 和 Borrowing

Reference 是一个地址，可以跟踪它访问存贮该地址的数据(引用保证在该引用的生命周期内指向特定类型始终有效)
Reference (`&`)默认是不可变的，`&mut T` 去声明可变

```rust
fn main() {
  let mut s = String::from("hello");
  change(&mut s);
  println!("{}", s); // hello, world
}

fn change(some_string: &mut String) {
  some_string.push_str(", world");
}
```

同一作用域下，要么有多个不可变引用，要么只有一个可变引用。

```rust
fn main() {
  let mut s = String::from("hello");
  let s1 = &s;
  let s2 = &s;
  println!("{s1}, {s2}");
  let s3 = &mut s;
  println!("{s3}");
}
```

Dangling 引用：一个引用（&T）指向了已经释放的内存
编译器保证引用永远不会是 Dangling 引用(通过 Ownership 和 Lifetime 避免)

```rust
fn main() {
  let s = String::from("hello");
  let r1 = &s; // 这叫 immutable reference，行为是 borrowing
  print_string(r1); // 把 s 传给函数，Rust 叫做 borrow（只是临时使用，没有取得所有权）
  println!("原来的 s 还能用：{}", s);
}
fn print_string(s: &String) {
  println!("你借来了：{}", s);
}
```

## 智能指针
