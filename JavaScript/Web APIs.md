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

- closest(selectors)
- computedStyleMap()
- getAnimations()
- getAttribute(attributeName)
- getBoundingClientRect() 返回元素大小及其相对于视口的位置
- requestFullscreen()
- scrollIntoView(Optional)

### event

- mouseout 移动光标使其不再包含在元素或其子元素之一时
- mouseover 光标移动到元素或其子元素之一时
- pointer* 鼠标/触摸板/手写笔都会被触发
- animation*
- transition*
- wheel

## AbortControl

在需要时中止一个或多个 Web 请求
