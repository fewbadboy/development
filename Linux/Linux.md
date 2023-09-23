# 介绍

发行家族(主要版本):

- Debian(Ubuntu， Linux Mint)
- Fedora(RHEL, CentOS, Oracle Linux)
- SUSE(SLES, openSUSE)
- ...

## Linux 关机

不管是重启系统还是关闭系统，首先要运行 sync 命令，把内存中的数据写到磁盘中

## 目录结构

- bin 存放最经常使用的命令
- boot 启动Linux时的核心文件
- dev Linux外部设备，存放方式和访问文件的方式是一样的
- etc  系统管理所需要的配置文件和子目录
- home 用户的主目录
- root 系统管理员的用户目录
- run 临时文件系统，存储系统启动以来的信息
- sbin 系统管理员使用的系统文件程序
- tmp 临时文件
- usr unix shared resources（共享资源）
- var 放着在不断扩充着的东西，我们习惯将那些经常被修改的目录放在这个目录下(eg. log)
- opt 给主机额外安装软件的目录
- proc (不在硬盘而是在内存，存储当前内核运行状态的一系列特殊文件，目录是个虚拟的)

## 重置root密码

进入单用户模式，修改密码

- 重启
- 进入menu
- 输入 e
- 第二行 kernel开头， 输入e 在右边输入 空格 single,然后回车
- 按 b 启动
- 更改密码的命令为 passwd

## 文件属性

- chown [-R] 属主名：属组名 文件名 change ownerp 修改所属用户和组 -R 递归操作
- chmod change mode 修改用户权限
- chgrp [-R] 属组名 文件名 change group 修改分组
- file 查看文件类型
ls -l 显示文件所属用户和组的属性
d：目录  -：文件  l：连接文档  b：装置文件里面的可供储存的接口设备 c：装置文件里面的串行端口设备，例如键盘、鼠标(一次性读取装置)
rwx 读4、写2、可执行1 三个一组，没响应的权限会出现-
|文件属性|属主权限|属组权限|其他用户权限|

## 文件和目录管理

常用命令,man 命令 来查看详细的内容

- ls list files
- cd change directory
- pwd print word directory
- mkdir -p 多级目录 make directory
- rmdir remove directory
- cp copy file
- rm [-fir] 文件或目录 remove
- mv move file
- touch 修改文件创建事件属性,文件不存在就新创建一个
- find 查找文件
- which/whereis 查找执行文件
- type 确定时都是内建命令

`cd ~` 回到自己用户目录即 /home/xxxx

查看文件内容

- cat [-AbEnTv] 从一行开始显示
- tac 从最后一行开始显示
- nl [-bnw] 显示的时候显示行号
- more 一页一页的显示，space 下一页，Enter 下翻一行，/字符 往下搜索'字符',:f 立刻显示当前行数，q 离开，b 往回翻页
- less 比more更好可以往前翻页，space/pagedown 向下翻页，/字符 向下搜索，?字符 向上搜索， n 重复前一个搜索， N 反向重复， q 离开
- head [-n number] 取出文件前几行
- tail [-n number] 取出文件后几行 -f 动态的查看文件

## 用户和分组管理

- useradd [-cdgGsu] `useradd –d  /home/sam -m sam` 为登录名sam产生一个主目录 /home/sam（/home为默认的用户主目录所在的父目录）
- userdel
- usermod
- passwd [-dflu] l 禁用，u 口令解锁， d 使账号无口令， -f 强迫下次登录修改口令
- groupadd
- groupdel
- groupmod
- newgrp
- users/who/w
- finger
- su - xxx 切换用户
- sudo 使用其他用户分身份执行,非切换了用户

和用户账号相关的系统文件`/etc/passwd`

## 压缩/打包

- gzip/gunzip
- tar -zcvf zip.tgz /file 解压 -zxvf
- bzip2
- cpio

## 磁盘管理

- df [-ahikHTm] [目录或文件名] 列出文件系统的整体磁盘使用量，k/m KBytes/MBytes 的容量显示各文件系统, h 易读的方式
- du [-ahskm] 文件或目录名称 检查磁盘空间使用量
- fdisk [-l] 装置名称 用于磁盘分区
- mkfs [-t 文件系统格式] 装置文件名 磁盘格式化
- mount [-t 文件系统] [-L Label名] [-o 额外选项] [-n]  装置文件名  挂载点  磁盘挂载
- umount [-fn] 装置文件名或挂载点

