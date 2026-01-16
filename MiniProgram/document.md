# Document

## [小程序隐私协议开发指南](https://developers.weixin.qq.com/miniprogram/dev/framework/user-privacy/PrivacyAuthorize.html)

## APP 构造器

## Page 构造器

适用于简单的页面

```js
Page({
  data: {},
  onLoad: function(options) {},
  onShow: function() {},
  onReady: function() {}
})
```

## Component 构造器

适用于复杂的页面，方法放在 `methods: {}` 中

自定义组件：

```json
{
  "component": true,
  "lazyCodeLoading": "requiredComponents",
  "usingComponents": {}
}
```

```js
Component({
  options: {
    pureDataPattern: /^_/, // 纯字段，不用于界面渲染
    dynamicSlots: true
  },
  properties: {
    threshold: {
      type: Number,
      value: 0.8
    }
  },
  observers: {
    'some.name': function(value) {}
  },
  data: {},
  behaviors: [], // 组件之间代码共享的特性
  lifetimes: {}, // 组件生命周期
  pageLifetimes: {}, // 组件所在页面的生命周期
  methods: {
    onLoad: function(options) {}
  }
})
```

## 页面栈

- 小程序启动时：将路由事件指定的页面推入页面栈
- `navigateTo`(目标必须为非 tabBar 页面): 打开新的界面并将其推入页面栈
- `redirectTo`: 将当前页的栈顶替换成新的页面
- `navigateBack`: 将页面当前栈顶的若干个页面依次弹出销毁
- `switchTab`: 切换到指定的 tab 页面(必须为 tabBar 页面)
  - 事件发生时，当前栈存在多余一页页面的时候，重复执行栈顶页面弹出销毁操作，直到剩余一个页面
    - 这一个页面是目标 tabBar 页面(触发 onShow 生命周期)
    - 这一个页面不是目标 tabBar 页面(从页面栈弹出，创建目标 tabBar 页面并推入页面栈)
- `reLaunch` `autoLaunch`: 销毁当前所有页面并载入新页面

## 模块化

```js
exports {}
```

## 组件

### 公共属性

- `data-*`
- `bind*/catch*`

## 重新启动策略

```json
{  
  "restartStrategy": "homePage"
}
```

## 立即版本更新

```js
const updateManager = wx.getUpdateManager()

updateManager.onCheckForUpdate(function (res) {
  console.log(res.hasUpdate)
})

updateManager.onUpdateReady(function () {
  wx.showModal({
    title: '更新提示',
    content: '新版本已经准备好，是否重启应用？',
    success(res) {
      if (res.confirm) {
        updateManager.applyUpdate()
      }
    }
  })
})

updateManager.onUpdateFailed(function () {
  // 新版本下载失败
})
```

## Skyline 渲染引擎

### worklet 动画

### [自定义路由](https://developers.weixin.qq.com/miniprogram/dev/framework/runtime/skyline/preset-route.html)

```js
/**
 * routeType:
 *  wx://bottom-sheet 等
 *  */ 
wx.navigateTo({
  url: '',
  routeType: ''
})
```

## 容器转场

`<open-container>`

## 插件

## 网络

不是云开发需要配置服务器域名

## 分包加载

## 消息推送

## 大屏适配

- `rpx` 单位
  
## XR-FRAME

3D 应用解决方案

## API

### 设备

- 键盘: `wx.onKeyboardHeightChange(res => res.height)`

### WXML

```js
const query = wx.createSelectorQuery()
query.select('#the-id').boundingClientRect(({ top }) => {})

this.animate(selector， keyframes, duration, callback)
```
