# Docker 示例

[The official Node.js docker](https://github.com/nodejs/docker-node#readme)

## Docker 构建 & 运行

```shell
# 构建镜像
docker build -t vue-vite-prod .

# 运行容器
docker run -d -p 80:80 vue-vite-prod
```
