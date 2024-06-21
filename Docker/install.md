# Install

## Centos

```shell
sudo yum update
yum install iproute ftp bind-utils net-tools wget -y

yum install docker -y

# 启动
systemctl start docker.service

# cat /etc/redhat-release
```

## ubuntu

```shell
# 内核信息
uname -a
cat /proc/version

sudo apt-get update
sudo apt-get install -y docker-ce

# cat /etc/issue
```

## busybox 镜像

一个集成了 linux 常用命令的精简工具箱
