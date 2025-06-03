# Commit 规范

[Git commit rules](https://www.conventionalcommits.org)
[CommitLint](https://github.com/conventional-changelog/commitlint)

## Commit 消息指导

```docs
type(scope?): subject

body?

footer?
```

基于 [Angular](https://github.com/angular/angular/blob/22b96b9/CONTRIBUTING.md#-commit-message-guidelines)

type:

1. build: 影响 build 或外部依赖项的更改
2. ci: CI 配置文件和脚本的更改
3. docs: 仅修改文档
4. feat:  添加新功能
5. fix: 修复 bug
6. pref: 提升性能
7. refactor: 重构(既不是修复 bug 也不是添加新的功能)
8. style: 不影响代码含义的更改(空格, 格式, 缺少分号等)
9. test: 添加或纠正测试代码
10. revert: body 部分应该是 `回复提交到 <hash>`
