# python

## 变量

```py
# 全局变量声明
global x
```

## 数据类型

```py
print(type(None))

# 字符串格式化
total = 10
text = 'python,\"Escape\"'
fmt = f"Hello, {text},{total:.2f}"
```

## 操作符

- 逻辑：and, or, not
- is
- in

## List

```py
# 浅拷贝
users = [{ 'name': 'cat'}] 
copyUsers = users.copy()
copyUsers[0].get('name') = 'dog'
# 同样会修改原始数据 users[0] 中 name
```

## Tuple

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

- 可变参数 *parament, 函数内部 parament 接收到一个 tuple
- 关键字参数 **kw, 允许传入任意个含参数名的参数

```python
def person(name, age, **kw):
  print('name:', name, 'age:', age, 'other:', kw)

extra = {'city': 'Beijing', 'job': 'Engineer'}
# name: Jack age: 24 other: {'city': 'Beijing', 'job': 'Engineer'}
person('Jack', 24, city=extra['city'], job=extra['job'])
person('Jack', 24, **extra) # 简化
```

## 高级特性

```python
# 列表生成式
import os
[d for d in os.listdir('.')] # 列出目录和文件
[x for x in range(1, 11) if x % 2 == 0] # if 后不能加 else, 改用下面写法
[x if x % 2 == 0 else -x for x in range(1, 11)]

# 生成器
g = (x * x for x in range(10))
next(g) # for n in g:

def odd():
  yield 1
  yield 3
  yield 5
o = odd()
next(o)

# 迭代器 Iterable

# reduce
from functools import reduce
dig = { '0': 0, '1': 1, '2': 2 }
def trans(s):
  return deg[s]
reduce(lambda x, y: x + y, map(trans, ['0', '1', '2']))
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

## 面向对象

```python
# 继承自 object
class Student(object):
  # 类属性
  type = 2

  # 限制实例绑定的属性
  __slots__ = ('score', 'sex', 'skill')

  def __init__(self, score):
    # private
    self.__score = score

  # ins.score 转化成 ins.get_score()
  @property
  def score(self):
    return self.__score

  # ins.score = 90 转化成 ins.set_score(90)
  @score.setter
  def score(self, value):
    self.__score = value

  # print 调用实例时调用
  def __str__(self):
    return 'Student score: %d' % self.score

  # 直接调用示例时调用 __repr__
  

li_ming = Student(90)

# dir(obj) 获取一个对象的所有属性和方法
# getattr/setattr/hasattr()

# 枚举类
from enum import Enum, unique
Sex = Enum('Sex', ('M', 'F'))

@unique
class Weekday(Enum):
  Sun = 0
  Mon = 1
```

## IO 编程

## 正则表达式

`re` 模块

```python
# 默认是贪婪匹配，尽可能多(通过 ? 变成非贪婪匹配)
import re
re.split(r'\s+', 'a b  c') # ['a', 'b', 'c']

# 分组
m = re.match(r'^(\d{3})-(\d{3,8})$', '029-5121986')
```

## built-in 模块

- collections
- base64
- urllib

## 网络编程

## 数据库

## 库

[django](https://www.djangoproject.com/)
[flask](https://flask.palletsprojects.com/en/stable/)

[MatPlotLib](https://matplotlib.org/) 图形可视化

[pandas](https://pandas.pydata.org/) 数据分析

[plotly](https://plotly.com/python/) 图形库

[NumPy](https://numpy.org/) 处理数组

[py game](https://www.pygame.org/docs/)

[pillow](https://pypi.org/project/pillow/) 图像处理

[pytorch](https://pytorch.org/) 深度学习库
