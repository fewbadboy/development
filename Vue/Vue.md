# Vue

## 嵌套对象或数组修改(新加属性)触发视图更新(更改响应式)

### 数组变化侦测

- push / pop / shift / unshift / sort / reverse
- splice

```js
/**
 * 修改嵌入数组内部属性(vue 2, 3)
 * vue 2 利用索引设置数组项不能检测到数据变动(导致UI无法渲染)
 * vm.items[indexOfItem] = newValue 错误
 * 更别说某一项里面添加属性
 *  */ 
const newValue = []
this.temp.userList.splice(index, 1, {
  ...this.temp.userList.at(index),
  property: newValue
})
```

### 嵌套对象

```js
// vue 2
// 响应式对象中添加一个 property，并确保这个新 property 同样是响应式的，且触发视图更新
// 
// Vue.set(Object/Array, propertyName/index, any)
this.$set(this.temp, 'property', newValue)

// vue 3
// ref对象所有对 .value 的操作都将被追踪，并且写操作会触发与之相关的副作用
const obj = ref({ name: 'vue' })
obj.value = {
  ...title.value,
  age: 18
}
obj.value.sex = 1

// 响应式转换时深层的，会影响到所有嵌套的属性
const ageRef = ref(0)
const obj = reactive({
  user: {
    name: 'vue'
  },
  age: ageRef // 会自动解包 obj.age
})
```

## 响应式

```js
// vue 3
computed({
  get: () => {},
  set: (value) => {}
}) // 默认 getter 函数

watchEffect(
  effect: (onCleanup: OnCleanup) => void,
  options
) // 立即运行函数，响应式地追踪其依赖，依赖更改时重新执行

const obj = reactive({
  user: { name: 'vue' },
  skill: [{ name: 'vue', year: 5 }]
})
const user = toRef(obj, 'user')
user.value === toRef(() => obj.user).value

toValue(user) // 与 unref() 类似，此函数会规范化getter 函数

toRefs(obj) // 将响应式对象转换为普通对象

const sRef = shallowRef({ name: 'vue' }) // 只有对 .value 的访问是响应式的
sRef.value.age = 18 // 不触发更新
triggerRef(sRef) // 须手动触发更新
sRef.value = { name: 'vue', age: 18 } // 触发更新

customRef() // 显式声明对其依赖追踪和更新触发的控制方式

shallowReactive() // 只有根级别的属性是响应式的,值为 ref 的属性不会被自动解包
```
