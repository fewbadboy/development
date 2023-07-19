# HTTP 标头

- `Accept-Ranges`: 浏览器发现改标头时，可以尝试继续中断了的下载，而不是重新开始
- `Access-Control-Allow-Credentials`：当请求的 credentials 模式（Request.credentials）为 include 时，浏览器仅在响应标头 Access-Control-Allow-Credentials 的值为 true 的情况下将响应暴露给前端的 JavaScript 代码。
- `Access-Control-Allow-Methods`：客户端所要访问的资源允许使用的方法或方法列表。
- `Access-Control-Expose-Headers`：指示那些响应标头可以暴露给浏览器中运行的脚本。
- `Cache-Control`: 通过指定指令来实现缓存机制。
- `Clear-Site-Data`：清除当前请求网站有关的浏览器数据（cookie，存储，缓存）。
- `Content-Disposition`：指示回复的内容该以何种形式展示，是以 `inline` 的形式（即网页或者页面的一部分），还是以 `attachment` 附件的形式下载并保存到本地。
- `Content-Range`：一个数据片段在整个文件中的位置。
- `Host`：指明了请求将要发送到的服务器主机名和端口号。
- `If-Match`：这是一个条件请求。在请求方法为 GET 和 HEAD 的情况下，服务器仅在请求的资源满足此首部列出的 ETag值时才会返回资源。而对于 PUT 或其他非安全方法来说，只有在满足条件的情况下才可以将资源上传。
- `Location`：需要将页面重新定向至的地址。
- `Origin`：请求的来源（协议、主机、端口）。
- `Range`：请求首部，告知服务器返回文件的哪一部分。如果服务器返回的是范围响应，需要使用 206 Partial Content 状态码。
