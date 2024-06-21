# document

## module

```shell
npm set init-author-email "example-user@example.com"
npm set init-author-name "example_user"
npm set init-license "MIT"
```

## semantic versioning

|Stage|Code states|Example version|
|:----:|:----:|:----:|
|Major release|Changes that break backward compatibility|2.0.0|
|Patch release|bug fixes|1.0.1|
|Minor release|new features|1.1.0|

continuous integration (CI) / deployment (CD)

```shell
# Run packages without downloading using npx
# npm >= 5.2
npx 

# Random errors
npm cache clean --force

npm install --verbose # if having trouble with npm install, to see more details

# For scoped modules
npm init --scope=@scope-name

# git remote add origin ...
# scope-name 不同于 npm 用户名时，可以创建 scope-name 的组织，再发布（先 npm login）
# To publish scoped public packages
npm publish --access public

# with dist tag (Distribution tags)
# avoiding dist-tags that start with a number or the letter "v"
# npm dist-tag add <package-name>@<version> [<tag>]
npm publish --tag tag-name

# deprecate
npm deprecate package-name message

# Granting access to private organization
# have two-factor authentication enabled, 123456 is the code from your authenticator application
npm owner add/rm user-name package-name --otp=123456

# 自动增加 package.json 中 patch 部分
npm version patch -m '修复部分bug' # major minor

# Updating version number
npm version 2.0.0
npm publish

# --save-prod --save-dev(-D)

# ^ 符号：表示在升级时只匹配主版本号不变的最新版本
# ~ 符号：表示在升级时只匹配主版本号和次版本号都不变的最新版本
npm update --latest

# private public access
# private to public
npm access public package-name
# conversely
npm access restricted package-name
# npm access grant/revoke


# 查看 connect 版本记录
# contributors / dependencies / 
npm view connect versions/dependencies

# outdated
npm outdated
```

[npm CLI](https://docs.npmjs.com/cli/v9/commands/npm)
