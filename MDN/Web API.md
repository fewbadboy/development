# Web API

## BackgroundFetch

管理可能需要较长时间的下载的方法
如果用户离线，进程将暂停，直到用户再次联网

## Badging

PWA 与此应用程序关联的图标上设置 Badge

## Beacon

向 Web 服务器发送异步且非阻塞的请求，请求不需要有 response

## Barcode Detection

条形码检测

## Background Tasks

`requestIdleCallback`

```js
// 任务队列
const taskList = [
  {
    handle: (args) => console.log(args),
    data: [1,2,3]
  }
];

function performNonCriticalTask(deadline) {
  while (deadline.timeRemaining() > 0 && taskList.length > 0) {
    const task = taskList.shift(); // 从任务队列中取出一个任务
    task.handle(task.data)
  }

  if (taskList.length > 0) {
    // 如果还有任务，继续请求下一个空闲回调
    requestIdleCallback(performNonCriticalTask, { timeout: 1000 })
  }
}

// 请求空闲回调
requestIdleCallback(performNonCriticalTask)
```

## Battery Status

## Broadcast Channel

允许浏览上下文（即windows、tabs、frames 或 iframe）和同源之间进行基本通信

## Clipboard

```js
...
const registration = await navigator.clipboard.read()
...
```

## Compression Streams

## CookieStore

```js
/**
 * options:
 *  name
 *  value
 *  domain: Default to null
 *  expires: Default to null.A timestamp, Unix time milliseconds
 *  path: Default to /
 *  sameSite:
 *    strict: Default. 仅发送 cookies 以响应来自 cookies 源站点的请求
 *    lax: 同源请求和跨站 Get 请求（如script,link,a,img,iframe等）发送 Cookie，跨站的 POST 请求除外
 *    none: 任何情况下均发送 Cookies
 */
CookieStore.set(options)
```

## Credential Management

## Device Memory

可用内存量 Random Access Memory

## Device orientation

## DOM

The DOM represents a document with a logical tree.
Each branch of the tree ends in a node, and each node contains objects.
DOM methods allow programmatic access to the tree.
With them, you can change the document's structure, style, or content.

### AbortController

根据需要终端请求

### Document

Instance properties

- documentElement
- lastModified

### Element

inherit [DOM](https://developer.mozilla.org/en-US/docs/Web/API/Document)

Methods

- getBoundingClientRect
- scrollIntoView

### Event

Properties

- currentTarget
- target

### EventTarget

Methods

- addEventListener
- dispatchEvent
- removeEventListener

### Node

### Range

## File

## File System

访问设备文件系统上的文件，`<input type=file>` 实现等

## FormData

FormData(form, submitter)

- 如果 submitter 具有 name 属性或为 `<input type="image">`，其数据将包含在 FormData 对象中
- 编码类型为 multipart/form-data 时，使用与表格相同的格式
- 简单 GET 提交查询参数也可以直接传递个 URLSearchParams 构造函数
- append/delete/get/getAll/has/keys/set/values/entries

## GeoLocation

## HTML DOM

### History

### Location

### Navigator

### Window

Methods

- getComputedStyle
- postMessage

## HTML Drag and Drop

DataTransfer 用于保存上下文之间传输的任何数据，例如拖放操作或剪贴板读/写

- dropEffect: none, copy, link, move
- effectAllowed: none, copy, copyMove,...
- files: readonly, 拖动操作涉及拖动文件时存在
- items: readonly, list 拖拽
- types: readonly
- clearData()
- getData()
- setData(format, data): 参数均是字符串
- setDragImage()

## Houdini

开发人员可以直接访问 CSS 对象模型

## IndexedBD

## MediaDevices

```js
navigator.mediaDevices.getUserMedia({
  video: true
  audio: true
}).then((stream) => {

})
```

## Media Session

一种自定义媒体通知的方法

## Picture-in-Picture

```js
// 将视频放回其初始框中
document.exitPictureInPicture()
// 请求视频进入画中画模式
video.requestPictureInPicture()
```

## Resize Observer

## Sensor

传感器

## Selection

## Web MIDI

Musical Instrument Digital Interface (MIDI) Devices

## Web Share

分享功能

## Web Speech

## Web Workers

- postMessage
- message event's

## WebOTP

web 应用程序将电话号码用作登录因素时验证该电话号码是否属于用户

## WebRTC

Web Real-Time Communication

## WebUSB

## WebVTT

Web Video Text Tracks
