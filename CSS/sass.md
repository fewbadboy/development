# Sass

SCSS 语法(`.scss`)

## 语法

## 样式规则

```scss
$primary: #81899b;
:root {
--primary: #{$primary};
}

.btn {
  padding: 8px 10px;

  &[type=primary] {
    background-color: #{$primary};
  }
}
```

## 变量

## 插值

用 `${}` 包裹位于以下位置的任何表达式

- 属性名声明
- 自定义属性值
- CSS at-rules
- @extends
- @imports
- 字符串

## At-Rules

额外功能以新的 at-rules 的形式出现, 如 `@use`

控制流 `@if` `@else` `@each` `@for` `@while`

## values

Lists: 可以用逗号、空格或斜杠分隔
Maps: (key: expression, ...)

## 操作符

## 内建模块

- sass::color
- sass::list
- sass::map
- sass::math
- sass::string

## DEMO

```scss
// _base.scss
...

// Modules
@use 'base';
@use 'sass:math';

@mixin theme($bg: #eeeeee) {
  background-color: $bg;
}

.success {
  @include theme;
}

// Extend
%message-shared {
  width: math.div(200px, 400px) * 100%;
  border: 1px solid #ccc;
}

.message {
  @extend %message-shared;
}
```
