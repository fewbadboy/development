# 工具库

## 编译器

[Babel](https://babeljs.io/) 将 ECMAScript 2015+ 代码转换为当前和旧版浏览器向后兼容版本

## 构建

[rollup js](https://rollupjs.org/)

[esbuild](https://esbuild.github.io/)

[create-react-app](https://create-react-app.dev/)

## 图表

[font awesome](https://fontawesome.com/)

[echarts](https://echarts.apache.org/zh/index.html)

[D3](https://d3js.org/)

[arc gis](https://developers.arcgis.com/)

[vis.js](https://visjs.org/)

[AntV](https://antv.antgroup.com/)

[Cytoscape.js](https://js.cytoscape.org/)

[p5](https://p5js.org/)

## canvas

[Canvas Confetti](https://www.kirilv.com/canvas-confetti/) 五彩纸屑
[roughJS](https://roughjs.com/) 手绘风格
[fireworks](https://github.com/crashmax-dev/fireworks-js) 烟花

## 地图

[leaf let js](https://leafletjs.com/)
[open layers](https://openlayers.org/)
[cesium](https://cesium.com/platform/cesiumjs/)
[Geo JSON 地图数据](https://datav.aliyun.com/portal/school/atlas/area_selector)
[Geo JSON 数据可视化](https://geojson.io/)
地理坐标系统转换 [proj4js](https://trac.osgeo.org/proj4js/)

## 视频

flv.js
[video js](https://docs.videojs.com/)
[LivePlayer](https://www.liveqing.com/docs/manuals/LivePlayer.html)

## UI

[ionicons](https://ionic.io/ionicons) 图标， icon font

[Prime Vue](https://primevue.org/)

[ant design](https://ant.design/)

[Element Plus](https://element-plus.org/)

[pro components](https://procomponents.ant.design/)

[UIverse](https://uiverse.io/)

## 桌面

[electron js](https://www.electronjs.org/)
[cordova](https://cordova.apache.org/)

## 3D

[book shaders](https://thebookofshaders.com/)
[learn webGL](https://learnwebgl.brown37.net/)
[webGL fundamentals](https://webglfundamentals.org/webgl/lessons/zh_cn/)
[webGL2 fundamentals](https://webgl2fundamentals.org/)

[three js](https://threejs.org/)

[HDR](https://polyhaven.com/hdris/skies)

## vue

[awesome-vue](https://github.com/vuejs/awesome-vue)
[vue use](https://vueuse.org/)
[vue-i18n](https://vue-i18n.intlify.dev/)

vue-puzzle-vcode

[nuxt](https://nuxt.com/)

[eslint-vue](https://eslint.vuejs.org/)

## react

[next.js](https://nextjs.org/)

## 框架

[solid js](https://www.solidjs.com/docs/latest)

[qwik](https://qwik.dev/)

[deno](https://deno.com/)

[nest js](https://nestjs.com/)

## 前端开发解决方案

[Open Tiny](https://opentiny.design/)

## 文档

[vue press](https://vuepress.vuejs.org/)
[documentation](https://docus.dev/)

## 规范

[jsdoc](https://www.jsdoc.com.cn/)
[Git commit rules](https://www.conventionalcommits.org)

## HTTP 请求

[axios](https://axios-http.com/)

```js
service.interceptors.response.use(
  response => {
    /**
     * 下载文件的信息
     * 进行跨域请求时，后端需要明确暴露该头部
     * Access-Control-Allow-Headers: Content-Disposition
     * Access-Control-Expose-Headers: Content-Disposition
     * 
     * headers: {
     *  'content-disposition': 'attachment;filename=xxx.xlsx'
     * }
     */
    const {
      status,
      config: { responseType },
      data: { size, arrayBuffer },
      data
    } = response
    if (/(blob|arrayBuffer)/.test(responseType)) {
      if (status === 200 && responseType === 'blob' ? size > 0 : arrayBuffer > 0)
        return Promise.resolve({ data: blobData, disposition: decodeURIComponent(response.headers['content-disposition'])})
      return Promise.reject(blobData)
    }
  }
)

export function fetchPostFile(data) {
  return request({
    url: '/post',
    method: 'post',
    responseType: 'blob',
    data
  })
}

export function downloadFile(fileName, data) {
  if (window.navigator.msSaveOrOpenBlob) {
    window.navigator.msSaveOrOpenBlob(data, fileName)
  } else {
    const url = window.URL.createObjectURL(new Blob([data], { type: '' }))
    const a = document.createElement('a')
    a.setAttribute('href', url)
    a.setAttribute('download', fileName)
    a.click()
    window.URL.revokeObjectURL(url)
  }
}

export function downloadURL(fileName, url) {
  const encodeURL = encodeURIComponent(url)
  const a = document.createElement('a')
  a.setAttribute('href', encodeURL)
  a.setAttribute('download', fileName)
  a.click()
}

```

[Mock Service Worker](https://mswjs.io/docs/)
[天气接口](https://openweathermap.org/)

## 服务器

[nitro](https://nitro.unjs.io/) Server Toolkit
[h3](https://h3.unjs.io/guide) short for HTTP
[cross ws](https://crossws.unjs.io/) cross-platform WebSocket servers

## 测试框架

[jest.js](https://jestjs.io/)

[cypress](https://www.cypress.io/) 自动化测试

[testing-library](https://testing-library.com/)

## 其他

[storybook](https://storybook.js.org/) 构建 UI 组件和界面

[sonar](https://www.sonarsource.com/) 代码安全检查

[latex](https://latex.js.org/)

[polyfill](https://polyfill.io/)

[node-webkit](https://nwjs.io/) 直接通过 DOM 方式调用所有的 Node.js 模块

[crypto js](https://cryptojs.gitbook.io/) 加密

[GreenSock Animation Platform](https://gsap.com/) 动画
[easing](https://easings.net/) Easing functions
[lottie](https://airbnb.io/lottie) 解析 Adobe After Effects 导出的 JSON 动画

[auto-animate](https://auto-animate.formkit.com/)

[heatmap js](https://www.patrick-wied.at/static/heatmapjs/) 热力图

[js13k games](https://js13kgames.com/) js 代码游戏

[create js](https://createjs.com/)

[async](https://caolan.github.io/async) 类似 Lodash

[crontab](https://crontab.guru/) CornTab 表达式验证

[css nano](https://cssnano.github.io/cssnano/) 基于 postcss 插件和环境构建

[Browserslist](https://browsersl.ist/)
