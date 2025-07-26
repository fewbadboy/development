# Started

```shell
# 基于当前目录下 Dockerfile创建镜像
docker build -t imageName:tag .
# 容器无论什么原因退出立刻重启
# run === create -> start
# -p hostPort:containerPort (可使用多次)
docker run -d -p 5000:5000 --restart=always --name containerName imageName cmd

# docker 容器启动个的 COMMAND
docker container list -a
docker restart container_id
docker exec -it container_id COMMAND
```

## logs

```shell
# -f 跟踪实时日志
# -t 日志中显示时间戳
# --since 10m (过去 10 分钟)
# --until
docker logs container_id
```

## 修改时区

```shell
echo Asia/ShangHai > /etc/timezone
```
