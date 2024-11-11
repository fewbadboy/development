# Started

```shell
# 基于当前目录下 Dockerfile创建镜像
docker build -t imageName:tag .
# 容器无论什么原因退出立刻重启
# pause / unpause 文件系统打快照
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

## 生成新镜像

```shell
# 进入容器添加相关服务，生成新的镜像
docker commit [options] container_id my_new_image:tag
```

Dockerfile 构建镜像底层也是 docker commit 一层一层构建

## 修改时区

```shell
echo Asia/ShangHai > /etc/timezone
```

## Docker Compose

1.use docker compose

share multi-container application

```yml
# docker-compose.yml file
services:
  app:
    image: node:18-alpine
    command: sh -c "yarn install && yarn run dev"
    ports:
      - 3000:3000
    working_dir: /app
    volumes:
      - ./:/app
    environment:
      MYSQL_HOST: mysql
      MYSQL_USER: root
      MYSQL_PASSWORD: secret
      MYSQL_DB: todos
  mysql:
    image: mysql:8.0
    volumes:
      - todo-mysql-data:/var/lib/mysql
    environment:
      MYSQL_ROOT_PASSWORD: secret
      MYSQL_DATABASE: todos
  mongo:
    image: mongo:4.2.8
    volumes:
      - mongodb:/data/db
      - mongodb_config:/data/configdb

volumes:
  todo-mysql-data:
  mongodb:
  mongodb_config:
```

```shell
# see version 
docker compose version

# at the root, create docker-compose.yml file

# start up application stack
docker compose up -d

# give you live output as it’s generated
# to see specific service, add the service name to the end of the logs command
# eg. docker compose logs -f app 
docker compose logs -f

# tear it all down
docker compose down
```

2.multi-stage builds

```shell
# Dockerfile
# CMD 启动容器时默认执行的命令
FROM node:20.11.0-alpine
ENV NODE_ENV=production
WORKDIR /app
COPY ./ /app
RUN npm install --registry=https://registry.npmmirror.com/ && npm run build

FROM nginx
RUN mkdir -p /app/web
COPY --from=0 /app/web /app/web
COPY nginx.conf /etc/nginx/nginx.conf
```

or

```shell
FROM node:14.18.0 AS build
WORKDIR /app
COPY ./ /app
RUN npm install --registry=https://registry.npmmirror.com/ && npm run build

FROM nginx
RUN mkdir -p /app/web
COPY --from=build /app/web /app/web
COPY nginx.conf /etc/nginx/nginx.conf
```
