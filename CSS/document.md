# CSS

[can i use](https://caniuse.com/webgl)

## 重构或重绘

[触发条件](https://csstriggers.com/)

## 库

[贝塞尔动画曲线生成器](https://cubic-bezier.com/)

## 自定义属性

```css
:root {
  /**
  * 自定义属性 --*
  */
  --primary-color: #409eff;
}

.primary {
  color: var(--primary-color);
}

/**
  * 支持嵌套
  */
.parent {
  &:hover {
  }
  .child {
  }
}
```

## 简写属性

```css
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

## 属性

- accent-color: 设置 input 元素 type 为 checkbox, radio, range 和 progress 元素背景色
- aspect-ratio
- backdrop-filter: 对元素后面的区域应用图形效果
- backface-visibility
- background
  - clip: text 背景绘制在前景文本内, border/padding/content-box
- caret-color: 插入符号颜色
- clip
  - path: 创建剪辑区域
  - rule
- color-scheme: 颜色方案(normal,dark,light)
- grid
  - auto-flow: 控制自动放置算法
  - auto-columns: min-content / minmax(20px, auto) / auto / 1fr 超出手动定义的行列后浏览器自动添加的
  - template-areas/rows/columns 手动定义的行列
- inset: top, bottom, right 和 left 属性值的简写
  - block-start/end
  - inline-start/end
- justify-items/self: 仅对 grid 布局有效
- mask: 通过在特定点遮罩或剪切图像来隐藏元素
  - image: 用作元素的遮罩层
- object:
  - fit
  - position
- offset: 沿定义路径对元素进行动画
  - anchor
  - distance
  - path
  - position
  - rotate
- outline
- place: 允许你同时沿块和行内方向对齐内容, align/justify-\* 的简写
  - self
  - content
  - items
- scroll
  - behavior: smooth/auto
  - snap-type
- scrollbar
- shape: 元素外添加一个形状
  - outside
  - margin
  - image-threshold
- text
  - decoration: 简写
  - emphasis: 简写 \*-style/color
  - indent: 设置块中文本行前的空格的长度
  - overflow: 溢出内容样式
  - shadow
  - transform: uppercase/lowercase/capitalize
  - wrap: 控制元素内的文本如何换行
- transition: 过度动画
  - timing-function: ease-in-out, linear, steps(integer, step-position) 位置默认 end
- user-select: 控制用户是否可以选择文本
