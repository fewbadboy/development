# document

## Math Utils

- clamp(value, min, max) === Math.min(Math.max(value, min), max)
- degToRad(degrees)
- radToDeg(radians)
- euclideanModulo(n, m) === ((n % m) + m) % m 限制在 0 - m-1 范围
- isPowerOfTwo(n) 二进制按位 与(&) 1 为 0，一直到最高位
- inverseLerp(x, y, value)
- lerp(x, y, t) ===  x + t * (y - x) 线性插值法(LinearInterpolant)
- damp(x, y, l, dt) === x+(y−x)×(1−math.exp(-l * dt)) 平滑逼近目标
- pingpong(x, len) === len -Math.abs((x % ( 2 * len)) - len) 限制一个数值在一个范围内反复往返
- randFloat(low, high)
- randFloatSpread(range) === [-range / 2, range/ 2]
- randInt(low, high)
- seededRandom(seed) === [0 ,1]

## Matrix4

- compose(position, quaternion, scale)

## Euler

- new Euler(0, 0, 0, 'XYZ') 表示旋转的应用顺序
- set(x, y, z, order)
- setFromRotationMatrix(matrix, order)
- setFromQuaternion(quaternion, order)

## Quaternion

[angleToQuaternion](https://www.euclideanspace.com/maths/geometry/rotations/conversions/angleToQuaternion/index.htm)

- setFromEuler(euler)
- setFromAxisAngle(axis, angle)

## Objects

- BatchedMesh 使用相同材质但使用不同变换来渲染大量对象时可以提升性能
- Group
- Mesh
- Sprite: canvas 创建自定义内容
- Bone
- Skeleton
- SkinnedMesh
- Sky 给场景创建一个天空环境

## books

[Computer Graphics: Principles and Practice](./books/computer%20graphics.pdf) 计算机图形学的经典之作,从基础理论到高级技术的广泛内容

[Fundamentals of Computer Graphics](./books/Fundamentals%20of%20Computer%20Graphics,%20Fourth%20Edition.pdf) 计算机图形学的基本概念和算法的全面介绍

[Real-Time Rendering](./books/Real-Time-Rendering-Fourth-Edition.pdf) 实时渲染的权威书籍

[Interactive Computer Graphics: A Top-Down Approach with WebGL](./books/Interactive%20Computer%20Graphics.pdf) WebGL进行图形编程

[Physically Based Rendering: From Theory to Implementation](./books/Physically-Based.Rendering.pdf) 物理基础渲染（PBR）的理论和实现，是学习高质量渲染技术的绝佳资源

[Advanced Global Illumination](./books/Advanced%20global%20illumination.pdf) 全局光照技术，适合希望深入研究光照模型和算法

[GPU Gems]() GPU编程的书籍，涵盖了许多先进的图形技术和优化策略

[OpenGL Programming Guide: The Official Guide to Learning OpenGL](./books/OpenGL.Programming.Guide.8th.pdf) OpenGL编程的权威指南

[Real-Time Collision Detection](./books/Real-Time_Collision_Detection.pdf) 实时碰撞检测技术

[Mathematics for 3D Game Programming and Computer Graphics](./books/Mathematics%20for%203D%20Game%20Programming%20and%20Computer%20Graphics.pdf) 计算机图形学和3D游戏编程所需的数学基础知识

[The Art of 3D Computer Animation and Effects]() 3D计算机动画和特效的艺术和技术
