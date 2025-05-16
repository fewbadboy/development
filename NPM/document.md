# document

```shell
# 淘宝镜像
npm install --registry=https://registry.npmmirror.com
```

## semantic versioning

|Stage|Code states|Example version|
|:----:|:----:|:----:|
|Major release|Changes that break backward compatibility|2.0.0|
|Minor release|new features|1.1.0|
|Patch release|bug fixes|1.0.1|

continuous integration (CI) / deployment (CD)

## commands

```shell

npm set init-author-email "example-user@example.com"
npm set init-author-name "example_user"
npm set init-license "MIT"
```

```shell
# 私有包变公共包
npm access public package-name
# 公共包变私有包
npm access restricted package-name

# Random errors
npm cache clean --force

# 
npm install --verbose # npm install 报错可以查看更多详情

# 管理配置文件
npm config set/get/delete/list/edit/fix

# 废弃
npm deprecate package-name message

# git diff 相似
npm diff --dif=a@1.1.0 --dif=a@1.1.1

# 运行本地或远程 npm 包的命令
npm exec [option] [--] <command>[@version] [args...]

# scoped modules
npm init xxx # npm exec create-xxx

# git remote add origin ...
# scope-name 不同于 npm 用户名时，可以创建 scope-name 的组织，再发布（先 npm login）
# To publish scoped public packages
npm publish --access public

# 避免 dist-tags 以数字或者字母 v 开头
# npm dist-tag add <package-name>@<version> [<tag>]
npm publish --tag tag-name

# 自动增加 package.json 中 patch 部分
# 自定更新 patch 部分为 2.0.1(基于上一个版本)
npm version patch -m '修复部分bug' # major.minor.patch

# 更新版本号
npm version 2.0.0
npm publish # 默认发布 private

# 查看需要更新的包
npm outdated --depth=0

# ^ 符号：表示在升级时只匹配主版本号不变的最新版本
# ~ 符号：表示在升级时只匹配主版本号和次版本号都不变的最新版本
npm update --latest

# 添加删除 username 开发者
npm owner add/rm username package-name

# 查看 package 版本记录
# contributors / dependencies / access
npm view package-name versions/dependencies/access

# 更新最新版本 重写 package.json
npx npm-check-updates -u
```
