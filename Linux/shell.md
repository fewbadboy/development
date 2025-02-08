# Shell 环境

Bourne Again Shell（/bin/bash）大多数Linux系统默认的Shell

```shell
#!/bin/bash
echo "shell"
```

`bash shell.sh`即可
或者执行`./shell.sh` 时需要`chmod +x ./shell.sh` 使脚本具有执行权限

## 变量

声明变量 `declare` 可以通过变量指定参数类型 `typeset` `local`

标准输入读取一行到变量 `read`

使用变量`$变量` 或者`${变量}` 推荐后者写法
循环出/etc目录下的文件名 `for file in $(ls /etc)` 或者 `for file in \`ls /etc\``

只读 `readonly variable_name`
删除变量 `unset variable_name`

```shell
# 环境变量 env

# 执行命令
eval 'll' 

# exec 用当前执行的命令去替换当前的shell进程，清理老进程的环境

# export 当前会话中，设置环境变量，并使其对所有子进程可见
```

字符串操作

- 获取长度 `${#variable_name}`
- 截取长度 `${variable_name:1:4}` 第二个字符开始，截取四个字符；第一个字符的索引是0
- 查找字符串位置 `expr index "$variable_name" io` 查找i或者o的位置，那个先出计算那个

```shell
# export 导出为环境变量

# 输出当前使用的shell
echo $SHELL 
echo $0

# 给 PATH 添加新的路径
export PATH="$PATH:/home/user/bin"
```

字符分为两类: 普通纯文本,元字符(Shell 的保留字符, 需要转义 如 $ 获取变量 * 通配符 )

`双引号`中的 $, `, 转义符还是会被解析成其特殊含义

整数运算: `$[]` 做运算 或 expr 或 $((算术表达式)) 或 let
数学运算高级适用工具: bc

```shell
## 彩色输出
# 0 重置 30 黑 31 红 32 绿 33 黄 34 蓝 35 洋红 36 青 37 白  - 文字
# 0 重置 40 47 白 - 背景
echo -e "\e[1;31m red \e[0m"
```

创建俩文件 `touch file_{A,B}` 创建 file_A, file_B

## 数组

括号表示数组，元素用空格符号隔开`数组名=(值1 值2 ... 值n)`

- 获取元素 `${数组名[下标]}` ${array_name[@]} 获取数组中的所有元素
- 获取数组长度 `${#array_name[*]}` or `${#array_name[@]}`
- 打印数组所有元素 `${array_name[*]}` or `${array_name[@]}`
- 获取索引列表 `${!array_name[*]}` or `${!array_name[@]}`

## 别名

```shell
# 新增别名(放入 ~./bashrc 文件永久有效)
alias install='sudo apt-get install'
# 取消别名 unalias 或者 alias install=''
unalias install  
```

## 设置日期及延时

```shell
# 日期
date
# a A 工作日, Sat Saturday
# b B 月份, Nov November
# d D , day mm/dd/yy
# y Y 年, 22 2022
# m 月份, 07
# I H 小时
# M 分钟
# S 秒
date +%s # 秒
date "+%H %M %S" # 时 分 秒
```

## 函数及传参

局部参数 local 声明

- $0 脚本本身
- $# 传递给脚本的参数个数
- $* 单字符显示所有向脚本传递的参数， 返回"1 2 3"
- $$ 当前脚本运行的进程ID
- $! 后台运行的最后一个进程ID
- $@ 单字符显示所有向脚本传递的参数 返回"1" "2" "3"
- $? 脚本或者命令的返回值(正常退出命令和脚本,0作为返回值)

## 运算符

- eq ne gt lt ge le (算术)
- ! -o(或) -a(与)
- || &&
- z 字符串长度是否为0，n相反; $ 监测字符串是否为空
- c(char) f x b(block) e(exist) d(directory) w(write) r L(link) （文件
- = == != > <

## 控制流

循环中加入 `sleep 2` 这样的命令降低循环速度

```shell
# if
if condition1
then
    command1
elif condition2
then
    command2
else
    commandN
fi

# for
for var in item1 item2 ... itemN
do
    command1 # $var
    command2
    ...
    commandN
done

for (( i=0; i<10 ; i++ )){
    command # $i
}

# while
while condition
do
    command
done

repeat() {
    while :
        do
            $@ && return
            sleep 30
        done
}

# 无限
while :
do
    command
done

# until
until condition
do
    command
done

# case...esac
case 值 in
模式1)
    command1
    command2
    ...
    commandN
    ;;
模式2)
    command1
    command2
    ...
    commandN
    ;;
esac

# break 跳出循环
# continue 继续循环
```

## 函数

定义格式

```shell
[ function ] funname [()]
{
    action;
    [return int;]
}
# 调用
funname 1 2 3
```

函数参数 函数内部$1表示第一个参数

## 执行文件

```shell
# . 命令
. filename

# source 命令
source filename

# source script.sh 
# 执行脚本后，修改当前环境变量, MY_VAR 在当前的 shell 中可用

# ./script.sh 
# 执行脚本后 MY_VAR 是一个孤立饿环境变量
# ./script.sh

# script.sh
export MY_VAR=Hello
echo $MY_VAR
```

## 录制回放终端会话

```shell
# 记录整个终端会话，包括输入输出
script my_session.log

# 推出
exit

scriptreplay
```

## find

- atime 最近一次访问文件时间(天)，amin 分钟
- mtime 文件内容最后一次修改
- ctime 文件元数据（权限等
- name 文件名
- exec 执行的命令

```shell
# 当前目录下最近七天内访问过的所有文件大小小于 2KB 的文件 (+7 超过七天)
find $PWD -type f -atime -7 -size -2k
```

## tr

translate 简写

```shell
echo "HELLO" | tr 'A-Z' 'a-z'
echo "HELLO" | tr '[:upper:]' '[:lower:]'
echo 12345 | tr '0-9' '98765210' # 87654  加密
```

## mktemp

为临时文件或目录创建唯一名字

```shell
filename=`mktemp` && echo $filename
```

## split csplit

分割文件和数据

```shell
# 每个文件 10kb -d -a 4 数字后缀 长度4
split -b 10k  data.file -d -a 4 split_file
# split_file0001

# 删除变量 VAR 中位于 % 右侧的通配符所匹配的字符串(例如除去扩展名)
# %% 模式是贪婪，会匹配最长结果 
${VAR%.*}

# 文件扩展名 ## 贪婪模式
${VAR#*.}
```

## 文本处理

```shell
# grep "pattern" filename1 ...

sed

awk

# 通过正则过滤
egrep -o '[a-zA-Z0-9]+' file
```

## 网络

```shell
# 网络配置
# ifconfig eth0 up/down 启用/关闭
ifconfig lo

# DNS
/etc/resolv.conf

# DNS 查找
host google.com

# 路由
route

# SSh 远程安全登录
ssh username@remote_host -p 22

# SCP(secure copy) 文件复制
scp filename username@remote_host:/home/path

# 连接无线网络
# 可以物理地址欺骗

# 公钥放在 ~/.ssh/authorized_keys
# ~/.ssh 私钥
ssh-keygen -t rsa

# 分析网络流量
lsof

# 网络状态
netstat

# 防火墙
iptables 

# 路由追踪
traceroute
```

## 服务

```shell
# service xxxx start/stop/restart/status
```
