# Web APIs

## Node

## NodeList

`Node.childNodes` 和 `document.querySelectorAll()` 返回 NodeList

## EventTarget

### methods

- addEventListener(type, listener, options)
- removeEventListener(type, listener, options)
- dispatchEvent(event)

## Element

### property

- classList
- className
- clientHeight: 包括 padding 但是不包括 borders, margins, 水平滚动条 (如存在减去水平滚动条高度)
- firstElementChild / lastElementChild
- previousElementSibling / nextElementSibling
- outerHTML 用给定字符串解析的节点去替换该元素
- scrollTop / scrollHeight (元素内容的高度，包括由于溢出而在屏幕上不可见的内容)
- slot
- tagName

### methods

- animate(keyframes, options)
  - options: 部分参数
    - delay
    - direction
    - duration
    - easing
    - iterations
- checkVisibility()
- closest(selectors): 遍历元素及其父元素
- getAnimations()
- getAttribute(attributeName)
- getBoundingClientRect(): 返回元素大小及其相对于视口的位置
- matches(selectors): 测试元素是否会被指定的 CSS 选择器选中
- querySelector(selectors)
- requestFullscreen()
- scrollTo/By(): 前者绝对位置，后者基于当前位置相对滚动
- scrollIntoView(Optional)

### event

- auxclick: 除了鼠标左键外的其他按钮
- input: input,select,textarea 元素用户操作而直接发生改变时触发
- mouseleave: 不冒泡，从元素本身离开时触发
- mouseout: 冒泡，从元素或子元素离开时触犯
- mouseover: 光标移动到元素或其子元素之一的上面时
- pointer*: 鼠标/触摸板/手写笔都会被触发
- animation*:
- transition*:
- wheel: Safari 不支持
- touch*: Safari 不支持
- transition*: 过度动画

## AbortControl

在需要时中止一个或多个 Web 请求
