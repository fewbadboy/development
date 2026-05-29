# ThreeJS

## 材质绘制软件

高模--低模

高模 -> 烘培贴图 (Render - Cycles)

PBR 写实

## Timer

- getDelta() 获取上一次调用 getDelta() 以来的时间间隔
- getElapsedTime() 获取自创建 Timer 以来的时间间隔
- update(timestamp) 更新内部状态

```js
// 用 delta 来移动物体，保证不同帧率下速度一致
// 不然 30fps 下动作很慢， 60fps 下动作很快，导致物体移动速度不一致
mesh.position.x += speed * delta;
```

## Object3D

- pivot = null 旋转和缩放的控制点
- position = (0,0,0)
- rotation = (0,0,0)
- scale = (1,1,1)
- matrixAutoUpdate = true, false 时静态对象关闭模型的矩阵更新，提高性能
- getObjectByName(name)
- getObjectsByName(name, value, result)
- lookAt(x, y, z)
- rotateOnAxis(axis, radius)
- traverse(callback)
- updateMatrix() 修改属性后手动更新矩阵

## Loader

抽象类

## MathUtils

- isPowerOfTwo(value)
- ceilPowerOfTwo(value) 向上取整到最近的 2 的幂次
- floorPowerOfTwo(value)
- degToRad
- radToDeg
- lerp(start, end, t) t 是 0 到 1 之间，返回 start 和 end 之间的值
- clamp(value, min, max) 超过范围就返回边界值
- pingpong(x, length) 随着 x 增加，返回值在 0 和 length 之间循环变化
- randInt(min, max)
- randFloatSpread(range) 返回 -range/2 到 range/2 之间的随机数

```js
function animate(timestamp) {
  cube.position.y = THREE.MathUtils.pingpong(timestamp / 1000, 1)
  requestAnimationFrame(animate)
}
```

## LOD(Level of Detail)

场景 = 多个模型渲染(在指定范围内加载，远离卸载)

- addLevel(object, distance)

```js
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'

const loader = new GLTFLoader()
const lod = new THREE.LOD()

Promise.all([
  loader.loadAsync('/model_high.glb'),
  loader.loadAsync('/model_mid.glb'),
  loader.loadAsync('/model_low.glb')
]).then(([high, mid, low]) => {

  lod.addLevel(high.scene, 0)
  lod.addLevel(mid.scene, 10)
  lod.addLevel(low.scene, 30)

  scene.add(lod)
})
```

Blender `修改器-生成-精简` 就是减少模型面数，高模 1.0 中模 0.3 低模 0.1

## InstanceMesh

大量相同的 geometry 和 material 但是 transformations 不同是可以来优化渲染的性能

## BatchedMesh

大量相同的 material 但是 geometry 和 transformations 不同是可以来优化渲染的性能

## BufferAttribute

THREE.BufferAttribute(array, itemSize)

## BufferGeometry

- setAttribute(name, attribute)
- setIndex(index: BufferAttribute | number[]) 设置索引，用于渲染三角形面

```js
const geometry = new THREE.BufferGeometry()
const vertices = new Float32Array([
  0, 0, 0,
  1, 0, 0,
  0, 1, 0
])
const uvs = new Float32Array([
  0, 0,
  1, 0,
  0.5, 1
])
geometry.setAttribute('position', new THREE.BufferAttribute(vertices, 3))
/**
 * 纹理坐标，顶点对应纹理贴图上的位置
 * u: 水平方向，v: 垂直方向，范围 0-1，(0,0) 左下角，(1,1) 右上角
 *  */ 
geometry.setAttribute('uv', new THREE.BufferAttribute(uvs, 2))
geometry.setIndex([0, 1, 2])
geometry.computeVertexNormals() // 计算顶点法线，用于光照效果

// geometry 顶点修改后必须在重新计算边界框和边界球
geometry.computeBoundingBox() // 计算模型的边界框
geometry.computeBoundingSphere() // 计算模型的边界球
```

边界框和边界球用于碰撞检测、视锥体裁剪等优化技术，可以提高渲染性能。

## Material

- Basic 不受光照影响，适合简单的颜色或纹理
- Lambert 适合需要柔和光照效果的对象
- Phong 支持高光和反射，适合需要强调光泽的对象
- Standard 基于物理的材质，支持金属度 metalness 和粗糙度 roughness 两个参数，适合写实效果
- Physical 基于物理的材质，支持金属度和粗糙度，适合写实效果

- alphaTest 透明测试，设置透明阈值，小于阈值的片元不渲染
- needsUpdate
- transparent = false

## Texture

