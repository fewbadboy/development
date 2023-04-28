# Commit 规范

[commitlint](https://github.com/conventional-changelog/commitlint)

## 模板

```docs
type(scope?): subject
body?
footer?
```

基于 [Angular](https://github.com/angular/angular/blob/22b96b9/CONTRIBUTING.md#-commit-message-guidelines)

type:
1. build: Changes that affect the build system or external dependencies
2. ci: Changes to our CI configuration files and scripts
3. docs: Documentation only changes
4. feat:  A new feature
5. fix: A bug fix
6. pref: A code change that improves performance
7. refactor: A code change that neither fixes a bug nor adds a feature
8. style: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
9. test: Adding missing tests or correcting existing tests