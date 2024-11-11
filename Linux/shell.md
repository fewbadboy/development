# Shell 环境

Bourne Again Shell（/bin/bash）大多数Linux系统默认的Shell

```shell
#!/bin/bash
echo "shell"
```

`bash shell.sh`即可
或者执行`./shell.sh` 时需要`chmod +x ./shell.sh` 使脚本具有执行权限

## 变量

命名只能是英文字母，数字，下划线
数字不能开头
不能是bash中的关键字

声明变量 `declare`可以通过变量指定参数类型 `typeset` `local`

标准输入读取一行到变量 `read`

使用变量`$变量` 或者`${变量}` 推荐后者写法
循环出/etc目录下的文件名 `for file in $(ls /etc)` 或者

```shell
for file in `ls /etc`
```

只读 `readonly variable_name`
删除变量 `unset variable_name`

```shell
# 执行命令
eval 'll' 

# exec 用当前执行的命令去替换当前的shell进程，清理老进程的环境

# export 当前会话中，设置环境变量，并使其对所有子进程可见
```

字符串操作

- 获取长度 `${#variable_name}`
- 截取长度 `${variable_name:1:4}` 第二个字符开始，截取四个字符；第一个字符的索引是0
- 查找字符串位置 `expr index "$variable_name" io` 查找i或者o的位置，那个先出计算那个

环境变量

```shell
# export 导出为环境变量

# 输出当前使用的shell
echo $SHELL 
echo $0

# BASH SHELL 的全路径
BASH

# cd xxx 会现在当前目录下查找，找不到进入 CDPATH 定义的路径
CDPATH

# 真实的用户ID
UID
# 有效的用户ID,也许变身了
EUID

# 函数体内部记录当前函数体的函数名
FUNCNAME

# 下一条命令在 history 中的编号
HISTCMD
HOSTNAME
HOSTTYPE # 主机架构
MACHTYPE # 主机类型的GUN标识
LANG # 当前系统语言环境
PWD # 当前目录
PATH # 命令的搜素路径

# 给 PATH 添加新的路径
export PATH="$PATH:/home/user/bin"
# ${parameter:+expression} parameter有值不为空使用expression的值
prepend() {
    [-d "$2"] && eval $1=\"$2\$\{$1:+':'\$$1\}\" && export $1
}
prepend PATH /opt/myapp/bin # PATH=/opt/myapp/bin:/user/bin:/bin
```

字符分为两类: 普通纯文本,元字符(Shell 的保留字符, 需要转义 如 $ 获取变量 * 通配符 )

`单引号`中的任何字符都只当是普通的字符(除了单引号自身,转义单引号也不行)

`双引号`中的 $, `, 转义符还是会被解析成其特殊含义,单引号中的所有字符都只是字面意思

```shell
export num=1
echo $num # 1
# or
echo ${num}

Date=`date` # Date=$(date)
echo $Date
```

整数运算: `$[]` 做运算 或 expr 或 $((算术表达式)) 或 let
数学运算高级适用工具: bc

```shell
echo $[1+1] # $[5**2]
expr 2 * 2
i=2
echo $((2*i+1))

let i+=6
echo $i

echo "scale=2;22/7" | bc # 3.14
no=100
echo "obase=10;ibase=2;$no" | bc # 4
echo "sqrt(100)" | bc
echo "10^10" | bc

## 彩色输出
# 0 重置 30 黑 31 红 32 绿 33 黄 34 蓝 35 洋红 36 青 37 白  - 文字
# 0 重置 40 47 白 - 背景
echo -e "\e[1;31m red \e[0m"
```

## 文件描述符

- 0 stdin 标准输入
- 1 stdout 标准输出
- 2 stderr 标准错误

```shell
cmd 2>&1 output.txt # or 简写 &> 将stderr 转成 stdout 然后将两者重定向到同一个文件

