
# 算法

## 滑动窗口

```js
while (right < n) {
  // 扩张
  right++
  while (不满足条件) {
    // 收缩
    left++
  }
}
```

## 递归 / DFS(Depth-First Search)

`有多深`

```js
function dfs(node, state) {
  if (!node) return false
  // if(!node.left && !node.right) {}
  if (是叶子 && 满足条件) return true
  return dfs(left, newState) || dfs(right, newState)
}
```

## BFS

`第几层/最短`

```js
// 入队列
const queue = [root] // 记录每一层的数据

while (queue.length) {
  const node = queue.shift()

  // 处理当前节点

  if (node.left) queue.push(node.left)
  if (node.right) queue.push(node.right)
}
```

## 动态规划（Dynamic Programming）

递归 + 记忆(表格)

```js
// 递归
function fib(n) {
  if (n <= 1) return 0
  return fib(n-1) + fib(n-2)
}

// DP
function fib(n) {
  if (n <= 1) return 0
  const dp = [0 , 1]
  for (let i = 2; i <= n; i++) {
    dp[i] = dp[i - 1] + dp[i - 2]
  }
  return dp[n]
}
```

DP 不是数学，是 表格填写

## 二分查找(已排序)

right 负责排除不可能， left 负责站在答案上

```js
function search(arr, target) {
  let left = 0
  let right = arr.length - 1
  while(left <= right) {
    // 
    const mid = left + ((right - left) >> 1)

    if (arr.at(mid) === target) {

    } else if (arr.at(mid) < target) {
      left = mid + 1 // left 左边一定是 小于 target 的
    } else {
      right = mid - 1 // right 右边边一定是 大于等于 target 的
    }
  }
  return left
}
```
