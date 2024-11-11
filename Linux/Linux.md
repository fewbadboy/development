# 介绍

发行家族(主要版本):

- Debian(Ubuntu， Linux Mint)
- Fedora(RHEL, CentOS, Oracle Linux)
- SUSE(SLES, openSUSE)
- ...

## 设置时区

```shell
timedatectl list-timezones
sudo timedatectl set-timezone Asia/Shanghai # UTC
```

## 目录结构

- bin  存放基本命令的二进制可执行文件
- boot 启动加载器和内核相关文件
- dev  Linux外部设备。用于访问硬件设备，如硬盘、打印机等
- etc  系统配置文件目录。网络配置，用户账户等
- home 每个用户的主目录，用户个人文件和配置文件均在这
- root 管理员的用户目录
- run  临时文件系统，存储系统启动以来的信息
- sbin 系统管理员使用的基本系统二进制文件
- tmp  临时文件，系统重启会清空
- usr  主要系统默认的用户级软件和共享数据，由操作系统的包管理器管理和维护
- opt  安装附加的、独立的第三方软件包，不由系统包管理器管理
- var  放着在不断扩充着的东西，将那些经常被修改的目录放在这个目录下(eg. log)
- proc 虚拟文件系统，提供系统进程和内核信息的接口
- lib  系统的共享库文件和内核模块
- mnt  挂载临时文件

## 字符处理

- 管道

```shell
echo 'test string 1' >> linux.txt
echo -e '\ntest string 2' >> linux.txt
cat linux.txt | tail -1 
# 搜索文本
cat linux.txt | grep -ic 'TEST' # 忽略大小写 统计包含的行数
# sort 排序 -n 数字排序 -t 指定分隔符 -k 指定第几列
cat linux.txt | sort -r # 反向排序
# uniq 去重

# cut 截取文本
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
```

## 文本处理工具 awk

[awk-command](https://likegeeks.com/awk-command/)
基于列的文本处理工具
每个非空格的部分叫域，从左到右分别 $1,...表示 $0 表示全部的域

## 周期性执行任务 cron

linux 和 quartz 框架的表达式不一样
linux cron:
[linux_core](./linux_cron.png)
quartz cron:
[quartz_cron](./quartz_cron.png)
