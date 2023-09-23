# document

```shell
# Run packages without downloading using npx
npx 

# Random errors
npm cache clean --force

npm install --verbose # if having trouble with npm install, to see more details

# For scoped modules
npm init --scope=@scope-name

# Publish scoped public packages
npm publish --access public

# with dist tag
npm publish --tag tag-name

# Granting access to private organization
# have two-factor authentication enabled, 123456 is the code from your authenticator application
npm owner add user-name package-name --otp=123456

# Updating version number
npm version <update_type> 
npm publish

# ^ 符号：表示在升级时只匹配主版本号不变的最新版本
# ~ 符号：表示在升级时只匹配主版本号和次版本号都不变的最新版本
npm update --latest

# 查看 connect 版本记录
# contributors / dependencies / 
npm view connect versions
```

[npm CLI](https://docs.npmjs.com/cli/v9/commands/npm)
