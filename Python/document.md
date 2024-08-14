# python

## dict

```python
d = {}
# d.get('name', None_Return_Value)
# d.pop('name') 删除 name
if 'name' in d:
  print(d['name'])
```

## 函数

- 默认值

```python
# 默认值必须指向 常量 或 None, 对象是存在以下情况
def add_end(L=[]):
  L.append('END')
  return L
    
print(add_end()) # ['END']
print(add_end()) # ['END', 'END']

# 修改后就不存在上述情况
def add_end(L=None):
  if L is None:
    L = []
  L.append('END')
  return L
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
# 迭代 for...in
# dict 默认迭代 key, 值时用 values(), 同时 key,value 用items()
# list 默认迭代值

# list 实现下标
for i, value in enumerate(['A', 'B', 'C']):
  print(i, value)

# 列表生成式
import os
[d for d in os.listdir('.')] # 列出目录和文件
[x for x in range(1, 11) if x % 2 == 0] # if 后不能加 else, 改用下面写法
[x if x % 2 == 0 else -x for x in range(1, 11)]

a,b = b, a+b # 赋值语句
# 相当于
t = (b, a+b)
a = t[0]
b = t[1]

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

## 函数式编程

- 装饰器

就是一个返回函数的高阶函数

## 模块

```python
# 命令行运行时通过，其他地方判断失败
if __name__ == '__main__':
  pass
```

- pip 安装第三方模块

```shell
# 查看 pip 指向 python 版本
# 取决于系统配置和安装 python 版本
pip --version
pip3 --version # 仅安装一个 python 版本时，指向同一版本
# Command:
# install/uninstall
# download
# inspect 检查 python 环境
# list 已安装包的列表
# show 展示安装完包的信息
# search
# 
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


# 多继承

# 枚举类
from enum import Enum, unique
Sex = Enum('Sex', ('M', 'F'))

@unique
class Weekday(Enum):
  Sun = 0
  Mon = 1
```

## IO 编程

- 序列化

```python
# pickle 模块

# JSON 模块
import json
```

## 进程和线程

- 多进程 `mulprocessing` 模块实现跨平台多进程，进程间通过 Queue, Pipes 等实现

同一变量，各自有一份拷贝存于每个进程，互不影响
可分布到多台机器上更加稳定

- 多线程 `threading` 高级模块

任何进程默认启动一个线程(主线程 MainThread), 主线程又可以创建新的线程
全局变量在线程中都是共享的(最大危险是多个线程同时修改一个变量)，通过一个锁解决

计算密集型(C语言，CPU计算能力)/IO 密集型(开发效率高的语言)

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

`deque` 高效实现插入和删除，适用于队列和栈

- base64
- urllib

## 网络编程

## 数据库
