# package

## badge-maker

## cssnano

基于 postcss 快速压缩

## picocolors

终端字符串样式颜色设定

## chokidar

跨平台文件监听库

## date-fns

浏览器或者 Node.js 中操作 date

## dotenv

从 `.env` 文件加载环境变量到 `process.env`

## execa

过程命令执行

## js-cookie

轻量级 处理 cookies

## jsencrypt

## husky

## OXlint

高性能编译器

## release-it

自动执行版本控制和包发布相关任务

## @inquirer/prompts

创建交互式的 CLI prompts

## mitt

event emitter / pubsub

## mqtt

Node.JS 和 Browser 实现处理 MQTT 协议

## @faker-js/faker

创建 mock 数据

## shosho

快捷方式设置

## svgo

SVG Optimizer

## @tweenjs/tween.js

动画

## p-retry

反复执行 promise-returning 或 async 函数

## postcss

使用 JS 插件转换样式

## xstate

状态管理和编排

## axios/undici

axios 广泛应用于前端
undici 完全支持 HTTP/1.1 和 HTTP/2，专门为 Node.js 环境设计

## preview html

```js
const Koa = require("koa");
const serve = require("koa-static");
const path = require("path");
const app = new Koa();
app.use(
  serve(path.join(__dirname, "dist"), {
    index: "index.html",
  })
);

app.listen(3000, () => {
  console.log("Koa server is running at http://localhost:3000");
});
```
