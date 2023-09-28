# Element

## article / footer

## aside

表示一个和其余页面内容几乎无关的部分，被认为是独立于该内容的一部分并且可以被单独的拆分出来而不会使整体受影响。

## audio

- crossorigin
  - anonymous 发送跨域请求时不携带验证信息
  - use-credentials
- currentTime 双精度浮点数，音频播放位置，单位 s
- duration 只读，总时长
- preload (autoplay 属性优先级高于 preload。autoplay被指定，浏览器自动显示下载)
  - none 音频不会被缓存
  - metadata 获取元数据(音频长度)
  - auto 用户可能会播放音频，整个音频都将被加载
- src 或用 source 元素或是 MediaStream

```js
const p = navigator.mediaDevices.getUserMedia({ audio: true, video: true }) // 麦克风 摄像头

// 分不同的轨道，可以控制音轨是否播放
p.then(function(stream) {
   console.log(stream.id)
})
```

## base

用于一个文档中包含的所有相对 URL 的根 URL

## caption

展示一个表格的标题

## cite

表示一个作品的引用，且必须包含作品的标题

## colgroup / col

定义表中的一组列表

## data / time

- value 指定内容和机器可读的翻译联系在一起

## datalist

其他表单空间可选择值

## del

- cite 解释修改原因 URI
- datetime 修改的时间和日期

## details / summary

被切换的时候显示内含的信息, `<summary>` 元素提供标签

- open 默认 false, 设置内容是否可见

## dl / dt / dd

包含术语定义以及描述的列表

## dfn

术语的一个定义

## fieldset / legend

对表单的控件元素分组

## figure / figcaption

代表一段独立的内容

## header

展示介绍性内容

## hgroup

代表文档标题和与标题相关联的内容，将 h1 - h6 元素和多个 p 元素组合一起

## hr

段落级元素之间的主题转换(伪元素添加标题、符号等)

## img

WebP 图像和动画的最佳选择

- crossorigin 默认关闭
  - anonymous 发送忽略凭据的跨源请求
  - use-credential 发送携带凭据的跨源请求（比如 cookie、X.509 证书和 Authorization 请求标头）
- loading
  - eager 默认值，立即加载图像（不管是否在可视窗口之外）
  - lazy 延迟加载图像
- srcset 逗号分隔一个或多个字符串表明用户代理的可能的图像，字符串可能是 URL 或 再加一个空格后附加宽度(w)或像素密度描述(x)
- role="img" 保证无障碍技术正确将 SVG 声明为图像内容

## input

- type
  - color
  - date
  - datetime-local
  - email
  - file
  - image
  - moth
  - number
  - password
  - range
  - search
  - tel
  - time
  - url
  - week
- autocomplete 自动填充特性
- pattern 使 value 有效的必须符合的正则
- required
- step 增量值

## ins

已经被插入文档中的文本

## kbd

## label

表示用户界面某个元素的说明

## main

应用的主题部分

## meter

显示已知范围的标量值或者分数值

- min 默认 0
- max 默认 1
- low 低值区间，必须比 min 属性值大
- high 定义了高值区间的下限值
- optimum 指示最佳取值
- value

## nav

```html
<nav>
  <ol>
    <li><a href="#">parent</a></li>
    <li>child</li>
  </ol>
</nav>
```

## select / optgroup / option

## picture

零或多个 source 元素 和 一个 img 元素组成来为不同的显示/设备场景提供图像版本
浏览器会选择最匹配的子 source 元素，如果没有匹配的，就选择 img 元素的 src 属性中的 URL

## section

通用独立的章节

## search

包含表单控件或与执行搜索或筛选操作相关的其他内容

## template / slot
