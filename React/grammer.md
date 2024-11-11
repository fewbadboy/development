# document

## 虚拟 DOM

维护一个*虚拟 DOM* 来跟踪 state , props 或 context 的变化，从而决定如何高效的更新真实的 DOM。

手动更改 DOM 时(不操作触发虚拟 DOM 更新机制相关的条件), React 无法感知到 DOM 的更改

## 渲染过程

渲染过程就是指 React 更新虚拟 DOM 并计算出需要更新真实 DOM 的操作

1. 触发渲染(组件初始化渲染和组件 state 已更新)
2. 渲染组件
3. 提交给 DOM

props, state, context 以及 hooks 输入的参数(如 useEffect 等的依赖项)更新时与这些内容有直接关系的部分重新渲染
父组件重新渲染，子组件可能重新渲染(使用 React.memo 跳过重新渲染)

## Pure

针对相同的输入(props, state, context 以及 hooks 输入的参数)，每次得到相同的输出结果

Props 和 state 是不可变的，Hooks 的返回值和参数是不可变的

```ts
function Post({ item }) {
  item.url = new Url(item.url, base); // 🔴 Bad: never mutate props directly
  return <Link url={item.url}>{item.title}</Link>;
}
function useIconStyle(icon) {
  const newIcon = { ...icon }; // ✅ Good: make a copy instead
  if (icon.enabled) {
    newIcon.className = computeStyle(icon);
  }
  return newIcon;
}
```

传递给 JSX 后，值是不可变的

## Using Hooks

以 `use`开头命名的 Functions 被称为 Hooks.
Hooks 仅在 React function 的顶部被调用.

## DOM Event

```ts
export default function Form() {
  const [value, setValue] = useState('');
  
  function handleChange(event: React.ChangeEvent<HTMLInputElement>) {
    setValue(event.currentTarget.value)
  }

  return (
    <>
      <input value={value} onChange={handleChange} />
    </>
  )
}
```

## Children

```ts
/**
 * React.ReactNode type union all the possible types passed as children in JSX
 * React.ReactElement only JSX elements
 */
interface RenderModal {
  title: string;
  children: React.ReactNode;
}
```

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

## Adding Interactivity

```ts
// scheduled using a snapshot of the state at the time the user interacted with it!
export default function Button({children}) {
  const [number, setNumber] = useState(0);
  
  // 传递参数时, onClick 用回调函数 () => handleClick(parameter)
  // 直接绑定 handleClick(parameter) 渲染时触发函数回调，会导致无限循环
  function handleClick() {}
  return(
    <div>
      <input onChange={event => {
        event.stopPropagation(); // preventDefault
        setTimeout(() => {
          /**
           * 触发事件后去修改 number, 显示的时候还是 snapshot 的值
           * 不是新修改的值
           */
          alert(`Number is: ${number}`);
        }, 5000);
      }} />
      <button
        onClick={handleClick}
      >
        {children}
      </button>
      </div>
  )
}
```

1. state 是隔离和私有的，更改其中一个组件不会影响另一个组件
2. 相同输入，相同输出(在严格模式下开发，React 调用每个组件的函数两次，有助于发现由不纯函数引起的错误)
3. Snapshot(快照): 通过用户交互时的状态快照去调度处理(存储状态当前也许发成了变化)
4. 将一系列状态更新加入队列处理(在下一次 render 前更新相同的状态多次时，通过更新函数去计算基于上一个状态的下一个状态)

    ```js
    setNumber(5);
    // n => n + 1 is called an updater function
    setNumber(n => n + 1); 
    setNumber(42);
    /**
     * 更新状态时状态参数队列化处理，在事件处理程序中的所有其他代码运行完毕后进行处理
     * next render: state queue first return 5, then 5 + 1, then 42(final result)
     */
    ```

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
