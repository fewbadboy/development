# Web APP

vue application for web

## Attention

File Encoding: UTF-8

## Initial

```shell
npm config set registry https://mirrors.cloud.tencent.com/npm/
npm config get registry
# 恢复到默认官方 registry https://registry.npmjs.org/
# npm config delete registry
mkdir web-vue && cd web-vue
git init

pnpm create vite . --template vue-ts

pnpm install
pnpm run dev
```

## Dev Dependencies

### husky

```shell
pnpm add --save-dev husky
# husky init
pnpm exec husky init
```

### eslint / lint-staged

```shell
pnpm create @eslint/config@latest

pnpm add -D lint-staged
pnpm husky add .husky/pre-commit "pnpm lint-staged"
```

### commit lint

```shell
pnpm add --save-dev @commitlint/config-conventional @commitlint/cli
# 注意文件编码 修改为 utf8
echo "export default { extends: ['@commitlint/config-conventional'] };" > commitlint.config.js
echo "npx --no -- commitlint --edit \$1" > .husky/commit-msg
```

### Mock

```shell
pnpm add @faker-js/faker --save-dev
```

## Dependencies

### Tailwind CSS

```shell
pnpm add -D tailwindcss postcss autoprefixer sass
npx tailwindcss init -ts
# auto sort Tailwind CSS class
pnpm add -D prettier prettier-plugin-tailwindcss
```

### Lightning CSS

极快的 CSS parser, transformer, bundler, and minifier

如果启用，CSS 文件将由 Lightning CSS 处理，而不是 PostCSS

这样就和 Tailwind CSS 的文件处理冲突，transformer 导致 Tailwind CSS 样式失效

不支持 CSS 预处理器

```shell
pnpm add -D lightningcss browserslist
```

### Ant Design Vue

```shell
pnpm add ant-design-vue @ant-design/icons-vue
pnpm add -D unplugin-vue-components

# International
pnpm add dayjs
```

### js-cookie

```shell
pnpm add js-cookie
pnpm add -D @types/js-cookie
```

### axios

```shell
pnpm add axios
```

### Vue Router

```shell
pnpm add vue-router
```

### Pinia

```shell
pnpm add pinia
```

### Lodash

```shell
pnpm add lodash
pnpm add -D @types/lodash
```

### ECharts

```shell
pnpm add echarts
```

### GSAP(GreenSock Animation Platform)

```shell
pnpm add gsap
```

### vis.js

[vis.js](https://visjs.org/)

### leaflet

```shell
pnpm add leaflet
```
