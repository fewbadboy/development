# 介绍

发行家族(主要版本):

- Debian(Ubuntu， Linux Mint)
- Fedora(RHEL, CentOS, Oracle Linux)
- SUSE(SLES, openSUSE)
- ...

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
- lib C 编译器的库

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

## 周期性执行任务 cron

linux 和 quartz 框架的表达式不一样
linux cron:
[linux_core](./linux_cron.png)
quartz cron:
[quartz_cron](./quartz_cron.png)
