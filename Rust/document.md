# Rust

## 安装

[installation](https://rust-lang.github.io/rustup/installation/index.html)

```shell
# 临时使用环境变量 
# export RUSTUP_DIST_SERVER "https://rsproxy.cn"

# 永久配置字节跳动镜像
setx RUSTUP_DIST_SERVER "https://rsproxy.cn"
setx RUSTUP_UPDATE_ROOT "https://rsproxy.cn/rustup"

# update 默认访问官方服务器 https://static.rust-lang.org
rustup update

# 验证安装成功
rustc -V
cargo -V
```

## Cargo 包管理工具

```shell
# 创建库 默认行为 --bin
cargo new rust_puppy --lib # 创建 library

# 编译然后运行 ./target/debug/rust_puppy
cargo build --release
# ./target/release/rust_puppy

# 编译并执行
cargo run
```

## 语法

### 基本类型

- 字符，固定 4 个字节内存，Unicode 类型
- 字符串是 UTF-8 编码，字符串所占的字节变化的(1-4),有助于大幅度降低占用空间

```rust
let hello = String::from("world");
let world = String::from("world");
//字符串 + 或者 += 右边的参数必须为字符串的 Slice 类型
let hello_world = hello + &world; // 相当于调用了标准库中的 add(self, s: &str)
```rust
// 1..5 不包含 5
// 生成 1 - 5 连续数值
for i in 1..=5 {

}
for ch in "中国人".chars() {
  println!("{}", c);
}
```

- &str `字符串字面量是切片`(Slice)

- () 单元类型，不占内存，main 函数返回的就是单元类型
- (i32, f64) 元组，长度固定，复合类型，索引获取元组成员 `("1", 2.00).0`
- 结构体

```rust
#[derive(Debug)]
struct User {
  username: String,
  email: String,
}

trait SomeTrait {}

impl SomeTrait for User {}

let user = User {
  username: String::from("rust"),
  email: String::from("rust@gov.cn"),
}

// 结构体更新
let user_n = User {
  email: String::from("rust@edu.cn"),
  ..user // 凡是没显示声明的字段全部从 user 自动获取
}
```

初始化示例每个字段都需初始化

- 数组

```rust
// 固定长度,所有元素是同一类型
let a: [i32; 3] = [1, 2, 3];
// 动态增长
let mut vet = Vector::new();
```

## Ownership(管理内存)

栈：数据占用已知且固定大小的内存空间
堆：大小未知或者可能变化的数据

```rust
#![allow(unused_variables)]
let hello = String::from("hello"); // &str 类型生成 String 类型
// "hello".to_string()

// String 类型转 &str
// &hello 
// &hello[..]
// &hello.as_str()

let world = hello; // move 转移所有权
```

## Borrow

可以有多个不可变引用

```rust
let mut hello = String::from("hello");
change_hello(&mut hello);

fn change_hello(str: &mut String) {
  str.push_str(", world")
}
```

## Copy

只发生在栈上，性能很高
任何基本类型的组合都是可以 Copy 的
不需要分配内存或者某些形式资源的类型也是可 Copy 的
如整数，布尔，浮点，字符，元组， 不可变引用 &T

### 字符串

智能指针，作为一个结构体(三部分组成，指针：指向堆上字节数组，已使用长度，分类容量)存储栈上

如果 String 的当前容量够，添加字符不会导致新内存分配(原有数据拷贝到新地址上)

重新分配的内存时原来内存的两倍

```rust
// 初始内存大小 20 字节
let mut s = String::with_capacity(20);
```

`utf8_slice` 库准确的从 UTF-8 字符串中获取子串，标准库是做不到的

## 生命周期

就是引用的有效作用域

生命周期标注并不会改变任何引用的实际作用域
标记的生命周期只是为了取悦编译器，让编译器不要难为我们

```rust
// 静态生命周期，和整个程序活得一样久，引用本身还是受限于它自己的作用域
let s: &'static str = "活得久";
```

1. 每个引用参数都会获得独立的生命周期
2. 若只有一个输入生命周期(函数参数只有一个引用类型)，那么该声明周期就会被赋予输出生命周期
3. 若存在多个输入生命周期且其中一个是 `&self` 或 `&mut self` 则 `&self` 的生命周期被赋予给所有的输出生命周期
4. 不满足 2, 3 时需要手动标注生命周期

Higher Ranked Trait Bounds(更高等级特征约束) `for<'a> &'a T: PartialEq<i32>`

Non-Lexical Lifetime(NLL)

```rust

```

## 模式匹配

`match` 多模式匹配 和 `if let` 一个模式匹配

```rust
let val = Some(4);
if let Some(v) = val {
  println!("{}", v);
} else {
  println!("{}", v);
}

// matches! 宏
matches!(val, Some(x) if x > 2); // 返回匹配结果 true 或 false

enum Message {
  Hello { id: i32 }
}
let msg = Message::Hello {id: 1}
match msg {
  Message::Hello {id: id_other @ 2..=10} => { // @ 允许为一个字段绑定另一个变量
    println!("{}", id_other);
  },
  Message::Hello { id } => {
    println!("Found some other id: {}", id);
  }
}

struct Point {
  x: i32,
  y: i32,
}
// @前绑定后解构
let p @ Point {x: px, y: py } = Point {x: 10, y: 23};
```

解构 Option: 一个变量要么有值，要么为空 None

```rust
enum Option<T> {
  None,
  Some(T),
}
enum Result<T, E> {
  OK(T),
  Err(E),
}
```

## 方法和关联函数

```rust
mod my {
  pub struct Rectangle {
    width: u32,
    pub height: u32,
  }
  impl Rectangle {
    // 关联函数 定义在 impl 且没有 self
    pub fn new() -> Self {
      Self {
        width: 0,
        height: 0,
      }
    }
    pub height(&self) -> u32 {
      return self.height;
    }
    // &self 是 self: &Self 的语法糖，Rectangle 的不可变借用
    // &mut self
    pub fn area(&self) -> u32 { 
      self.width * self.height
    }

    pub fn can_hold(&self, other: &Rectangle) -> bool {
      self.width > other.width && self.height > other.height
    }
  }
}
```

## 泛型和特征(Trait)

```rust
struct Point<T> {
  x: T,
  y: T,
}

impl<T> Point<T> {
  fn x(&self) -> &T {
    &self.x
  }
}

fn generic<T>(_s: T) {}
// 显示指定类型参数 generic::<char>('a')
```

```rust
use std::fmt::Display;
// Trait
pub trait Summary{
  type Item; // 关联类型，声明的一个自定义类型

  fn summarize(&self) -> String;
  // 或者添加默认实现
  // fn summarize(&self) -> String{
  //   // 默认实现
  //   String::from("Read more")
  // }
}

impl Summary for Point<T> {
  type Item = i32;
  fn summarize(&self) -> String {
    format!("x:{}, y:{}", self.x, self.y)
  }
}

// trait 作为参数
pub fn notify(item: &(impl Summary + Display)) {}
//  形如 T: Summary 被称为 特征约束
// 多重约束
// pub fn notify<T: Summary + Display>(item: &T) {}
// Where 约束, 返回类型实现了 Summary 特征
pub fn notify<T>(item: &T) -> impl Summary 
// 返回确定单一类型， if / else 中假如是俩类型，那么就视为是两种类型
// 不是任意实现了 Summary 的类型
where T: Summary + Display {
  Point { x: 1, y: 1 }
}

// derive 派生 trait, 标记的对象自动实现对应的默认 trait，继承相应的功能
// 给结构体标记 Debug 特征后，就可以使用 {:?} 形式输出结构体对象了
#[drive(Debug)] 

fn sum(T: std::ops::Add<Output = T>)(x: T, y: T) -> T {
  x + y
}

// 特征对象 Box<dyn 特征对象> 或 &dyn 特征对象, 实例化时 Box::new(xx)
// self 当前实例对象
// Self 特征或者方法类型别名

// 完全限定语法(存在多个同名函数的时候使用)
// <Type as Trait>::function(receiver_if_method, next_arg, ...);
```

## 集合

```rust
{
  let mut v: Vec<i32> = Vec::new();
  let first = &v[0]
  v.push(1); // 数组可变，旧数组大小不够时 重新分配内存，拷贝旧数据 之前引用显然无效
}

{
  let v = vec![1, 2, 3];
  // &v[0] 数组越界报错退出
  // v.get(0) 内除处理数组越界 有值Some(x),没值 None
  let mut v1 = Vec::new();
  for i in &v {
    v1.push(*i);
  }
}
```

HashMap 使用的哈希函数是 SipHash，它的性能不是很高，但是安全性很高
对于小型的 Key （例如整数）或者大型 Key （例如字符串）来说，性能还是不够好，可考虑换哈希函数[ahash](https://github.com/tkaitchuck/ahash)
