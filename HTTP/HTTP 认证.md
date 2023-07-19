# HTTP 认证

## WWW-Authenticate 与 Proxy-Authenticate 标头

响应标头指定了为获取资源访问权限而进行身份验证的方法。

```code
WWW-Authenticate: <type> realm=<realm>
Proxy-Authenticate: <type> realm=<realm>
```

`<type>` 指的是验证的方案（“Basic”是最常见的验证方案，会在下面进行介绍）。realm 用来描述进行保护的区域，或者指代保护的范围。

常见的验证方案包括：

- Basic base64 编码凭据
- Bearer bearer 令牌通过 OAuth 2.0 保护资源。
- HOBA 阶段三，HTTP Origin-Bound 认证，基于数字签名。

## Authorization 与 Proxy-Authorization 标头

用来向（代理）服务器证明用户代理身份的凭据。

```code
Authorization: <type> <credentials>
Proxy-Authorization: <type> <credentials>
```