# tee 既可以重定向数据到文件,还可以提供一份重定向数据的副本作为管道的后续命令 默认是覆盖 -a 选项追加
# tee 只能从 stdin 中读取
cmd | tee -a FILE1 FILE2 | otherCommand
```

`exec` 创建新的文件描述符

```shell
# 创建一个文件描述符4
exec 4>output.txt
echo newline >&4
cat output.txt # newline 
```

`#!` 不会被理解成注释,因此跟着的部分必须是某个解析器的路径

命令替换: 将命令的标准输出作为值赋给某个变量,一种是反引号,一种是 $()

大括号最常见的用法就是引用变量原型

创建俩文件 `touch file_{A,B}` 创建 file_A, file_B

## 数组

括号表示数组，元素用空格符号隔开`数组名=(值1 值2 ... 值n)`

- 获取元素 `${数组名[下标]}` ${array_name[@]} 获取数组中的所有元素
- 获取数组长度 `${#array_name[*]}` or `${#array_name[@]}`
- 打印数组所有元素 `${array_name[*]}` or `${array_name[@]}`
- 获取索引列表 `${!array_name[*]}` or `${!array_name[@]}`

### 关联数组

当使用字符串(用户名、非顺序数字等)作为索引时，关联数组比数字索引数组更好使用

```shell
# 定义关联数组
declare -A fruits_array
fruits_array=([apple]='100 dollars' [orange]='200 dollars')
# 使用独立索引赋值
fruits_array[apple]='150 dollars'
```

## 别名

```shell
# 新增别名(放入 ~./bashrc 文件永久有效)
alias install='sudo apt-get install'
# 取消别名 unalias 或者 alias install=''
unalias install  
```

## read 命令

```shell
# -n number_of_chars 限制输入的字符
# -s 无回显方式,适合密码设置
# -p 显示提示信息
# -t timeout 限时输入
# -d delimiter_char 指定结束符
# 输入 \ 相当于换行
read -n 5 var
$var
```

## 采集终端信息

处理当前终端的行列号及光标位置等
tput 和 stty 是两款终端处理工具

```shell
# 获取终端的行、列数
tput cols
tput lines
# 加粗
tput bold
# tput dim
# tput smul - start mode underline
# tput rmul - remove mode underline
# tput rev - turn on reverse mode
# tput smso - start mode standout
# tput rmso - remove mode standout
# tput sgr0 - turn off all attributes
# 设置背景颜色 setb 前景色 setf 取值 0-7 Black Red Green Yellow Blue Magenta紫 Cyan青 white
tput setb 0
# 清屏
tput clear
# 保存当前光标位置
tput sc
# 恢复到光标上次保存的位置
tput rc
# 移动到 row col
tput cup 10 13
# 光标可见
tput civis
# 光标不可见
tput cnorm
# 删除从当前光标位置到行尾的所有内容
tput ed

# stty -echo 禁止将密码发送到终端
# stty echo 允许发送输出
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

```shell
#!/bin/bash
echo Count:
tput sc

# 循环 
# { 1...6 } 比 seq 0 40 略微快 
for count in `seq 0 40`
do
    tput rc
    tput ed
    echo -n $count
    sleep 1
done
```

## 调试

```shell
# -x 选项启用 shell 脚本的跟踪调试功能
bash -x script.sh
# or
sh -x script.sh

# set -x 和 set +x 对之间的脚本部分调试
# set -v 命令进行读取的时候显示输入  set +v 禁止显示输入
```

## 注释

- 单行 `#`
- 多行

  ```shell
  :<<EOF
  注释内容...
  注释内容...
  EOF
  ```

## 函数及传参

大多数现在系统中 true 作为 /bin 中的一个二进制文件实现的，这就意味这每次循环执行 true ，shell 就不得不生成一个进程。为了避免这种情况，可以使用 shell 内建的命令 `:` ,该命令退出状态总是 0

- $0 脚本本身
- $# 传递给脚本的参数个数
- $* 单字符显示所有向脚本传递的参数， 返回"1 2 3"
- $$ 当前脚本运行的进程ID
- $! 后台运行的最后一个进程ID
- $@ 单字符显示所有向脚本传递的参数 返回"1" "2" "3"
- $? 脚本或者命令的返回值(正常退出命令和脚本,0作为返回值)

