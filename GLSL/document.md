# GL Shader Language

## 函数

造型函数 [sin](https://thebookofshaders.com/glossary/?search=sin)

- sin
- asin
- cos
- acos
- tan
- atan
- pow
- exp
- log
- sqrt
- abs
- sign
- ceil
- floor `空间切块`
- fract = x - floor(x) `获取格子内部坐标`
- mod = x - y * floor(x/y) `获取格子内部坐标`
- step(edge, x) `判断边界`
  - x[i] < edge[i] ? 0.0 : 1.0
- smoothstep(edge0, edge1, x) 平滑的 0-1 函数 `平滑边界`
  - edge0 低边界
  - edge1 高边界
  - x 输入值
- mix(x, y, a) = x *(1 - a) + y* a `线性插值`
- mix(x, y, a) 线性插值函数
- min
- max
- clamp(x, minVal, maxVal) = min(max(x, minVal), maxVal)
- texture2D(sampler, uv)
- dot(x1, x2)
- distance(x1, x2)
- length(x)
- vec2(cos, sin)
- mat2(cos, -sin, sin, cos) 旋转
- mat2(_scale.x, 0., 0.,_scale.y) 缩放
- rand(x) 随机数函数

step(1. , mod(x, 2.)) `判断奇偶数行`

## ThreeJS Shader

ThreeJS [生成 prefixVertex 及 prefixFragment 代码](https://github.com/mrdoob/three.js/blob/dev/src/renderers/webgl/WebGLProgram.js#L441)

1. 拼接 precision
2. 注入 defines
3. 注入 attributes
4. 注入 uniforms
5. 注入 varying
6. 展开 #include
7. 根据材质功能增加代码

编译 shader 前会自动注入以下大类

| 类型 | 来源 |
| --- | --- |
| attribute | geometry |
| uniform | render/camera/material |
| varying | chunk 系统 |
| define | renderer 功能开关 |
| precision | WebGL 默认精度 |
| function | ShaderChunk |
| struct | lights/fog/shadow |

## 函数效果

[多项式造型函数](http://www.flong.com/archive/texts/code/shapers_poly/index.html)
[脉冲](https://thebookofshaders.com/edit.php#05/impulse.frag)
[立方脉冲](https://thebookofshaders.com/edit.php#05/cubicpulse.frag)
[指数步长](https://thebookofshaders.com/edit.php#05/expstep.frag)
[抛物线](https://thebookofshaders.com/edit.php#05/parabola.frag)
[平滑曲线](https://thebookofshaders.com/edit.php#05/pcurve.frag)
[缓动函数](https://easings.net/)

## Aspect Ratio 屏幕比例

1:1 变成 16：9, `st.x *= u_resolution.x/u_resolution.y;`

## Patterns

[Traditional Methods of Pattern Designing: An Introduction to the Study of the Decorative Art](https://archive.org/details/traditionalmetho00chririch/page/n3/mode/2up)

## ShaderToy

[水波纹](https://www.shadertoy.com/view/4s2SRt)

[像素精灵](https://pixelspiritdeck.com/)