`df -h /etc` 具体目录下磁盘的容量

## 字符处理

- 管道

```shell
echo 'test string 1' >> linux.txt
echo -e '\ntest string 2' >> linux.txt
echo -e '\ntest string 3' >> linux.txt
# 显示最后一行
cat linux.txt | tail -1 
# 搜索文本
cat linux.txt | grep -ic 'TEST' # 忽略大小写 统计包含的行数
# sort 排序 -n 数字排序 -t 指定分隔符 -k 指定第几列
cat linux.txt | sort -r # 反向排序
# uniq 去重

# cut 截取文本
# cut -c 2-5  打印 2-5 列的字符
# cut -f 2 -d ':'  冒号分割结果的结果 fields(f) 中的第二列

# tr 文本转换或删除
# paste 文本的合并
# split 分割大文件(行、二进制文件按大小)
```

## 网络管理

- 网卡配置

```shell
# ifconfig  检查和配置网卡
# MTU 网卡一次传输的最大分包

# 手动设置网卡 eth0 的 IP 地址
ifconfig eth0 192.168.18.19 netmask 255.255.255.0 # 简写 ifconfig eth0 192.168.18.19/24
# 手动启用/断开网卡(不适用于远程)
ifconfig eth0 up/down # 等同 ifup/ifdown eth0

# 推荐重启网络服务
service network restart

# RedHat 网络配置文件路径 eth0 网卡的配置 /etc/sysconfig/network-scripts/ifcfg-eth0 
# dhcp(Dynamic Host Configuration Protocol)/static/none

# /etc/sysconfig/network-scripts/ifcfg-eth1  文件配置信息
#  DEVICE=eth1
#  TYPE=Ethernet
#  ONBOOT=yes # 开机启动
#  NM_CONTROLLED=yes
#  BOOTPROTO=static
#  IPADDR=172.26.2.30
#  NETMASK=255.255.255.0
#  GATEWAY=172.26.2.254
#  DNS1=202.96.128.86
#  IPV6INIT=no
#  USERCTL=no

```

- 路由和网关设置
两台主机在同一网段内都是激活的，则两台主机具有直接通信的能力(通过交换机或者简易HUB)
两台主机处于不同的网段只能通过路由器才能通信

```shell
# 添加/删除网关(系统重启就不存在了， 需要去网卡配置文件中设置 gateway)
route add/del default gw 192.168.159.2 
# 当前系统的路由表
route -n
```

- DNS 全互联网上的主机名及其 IP 地址对应关系的数据库

```shell
# /etc/hosts 加快域名解析

# /etc/resolv.conf  设置DNS服务器的IP地址及DNS域名
```

- 网络测试工具

```shell
#  ping  -c 次数 -i 间隔 -w 超时退出

# host 查询 DNS 记录

# traceroute 
```

## 进程管理

```shell
# 查看进程
ps
top

# pidof dhcp 快捷查找进程 PID

# 终止进程
kill -l # 查看 kill 后可以跟的信号代码
killall

# yum install lsof -y 
# lsof 可查看现在系统打开的文件有哪些

# 进程优先级调整(-20-19,值越低优先级越高)
# nice renice
# 已启动的进程 renice 修改(以下是修改进程 5555 优先级为 -10)
renice -10 -p 5555
```

## 安装软件

```shell
# 系统变量
echo $PATH
# 追加环境变量中的目录 /root
export PATH=$PATH:/root # 暂时生效

# 当前用户生效
vim ~/.bash_profile # 在PATH 后用 : 分割,追加文件目录
# source 同事返回脚本最后一个命令的返回状态
source ~/.bash_profile # 使修改的配置生效


# 所有用户生效
vim /etc/profile
./profile # 使其生效

# Linux 源码一般在 /usr/local/src 目录下 wget https:xxx

# rpm 包管理器安装
rpm -ivh PACKAGE_NAME-VERSION.rpm

# yum 安装,可以自行解决依赖关系
```

## 正则表达式

```shell
## * 匹配前面的字符任意次
grep 'ab*' demo.txt

## \{n,m\}、 \{n,\} 
grep 'a\{1,4\}b' demo.txt

## ^ 开头 & 结尾 [] 方括号内出现的任意字符

## \d 等价 [0-9]
grep -P '\d' name.txt
## \b 单词边界
## \w 等价 [0-9a-zA-Z]

## ? + | () 
## {} 列出括号内逗号隔开的字符
```

