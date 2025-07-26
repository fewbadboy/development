# At-rules

## @layer

声明一个级联层，也可用于定义多个级联层时的优先级顺序

```css
/* 声明在后面的优先级高，class="alert" 内部颜色为 blue */
@layer module, state;

@layer module {
  .alert {
    color: red;
  }
}
@layer state {
  .alert {
    color: blue;
  }
}
```

## @import

位于任何其他 @ 规则（@charset 和 @layer 除外）和样式声明之前
