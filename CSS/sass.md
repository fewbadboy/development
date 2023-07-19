# Sass

## Syntax

Statements(语句) are separated by semicolons(;)

### Top-Level Statements

Module loads, using `@use`
Imports, using `@import`
Mixin definitions using `@mixin`
Function definitions using `@function`

### Special Functions

url()
element()

### Other Expressions

Variables, begins with `$`
Function calls, like `nth($list, 1)` or `var(--main-bg-color)`, which may call Sass core library functions or user-defined functions
Special functions, like `calc(1px + 100%)` or `url(http://myapp.com/assets/logo.png)`, that have their own unique parsing rules
The parent selector, `&`(it’s only allowed at the beginning of compound selectors)
The value `!important`, which is parsed as an unquoted string

## Style Rules

```scss
@use "sass:meta";
$primary: #81899b;
$font-family-sans-serif: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto;

:root {
  --primary: #{$primary};
  --font-family-sans-serif: #{meta.inspect($font-family-sans-serif)}; // 插值会移除引号，meta.inspect保留引号
}
```

### Placeholder Selectors

starts with a `%` and it's not included in the CSS output

```scss
%tool {
  width: 100%
}

.action-button {
  @extend %tool;
  color: #cddc39;
}
```

## Variables

treat hyphens and underscores as identical(相同)
Default Values, provides the `!default` flag

```scss
// _library.scss
$black: #000 !default;
code {
  color: $black;
}

// styles.scss
@use 'library' with(
  $black: #111
);

// Advanced Variable Functions
@use "sass:map";

// With Map
$theme-colors: (
  "success": #28a745,
  "info": #17a2b8,
  "warning": #ffc107,
);

.alert {
  // Instead of $theme-color-#{warning}
  background-color: map.get($theme-colors, "warning");
```

## Interpolation

`#{}` in any of the following places:
Selectors in style rules
Property names in declarations
Custom property values
CSS at-rules
@extends
Plain CSS @imports
Quoted or unquoted strings
Special functions
Plain CSS function names
Loud comments

Interpolation returns unquoted strings that can’t be used for any further math, instead of writing `#{$width}px`, write `$width * 1px`—or better yet
the quotation marks around quoted strings are removed(字符串上的引号被移除)。convert quoted strings to unquoted strings to use the `string.unquote()` function

## At-Rules

1. `@use` loads mixins, functions, and variables from other Sass stylesheets, loaded by @use are called "modules"
@use "variables" will automatically load variables.scss, variables.sass, or variables.css
Modules will always be loaded relative to the current file first(总是相对当前文件加载而不需要设置 ./)
以 _ 开头的Sass 文件仅用于作为模块加载
2. `@forward`
3. `@import` combining multiple stylesheets' CSS together
4. `@mixin` and `@include`
5. `@function`
6. `@extend` one selector should inherit the styles of another
7. `@error`
8. `@debug`
9. `@at-root` `@at-root (with: <rules...>)` `@at-root (without: <rules...>)`
10. `@if` and `@else`
11. `@each <variable> in <expression>`
12. `@for <variable> from <expression> through <expression>` `@for <variable> from <expression> to <expression>`(the final number is excluded)
13. `@while`
14. `@media` `@support` `@keyframes`
15. `@<name> <value> { ... }` font-face, counter-style

## Values

1. Numbers
2. Strings
3. Colors
4. Lists `$sizes: 40px, 50px, 80px;list.nth(10px 12px 16px, 2)` 12px
5. Maps `$font-weights: ("regular": 400, "medium": 500, "bold": 700);`
6. Boolean
7. null
8. calc()/clamp()/min()/max()

## Operators

```scss
@debug 1px == 1px; // true
@debug 1px != 1em; // true
@debug 96px >= 1in; // true
@debug 1000ms <= 1s; // true
@debug 10s + 15s; // 25s
@debug 1in - 10px; // 0.8958333333in
@debug 5px * 3px; // 15px*px
@debug 1in % 9px; // 0.0625in
@mixin btn(decimal, number) {
  width: $decimal * 100%;
  border-width: $number * 1px;
}
@debug not true; // false
```

## Build-In Modules

1. sass:color
2. sass:list
3. sass:map
4. sass:math
5. sass:meta
6. sass:selector
7. sass:string

## Breaking Changes

## Command Line

## JavaScript API
