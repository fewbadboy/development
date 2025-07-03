# document

## ignore

[gitignore](https://github.com/github/gitignore)

## 命令

```shell
# .gitignore 规则
# / 开头防止递归， / 结尾指定目录
# glob 模式 ? 只匹配一个字符(除/)， * 匹配零个或多个字符(除/)， ** 匹配任意层级的目录，! 表示不忽略

# 查看配置
git config --list

# 设置用户信息
git config --global user.name "Git"
git config --global user.email git@git.com

# 凭据管理策略 cache 保存内存，安全，重启失效；store 明文存磁盘，永久保存
git config --global credential.helper 'cache --timeout=3600' # 缓存一小时

# -b <branch-name>
git init --initial-branch=main # default name: master
```

## branch

```shell
# 当前所在的提交对象上创建一个指针(分支名)
git branch testing

# 删除本地分支 dev
git branch -d dev

# 重命名当前分支
git branch -M main

# 删除远程分支
# 服务器的默认名称: origin
git push origin --delete branch-name
```

## checkout

```shell
# 保持工作目录和暂存区一个干净的状态(都已提交)
# 切换分支 main
git checkout main

# 创建并切换分支 dev
# 等效于 git branch dev, git checkout dev
git checkout -b dev

# 撤销所作的修改(未 add)
git checkout -- a.txt
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

## diff

```shell
# 显示尚未暂存和暂存区域快照之间的差异
git diff

# 已暂存和最后一次提交的文件差异
git diff --staged

# 已提交，未推送 与 远程仓库的文件差异
git diff master origin/master
```

## grep

方便地从提交历史、工作目录、甚至索引中查找一个字符串或者正则表达式。

## log

```shell
# 查看提交历史
git log -p -2 # --patch 最近 2 次提交
git log --stat # 简略统计信息
git log --pretty=formate:"%h  - %an : %s" --graph # --pretty=oneline
# since/after until/before
git log --since=2.weeks --author='any' --committer='some' --grep='匹配的提交说明'
```

| 选项 | 说明                                           |
| :--: | :--------------------------------------------- |
|  %H  | 提交的完整哈希值                               |
|  %h  | 提交的简写哈希值                               |
|  %T  | 树的完整哈希值                                 |
|  %t  | 树的简写哈希值                                 |
|  %P  | 父提交的完整哈希值                             |
|  %p  | 父提交的简写哈希值                             |
| %an  | 作者名字                                       |
| %ae  | 作者的电子邮件地址                             |
| %ad  | 作者修订日期（可以用 --date=选项 来定制格式）  |
| %ar  | 作者修订日期，按多久以前的方式显示             |
| %cn  | 提交者的名字(项目核心人员，将补丁并于项目的人) |
| %ce  | 提交者的电子邮件地址                           |
| %cd  | 提交日期                                       |
| %cr  | 提交日期（距今多长时间）                       |
|  %s  | 提交说明                                       |

## merge

```shell
# 先切换到 master 分支，再合并 dev 分支到 master 分支
git checkout master
git merge dev
# 删除本地 dev 分支
git branch -d dev
# 删除远程分支
git push origin -d brach-name

# 冲突时（俩分支对同一文件的同一部分做了不同的修改，解决冲突需要合并或者二选一
# <<<<<<<< HEAD:index.html
# xxxxxxxxxx part1 本地分支内容
# =========
# xxxxxxxxxx part2
# >>>>>>>> iss53:index.html 合并进来的
```

## rebase

将提交到某一分支上(dev)的所有修改都移至另一分支上(master)

```shell
git checkout dev
git rebase master

# 快进合并 master 分支，使之包含来自 dev 分支的修改
# git rebase <basebranch> <topicbranch>
git rebase master dev

# rebase 后出现冲突时，解决冲突后执行
git rebase --continue

```

## reset

取消暂存的文件

```shell
# --soft 仅仅恢复头指针，其他的不变
git reset --soft

# --mixed 默认 重置暂存区的文件与上一次的提交(commit)保持一致，工作区文件内容保持不变
git reset --mixed

# --hard 一切全部恢复（重置了工作目录和暂存区）
# 每次提交包含一个指向上次提交对象(父对象)的指针
# HEAD 上一次提交的快照，下一次提交的父结点
git reset --hard HEAD
```

## restore

```shell
# 取消暂存的文件 a.txt
git restore --staged a.txt
```

## tag

给某个提交打"别名"
