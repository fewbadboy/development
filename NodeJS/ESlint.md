# ESlint

## Quick start

```shell
#  install and configure ESLint
pnpm create @eslint/config@latest
```

```js
import globals from 'globals';
import js from "@eslint/js";
import markdown from "@eslint/markdown";
import jsdoc from 'eslint-plugin-jsdoc';
const { defineConfig，globalIgnores } = require("eslint/config");

module.exports = defineConfig([
  globalIgnores(['dist/**/*']),
  {
    // configuration object
    languageOptions: {
      globals: {
        ...globals.browser
      }
    }
    plugins: [js],
    extends: ["js/recommended"],
  },
]);
```

## Rules

- off or 0
- warn or 1
- error or 2

## Configuration Objects

```js
{
  name: '', // 配置对象名称
  files: [
    'src/**/*.js'
  ], // ESlint 验证文件
  ignores: [], // ESlint 跳过文件
  extends: [], // ESlint 附加配置
  languageOptions {
    ecmaVersion: 'latest',
    sourceType: 'module', // JavaScript 源码类型
    globals: { window: 'readonly' }, // 声明全局允许的变量
    parser: 'espree', // 解析器(转 AST)
    parserOptions: {} // 给 parser 提供附加选项
  },
  linterOptions:{
    noInlineConfig: false, // 允许写 ESlint 内联注释
    reportUnusedDisableDirectives: 'warn'
    reportUnusedInlineConfigs: 'off'
  },
  processor, // 代码预处理器
  plugins, // 第三方扩展(规则等)
  rules, // 启用或关闭规则
  settings // 插件提供共享配置
}
```

## Command Line Interface

```shell
# --fix-dry-run 不会修改文件
eslint --fix .
```
