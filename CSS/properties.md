# Properties

## 自定义 CSS 属性遍历

```css
--main-color: #409eff;
```

## accent-color

`<input type="checkbox">` `<input type="radio">` `<input type="range">` 及 `<progress>`

## animation

- animation-composition: replace/add/accumulate
- animation-direction: normal/reverse/alternate/alternate-reverse
- animation-fill-mode: none/forwards/backwards/both
- animation-play-state: paused/running
- animation-timing-function: ease-in-out/linear/steps/cubic-bezier

## aspect-ratio

## backdrop-filter

## background

- background-attachment: fixed/local/scroll
- background-clip: border-box/padding-box/content-box/text
- background-origin: border-box/padding-box/content-box

## block/inline-size

## caret

- caret-color

## clip

- clip-path: inset/circle/ellipse/polygon/path/rect

## column

- column-count
- column-rule
- column-width

## grid

```css
.wrapper {
  display: grid;
  grid-template-areas: 
    "a a b"
    "a a b";
  /* 显示网格轨道 */
  grid-template-columns: repeat(3, 1fr); /* fraction */
  grid-template-rows: 100px;
  /* 隐式网格(超出定义网格内容的项，自动创建轨道的行为) */
  grid-auto-columns: 100px;
  grid-auto-rows: 100px;
  /* 决定多余项被如何放置到隐式表格中 */
  grid-auto-flow: row;
  /* 网格间距 */
  grid-column-gap: 16px;
  grid-row-gap: 16px;
}
```

## inset

## object

- object-fit
- object-position

## offset

- offset-anchor
- offset-distance
- offset-path
- offset-position
- offset-rotate

## user-select
