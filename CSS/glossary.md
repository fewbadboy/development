# Glossary

## normal flow

## formatting context

`<button>` 和 `<input>` 默认 `display: flow-root`

- block formatting context(BFC)

  相邻块边框的垂直 margin 是折叠在一起的(塌陷)
  内部包含浮动元素
  不包含外部浮动元素

  以下情况会创建一个 BFC

  1. html 元素
  2. float 不是 none 的元素
  3. position 为 absolute 或 fixed
  4. display 为 inline-block，table-option/cell，flow-root，flex，grid
  5. contain 为 layout
  6. column-count 和 column-width 不为 auto
  7. column-span 为 all

- inline formatting context