贴图大小推荐：512 / 1024/ 2048，压缩(KTX2)

```js
import { KTX2Loader } from 'three/examples/jsm/loaders/KTX2Loader.js'

// Blender 导出时 Draco 压缩过
import { DRACOLoader } from 'three/examples/jsm/loaders/DRACOLoader.js'

// Vite 时
// dracoLoader.setDecoderPath('/draco/')
// public/draco/* 文件位置
```

基础色 / 漫反射 JPG PNG

金属(白金属，黑非金属) 粗糙(白粗糙-漫反射，黑光滑-镜面反射) PNG

- 凹凸图

黑凹白凸，PS(去色，调整对比度)

- 法线图 PNG，[NormalMap-Online](https://cpetry.github.io/NormalMap-Online/)

灰度图 -> 法线贴图

AO 环境光 增强阴影细节

- HDR 环境贴图决定真实感

```js
import { RGBELoader } from 'three/examples/jsm/loaders/RGBELoader.js'
new RGBELoader()
  .load('/hdr.hdr', (texture) => {
    texture.mapping = THREE.EquirectangularReflectionMapping
    scene.environment = texture
  })
```

通常占据 `width * height * 4 * 1.33` 字节的内存

```js
texture.rotation = THREE.MathUtils.degToRad(45)
```

GP 渲染时、片元(pixel/fragment)对应纹理上的某个点(uv坐标)，GPU 需要采样(sample)纹理颜色
屏幕上的片元数量不一定和纹理数量一样，因此需要放大/缩小纹理

- 片元比纹理像素，多放大纹理 `magFilter` 设置放大采样的方式

- needsUpdate = false, 设置为 true 后，`触发`向 GPU 上传纹理数据，在下一次渲染时更新纹理数据
- center = (0, 0) 设置纹理中心位置，左下
- offset = (0, 0) 设置纹理偏移量
- repeat = (1, 1) 设置纹理重复次数
- rotation = 0 设置纹理旋转角度，单位弧度, 正值逆时针旋转

## Curve

- getLength() 获取曲线的长度
- getPointAt(t, optionalTarget)  获取 t 位置的点，t 是 0 到 1 之间
- getTangentAt(t, optionalTarget) 获取 t 位置的切线，t 是 0 到 1 之间
- getPoints(division = 5) 返回曲线形状的 division + 1 个点
- getSpacedPoints(division = 5) 返回 division 个等间距的点

## Path extends CurvePath extends Curve

2D 路径

- lineTo(x, y)
- moveTo(x, y)
- setFromPoints(points)

## Vector3

- addVectors(v1, v2)
- subVectors(v1, v2)
- addScalar(s) 加上一个标量，返回当前向量
- crossVectors(a, b) 右手坐标系，a 食指方向，b 中指方向，结果拇指方向
- distanceTo(v) 计算到另一个向量的距离
- distanceToSquared(v) 计算到另一个向量的距离的平方
- dot(v) 向量的点积，返回一个标量，表示两个向量的方向是否相同
- floor() 向下取整
- negate() 取负
- normalize() 归一化
- set(x, y, z) 设置向量的值
- setComponent(index, value) 设置向量的第 index 个分量的值(0, 1, 2 分别对应 x, y, z)
- setX/Y/Z(x)
- setLength(length) 设置向量的长度

## GLTF

- glTF文件中的数据是用来渲染的，GPU 不用处理就可以直接使用
- OBJ、DAE 顶点被存储为文本，需要解析，文本顶点位置比二进制大 3 到 5 倍
- GLTF 格式支持 Draco 压缩，压缩后文件大小更小，加载更快

## Shader

- 顶点着色器：处理顶点数据，计算顶点位置、法线等属性
- 片元着色器：处理每个像素，计算像素的颜色、透明度等属性

```js
const uniforms = {
  texture: {
    type: 't',
    value: null
  },
  iTime: {
    type: 'f',
    value: 0,
  },
}

const vertexShader = `
  attribute vec3 position;
  attribute vec2 uv;

  varying vec2 vUv;

  void main() {
    vUv = uv;
    gl_Position = projectionMatrix *modelViewMatrix* vec4(position, 1.0);
  }
`

const fragmentShader = `
  varying vec2 vUv;

  uniform sampler2D texture;

  void main() {
    gl_FragColor = texture2D(texture, vUv);
  }
`

const material = new THREE.ShaderMaterial({
  uniforms,
  vertexShader,
  fragmentShader,
})
```

## GPU 资源需要释放

- Geometry
- Material
- Texture
- WebGLRenderTarget
- Controls
- EffectComposer
- Renderer