### 导出函数

```shell
function getIP() { /sbin/ifconfig $1 | grep 'inet' }
echo "getIP eth0" > test.sh
export -f getIP # 导出函数
# 这样函数的作用域就可以扩展到子进程中去了
sh test.sh
```

## 运算符

- eq ne gt lt ge le (算术)
- ! -o(或) -a(与)
- || &&
- z 字符串长度是否为0，n相反; $ 监测字符串是否为空
- c(char) f x b(block) e(exist) d(directory) w(write) r L(link) （文件
- = == != > <

## echo

- e 转义输出
- n 不换行输出

## printf

## 进程资源限度

ulimit

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

## 输入/输出重定向

- command > file 将输出重定向到 file。
- command >> file 将输出以追加的方式重定向到 file。
- command < file 将输入重定向到 file。
- n > file 将文件描述符为 n 的文件重定向到 file。
- n >> file 将文件描述符为 n 的文件以追加的方式重定向到 file。
- n >& m 将输出文件 m 和 n 合并。
- n <& m 将输入文件 m 和 n 合并。
- << tag 将开始标记 tag 和结束标记 tag 之间的内容作为输入。

执行某个命令但是不在屏幕上显示输出结果
`command > /dev/null`
Shell 也可以包含外部脚本

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

## xargs

处理针对参数太长的问题

## tr

translate 简写

```shell
echo "HELLO" | tr 'A-Z' 'a-z'
echo "HELLO" | tr '[:upper:]' '[:lower:]'
echo 12345 | tr '0-9' '98765210' # 87654  加密
```

## md5sum sha1sum

文件校验和核实

## sort uniq

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

## 多文件重命名和移动

```shell
#!/bin/bash
count=1;
for img in `find . -iname '*.png' -o -iname '*.jpg' -type f -maxdepth 1`
do
    new=image-$count.${img##*.}
    mv $img $new
    # 执行表达式
    let count++
done
```

```shell
rename *.JPG *jpg
rename 's/ /_/g' *.xml # 文件名中的空格替换成字符 _

# 移入指定目录
find path -type f -name '*.mp3' -exec mv {} target_dir \;
```

## 拼写检查

词典库 `/usr/share/dict`

```shell
# -q 禁止产生任何输出
grep "^$1$" path -q
# 上一条命令执行情况 0 成功
$?

# 默认搜索 /usr/share/dict/word
look word

look hello path
```

## 交互输入自动化

如 ssh

```shell
read -p "name:" name
read -p "age:" age
echo $name $age

# 一次搞定 escapes 会解析转义序列
echo -e "Brain\n18\n" | ./code.sh


# 输入文本
echo -e "Brain\n18\n" > input.data
```

## 并行进程加速命令执行

```shell
#!/bin/bash
array=()
for file in File1.iso
do
    # 命令后端执行并继续执行命令，
    # 最后一个后台进程
    md5sum $file & array+=("$!")
done

# 等待所有进程的结束
wait ${array[@]}
```

## 文件操作

```shell
# 创建一个内容全部为0 1MB 大小的文件
dd if=/dev/zero of=junk.data bs=1M count=1

# comm 必须使用两个排过序的文件做比较
# 交集 差集 
# sort 排序文档
sort a.txt -o a.txt;sort b.txt -o b.txt
comm a.txt b.txt -1-2


# 改变文件权限
chmod u=rwx g=rw o=r filename
chmod u+x filename # 改变当前用户下文件可执行
chmod a-x filename # 所有权限用户删除权限用 -
chmod 777 . -R # 递归方式修改权限
chattr +i file # 文件不可修改 -i 恢复之前状态

# 创建空白文件或修改文件时间戳
touch filename # -a 访问时间 -m 修改时间

# ISO 写入 USB
dd if=image.iso of=/dev/sdb1

# 查找并修复文件差异
diff a.txt b.txt

# 列出目录
ls -d */
ls -F | grep "/$"
ls -l | grep "^d"
find . -type d -maxdepth 1 -print

# 快速定位
pushd /var/www # 多添加几个
dirs # 查看栈内容
pushd +3 # 切换到编号 3 的路径
popd # 删除最近压入栈的路径并切换到写一个目录

# 统计行数 单词 和字符
cat filename | wc -l
cat filename | wc -w
cat filename | wc -c

# 目录树
tree . -P PATTERN # 显示匹配指定模式的文件

# 视频图像处理
ffmpeg
```

