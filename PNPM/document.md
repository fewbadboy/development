# pnpm

## CLI

```shell
# install / update
npm install -g pnpm

# 从缓存中安装 package
# 为 project 安装所有的依赖
pnpm i --offline

# 更新 package.json -L(--latest)
pnpm up --latest

# 添加/删除 package
pnpm add/remove pkg

# 审计: 查看已经安装包的已知安全问题
pnpm audit 

# 检查过期 package
pnpm outdated

# run-script 的别名
pnpm run

# 项目范围执行 shell 命令
pnpm exec

# pnpx 别名，类似 npx
pnpm dlx

# 从 create-* / @foo/create-* 工具包创建项目
pnpm create

# 部署
pnpm deploy 

# 发布
pnpm publish --access public
```
