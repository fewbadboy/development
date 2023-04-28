# Tips

## load gltf file

```shell
yarn add gltf-loader -D
```

vue.config.js

```js
module.exports = {
  ...
  chainWebpack(config) {
    config.module
      .rule('gltf')
        .test(/\.gltf$/)
        .use('gltf')
          .loader('gltf-loader')
  }
}
```

Or

```js
module.exports = {
  ...
  chainWebpack(config) {
    config.module
    .rule('url')
      .test(/\.gltf$/)
      .use('url')
        .loader('url-loader')
  }
}

```

## Shaders

```js
var shaderMaterial = new THREE.ShaderMaterial( {
  vertexShader: document.getElementById( 'vertexShader' ).textContent,
  fragmentShader: document.getElementById( 'fragmentShader' ).textContent
})


```
