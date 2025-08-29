# document

## gitignore

```shell
# .gitignore 规则
# / 开头防止递归， / 结尾指定目录
# glob 模式 ? 只匹配一个字符(除/)， * 匹配零个或多个字符(除/)， ** 匹配任意层级的目录，! 表示不忽略
```

## 命令

```shell
git init --initial-branch=main # default name: master
```

## branch

```shell
# 当前所在的提交对象上创建一个指针(分支名)
git branch testing

# 删除本地分支 dev
git branch -d dev
# 删除远程分支
git push origin --delete branch-name

# 重命名当前分支
git branch -M main
```

## checkout

```shell
# 切换分支 main
git checkout main

# 等效于 git branch dev, git checkout dev
git checkout -b dev
```

## clone

```shell
# git 克隆的服务器的默认名称: origin

# 显示读写远程仓库使用的 Git 保存的简写与其对应的 URL
git remote -v
# git remote show <remote> 查看远程仓库的更多信息
# git remote rename <old> <new>
# git remote remove <name>

# 添加远程仓库 git remote add <originName> <url>
git remote add originName https://github.com/fewbadboy/development.git

# 命令中就可以用 远程名 代替整个 URL
# 从远程仓库下载最新的提交、分支和标签，但它并不会自动合并或更新本地分支
git fetch originName

# 包含了两个操作：git fetch 和 git merge 它从远程仓库下载最新提交，并自动将其合并到当前分支
# --rebase 非默认的合并（merge）方式将远程分支的更新整合到当前分支中
git pull [<远程名>] [<远程分支名>:<本地分支名>]

# 推送到上游 git push <remote> <branch>
# 将本地的 master 分支推送到远程 master 分支，远程不存在时会自动创建
git push origin master

# 将本地的 master 分支推送到远程 main 分支
git push origin master:main
```

## commit

```shell
# -a 跳过 git add 步骤
git commit -a -m "initial project"

# 不修改提交信息的前提下，修改上一次提交的内容
git commit --amend --no-edit
```

## merge

```shell
# 先切换到 master 分支，再合并 dev 分支到 master 分支
git checkout master
git merge dev

# 冲突时（俩分支对同一文件的同一部分做了不同的修改，解决冲突需要合并或者二选一
# <<<<<<<< HEAD:index.html
# xxxxxxxxxx part1 本地分支内容
# =========
# xxxxxxxxxx part2
# >>>>>>>> iss53:index.html 合并进来的
```

## rebase

```shell
# 把 master 分支的更改合并到你当前的 dev 分支
git checkout dev
git rebase master

# rebase 后出现冲突时，解决冲突后执行
git rebase --continue
```
