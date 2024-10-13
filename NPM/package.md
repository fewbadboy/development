# package.json

## 属性

部分属性说明

### 安装本地路径下文件

```shell
# ../foo/bar
# ~/foo/bar
# ./foo/bar
# /foo/bar
npm install -S ./foo/bar
```

```json
{
  "name": "",
  "version": "",
  "description": "",
  "repository": "",
  "license": "",
  "sideEffects": "",
  "type": "module",
  "exports": {
    ".": {
      "types": "./dist/index.d.ts",
      "import": {
        "types": "./dist/index.d.mts",
        "default": "./dist/index.mjs"
      },
      "require": {
        "types": "./dist/index.d.cts",
        "default": "./dist/index.cjs"
      }
    }
  },
  "main": "./dist/index.cjs",
  "module": "./dist/index.mjs",
  "types": "./dist/index.d.cts",
  "files": [
    "dist"
  ],
  "packageManager": "pnpm@9.12.0"
}
```

```json
{
  "browser": "模块打算在客户端使用,应该用来代替 main 字段",
  "bin": "npm 安装后可在命令行执行的命令及文件",
  "scripts": {
    "prepare": "将在发布之前运行"
  },
  "os": [""],
  "cpu": [""]
}
```

### 生命周期 scripts

在特定情况下自动触发的脚本钩子
脚本钩子命名格式： `pre<event>`, `post<event>` `event`

- prepare
  - 在 `npm publish` 和 `npm pack` 命令之前执行
  - 在安装依赖之后触发(执行无任何参数的 `npm install`)
