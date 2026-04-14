# ThreeJS

## 材质

Principled BSDF

## 贴图

贴图大小推荐：512 / 1024/ 2048，压缩(KTX2)

```js
import { KTX2Loader } from 'three/examples/jsm/loaders/KTX2Loader'

// Blender 导出时 Draco 压缩过
import { DRACOLoader } from 'three/examples/jsm/loaders/DRACOLoader'

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

## 材质绘制软件

- Substance 3D Painter
- Quixel Mixer
- ArmorPaint

高模--低模

高模 -> 烘培贴图 (Render - Cycles)

PBR 写实

## LOD(Level of Detail)

👉 “LOD + 分块加载（大场景）”
👉 “视锥体裁剪（Frustum Culling）”
👉 “八叉树优化（Octree）”

远近模型切换

场景 = 多个模块(在范围内加载，远离卸载)

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

Collapse 塌陷 -自动合并顶点(将面)
Planar 平面 - 合并共面的面

自动化 pipeline： 模型打标签 + Python 导出脚本
