# document

## Using Hooks

以 `use`开头命名的 Functions 被称为 Hooks.
Hooks 仅在 React function 的顶部被调用.

## Style Props

```ts
/**
 * React.CSSProperties union of all the possible CSS properties
 */
interface ComponentProps {
  style: React.CSSProperties;
}
```

## JSX

1. 返回单个根元素(多元素时用`<Fragment>`或`<></>`包裹起来)
2. 关闭所有的 tags
3. 许多属性和元素都是 camelCase 命名(`aria-*` 和 `data-*` 除外)React `className` 替换 `class` 属性
4. 动态值用花括号 `{}` 替换 `""` (花括号还可以写入 JS 逻辑)
5. 条件运输符 `? :`
6. 逻辑运算符 `&&`
7. 渲染列表使用 `filter()` `map()`
8. `key={crypto.randomUUID()}` key 在兄弟节点之间必须唯一(后端更新数据时需要更新)
9. 保持组件是 pure. `must always return the same JSX given the same inputs.`
10. side effects(需要手动操作 DOM 或与外部系统交互的行为)

## Managing State

1. reacting input with state

    ```ts
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    // useState trigger a re-render
    const fullName = `${firstName} ${lastName}`; // calculating fullName
    ```

2. 在 state 中避免重复或多余(the same object at different state)
3. 避免深层嵌套 state(consider making it flat)
4. 传递一个不同的 `key` 去重新创建和初始化所有 state 状态值
5. 使用 Reducer 和 Context

## refs

记录一些信息但是不触发新的渲染

```js
const num = useRef(0) // return { current: 0 }
```

1. 存储 [timeout IDs](https://developer.mozilla.org/zh-CN/docs/Web/API/Window/setTimeout)
2. 存储和操作 DOM 元素
3. 存储不需要在 JSX 上去计算的对象
4. 创建自己定义组件上的 ref
   1. `forwardRef((props, ref) => { return <input {...props} ref={ref} /> })`

## Effect

Effects 让你在渲染完成后运行一些代码，以便组件与 React 之外的某些系统同步

与渲染无关但是需要执行的操作，如：数据获取，手动更新 DOM，设置定时器，记录日志等

```ts
// useEffectEvent
// requestAnimationFrame
useEffect(() => {
  // 每次渲染后都会运行，不推荐
  // 禁止内部改变状态（会导致无限循环）
});

useEffect(() => {
  // 组件首次挂载时运行，相当于 componentDidMount
}, []);

useEffect(() => {
  // 开发模式下 React 故意 remount, 是为了提示别忘记实现 cleanup function
  // 组件首次挂载时运行和依赖项 a 发生变化时运行
  return () => {
    
    // 实现清理函数修复 remount
    // next time 之前和 Unmount 时触发
}, [a]);

// 相关事件处理, 防止事件依赖 state/props 更改整个 effect 重新触发渲染
useEffectEvent(() => {

})
```
