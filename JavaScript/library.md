# 工具库

## Logo

[Cool Text](https://cooltext.com/)

## 字体、icon

[google fonts](https://fonts.google.com/)
[google icons](https://fonts.google.com/icons)
[ionicons](https://ionic.io/ionicons) 图标， icon font
[icon 集合](https://icones.js.org/)

## UI

[Prime Vue](https://primevue.org/)
[Element Plus](https://element-plus.org/)
[UIverse](https://uiverse.io/)

## color

[duotones](https://duotones.co/)
[flat ui colors](https://flatuicolors.com/)

## 图表

[font awesome](https://fontawesome.com/) 使用 icons
[echarts](https://echarts.apache.org/zh/index.html)
[vis.js](https://visjs.org/)
[ArcGIS](https://developers.arcgis.com/) SDKs 支持多种语言库
[AntV](https://antv.antgroup.com/)
[Cytoscape.js](https://js.cytoscape.org/) 网络图可视化
[Vue Flow](https://vueflow.dev/)
[p5](https://p5js.org/)

## canvas

[Canvas Confetti](https://www.kirilv.com/canvas-confetti/) 五彩纸屑
[roughJS](https://roughjs.com/) 手绘风格
[fireworks](https://github.com/crashmax-dev/fireworks-js) 烟花

## 地图

[leaf let js](https://leafletjs.com/)
[open layers](https://openlayers.org/)

[cesium](https://cesium.com/platform/cesiumjs/) 3D 地理空间可视化

[Geo JSON 地图数据](https://datav.aliyun.com/portal/school/atlas/area_selector)
[Geo JSON 数据可视化](https://geojson.io/)

## 视频

[video js](https://docs.videojs.com/)
[LivePlayer](https://www.liveqing.com/docs/manuals/LivePlayer.html)

## 3D

[book shaders](https://thebookofshaders.com/)
[learn webGL](https://learnwebgl.brown37.net/)
[webGL fundamentals](https://webglfundamentals.org/webgl/lessons/zh_cn/)
[webGL2 fundamentals](https://webgl2fundamentals.org/)
[HDR](https://polyhaven.com/hdris/skies)

## vue

[awesome-vue](https://github.com/vuejs/awesome-vue)
[vue use](https://vueuse.org/)

vue-puzzle-vcode

## 文档

[documentation](https://docus.dev/)

## 规范

[jsdoc](https://www.jsdoc.com.cn/)

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
     *  'content-disposition': 'attachment;filename=xxx.xlsx' 仅支持 ASCII，不支持中文、空格、特殊字符
     *  'content-disposition': 'attachment;filename*=UTF-8''xxx.xlsx' 支持 UTF-8 编码
     * }
     */
    const {
      status,
      config: { responseType },
      data: { size, arrayBuffer },
      data: blobData
    } = response
    if (/(blob|arrayBuffer)/.test(responseType)) {
      if (status === 200 && responseType === 'blob' ? size > 0 : arrayBuffer > 0)
        return Promise.resolve({ 
          data: blobData,
          disposition: decodeURIComponent(response.headers['content-disposition'])
        })
      return Promise.reject(blobData)
    }
  }
)

export function fetchPostFile(params) {
  return request({
    url: '/get',
    method: 'get',
    responseType: 'blob',
    params
  })
}
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

## 其他

[sonar](https://www.sonarsource.com/) 代码安全检查
[latex](https://latex.js.org/)
[crypto js](https://cryptojs.gitbook.io/) 加密
[GreenSock Animation Platform](https://gsap.com/) 动画
[easing](https://easings.net/) Easing functions
[scroll magic](https://scrollmagic.io/)
[lottie](https://airbnb.io/lottie) 解析 Adobe After Effects 导出的 JSON 动画
[create js](https://createjs.com/)

[Browserslist](https://browsersl.ist/)