## 文本处理工具 sed (stream editor)

默认情况下不会改变源文件，只是对进过 sed 命令的流进行修改，将结果输出到控制台

以行为单位的文本处理工具

```shell
## 替换文本
sed -e 's/line/LINE/g; s/this/That/g' name.txt
## 删除指定的行, 输出到一个新文件
sed '2d' name.txt > new.txt
sed '1,3d' name.txt # 删除指定范围
sed '3!d' name.txt  # 保留指定行
sed '/Empty/d' name.txt # 删除包含Empty的行
sed '/^$/d' name.txt # 删除空行

# 字符替换
sed 'y/OLD/NEW/' name.txt
sed 's/love/**&**/g' name.txt # & 保存搜索字符替换其他字符 love 替换成 **love**
sed 's/\(love\)able/1rs/' name.txt # \(\)保存匹配的结果  结果 lovers

# 插入文本 i 匹配行前插入， a 匹配行后插入
sed '2 i Insert' name.txt
sed '/Second/i\Insert' name.txt # 匹配字符的上一行插入

# 读入文本 r
sed '/^&/r person.txt' name.txt # person.txt 放在空行后面

# 打印行
sed -n '2,4p' name.txt

# 写文件
sed -n '2,4w output.txt' name.txt

# sed.rules 文件内容 s/this/that/g /^$/d
sed -f sed.rules name.txt

# 替换匹配行的下一行的内容
sed '/^$/{n;s/line/LINE/g}' name.txt
```

## 文本处理工具 awk

[awk-command](https://likegeeks.com/awk-command/)
基于列的文本处理工具
每个非空格的部分叫域，从左到右分别 $1,...表示 $0 表示全部的域

```shell
akw 'print $1' column.txt
# 指定域分隔符 -F
akw -F . 'print $1' column.txt

# 内部变量 NF 获取每行中域的个数
akw 'print $(NF - 1)' column.txt

# 截取字符串 substr
akw 'print substr($1,0,4)' column.txt

# 确定长度 length 当前行总长度
akw 'print length($1)' column.txt

# 求和
# cal.awk
awk -f cal.awk column.txt
```

```shell
# cal.awk
# 运行前
BEGIN {
  total = 0
}
# 运行中
{
  total += $3
}
# 运行后
END {
  print total
}
```

## vim

![vim键盘图](./vi-vim-cheat-sheet-sch.gif)

- 命令模式
- 输入模式

Esc 进入命令模式 
i 编辑模式 

yy 复制当前行 nyy 复制n行
p 粘贴
dd 删除当前行 ndd 删除n行
u 撤销最近一次操作
:n 移动到n行
n 回车 向下移动n行
/xx 搜索 xx 在光标位置向下查找
?xx 在光标位置向上查找
:n1,$s/word1/word2/g n1-z最后一行之间的word1 替换成 word2

## yum/apt RPM包管理

yum: 红帽系列

apt: Debian系列

## at 单一时刻执行一次任务

```shell
# 30 分钟后安排一个任务
at now + 30 minutes
at> /sbin/shutdown -h now
at> <EOF>
```

## 周期性执行任务 cron

linux 和 quartz 框架的表达式不一样
linux cron:
[linux_core](./linux_cron.png)
quartz cron:
[quartz_cron](./quartz_cron.png)

```shell
# 启动 cron 服务
service crond start
# 查看 cron 状态
service crond status

# 用户可以通关 crontab 设置自己的计划任务
# * * * * * [*] command 
# 分钟(0-59) 小时(0-23) 日期(1-31,?) 月份(1-12) 星期(0-6,?) 执行的命令
# 每分钟可以使用 * 和 */1 表示

# 从 20s 开始每隔 30s 执行一次（每分钟重新从20s开始）
20/30 * * * *

# 从23点到3点 每小时重启 httpd 进程
* 23-3/1 * * * ? service httpd restart

crontab -e # 编辑定义自己的任务
crontab -1 # 查看设置的任务
crontab -r # 删除所以的任务
```

## 任务前后台切换

`&` 任务放入后台运行

```shell
# ctrl z 暂停前台任务，然后 jobs 查看任务编号
# 查看
jobs 
# bg 把任务1放到后台继续执行
bg 1
# fg 吧后台任务调到前台来执行
fg 1
```
