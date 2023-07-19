# CSP 指令

Content-Security-Policy 内容安全策略

服务器返回 `Content-Security-Policy` HTTP 标头 或 `<meta>`元素来配置

```html
<meta
  http-equiv="Content-Security-Policy"
  content="default-src 'self'; img-src https://*; child-src 'none';"
/>
```

## 缓解跨站脚本攻击

CSP 通过指定有效域——即浏览器认可的可执行脚本的有效来源——使服务器管理者有能力减少或消除 XSS 攻击所依赖的载体。

## 缓解数据包嗅探攻击

服务器还可指明哪种协议允许使用

## 编写策略

[default-src](https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Headers/Content-Security-Policy/default-src)

## HSTS

`Strict-Transport-Security`（通常简称为 HSTS）响应标头用来通知浏览器应该只通过 HTTPS 访问该站点，并且以后使用 HTTP 访问该站点的所有尝试都应自动重定向到 HTTPS

[https://hstspreload.org](https://hstspreload.org) 推荐将其设置为 2 年

`Strict-Transport-Security: max-age=63072000; includeSubDomains; preload`

## X-Content-Type-Options

服务器用来提示客户端一定要遵循在 Content-Type 首部中对 MIME 类型 的设定，而不能对其进行修改。

## X-Frame-Options

用来给浏览器指示允许一个页面可否在 `<frame>`、`<iframe>`、`<embed>` 或者 `<object>` 中展现。
