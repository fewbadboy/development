# HTTP 范围请求

响应中存在 `Accept-Ranges` 首部（并且它的值不为“none”），那么表示该服务器支持范围请求。 `Content-Length` 响应首部则表示这一部分内容在整个资源中所处的位置。服务器端会返回状态码为 206 Partial Content。
