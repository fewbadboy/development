# Tools

## express

```shell
npm install express
```

[middleware](http://expressjs.com/en/resources/middleware)

## [chalk](https://github.com/chalk/chalk)

Terminal string styling done right

## [serve-static](https://github.com/expressjs/serve-static)

Create a new middleware function to serve files from within a given root directory

```js
var express = require('express')
var serveStatic = require('serve-static')

var app = express()

// 预览已经 build 完的包
app.use(serveStatic('./dist', { index: ['index.html', 'index.htm'] }))
app.listen(3000)
```
