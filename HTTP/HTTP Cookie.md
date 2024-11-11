# HTTP Cookie

## 创建 Cookie

服务器收到 HTTP 请求后，服务器可以在响应标头里面添加一个或多个 `Set-Cookie` 选项。
浏览器收到响应后通常会保存下 Cookie，并将其放在 HTTP Cookie 标头内，向同一服务器发出请求时一起发送。

会话期 Cookie 会在当前的会话结束之后删除。
持久性 Cookie 在过期时间（`Expires`，只与客户端有关）指定的日期或有效期（`Max-Age`）指定的一段时间后被删除。

有两种方法可以确保 Cookie 被安全发送，并且不会被意外的参与者或脚本访问：`Secure` 属性和 `HttpOnly` 属性。

`Domain` 和 `Path` 标识定义了 Cookie 的作用域：即允许 Cookie 应该发送给哪些 URL。

`SameSite` 属性允许服务器指定是否/何时通过跨站点请求发送。

- Strict：仅对同一站点的请求发送 cookie
- Lax：cookie 不会在跨站请求中被发送，但 cookie 在用户从外部站点导航到源站时，cookie 也将被发送（如跟随一个链接）。Chrome 默认值。
- None：同站请求和跨站请求下继续发送 cookie，但仅在安全的上下文中（还必须设置 Secure 属性）

## 第三方网站请求时携带 Cookie

向第三方网站请求的时候携带 Cookie 呢？需要满足如下条件：

- 网站开启 https 并将 Cookie 的 Secure 属性设置为 true
- Access-Control-Allow-Origin 设置为具体的 origin，而不是 *
- Access-Control-Allow-Credentials 设置为 true
- SameSite 属性设置为 None
