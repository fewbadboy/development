# Dart

## Variables

```dart
var name = 'dart'; // 赋初值时，类型就推断固定下来
int? lineCount; // 初始值为 null
int lineCount = 0; // 非 null 值必须初始化值
late String description; // 延迟初始化非空变量
final timeNow = DateTime.now(); // 变量只能赋值一次
const pi = 3.14159 ； // 编译时确定
// 通配符 _: 声明非绑定的局部变量或参数
```

## Operators

- 类型：as/is/is!

is 不确定类型前检查类型

- 级联: ../?..

```dart
var document.querySelector('#confirm')
  ?..classList.add('important')
  ..onClick.listen((e) => ..)
  ..scrollIntoView();
```

- 扩展: .../...?

## 类型

- String

```dart
// 插值组合，单个变量不用插值
'Hello, $name! You are ${year - birth} years old.'
```

- Record: 固定大小，异构(每个字段不同类型)和类型化的(编译器都是有明确类型)

```dart
({ a: String, b: int }) record; // 声明
record = (a: 'dart', b: 12); // 初始化
final (:name, :age) = (name: 'dart', age: 12); // 解构


var pair = (12，'dart', a: 2);
pair.$1 // 12
pair.a // 2
```

- Null

```dart
Person? person;

// person != null
if (person var case p?) {

} else {

}
```

- List: `isEmpty` `isNotEmpty`
- Set

```dart
<String>{'dart', 'flutter'}
```

- Map

```dart
final json = <String, dynamic>{ 'name': 'dart', 'age': 12 };
```

- Object: 除了 Null 外其他类的父类
- Enum
- Future 和 Stream
- Iterator
- dynamic
- void

## Typedefs

```dart
typedef IntList = List<int>;
```

## Patterns

```dart
(num, Object) record = (1, 's');
var (i as int, s as String) = record;

switch(shape) {
  case var s when s > 0:
    print('> 0')
}

var isPrimary = switch(color) {
  Color.red || Color.yellow => true,
  _ => false
};
```

## Function

```dart
external void someFunc(int i); // 声明外部函数

Iterable<int> naturalsTo(n) sync* {
  int k = 0;
  while (k < n) yield k++;
}
```

## 导入

```dart
// 当你需要进入包的 lib 目录时, 使用 package: import
import 'package:lib1.dart' show foo; // 导入 foo
import 'package:lib1.dart' hide foo; // 除了 foo 都导入

import 'package:lib1.dart' deferred as lib1; // 懒加载库
```

## Class

```dart
class Point {
  int x = 0;
  int y = 0;

  static const origin = (0, 0);
  static final Map<String, Logger> _cache = <String, Logger>{};

  Point(this.x, this.y);
  Point.origin(): x = 0, y = 0; // 命名构造函数
  Point.alongXAxis(double x): this(x, 0); // 重定向构造函数

  // 工厂 构造函数(不能 new， 可能不会返回新的对象)
  factory Point(String name) {
    return _catch.putIfAbsent(name, () => Logger._internal(name));
  }
}

class Vector2d {
  final double x;
  final double y;

  Vector2d(this.x, this,y);
}

class Vector3d extends Vector2d {
  final double z;

  Vector3d(super.x, super.y, this.z);
}
```

### Mixins

复用多类的层次

```dart
mixin A {}

class B with A {}
```

### Enum

```dart
enum Color { red, green, blue }
```

### Extension

```dart
// extension method
extension NumberParsing on String {
  int parseInt() {
    return int.parse(this);
  }
}
// extension type
extension type ...
```

### 类修饰符

- base 强制继承类或 mixin 的实现
- interface
- final 当前库中的代码可以继承这个类，其他库无法继承它
- sealed 要创建已知的、可枚举的子类型集

## 并发

### 异步编程

```dart
Future<String> _readFileAsync() async {
  final file = File(..);
  final contents = await file.readAsString();
  return contents.trim();
}

Stream<int> sumStream(Stream<int> stream) async* {
  var sum = 0;
  await for (final value in stream) {
    yield sum += value;
  }
}
```

### Isolate

多核 CPU，代码在隔离区(每个隔离有自己的内存和运行事件循环的单个线程)运行而不是线程中运行

```dart
Isolate.run();
Isolate.spawn();
```

## Core libraries

- jsonDecode/jsonEncode
- utf8.decode/encode
