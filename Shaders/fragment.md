# Fragment

## 遵守和理解

你将一遍又一遍的回到 0.0 到 1.0 的区间

## 案例一

```js
#ifdef GL_ES
precision mediump float;
#endif

#define PI 3.14159265359

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

float plot(vec2 st, float pct){
  return  smoothstep( pct-0.02, pct, st.y) - smoothstep( pct, pct+0.02, st.y);
}

void main() {
    vec2 st = gl_FragCoord.xy/u_resolution;

    // Smooth interpolation between 0.1 and 0.9
    float y = smoothstep(0.1,0.9,st.x);

    vec3 color = vec3(y);

    float pct = plot(st,y);
    
    color = (1.0-pct)*color+pct*vec3(0.0,1.0,0.0);

    gl_FragColor = vec4(color,1.0);
}
```

[结果](./shader_1.png)

## 案例二

```js
#ifdef GL_ES
precision mediump float;
#endif

#define PI 3.14159265359

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

vec3 colorA = vec3(1.0,0.0,0.0);
vec3 colorB = vec3(0.0,1.0,0.000);
void main() {
    vec2 st = gl_FragCoord.xy/u_resolution.xy;
    vec3 color = vec3(0.0);

    vec3 pct = vec3(st.x);

    pct.r = step(0.2, st.x);
    pct.g = step(0.4, st.x);
    pct.b = step(0.6, st.x);
    float c = step(0.8, st.x);

    color = mix(colorA, colorB, pct);

    color = mix(color,vec3(0.0,1.0,0.0),pct.r);
    color = mix(color,vec3(1.000,0.276,0.969),pct.g);
    color = mix(color,vec3(0.204,0.643,1.000),pct.b);
    color = mix(color,vec3(1.000,0.787,0.285),c);

    gl_FragColor = vec4(color,1.0);
}
```

[结果](./shader_2.png)

## 案例三

```js
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

float plot(vec2 st, float pct){
  return  smoothstep( pct-0.02, pct, abs(st.x-st.y));
}

void main() {
    vec2 st = gl_FragCoord.xy/u_resolution.xy;
    vec3 color = vec3(0.0);
    
    // 长方形
    // vec2 bl = step(vec2(0.1, 0.2),st); 
    // vec2 tr = step(vec2(0.1, 0.2),1.0-st); 
    // color = vec3(bl.x * bl.y * tr.x * tr.y);
    
    // 模糊窗口
   	// vec2 bl = smoothstep(vec2(0.0), vec2(0.1, 0.2),st); 
    // vec2 tr = smoothstep(vec2(0.0), vec2(0.1, 0.2),1.0-st); 
    // color = vec3(bl.x * bl.y * tr.x * tr.y);
    
    // floor 实现
    vec2 f1 = floor(vec2(st.x * 3.0, st.y * 3.0));
    vec2 f2 = floor(vec2((1.0-st.x)*3.0, (1.0-st.y)*3.0));
    color = vec3(f1.x * f1.y * f2.x * f2.y);
    gl_FragColor = vec4(color,1.0);
}
```

[结果](./shader_3.png)

## 案例四

要实现的效果如下图：
[示例](./shader_4_example.png)

1. 依次设定每个不同颜色的长方形的位置即可
2. 同色的大长方形加黑色长方形

```js
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

vec3 rect(vec2 lb, vec2 rt, vec2 st){
    vec2 bl = step(lb,st); 
    vec2 tr = step(rt,1.0-st); 
    return vec3(bl.x * bl.y * tr.x * tr.y);
}

void main() {
    vec2 st = gl_FragCoord.xy/u_resolution.xy;
    vec3 color = vec3(0.0);
    color.r = floor(st.x);
    color.g = floor(st.x);
    color.b = floor(st.x);
    
    // 1
    color += rect(vec2(0.,0.7), vec2(0.9,0.), st)*vec3(1.0,.0,.0);
    color += rect(vec2(0.15,0.7), vec2(0.7,0), st)*vec3(1.0,.0,.0);
    color += rect(vec2(0.,0.5), vec2(0.9,0.35), st)*vec3(1.0,.0,.0);
    color += rect(vec2(0.15,0.5), vec2(0.7,0.35), st)*vec3(1.0,.0,.0);
    color += rect(vec2(0.35,0.7), vec2(0.15,0.), st)*vec3(1.0,1.0,1.0);
    
    color += rect(vec2(0.35,0.5), vec2(0.,0.35), st)*vec3(1.0,1.0,1.0);
    
    color += rect(vec2(0.9,0.7), vec2(0.,0.), st)*vec3(.0,1.,.0);
    
    color += rect(vec2(0.6,0.), vec2(0.,0.9), st)*vec3(.0,0.,1.);

    // 2
    // color += rect(vec2(0.,0.6), vec2(0.7,0.), st)*vec3(1.0,.0,.0);
    // color += rect(vec2(0.7,0.6), vec2(0.,0.), st)*vec3(.0,1.0,.0);
    // color += rect(vec2(0.35,0.6), vec2(0.35,0.), st)*vec3(1.0,1.0,1.0);
    // color *= vec3(1.0) - rect(vec2(0.,0.775), vec2(0.,0.175), st)*vec3(1.,1.,1.);
    
    gl_FragColor = vec4(color,1.0);
}
```

