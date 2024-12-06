# CSS

[can i use](https://caniuse.com/webgl)

## 重构或重绘

[触发条件](https://csstriggers.com/)

## 库

[贝塞尔动画曲线生成器](https://cubic-bezier.com/)

[tailwindcss](https://tailwindcss.com/)

## 自定义属性

```css
:root {
  /**
   * 自定义属性
   * --*
  */

  --primary-color: #409eff;
}

.primary {
  color: var(--primary-color);
}

```

## 简写属性

```css
/**
 * 取决于元素的 writing mode、方向性和文本方向
 * *-block-*:
 * *-inline-*:
 *
 * place-content/items/self: align-content justify-content;
 */
.place {

  place-content: center;
}
```

## 过滤器(filter)

- blur: 高斯模糊，默认参数 0
- brightness: 亮度， 默认参数 1
- contrast: 对比，默认参数 1
- drop-shadow: 阴影，参数 box-shadow
- grayscale: 灰度，默认参数 0
- hue-rotate: 旋转色调，默认参数 1
- invert: 倒置，默认参数 0
- opacity: 透明度，默认参数 1
- saturate: 饱和度，默认参数 1
- sepia: 棕褐色，默认参数 0
