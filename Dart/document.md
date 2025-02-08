# Dart

Flutter 开发多平台使用语言

## 基础表达式

```dart
// 变量声明
var solar = 1.0;

// null 或 string 类型, 初始化时值为 null
String? name;

// 非空变量使用前必须初始化值
int age = 0;

// 延迟初始化
late int year；

/**
  * const 变量是编译时的常量(隐式包含了 final)
  * 实例变量可以是 final 但不能是 const
  * final 对象的字段可以修改，变量只能被赋值一次
  * const 对象及其字段都不能被修改
 */ 

// 导入和排除指定的部分
import 'package:lib/math.dart' show sin;
import 'package:lib/math.dart' hide sin;

// 枚举
enum Gender { Male, Female }

// 自定义 metadata 注释
class Todo {
  final Sting name;
  final String thing;

  const Todo(this.name, this.thing);
}

@Todo('Dash', '实现当前函数')
void doSomething() {}
```

### 操作符

- `?[]` 条件下标访问
- `~/` 返回值类型 int
- `..` `?..` 级联操作符：对同一对象进行操作

```dart
querySelector('#video')
?..classes.add('mark')
..onClick.listen((e) => {})
```

- `...?` 扩展运算符排除 null

```dart
var list1 = [1, 2, 3];
var list2 = [4, ...?list1];
```

- `is` 对象是某个类型 `is!` `as` 类型转换
- `??=`

## 类型

```dart
// num 声明变量时可操作 int 和 double 俩类型
num x = 1;
x += 2.5;
```

- Numbers/String/Boolean
- Function

```dart
const list = ['a'];
// 定义函数时，使用 {param1, param2,...} 指定命名参数
void say({ required String name, int age = 18 }) {}

// 函数调用
say(name: 'dart', age: 19);

// 位置参数, age 可选参数
void say(String name = 'dart', [int age = 18]){}

// 匿名函数 和 仅有一个返回值时函数的简写
list.map((item) {
  return item.toUpperCase();
}).toList();
list.map((item) => item.toUpperCase()).toList();

Iterator<int> asynchronousTo(int n) async* {
  int k = 0;
  while(k < n) yield k++;
}
```

- Record

```dart
var record = (42, name: 'dart', 'dart');
record.$1
record.name
record.$2

({String name, int age}) userInfo(Map<String, dynamic> json) {
  return (name: json['name'] as String, age: json['age'] as int);
}

// 结构多个返回值 或 类的实例
final (:name, :age) = userInfo(json);
```

- Collections
  - List: `[]`
  - Map: `Map<String, dynamic>`
  - Set: `<String>{}`
- Generics: 泛型
- Typedefs：类型别名
- Enum, Future, Stream, Iterator, dynamic, void

## 模式匹配

```dart
// https://dart.dev/language/pattern-types
switch(s) {
  // 守卫
  case var s when s > 90:
    print(s);
    continue newCase;
  // List
  case [int a, int b]:
    print(s);
  // relation
  case >= 1 && <= 10:
    print('in range');
  // https://dart.cn/language/pattern-types
  newCase:
  default:
}

switch(s) {
  >10 => print('>10'), 
  10 => print('== 10'),
  _ => print('< 10')
}

// 解构
// 通配符 _ 不会绑定或者赋值任何变量
var list = [1, 2, 3];
var [_, two, _] = list;
```

## 控制流

- if-case

```dart
// if-case
if (p case [int x, int y]) return Point(x, y);
```

## 类

```dart
class Person {
  static const mark = 'person'

  final String name;
  final Gender sex;

  // 常量 构造函数
  // const Person()

  // factory 构造函数
  // 

  Person(this.name, this.sex);
  // :(初始化列表) 表示 初始化成员变量，并且在构造函数体执行之前被调用
  // 构造函数带有 命名构造函数，初始化列表通常用于初始化类中的字段
  // 命名构造函数：实现多构造函数
  Person.unNamed(): name = '';
  // 重定向构造函数
  Person.man(String name): this(name, Gender.M);

  // Getter 和 Setter
  String get name => name;
  set name(String n) => name = n;

  String say(String s) => '$s';

  @override
  void noSuchMethod(Invocation invocation) {
    print('You tried to use a non-existent member: ${invocation.memberName}');
  }
}

class Vector {
  final int x, y;

  Vector(this.x, this.y);

  // 自定义操作符 https://dart.cn/language/methods/#operators
  Vector operator +(Vector v) => Vector(x + v.x, y + v.y)
}

// mixin
mixin Material {}

class Chair extend Vector with Material {}

// 既可以是类也可以是 mixin
mixin class Cookie {}
```

- 扩展方法

`extension <extension name>? on <type> {}`

```dart
extension on String {
  bool get isBlank => trim().isEmpty();
}
```

- 扩展类型 `extension type`

```dart

```

- 可调用的对象， 实现 `call()` 方法

## 类型修饰符

- abstract: 整个接口的具体实现
- base: 强制继承 或 mixin
- final: 修饰类时无法被继承和实现(Dart 3.x 引入 sealed, 或者通过抽象类限制)
- interface: 定义接口
- sealed: 隐式抽象(限制更细力度的继承)
- mixin

## 并发(Concurrency)

- Feature: 表示异步操作的结果，该操作最终将以值或错误完成

```dart
const oneSeconds = Duration(seconds: 1);
Feature<void> deleteFiles() async {
  await Future.delayed(oneSeconds);
}

Feature<void> copyFiles() async => {}

await Feature.wait([
  deleteFiles(),
  copyFiles()
]);
```

- async-await 和 yield
- Streams

```dart
Future<int> sumStream(Stream<int> stream) async {
  var sum = 0;
  await for (final value in stream) {
    sum += value;
  }
  return sum;
}

Stream<int> countStream(int to) async* {
  for (int i = 1; i <= to; i++) {
    yield i;
  }
}

void main() async {
  var stream = countStream(10);
  var sum = await sumStream(stream);
  print(sum); // 55
}
```

- Isolates: `Isolate.run()` `Isolate.spawn()` 去实现
