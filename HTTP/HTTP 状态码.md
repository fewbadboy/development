# HTTP 状态码

## 1xx

## 2xx

- 200 ok 请求已经成功。默认情况下状态码为 200 的响应可以被缓存。PUT/DELETE 请求成功通常返回状态码 201/204。
- 201 Created 表示请求已经被成功处理，并且创建了新的资源。
- 202 Accepted 表示服务器端已经收到请求消息，但是尚未进行处理。
- 203 Non-Authoritative Information 表示请求已经成功被响应，获得的负载与源头服务器的状态码为 200 (OK) 的响应相比，经过了拥有转换功能的 proxy（代理服务器）的修改。
- 204 No Content 使用惯例是，在 PUT 请求中进行资源更新，但是不需要改变当前展示给用户的页面。
- 205 Reset Content 通知客户端重置文档视图，比如清空表单内容、重置 canvas 状态或者刷新用户界面。
- 206 Partial Content 表示请求已成功，并且主体包含所请求的数据区间，该数据区间是在请求的 Range 首部指定的。

## 3xx

- 304 Not Modified 说明无需再次传输请求的内容，也就是说可以使用缓存的内容。
- 308 Permanent Redirect 说明请求的资源已经被永久的移动到了由 Location 首部指定的 URL 上。

## 4xx

- 400 Bad Request 服务器因某些被认为是客户端错误的原因，而无法或不会处理该请求。
- 401 Unauthorized 客户端错误，指的是由于缺乏目标资源要求的身份验证凭证，发送的请求未得到满足。
- 403 Forbidden 客户端错误，指的是服务器端有能力处理该请求，但是拒绝授权访问。
- 404 Not Found 服务器无法找到所请求的资源。
- 405 Method Not Allowed
- 406 Not Acceptable 服务器端无法提供与 Accept-Charset 以及 Accept-Language 消息头指定的值相匹配的响应。
- 407 Proxy Authentication Required 缺乏位于浏览器与可以访问所请求资源的服务器之间的代理服务器（proxy server ）要求的身份验证凭证。
- 408 Request Timeout 服务器想要将没有在使用的连接关闭。
- 410 Gone 请求的目标资源在原服务器上不存在了，并且是永久性的丢失。
- 411 Length Required 于缺少确定的Content-Length 首部字段，服务器拒绝客户端的请求。
- 413 Content Too Large

## 5xx

- 500 Internal Server Error
- 502 Bad Gateway
