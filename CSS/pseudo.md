# Pseudo

## Element

- cue: WebVTT cues
- file-selector-button: 针对 type="file" 的 input 元素
- first-letter
- marker: 列表项的标记框
- placeholder

## Class

- active 鼠标按下状态
- any-link 匹配所有 :link 或 :visited 的元素
- autofill: 完成 autocomplete 后的样式
- checked
- default: 设定默认值元素的样式
- dir([ltr | rtl])
- disabled: 重置 disabled 样式
- empty: 空元素设置
- first/last-child: 兄弟元素中的中的第一个元素
- first/last-of-type: 兄弟元素中同类型的元素
- nth-child(-n+3): 根据元素在父元素的子列表中的索引来匹配。 even 偶数， odd 奇数
- nth-last-of-child(): 基于同类型的兄弟元素
- only-child: 等价于 :first-child:last-child
- only-of-type: 基于在兄弟元素中的相同类型(tag 名)
- focus: select 影响 option
- focus-visible: select 不影响 option
- focus-within: 元素或其任何后代被聚焦，则匹配该元素
- fullscreen: 元素在全屏下样式
- has()
- host: shadow DOM 的 shadow host
- hover
- in-range: input 元素范围值在 min 和 max 之间(date 同样可以设置)
- out-of-range:
- indeterminate: 半选状态(checkbox)
- invalid/valid: 表单验证样式
- is(): 选择器列表作为其参数，并选择该列表中的选择器之一
- lang(): 针对不同 lang 属性设置样式
- link: 有 href 属性的元素
- modal
- not():
- optional: 针对无 required 属性的 input select textarea 元素设置
- picture-in-picture:
- placeholder-shown: placeholder 样式
- popover-open
- read-only: 不可编辑的元素
- read-write:
- required:
- root: 档的树的根元素
- state(): 自定义元素的自定义状态
- target: URL fragment 匹配的 id 元素
- user-valid
- user-invalid
- where()