## 文本处理

```shell
# grep "pattern" filename1 ...
# grep pattern filename1 ...
# 匹配结果的后的 3 行（B 前）
grep 5 -A 3

# 切分2, 3列文件 d(设置分隔符，默认制表符)
cut -f 2,3 -d " " filename

# 替换文本
sed -i 's/pattern/replace_string/2g' filename # 每行首次匹配的内容 , 2g 第二次出现的替换, i 修改后的数据替换原文件

# awk 高级文本处理
# awk 'BEGIN {} pattern{} END{}' file
var1="name"
seq 5 | awk 'BEGIN { sum = 0; } { print $1,$v1; sun+=$1 }' v1=$var1  # $1 第一个字段（列）的文本内容,NR 当前行号，NF 字段数量
seq 10 | awk 'NR==1,NR==3' file # 打印前三行
ls -l | awk '{print $1 " " $8}' # 打印 1， 8 列

# 按列合并多文件（默认分隔符制表符， 设置为 ，）
paste file1 file2  -d ","

# 逆序形式打印
seq 4 | tac

# 通过正则过滤
egrep -o '[a-zA-Z0-9]+' file

# 替换和生成不同长度字符
echo ${var/string/replaced_string}
echo ${var:start_position:length}
```

## 网页

```shell
# 下载远程文件, HTTP/FTP 提供认证
wget --user name --password pass --cookie "name=name;" --limit-rate 20m URL ...

# 下载及发送各种 HTTP 请求和指定请求头部
# 断点续传 I(head) 打印响应头部信息
curl -C - URL

# 翻译
wget git.io/trans
chmod 755 ./trans
trans en:fr "I love linux"
```

## 压缩解压缩

```shell
# c 创建归档文件 
# f 归档文件名
# v(verbose 更多细节)
# x 归档文件提取到当前目录
# z gzip 压缩 

# 创建 gzip 压缩
tar -czvf archive.tar.gz file1 file2 ...

# 解压 gzip 压缩 -xzvf

# 单个文件压缩
gzip file
gunzip file.gz

# 递归归档 zip 格式
zip -r files.zip folder file1 ...
unzip files.zip

# 定期备份 
# crontab 分钟(0-59) 小时(0-23) 天(1-31) 月份(1-12) 星期(0-6)
# * * * * * script/command

# 每隔一个月，在周末 早上 3 点到 6 点每 5 分钟执行一次
# */5 3-6 * */2 0,6
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

## 监视

```shell
# 磁盘使用情况
# du disk usage
# df disk free
df -h 

# time 测量程序执行的时间
# Real 总时间 User 除去阻塞时间 Sys 内核中的CPU时间
time ls

# who w users uptime last lastb(失败的用户登录信息)

# watch 定时监控
# inotifywait 监视文件并报告何时发生了某种事情

# syslog 记录日志 
logger -t TAG log_message
tail -n 2 /var/log/messages

# 监视磁盘：实时显示系统资源使用情况
top

# 检查磁盘及文件系统错误
fsck

# 显示进程
ps -ax(all) 

# which 在 PATH 中查找可执行的文件的位置
# whereis 返回路径，手册及源码的路径
# file 确认文件类型

# 进程操作 - 信号处理
kill -l # 列出所有的可用信号
kill -9 process_id # 杀死进程

# 终端用户发信息
write username tty1
wall # 广播

# /proc 伪文件(linux 内核的内部数据)

```

## 服务

```shell
# service xxxx start/stop/restart/status
```
