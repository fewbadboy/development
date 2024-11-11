# HTTP 缓存

## 私有和共享缓存

响应具有 Authorization 标头，则不能将其存储在私有缓存。

```code
Cache-Control: no-store, no-cache, max-age=0, must-revalidate, proxy-revalidate
```

## 基于 Age 的缓存策略

age 是自响应生成以来经过的时间。

```code
...
Cache-Control: max-age=604800
Age: 86400
```

收到该响应的客户端会发现响应在剩余的 518400 秒内有效

## Expires 或 max-age

```code
Expires: Tue, 28 Feb 2022 22:22:22 GMT
```

时间格式难以解析。

Expires 标头使用明确的时间而不是通过指定经过的时间来指定缓存的生命周期。
有可能通过故意偏移系统时钟来诱发问题；因此，在 HTTP/1.1 中，Cache-Control 采用了 max-age——用于指定经过的时间。

如果 Expires 和 Cache-Control: max-age 都可用，则将 max-age 定义为首选。

## Vary 响应

## 验证响应

HTTP 有一种机制，可以通过询问源服务器将陈旧的响应转换为新的响应。

### If-Modified-Since

```code
Cache-Control: max-age=3600
```

客户端通过使用包含 If-Modified-Since 请求标头的条件，以询问服务器自指定时间以来是否有任何的改变。
如果内容自指定时间以来没有更改，服务器将响应 304 Not Modified。收到该响应后，客户端将存储的陈旧响应恢复为新鲜的，并可以在剩余的 1 小时内重复使用它。

### ETag/If-None-Match

ETag 响应标头的值是服务器生成的任意值。

如果响应是陈旧的，则客户端获取缓存响应的 ETag 响应标头的值，并将其放入 If-None-Match 请求标头中，以询问服务器资源是否已被修改。
如果服务器为请求的资源确定的 ETag 标头的值与请求中的 If-None-Match 值相同，则服务器将返回 304 Not Modified
如果服务器确定请求的资源现在应该具有不同的 ETag 值，则服务器将其改为 200 OK 和资源的最新版本进行响应。

## 强制重新验证

如果你不希望重复使用响应，而是希望始终从服务器获取最新内容，则可以使用 no-cache 指令强制验证。

响应中添加 `Cache-Control: no-cache`  Last-Modified 和 ETag

## 不使用缓存

`no-cache` 指令不会阻止响应的存储，而是阻止在没有重新验证的情况下重用响应。

`Cache-Control: no-store` no-store 指令阻止存储响应，但不会删除相同 URL 的任何已存储响应。
但是，不建议随意授予 no-store，因为你失去了 HTTP 和浏览器所拥有的许多优势，包括浏览器的后退/前进缓存。

在这种情况下，no-store 并不总是最合适的指令：

- 不与其他用户共享： `Cache-Control: private`
- 每次都提供最新的内容：`Cache-Control: no-cache`
- 兼容过时的实现：`Cache-Control: no-store, no-cache, max-age=0, must-revalidate, proxy-revalidate`
