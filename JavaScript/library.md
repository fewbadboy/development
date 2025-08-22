# 工具库

## 图表

[vis.js](https://visjs.org/)
[ArcGIS](https://developers.arcgis.com/)
[AntV](https://antv.antgroup.com/)

[Cytoscape.js](https://js.cytoscape.org/) 网络图可视化

[Vue Flow](https://vueflow.dev/)

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

## 视频

[video js](https://docs.videojs.com/)
[LivePlayer](https://www.liveqing.com/docs/manuals/LivePlayer.html)

## 3D

[HDR](https://polyhaven.com/hdris/skies)

## HTTP 请求

[axios](https://axios-http.com/)

```js
service.interceptors.response.use((response) => {
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
    data: blobData,
  } = response;
  if (/(blob|arrayBuffer)/.test(responseType)) {
    if (status === 200 && responseType === "blob" ? size > 0 : arrayBuffer > 0)
      return Promise.resolve({
        data: blobData,
        disposition: decodeURIComponent(
          response.headers["content-disposition"]
        ),
      });
    return Promise.reject(blobData);
  }
});

/**
 * 请求配置
 * data: 仅适用 'PUT', 'POST', 'DELETE 和 'PATCH' 请求方法
 * RESTful API, 大多数框架推荐 delete 用 Path 参数
 * params: 与请求一起发送的 URL 参数,必须是一个简单对象或 URLSearchParams 对象
 */
export function fetchPostFile(params) {
  return request({
    url: "/get",
    method: "get",
    responseType: "blob",
    params,
  });
}
export function fetchPostFile(data) {
  return request({
    url: "/post",
    method: "post",
    responseType: "blob",
    data,
  });
}

export function downloadFile(fileName, data) {
  if (window.navigator.msSaveOrOpenBlob) {
    window.navigator.msSaveOrOpenBlob(data, fileName);
  } else {
    const url = window.URL.createObjectURL(new Blob([data], { type: "" }));
    const a = document.createElement("a");
    a.setAttribute("href", url);
    a.setAttribute("download", fileName);
    a.click();
    window.URL.revokeObjectURL(url);
  }
}

export function downloadURL(fileName, url) {
  const encodeURL = encodeURIComponent(url);
  const a = document.createElement("a");
  a.setAttribute("href", encodeURL);
  a.setAttribute("download", fileName);
  a.click();
}
```

## 其他

[GreenSock Animation Platform](https://gsap.com/)

[lottie](https://airbnb.io/lottie) 解析 Adobe After Effects 导出的 JSON 动画

[create js](https://createjs.com/)

[Browserslist](https://browsersl.ist/)

[socket.io](https://socket.io/) 双向通信
