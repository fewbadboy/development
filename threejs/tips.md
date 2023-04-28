# Tips

## multiple layers

```js
// Raycaster
const raycaster = new Raycaster()
raycaster.layers.enable(0)
raycaster.layers.enable(1)
raycaster.layers.enable(2)

const camera = new PerspectiveCamera(...)
camera.layers.enable(0)
camera.layers.enable(1)
camera.layers.enable(2)

// object set layer
object.layers.set(index)

```

## Quaternion and Euler

```js
/**
 * 四元数转欧拉
 * @param { Object } orientation
 * @return {String} object
 */
export function quaternionToEulerAngles(orientation) {
  const { w, x, y, z } = orientation
  return {
    roll: Math.atan2(2 * (y * z + w * x), w * w - x * x - y * y + z * z),
    pitch: Math.asin(2 * (w * y - x * z)),
    yaw: Math.atan2(2 * (x * y + w * z), w * w + x * x - y * y - z * z)
  }
}

/**
 * 仅在z轴旋转（导航角）转四元数 roll-x pitch-y yaw-z
 * w = cos(x/2)cos(y/2)cos(z/2) + sin(x/2)sin(y/2)sin(z/2)
 * x = sin(x/2)cos(y/2)cos(z/2) - cos(x/2)sin(y/2)sin(z/2)
 * y = cos(x/2)sin(y/2)sin(z/2) + sin(x/2)cos(y/2)sin(z/2)
 * z = cos(x/2)cos(y/2)sin(z/2) - sin(x/2)sin(y/2)cos(z/2)
 */
export function yawToQuaternion(rad) {
  const { PI, cos, sin } = Math
  return {
    w: cos(PI / 360 * rad),
    x: 0,
    y: 0,
    z: sin(PI / 360 * rad)
  }
}

/**
 * 起始点 { x, y }
 * Z轴旋转 z deg
 * @param {Object} robot
 * @return {Array} []
 */
export function triangleArray(position, rad) {
  // 半径 20  图标大小 32
  const { x, y } = position
  const p = Math.PI
  return [
    [y + 16 + 20 * Math.sin(rad), x + 20 * Math.cos(rad)],
    [y + 16 + 20 * Math.cos(p / 3 + rad), x - 20 * Math.sin(p / 3 + rad)],
    [y + 16 - 20 * Math.sin(p / 6 + rad), x - 20 * Math.cos(p / 6 + rad)]
  ]
}
```
