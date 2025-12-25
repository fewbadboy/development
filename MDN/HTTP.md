# HTTP

## 缓存

```bash
# 私有缓存
Cache-Control: private
# 基于 age 的缓存(定义经过的时间)
Cache-Control: max-age=604800
# 指定有效期
Expires: Tue, 28 Feb 2025 22:22:22 GMT
# Vary 响应
# ETag
```

## 身份认证

```bash
Authorization: 'Bearer'
Proxy-Authorization: 'Bearer'
```

## 范围请求

`Accept-Range` `Content-Length`

- Strict
- Lax: 跨站 GET，顶级导航
- None: 必须配合 Secure

## 同源策略

服务器返回响应头 `Access-Control-Allow-*`

## 内容安全策略 (Content Security Policy)

防范跨站脚本 (Cross-site scripting，XSS) 攻击和数据注入等攻击

默认网页内容使用标准的同源策略。可以通过 HTTP 标头 `Content-Security-Policy` 及 `<meta>` 元素配置该策略

```text
Content-Security-Policy: default-src 'self'; report-uri http://reportcollector.example.com/collector.cgi
Set-Cookie: SameSite=Lax
```

或

```html
<meta
  http-equiv="Content-Security-Policy"
  content="default-src 'self'; img-src https://*; child-src 'none';"
/>
```

## Header

- `Content-Disposition`: 在 `multipart/form-data` 类型的响应中说明返回的内容的展示形式
