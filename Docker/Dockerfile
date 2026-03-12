# -------- Stage 1: Build Vue Frontend --------
FROM node:24.14.0-alpine AS ui-builder

RUN corepack enable

WORKDIR /frontend

# 1. 只复制依赖描述
COPY package.json pnpm-lock.json ./
# 如果你修补了任何包，在运行 pnpm fetch 前包含补丁
COPY patches patches

# 2. 下载依赖 pnpm store（.pnpm-store）（可缓存）
RUN pnpm fetch --prod

# 3. 再复制源码
COPY . .

# 4. 离线安装（秒级
RUN pnpm install --prod --offline

RUN pnpm build

# -------- Stage 2: nginx Production --------
FROM nginx:1.25.2-alpine

# 添加 Maintainer 信息
LABEL maintainer="fewbadboy"

# 删除默认页面
RUN rm -rf /usr/share/nginx/html/*

COPY --from=ui-builder /frontend/dist/ /usr/share/nginx/html/

# 复制 nginx SPA 配置
COPY nginx.conf  /etc/nginx/conf.d/default.conf

EXPOSE 80