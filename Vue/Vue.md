# Vue

## [vue-macros](https://vue-macros.dev/)

## 嵌套对象或数组修改(新加属性)触发视图更新(更改响应式)

### 数组变化侦测

侦听响应式数组的变更方法

- push / pop / shift / unshift / sort / reverse / splice

```js
/**
 * vue 2 利用索引设置数组项不能检测到数据变动(导致UI无法渲染)
 * vm.items[indexOfItem] = newValue 错误
 */ 
const newValue = []
this.temp.userList.splice(index, 1, {
  ...this.temp.userList.at(index),
  property: newValue
})
```

## 3.x

### 响应式

- ref: 深层嵌套的对象、数组或者 JavaScript 内置的数据结构具有深入响应式。将值包装在特殊对象中
  - 模板渲染上下文中，只有顶级的 ref 属性才会被解包
  - 作为响应式对象的属性被访问或修改时自动解包
  - 作为响应式数组或原生集合类型 (如 Map) 中的元素被访问时，它不会被解包
  
  ```js
  const count = ref(0)
  const state = reactive({
    count
  })
  state.count = 1
  console.log(state.count)

  // 需要 处理 state.id 为顶层属性 模板才会自动解包
  const object = { id: count }
  const { id } = object
  // 模板生效 {{id}}
  ```

- shallowRef: 只追踪 .value 的访问(放弃深层响应式)
- reactive: 使对象本身具有响应性，深层转换，返回一个原始对象的 Proxy
  - 局限
    - 仅使用于对象类型
    - 不能操作原始类型(string,number,boolean)
    - 不能替换整个对象(不然丢失引用关系)
    - 解构丢失响应式连接
- shallowReactive:

### nextTick

要等待 DOM 更新完成后再执行额外的代码

```js
import { nextTick } from 'vue'

async function increment() {
  // ref change
  await nextTick()
  // 现在 DOM 已经更新了
}
```

### 计算属性

默认只读，但是某些特殊场景可以用下面实现“可写”的属性
getter 只做计算不应该有任何副作用(如异步请求货修改DOM)

```js
const firstName = ref('')
const lastName = ref('')
const fullName = computed({
  get() {
    return `${firstName.value} ${lastName.value}`
  },
  set(newValue) {
    [firstName, lastName] = newValue.split(' ')
  }
})
```

### 样式绑定

组件存在多个根元素，需要指定那个根接受父组件上的样式

```vue
<div>vue</div>
<div :class="$attrs.class">main</div>
```

绑定多个属性时可以用对象完成

```vue
<div v-bind="obj">
  {{ callback('hello') }}
</div>
```

### 条件渲染

v-if 表达式为真值时渲染
v-show 切换 display 的 CSS 属性，不支持在 template 元素上使用

### 列表渲染

v-for 与 数组

```html
<ul>
  <li v-for="(item, index) in arr" :key="index">
    {{ index }}:{{ item }}
  </li>
</ul>
```

v-for 与 对象

```html
<ul>
  <template v-for="(value, key, index) in myObject" :key="key">
    <li>{{ key }}:{{ value }}</li>
    <li>{{ index }}</li>
  </template>
</ul>
```

### 事件处理

```html
<!-- 使用特殊的 $event 变量 -->
<button @click="warn('Form cannot be submitted yet.', $event)">
  Submit
</button>

<!-- 使用内联箭头函数 -->
<button @click="(event) => warn('Form cannot be submitted yet.', event)">
  Submit
</button>
```

按键修饰符：

- ctrl: 当按下 Ctrl 时，即使同时按下 Alt 或 Shift 也会触发
- exact: 精确控制

### 表单输入

```html
<!-- 简写 v-model="text" -->
<input
  :value="text"
  @input="(event) => text = event.target.value"
>
```

- 设定 value 属性时， value 属性值优先级最高
- 文本类型的 input,textarea: 绑定 value 属性，监听 input 事件
- checkbox,radio: 绑定 checked 属性监听 change 事件(value 可绑定属性值)

```html
<input
  type="checkbox"
  v-model="toggle"
  true-value="yes"
  false-value="no"
/>
```

- select: 绑定 value 属性，监听 change 事件
- 修饰符：lazy,number,trim

### 侦听器

- watch(reactive, (newValue, oldValue, onCleanup))
  - 监听响应式对象属性值用 getter 函数

  ```js
  const user = reactive({ name: '' })
  const total = ref(0)
  // getter 去监听一个 reactive 对象
  const data = ref(null)
  const stopWatch = watch(() => user.name, (name) => {
    // 等待一些异步数据时
    if (data.value) {}
  })
  // 多个来源
  watch([total, () => user.name], ([newTotal, newName]) => {
    // 立即执行，且当监听属性改变时再次执行
  },{ immediate: true })
  ```

- watchEffect(onCleanup): 自动跟踪回调的响应式依赖，父组件更新之后所属组件 DOM 更新之前调用回调
- watchPostEffect: vue 更新后执行回调
- watchSyncEffect: 同步侦听器，会在 vue 进行任何更新前触发
- onWatcherCleanup: 清理副作用

### 模板引用

