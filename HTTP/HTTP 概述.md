# HTTP 概述

## Data URL 语法

`data:[<mediatype>][;base64],<data>`

mediatype 是一个 MIME(媒体类型) 类型，数据包含[RFC 3986中定义的保留字符](https://datatracker.ietf.org/doc/html/rfc3986#section-2.2)时必须进行 [百分号编码](https://developer.mozilla.org/zh-CN/docs/Glossary/Percent-encoding)

[Base 64编码](https://developer.mozilla.org/zh-CN/docs/Glossary/Base64)

浏览器可以通过请求头 Content-Type 来设置 X-Content-Type-Options 以阻止 MIME 嗅探。
[常见的 MIME 类型](https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types)
[media-types](https://www.iana.org/assignments/media-types/media-types.xhtml)
