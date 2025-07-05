# python

## 变量

```py
# 全局变量声明
global x
```

## Set

```py
sex = { 'Male', 'Female' }
```

## Functions

- 默认值

```python
# 默认值必须指向 常量 或 None
def add_end(L=None):
  if L is None:
    L = []
  L.append('END')
  return L

# Lambda 表达式
double = lambda x : x * 2
```

- 可变参数 \*parament, 函数内部 parament 接收到一个 tuple
- 关键字参数 \*\*kw, 允许传入任意个含参数名的参数

```python
def person(name, age, **kw):
  print('name:', name, 'age:', age, 'other:', kw)

extra = {'city': 'Beijing', 'job': 'Engineer'}
# name: Jack age: 24 other: {'city': 'Beijing', 'job': 'Engineer'}
person('Jack', 24, city=extra['city'], job=extra['job'])
person('Jack', 24, **extra) # 简化
```

## 模块

```python
# 命令行运行时通过，其他地方判断失败
if __name__ == '__main__':
  pass
```

- pip 安装第三方模块

```shell
# https://pypi.org/
pip -h
```

## 正则表达式

`re` 模块

```python
# 默认是贪婪匹配，尽可能多(通过 ? 变成非贪婪匹配)
import re
re.split(r'\s+', 'a b  c') # ['a', 'b', 'c']

# 分组
m = re.match(r'^(\d{3})-(\d{3,8})$', '029-5121986')
```
