# At-rules

## @layer

声明一个级联层，也可用于定义多个级联层时的优先级顺序

```css
/* 声明在后面的优先级高，class="alert" 内部颜色为 blue */
@layer module, state;

@layer state {
  .alert {
    color: blue;
  }
}

@layer module {
  .alert {
    color: red;
  }
}
```

## @supports
