# document

## 位置

和 canvas 拥有相似的坐标或网格系统(左上原点)

## Path

- Line
  - M x y, m dx dy (move to)
  - L x y, l dx dy (line to)
  - H x, h dx (horizontal)
  - V y, v dy (vertical)
  - Z, z (close path)
- Curve
  - C x1 y1, x2 y2, x y (cubic)
  - S x2 y2, x y (smooth)
  - Q x1 y1, x y (quadratic)
  - T x y (smooth quadratic) 只有前一个命令是 Q / T 时才有效
  - A rx ry a-axis-rotation large-arc-flag sweep-flag x y (arcs)
![示例](./svgarcs_flags.png)

## Gradient

## Tool

[developer](https://developer.mozilla.org/en-US/play)
