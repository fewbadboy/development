# performance

## 更换小版本镜像(alpine)

```dockerfile
FROM node:18-alpine3.14
```

## 多阶段构建

docker 分层存储，Dockerfile 里每一行指令是一层，会做缓存
每次 docker build 的时候只会从变化的层开始重新构建，没变的层直接复用

最终构建出来的镜像没有源代码，这样镜像就小很多

`&&` 合并多个层，减少冗余

```dockerfile
FROM node:18-alpine3.14 as build-stage
ENV NODE_ENV=production
WORKDIR /app
COPY package* yarn.lock .
RUN npm config set registry https://registry.npm.taobao.org/ && npm install
COPY . .
RUN npm run build

# production state
FROM node:18-alpine3.14 as production-stage
ENV NODE_ENV=production
COPY --from=build-stage /app/dist /app
COPY --from=build-stage /app/package.json /app/package.json
WORKDIR /app
# 安装启动服务依赖的相关资源
RUN npm config set registry https://registry.npm.taobao.org/ && npm install --production
EXPOSE 3000
CMD ["node", "/app/main.js"]
```

```dockerfile
FROM nginx:alpine
RUN mkdir -p /app/web
COPY --from=0 /app/web /app/web
COPY nginx.conf /etc/nginx/nginx.conf
```

```js
// node build/index.js --preview
const { run } = require('runjs')
const chalk = require('chalk')
const config = require('../vue.config.js')
const rawArgv = process.argv.slice(2)
const args = rawArgv.join(' ')

if (process.env.npm_config_preview || rawArgv.includes('--preview')) {
  const report = rawArgv.includes('--report')

  run(`vue-cli-service build ${args}`)

  const port = 9526
  const publicPath = config.publicPath

  var connect = require('connect')
  var serveStatic = require('serve-static')
  const app = connect()

  app.use(
    publicPath,
    serveStatic('./web', {
      index: ['index.html', '/']
    })
  )

  app.listen(port, function () {
    console.log(chalk.green(`> Preview at  http://localhost:${port}${publicPath}`))
    if (report) {
      console.log(chalk.green(`> Report at  http://localhost:${port}${publicPath}report.html`))
    }

  })
} else {
  run(`vue-cli-service build ${args}`)
}

```

## ARG 增加构建灵活性

```dockerfile
FROM node:18-alpine3.14
ARG aaa
ARG bbb

WORKDIR /app

COPY ./test.js .

ENV aaa=${aaa} \
  bbb=${bbb}
CMD ["node", "/app/test.js"]
```

```shell
docker build --build-arg aaa=1 --build-arg bbb=2  -t arg-test -f 333.Dockerfile .
```

## CMD 结合 ENTRYPOINT

CMD 启动命令是可以重写的，ENTRYPOINT 就不会(依然执行 dockerfile 中 ENTRYPOINT 的命令)

```dockerfile
FROM node:18-alpine3.14
ENTRYPOINT ["echo", "one"]
CMD ["two"]
```

```shell
docker run cmd-test three # one three
```

## COPY vs ADD

都是把宿主机的文件复制到容器，但是对 tar.gz 这种文件处理上

ADD 解压缩然后复制到容器

COPY 是完整的复制到容器里

```shell

```

## 防止容器启动后退出

`tail -f /dev/null`

这样就可以 `docker exec <container>` `docker attach <container>`

```shell
docker run -d ubuntu bash -c "shuf -i 1-10000 -n 1 -o /data.txt && tail -f /dev/null"

docker ps -a

docker exec <container-id> cat /data.txt
```
