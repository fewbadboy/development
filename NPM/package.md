# package.json

## 属性

```json
{
  "sideEffects": "",
  "exports": {
    ".": "精细控制哪些模块/路径可以导出，支持子路径"
  },
  "main": "CommonJS 程序的主要入口点",
  "module": "ES Module 主入口",
  "types": "TypeScript 类型定义文件入口",
  "bin": "npm 安装后可在命令行执行的命令及文件",
  "files": [
    "发布到 npm 时包含的文件（白名单机制）"
    "dist"
  ],
  "browser": "浏览器环境下的替代入口文件",
  "engines": {
    "node": ">=22.11.0"
  },
  "browserslist": "浏览器兼容配置",
  "workspaces": "查找的本地文件系统中的位置",
  "packageManager": "pnpm@9.12.0"
}
```
