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

## backdrop-filter

## caret

- caret-color

## clip

- clip-path: inset/circle/ellipse/polygon/path/rect

## grid

```css
.wrapper {
  display: grid;
  /* 显示网格轨道 */
  grid-template-areas: 
    "a a b"
    "a a b";
  /* 显示网格轨道的大小 */
  grid-template-columns: repeat(3, 1fr); /* fraction */
  grid-template-rows: 100px;
  /* 内容超出显示定义的网格轨道，自动创建的轨道的行为 */
  grid-auto-columns: 100px;
  grid-auto-rows: 100px;
  grid-auto-flow: row;
  /* 网格间距 */
  grid-column-gap: 16px;
  grid-row-gap: 16px;
  /* 网格项的行和列 */
  grid-row: 1 / 3;
  grid-column: 1 / span 2;
}
```

## offset

- offset-anchor
- offset-distance
- offset-path
- offset-position
- offset-rotate
