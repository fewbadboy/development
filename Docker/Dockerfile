FROM node-alpine:18

ENV APP_HOME=/badboy/app

RUN mkdir -p /badboy/app

WORKDIR ${APP_HOME}

COPY package*.json ${APP_HOME}/

# 运行指定命令
RUN --mount=type=secret,id=npmrc,target=/root/.npmrc npm install

COPY . ${APP_HOME}

# 启动容器时默认执行的命令
CMD ["node", "src/index.js"]
