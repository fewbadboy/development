# document

相关数学知识：3rd Edition of Mathematics for 3D Game Programming and computer Graphics 或 2nd Edition of Essential Mathematics for Games and Interactive Applications

## Glossary

### qualifiers

1. attribute 从GL环境到顶点着色器的只读共享数据
2. const
3. uniforms 从GL环境到顶点或片段着色器的只读共享数据
4. varying 从顶点着色器到片段着色器的共享数据
5. precision
6. highp
7. mediump
8. lowp
9. in
10. out
11. inout

### built-in variables

1. gl_Position
2. gl_PointSize
3. gl_PointCoord
4. gl_FrontFacing
5. gl_FragCoord
6. gl_FragColor

### built-in constants

1. gl_MaxVertexAttribs
2. gl_MaxVertexVaryingVectors
3. gl_MaxVertexTextureImageUnits
4. gl_MaxCombinedTextureImageUnits
5. gl_gl_MaxTextureImageUnits
6. gl_MaxFragmentUniformVectors
7. gl_MaxDrawBuffers

### trigonometric function

1. radians 返回 PI * degrees / 180.0
2. degrees 返回 180.0 * radians / PI

### exponential function

1. exp2
2. log2
3. sqrt
4. inversesqrt

### common function

1. abs
2. sign
3. ceil: ceil(sin(x)): 1 的电子波，floor(sin(x)): -1 的电子波
4. fract：x - floor(x) 获取小数部分
5. mod：x - y * floor(x / y)
6. min
7. max
8. clamp(x, minVal, maxVal): min(max(x, minVal), maxVal)
9. mix(x, y, a): x*(1-a)+y*a
10. step(edge, x)
11. smoothstep(edge0, edge1, x)

### geometric function

1. length
2. distance
3. dot
4. cross
5. normalize
6. facefoward
7. reflect
8. refract

### matrix function

1. matrixCompMult

### vector relational function

1. lessThan
2. lessThanEqual
3. greatThan
4. greatThanEqual
5. qual
6. notEqual
7. any
8. all
9. not

### texture lookup function

1. texture2D
2. textureCube
