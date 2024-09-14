# Primary Math

[maths](https://www.euclideanspace.com/maths/algebra/index.htm)

## 以 pivot 点旋转 theta 弧度

```js
/**
 * @param {Object} point
 * @param {Number} theta
 * @param {Object} pivot
 * @return {Object}
 */
function rotateFrom(point, theta, pivot) {
  const sinTheta = Math.sin(theta)
  const cosTheta = Math.cos(theta)
  const cx = pivot.x, cy = pivot.y
  const x = point.x - cx, y = point.y - cy;

  return {
    x: x * cosTheta - y * sinTheta + cx,
    y: x * sinTheta + y * cosTheta + cy
  }
}

rotateFrom({x: 1, y: 2}, -Math.PI/2, {x: 1, y: 1}) // {x: 2, y: 1}
```
