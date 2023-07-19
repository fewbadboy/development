# Vue

## Vue Loader

是 webpack 的 loader,允许以单文件组件格式撰写 vue 组件

1. Scoped CSS
希望 `scoped` 样式中的选择器作用的更深，影响子组件使用操作符 `>>>`，但Sass 之类预处理器深度作用选择器无法解析上面的操作符，需要 `::v-deep` 或 `/deep/` 取代(都是操作符 >>> 的别名)
2. CSS Modules

向 css-loader 传入 modules: true 来开启

```js
// webpack.config.js
{
  module: {
    rules: [
      {
        test: /\.css$/,
        oneOf: [
          // 这里匹配 `<style module>`
          {
            resourceQuery: /module/,
            use: [
              'vue-style-loader',
              {
                loader: 'css-loader',
                options: {
                  // 开启 CSS Modules
                  modules: true,
                  // 自定义生成的类名
                  localIdentName: '[local]_[hash:base64:8]'
                }
              }
            ]
          },
          // 这里匹配普通的 `<style>` 或 `<style scoped>`
          {
            use: [
              'vue-style-loader',
              'css-loader'
            ]
          }
        ]
      },
      {
        test: /\.scss$/,
        use: [
          'vue-style-loader',
          {
            loader: 'css-loader',
            options: { modules: true }
          },
          'sass-loader'
        ]
      }
    ]
  }
}
```

```css
<style module>
.red {
  color: red;
}
</style>
```

module 特性指引 Vue Loader 作为名为 $style 的计算属性
3. 自定义块
4. 代码校验
