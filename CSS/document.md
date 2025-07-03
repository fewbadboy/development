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

/**
  * 支持嵌套
  * .parent .child {}
*/
.parent {
  & ~ .child {
  }
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

/**
 * flex 属性值说明
 * 1. 单值语法
 *  flex: <flex-grow> 1 0%
 *  flex: 1 1 <flex-basis>
 * 2. 双值语法
 *  flex: <flex-grow> <flex-shrink> 0%
 *  flex: <flex-grow> 1 <flex-basis>
 * 3. 三值语法
 *  flex: <flex-grow> <flex-shrink> <flex-basis>
*/
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

## 属性

- accent-color: 设置 input 元素 type 为 checkbox, radio, range 和 progress 元素背景色
- all：所有属性的简写
- backdrop-filter: 将过滤器效果应用于元素后面的区域
- backface-visibility: 设置元素的背面朝向用户时是否可见
- background
  - attachment: fixed 相对 viewport, locale, scroll(不随内容滚动) 相对元素
  - clip: text 背景绘制在前景文本内, border/padding/content-box
  - origin: border/padding/content-box
  - repeat: space(尽可能重复，不进行裁剪) round(拉伸)
- block-size: 依据 writing-mode 设置宽高
- inline-size: 依据 writing-mode 设置宽高
- border
  - collapse: table 元素内部单元格是否共享或单独的边框
  - image-slice: 将指定的图像分成多个区域
  - image-outset: 设置元素的边框图像与其边框之间的距离
  - block: 依据 writing-mode, direction, 和 text-orientation 简写
  - inline: 同上
- box
  - shadow: inset offset-x offset-y blur-radius spread-radius color
  - sizing: 设置如何计算元素的总宽度和高度
  - decoration-break: clone/slice 元素内容跨行渲染方式
- caret-color: 插入符号颜色
- clip-path: 创建剪辑区域
- color-scheme: 颜色方案(normal,dark,light)
- column
  - count: 列数目
  - rule: rule-width, rule-style, rule-color 的简写
- float: 从页面的正常流程中移除，但仍保留为流程的一部分(文字环绕)
- grid
  - auto-flow: 控制自动放置算法
  - auto-columns: min-content / minmax(20px, auto) / auto / 1fr 超出手动定义的行列后浏览器自动添加的
  - grid-template-areas/rows/columns 手动定义的行列
- gap: row/column-gap
- inset: top, bottom, right 和 left 属性值的简写
  - block-start/end
  - inline-start/end
- justify-content: space-evenly 着容器的主轴，使得每个项目周围都有相等的空间
- justify-items/self: 仅对 grid 布局有效
- mask: 通过在特定点遮罩或剪切图像来隐藏元素
  - image: 用作元素的遮罩层
  - repeat
- object
  - fit:调整元素(img,video)大小适应容器
  - position
- offset: 沿定义路径对元素进行动画
  - anchor
  - distance
  - path
  - position
  - rotate
- outline
  - offset: 轮廓偏移
- place: 允许你同时沿块和行内方向对齐内容, align/justify-\* 的简写
  - self
  - content
  - items
- resize: boh/horizontal/vertical 元素可调整大小
- scroll
  - behavior: smooth/auto
- scrollbar-\*
- shape: 元素外添加一个形状
  - outside
  - margin
  - image-threshold
- stop:
  - color
  - opacity
- text
  - decoration: 简写
  - emphasis: 简写 \*-style/color
  - indent: 设置块中文本行前的空格的长度
  - overflow: 溢出内容样式
  - shadow
  - transform: uppercase/lowercase/capitalize
  - wrap: 控制元素内的文本如何换行
- transform
  - origin: 默认 center
- transition: 过度动画
  - timing-function: ease-in-out, linear, steps(integer, step-position) 位置默认 end
- user-select: 控制用户是否可以选择文本
- visibility: visible/hidden
- white-space: 空白处理
- word
  - break
  - spacing: 单词之间、标签之间的空间长度

## 选择器

- &: 嵌套选择器

## 组合器

- +: 有相同的父元素且匹配元素紧跟着第一个元素之后
- ~: 有相同的父元素且匹配第一个元素之和的兄弟元素

## 函数

- drop-shadow
- fit-content
- inset
- minmax
- path
- steps
