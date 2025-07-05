# web security

[web_security](https://infosec.mozilla.org/guidelines/web_security)

## CSP

生产环境需要服务器配置

配置示例：

unsafe-inline 重启内联 JavaScript, 默认是禁止的

```shell
Content-Security-Policy:
  default-src 'self';
  script-src 'self' 'unsafe-inline';
  style-src 'self' 'unsafe-inline';
  img-src 'self' data:;
  connect-src 'self';
  font-src 'self';
  frame-src 'none';
  object-src 'none';
  base-uri 'self';
  form-action 'self';
```
