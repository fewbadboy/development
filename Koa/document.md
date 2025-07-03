# Koa

## 示例

```js
const Koa = require("koa");
const Router = require("koa-router");
const koaBody = require("koa-body");

const app = new Koa();
const router = new Router();

app.use(
  koaBody({
    multipart: true, // 支持 multipart/form-data
    urlencoded: true, // 支持 x-www-form-urlencoded
    json: true, // 支持 application/json
    formidable: {
      uploadDir: "./uploads", // 上传文件保存路径
      keepExtensions: true, // 保留扩展名
    },
  })
);

router.post("/json", async (ctx) => {
  // Content-Type: application/json
  ctx.body = {
    type: "json",
    data: ctx.request.body,
  };
});

router.post("/form", async (ctx) => {
  // Content-Type: application/x-www-form-urlencoded
  ctx.body = {
    type: "form",
    data: ctx.request.body,
  };
});

router.post("/upload", async (ctx) => {
  // Content-Type: multipart/form-data
  const files = ctx.request.files;
  const fields = ctx.request.body;
  ctx.body = {
    type: "form-data",
    fields,
    files,
  };
});

app.use(router.routes());
app.use(router.allowedMethods());

app.listen(3000, () => {
  console.log("Server running on http://localhost:3000");
});
```
