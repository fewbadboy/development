# 高效指南

## 代码风格

- 在 lib， package， 文件夹及源文件中使用 `lowercase_with_underscores` 方式命名
- 类成员，变量，参数等 `lowerCamelCase` 风格命名
- 优先使用 _， __ 等来表示未使用的回调参数
- 顺序
  - dart:
  - package:
  - 项目相关导入语句
  - export

## 文档注释

- `///` 语法注释，配合 `dart doc` 解析生成为文档网页
- `[]` 文档注释中引用作用域内的标识符
- 反引号标注代码

## 用法示例

- 库
  - 不要导入 package 下 src 目录下的库(被认为是自己实现的私有库)
  - import 时优先使用相对目录
- Null
  - 不要明确声明初始化 null，而是类似`Item? item;`
  - bool 条件表达式遇到 null 时`nullableBool ?? false`或检查 `!= null`
  - 空类型检查 `if(this.response case var response?)` 或 将字段`this.response`分配给局部变量
- 字符串
  - 相邻字符串不需要 `+`，只需要挨着就行
  - 插值形式组合字符串
- 集合
  - 尽可能使用集合字面量 `<Point>[]` `<String, Address>{}` `<int>{}`
  - 判空使用 `.isEmpty` 或 `.isNotEmpty`
  - `whereType()` 按类型过滤，`obj.where((i) => i is int)`
- 函数
  - 不要使用 lambda 代替 tear-off(Dart 特性，函数的引用) `charCodes.forEach(print)`
- 变量
  - 大多数局部变量应该用 `var`(可重新分配) 或 `final`(不可重新分配) 声明
  - 避免保存可计算的结果(无需去构造函数中计算某些结果，而是用方法实现)
  - 推荐 `final` 关键字创建只读属性
  - 对简单成员使用 `=>`
  - 使用 `this`
    - 要访问的局部变量和成员变量命名一样的时候
    - 重定向到一个命名函数的时候
  - 尽可能初始化定义的变量
- 构造函数
  - 尽可能使用初始化参数形式 `Person(this.name)`

## API 设计

- 命名
  - 名词短语命名不是布尔类型的变量和属性
  - 非命令式动词短语命名布尔类型变量和属性
    - isEnabled, wasShown, willFire
    - hasElements, canClose, shouldConsume, mustSave
    - ignoresInput, wroteFile
  - 省略命名布尔参数的动词
  - 命令式动词短语命名带有副作用的函数或方法
  - to___() 命名把对象转换成一个新的对象函数
  - as___() 命名把对象转换成另一种表现形式的函数
  - 助记符约定

  ```dart
  // E 集合元素类型
  class List<E> {}
  // K V 关联结合的 key 和 value
  class Map<K, V> {}
  // R 返回值
  abstract class Expression<R> {
    R visitBinary(BinaryExpression node);
  }
  // 具有单个类型参数的泛型，使用 T S U
  ```

- 库
  - `_` 开头成员只能在库的内部被访问，是库的私有成员
- 类
  - 纯粹的 mixin 或 class 而不是 mixin class
  - 指定字段或顶级变量为 final
  - dynamic 替换推断失败的情况
  - function 类型注解的特征更明显 `bool isValid(bool Function(String) test) => ...`
  - 不要使用 `==` 和可空值比较

## Packages

- `dart pub add`  直接修改 pubspec 依赖
- `dart pub get` 手动修改 pubspec 文件后，安装依赖
