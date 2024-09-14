# Vue

## 嵌套对象或数组修改(新加属性)触发视图更新(更改响应式)

### 数组变化侦测

- push / pop / shift / unshift / sort / reverse
- splice

```js
/**
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