[结果](./shader_4.png)

[结果](./shader_4.1.png)

## 实例五

圆

```js
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

void main() {
    vec2 st = gl_FragCoord.xy/u_resolution.xy;
    // 径向渐变
    // float pct = distance(st, vec2(0.5));
    // float pct = length(st - vec2(0.5));
    // vec2 t = st - vec2(0.5);
    // float pct = sqrt(t.x * t.x + t.y * t.y );

    // 有没有其他方法来实现这样画布内圆形渐变的效果？？


    // 圆
    float pct =  step(0.2 ,distance(st,vec2(0.5)));

    // 圆2-边界顺滑(一闪一闪亮晶晶？)
    // float a = abs(sin(u_time));
    // float len = distance(st,vec2(0.5));
    // float pct = smoothstep(.3*a, 0.295*a, len); // dot(len, len)

    // 椭圆
    // float pct = distance(st,vec2(0.4)) + distance(st,vec2(0.6));

    // float pct = sqrt(t.x * t.x + abs(t.y) );

    // float pct = sqrt(t.x * t.x + t.y);

    vec3 color = vec3(1.0 - pct, 0., 0.);
    gl_FragColor = vec4(color,1.0);
}
```

[结果](./shader_5.png)

[结果](./shader_5.1.png)

[结果](./shader_5.2.gif)

## 实例六

```js
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

void main(){
  vec2 st = gl_FragCoord.xy/u_resolution.xy;
  st.x *= u_resolution.x/u_resolution.y;
  vec3 color = vec3(0.0);
  float d = 0.0;

  // Remap the space to -1. to 1.
  st = st *2.-1.;

  // Make the distance field
  d = length( abs(st)-.2 );
  // d = length( min(abs(st)-.3,0.) );
  // d = length( max(abs(st)-.3,0.) );

  // Visualize the distance field, 
  gl_FragColor = vec4(vec3(fract(d*10.0)),1.0);

  // Drawing with the distance field
  // gl_FragColor = vec4(vec3( step(.3,d) ),1.0);
  // gl_FragColor = vec4(vec3( step(.3,d) * step(d,.4)),1.0);
  // gl_FragColor = vec4(vec3( smoothstep(.3,.4,d)* smoothstep(.6,.5,d)) ,1.0);
}
```

[结果](./shader_6.png)

## 实例七

每个像素的半径喝角度笛卡尔坐标映射到极坐标

```code
vec2 pos = vec2(0.5)-st;
float r = length(pos) * 2.0;
float a = atan(pos.y, pos.x);
```

```js
#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

void main(){
  vec2 st = gl_FragCoord.xy/u_resolution.xy;
  st.x *= u_resolution.x/u_resolution.y;
  vec3 color = vec3(0.0);
  float d = 0.0;

  // Remap the space to -1. to 1.
  st = st *2.-1.;

  // Make the distance field
  d = length( abs(st)-.2 );
  // d = length( min(abs(st)-.3,0.) );
  // d = length( max(abs(st)-.3,0.) );

  // Visualize the distance field, 
  gl_FragColor = vec4(vec3(fract(d*10.0)),1.0);

  // Drawing with the distance field
  // gl_FragColor = vec4(vec3( step(.3,d) ),1.0);
  // gl_FragColor = vec4(vec3( step(.3,d) * step(d,.4)),1.0);
  // gl_FragColor = vec4(vec3( smoothstep(.3,.4,d)* smoothstep(.6,.5,d)) ,1.0);
}
```

[结果](./shader_6.png)

[y = abs(cos(x*12.)*sin(x*3.))*.8+.1](./shader_6.1.png)

[y = smoothstep(-.5,1., cos(x*10.))*0.2+0.5](./shader_6.2.png)
