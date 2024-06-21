# HTTP 标头

- `Authorization`:
  - Basic username:password 转 base64
  - Bearer OAuth 2.0
- `Accept-Ranges`: 浏览器发现改标头时，可以尝试继续中断了的下载，而不是重新开始
- `Access-Control-Allow-Origin`：值是明确来源时，头部还得附加 `Vary: Origin`
- `Access-Control-Expose-Headers`：指示那些响应标头可以暴露给浏览器中运行的脚本。
- `Authorization`: 401 状态码时，至少包含 `WWW-Authenticate` 响应头
- `Clear-Site-Data`：only HTTPS, 清除当前请求网站有关的浏览器数据（cookie，存储，缓存）。
- `Content-Disposition`：指示回复的内容该以何种形式展示，是以 `inline` 的形式，还是以 `attachment` 附件的形式下载并保存到本地。
- `Content-Range`： unit range-start-range-end/size 一个数据片段在整个文件中的位置。
- `Content-Security-Policy`
- `If-Match`：在请求方法为 GET 和 HEAD 的情况下，服务器仅在请求的资源满足此首部列出的 ETag值时才会返回资源。而对于 PUT 或其他非安全方法来说，只有在满足条件的情况下才可以将资源上传。
- `Location`：需要将页面重新定向至的地址。
- `Range`：请求首部，告知服务器返回文件的哪一部分。如果服务器返回的是范围响应，需要使用 206 Partial Content 状态码。
- `Set-Cookie`: SameSite=Lax; Secure; cookie 不会在请求跨域图片、iframe、POST 表单 action 情况下不发送
- `Upgrade` only HTTP 1.1, 升级已经建立的连接为不同的协议(HTTP1.1 to 2.0, HTTP or HTTPS to WebSocket)
- `Last-Modified`
- `ETag`