- useTemplateRef(): 组合式 API 中获取引用
  - v-for 模板引用时，ref 数组并不保证与源数组相同的顺序

### setup

使用 `<script setup>` 的组件是默认私有的， 需要 `defineExpose` 宏显示暴露才让父组件访问内部属性

### 组件

JavaScript 中 camelCase, DOM模板中都需要转换成 kebab-case

- key: 强制替换一个元素/组件而不是复用它
- defineProps(): 只读，单向绑定原则

```js
const { propA } = defineProps({
  propA: {
    // 多种类型
    type: [String, Number],
    required: true
  },
  propB: {
    type: Number,
    default: 1
  },
  propC: {
    type: Object,
    default(rawProps) {
      return { message: 'vue' }
    }
  },
  propD: {
    validator(value, props) {
      // value 必须匹配其中的一个值
      return ['success', 'warning', 'danger'].includes(value)
    }
  }
})
```

- defineEmits()

```js
defineEmits({
  // 校验 submit
  submit: ({ name, password }) => {
    if (name && password) {
      return true
    } else {
      return false
    }
  },
})
```

- 特定元素内部限制元素类型， 使用特殊的 `is="vue:component-name"`
- defineModel(): 组件实现双向绑定

```js
// <MyComponent v-model:title="bookTitle" />
// 修饰符
const [model, titleModifiers] = defineModel('title', {
  set(value) {
    if (titleModifiers.capitalize) {
      return value.charAt(0).toUpperCase() + value.slice(1)
    }
    return value
  }
})
```

- defineOptions()

```js
// <main v-bind="$attrs"></main>
defineOptions({
  // 禁用属性继承
  inheritAttrs: false
})

// JavaScript 访问贯穿属性
const attrs = useAttrs()
```

- Slots

```html
<div class="container">
  <header>
    <slot name="header" v-bind="item"></slot>
  </header>
  <main>
    <slot></slot>
  </main>
  <footer>
    <slot name="footer"></slot>
  </footer>
</div>

<BaseLayout>
  <template #header="slotProps">
    <!-- content for the header slot -->
  </template>
  <template #[dynamicSlotName]="slotProps">
    <!-- 动态插槽名，不能绑定 name 属性 -->
  </template>

</BaseLayout>
```

- provide / inject
- defineAsyncComponent()

```js
const AsyncComp = defineAsyncComponent({
  loader: () => import('./Foo.vue'),
  loadingComponent: LoadingComponent,
  delay: 200,
  errorComponent: ErrorComponent,
  timeout: 3000
})
```

- 可组合(composable): 是一个用组合式 API 封装和重用逻辑的函数

```js
import { toValue } from 'vue'
function useFuture(maybeRefOrGetter) {
  // a ref or a getter,
  // its normalized value will be returned.
  const fetchData = () => {
    const value = toValue(maybeRefOrGetter)
  }

  watchEffect(() => {
    fetchData()
  })
}
```

- 自定义指令
  - 在 `<script setup>` 中，以 v 前缀开头的 camelCase 变量可以用作自定义指令
  - 全局：`app.directive('highlight', {})`
  - hooks
    - created(el, binding, node)
    - beforeMount
    - mounted
    - beforeUpdate
    - updated
    - beforeUnmount
    - unmounted
- 插件

```js
app.use(myPlugin, {
  // optional option
})

const myPlugin = {
  install(app, options) {
    /**
     * configure the app
     * 
     * app.config.globalProperties
     * app.component()
     * app.directive()
     * app.provide()
     */ 
  }
}
```

- 内置组件
  - Transition
    - 过度类：v-enter/leave-from/to/active 中实现 transition 或 animation 动画
    - 自定义动画类
      - enter-from/active/to-class
      - leave-from/active/to-class
    - 支持嵌套
    - key: 强制重新渲染动画
    - JavaScript hooks: 添加 `:css="false"` prop
      - before-enter/leave(el)
      - enter/leave(el, done)
      - enter/leave-cancelled(el)
      - after-enter/leave(el)
  - TransitionGroup
    - v-move: 对移动元素引用过度 `v-leave-active { position: absolute; }`
  - KeepAlive
  - Teleport: defer 推迟 Teleport 的目标解析，直到应用程序的其他部分解析完成
  - Suspense

```css
.bounce-enter-active {
  animation: bounce-in 0.5s;
}
.bounce-leave-active {
  animation: bounce-in 0.5s reverse;
}
@keyframes bounce-in {
  0% {
    transform: scale(0);
  }
  50% {
    transform: scale(1.25);
  }
  100% {
    transform: scale(1);
  }
}
```

### 性能优化

- [virtual scrolling](https://vue-virtual-scroller-demo.netlify.app/)
- 请记住，组件实例比普通 DOM 节点要昂贵得多，而且为了逻辑抽象创建太多组件实例将会导致性能损失

### 渲染函数

```js
import { h } from 'vue'
const node = h(
  'div', // type
  { id: 'foo', class: 'bar' }, // props
  [
    /* children */
  ]
)
```

## 防抖、节流、中断请求

防抖、节流：控制请求点击频率
中断请求(`AbortController`)：响应时间久的请求的处理
