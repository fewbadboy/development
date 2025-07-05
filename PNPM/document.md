# pnpm

[Docker 容器中安装](https://pnpm.io/zh/installation#%E5%9C%A8-docker-%E5%AE%B9%E5%99%A8%E4%B8%AD)

## CLI

```shell
# 从缓存中安装 package
# 为 project 安装所有的依赖
pnpm install

# 更新 package.json 及 pnpm-lock.yaml
pnpm update --latest

# 审计: 查看已经安装包的已知安全问题
pnpm audit --fix # 强制修复到不易受攻击的版本

# 检查过期 package
pnpm outdated

# run-script 的别名
pnpm run

pnpm dlx # 别名 pnpx

# 更新 pnpm
pnpm self-update

# 部署
pnpm deploy

# 发布
pnpm publish --access public
```

## Docker

[使用 Docker](https://pnpm.io/zh/docker)
