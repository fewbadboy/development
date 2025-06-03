# Web APIs

## Crypto

- AES 对称加密
- RSA-OAEP 非对称加密

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
- previousElementSibling / nextElementSibling
- outerHTML 用给定字符串解析的节点去替换该元素
- scrollTop / scrollHeight (元素内容的高度，包括由于溢出而在屏幕上不可见的内容)
- slot
- shadowRoot 元素结构、样式及行为封装，防止外部样式污染
- tagName

### methods

- closest(selectors): 遍历元素及其父元素
- getBoundingClientRect(): 返回元素大小及其相对于视口的位置
- scrollTo/By(): 前者绝对位置，后者基于当前位置相对滚动
- scrollIntoView(Optional)

## AbortControl

在需要时中止一个或多个 Web 请求
