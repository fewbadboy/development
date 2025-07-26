# document

## NODE_ENV

`development` `testing` `staging` `production`

```shell
# 分析代码
NODE_ENV=development node --prof app.js
# 处理成可理解的文件
node --prof-process isolate-0xnnnnnnnnnnnn-v8.log > processed.txt
```

## WebAssembly

[WasmTime](https://docs.wasmtime.dev/introduction.html)

## Environment Variable(实验性)

设置环境变量
多个环境变量文件时，后续的会重写之前存在的变量

```js
node --env-file=.env --env-file=.development.env test.js
```

## 事件

`libuv` 库实现
