# HTTP 重定向

## HTTP 协议重定向

重定向响应包含以 3 开头的状态码，以及 Location 标头
HTTP 协议的重定向机制永远最先触发。

## `<meta>` 元素

```html
<meta http-equiv="Refresh" content="0; URL=http://example.com/" />
```

content 属性的值开头是一个数字，指示浏览器在等待该数字表示的秒数之后再进行跳转。建议始终将其设置为 0 来获取更好的无障碍体验。

`如果可能，请采用 HTTP 协议的重定向机制，而不要使用 <meta> 元素重定向。开发人员修改了 HTTP 重定向，而忘记修改 HTML 页面的重定向，那么二者就会不一致。`

## DOM 的 JavaScript 重定向机制

```js
window.location = 'http://example.com/'
```
