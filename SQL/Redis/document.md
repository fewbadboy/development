# Redis

## Centos 安装

```shell
# 更新 yum
yum -y update

yum install redis

# 启动
systemctl start redis

# status stop restart

# 进入客户端 exit 退出
redis-cli

# 开启远程连接
vim /etc/redis/redis.conf
# bind 改为 0.0,0.0
# requirepass 密码
systemctl restart redis
```

## Ubuntu 安装

```shell
# lsb_release -a
# apt

# running a very minimal distribution,  need to install
# sudo apt install lsb-release curl gpg
curl -fsSL https://packages.redis.io/gpg | sudo gpg --dearmor -o /usr/share/keyrings/redis-archive-keyring.gpg

echo "deb [signed-by=/usr/share/keyrings/redis-archive-keyring.gpg] https://packages.redis.io/deb $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/redis.list

sudo apt-get update
sudo apt-get install redis
# sudo systemctl status redis-server

# redis-server -v

redis-cli
```

## 命令

```shell
# 配置密码
# /etc/redis.config
bind 0.0.0.0
requirepass your_strong_password_here

# 开机自启动
sudo systemctl enable redis-service

# 启动 Redis 服务
sudo systemctl start redis-service

# 远程执行
redis-cli -h host -p port -a password

# 验证密码
auth 123456

# 退出
quit

# 验证服务是否启动
ping

# 切换数据库(默认16个编号0-15)
# 默认数据库 0
select 0

# 移动数据 data 到数据库 1
move data 1

# 匹配和给定模式匹配的所有键
keys *token*

exists key # 0 不存在， 1 存在

# 设置键的到期时间（毫秒）
psetex key 30000 value

expire key 20 # 秒

# 移除过期的键
persist key

# 获取剩余的到期毫秒
pttl key

# 获取键到期的剩余时间
ttl

# 获取类型
# string, list(l...), hash(h...), set(s...), sorted set(z...)
type key 
```
