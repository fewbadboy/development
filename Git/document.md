# document

## ignore

[gitignore](https://github.com/github/gitignore)

## 安装及配置

```shell
git config --global user.name "Git"
git config --global user.email git@git.com

# 针对特定项目使用不同的用户名称和邮件地址时，运行没 --global 选项的配置即可

# git help <verb>

git init
# Untracked -> Index Added
git add *.js

# -a 自动把所有已经跟踪过的文件暂存起来一并提交
git commit -a -m "initial project"

# 文件的状态
git status

# 移除文件,之前已经修改过或者放入暂存区的文件
# 不添加入快照的文件删除无法恢复
git rm -f log/\*.log

# 移动文件
git mv file_from file_to
```

## branch

```shell
# HEAD 指向当前所在的分支

# 创建一个可以移动的新的指针
git branch testing

# 删除本地分支 dev
git branch -d dev

# 重命名当前分支
git branch -M main

# 删除远程分支
# 服务器的默认名称: origin
git push origin --delete branch-name

# 显示远程分支
git branch -r
```

## checkout

```shell
# 保持工作目录和暂存区一个干净的状态(都已提交)
# 切换分支 main
git checkout main

# 创建并切换分支 dev
# 等效于 git branch dev, git checkout dev
git checkout -b dev

# 撤销之前所作的修改, 就是用最近一次提交版本去覆盖
git checkout -- a.txt
```

## clone

```shell
# git 克隆的服务器的默认名称: origin
# -v 显示读写远程仓库使用的 Git 保存的简写与其对应的 URL
# git remote show <remote> 查看远程仓库的更多信息
# git remote rename <old> <new>
# git remote remove <name>
git remote show

# 添加远程仓库 git remote add <shortname> <url>
git remote add shortname https://github.com/fewbadboy/development.git

# 命令中就可以用 shortname 代替整个 URL
# 从远程仓库下载最新的提交、分支和标签，但它并不会自动合并或更新本地分支
git fetch shortname

# 包含了两个操作：git fetch和git merge。它从远程仓库下载最新提交，并自动将其合并到当前分支。
git pull shortname

# 推送到上游 git push <remote> <branch>
# 将本地的 master 分支推送到远程 master 分支，远程不存在时会自动创建
git push origin master

# 将本地的 master 分支推送到远程 main 分支
git push origin master:main

# 不想在每一次推送时都输入用户名与密码,保存在内存中几分钟
git config --global credential.helper cache
```

## commit

```shell
# 遗漏文件没添加，或提交信息写错了重新提交
git add *.doc

# 修补提交
git commit --amend 
```

## diff

```shell
# 显示尚未暂存和暂存区域快照之间的差异
git diff

# 已暂存和最后一次提交的文件差异
# git diff --staged
git diff --cached

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

|选项|说明|
|:----:|:----|
|%H|提交的完整哈希值|
|%h|提交的简写哈希值|
|%T|树的完整哈希值|
|%t|树的简写哈希值|
|%P|父提交的完整哈希值|
|%p|父提交的简写哈希值|
|%an|作者名字|
|%ae|作者的电子邮件地址|
|%ad|作者修订日期（可以用 --date=选项 来定制格式）|
|%ar|作者修订日期，按多久以前的方式显示|
|%cn|提交者的名字(项目核心人员，将补丁并于项目的人)|
|%ce|提交者的电子邮件地址|
|%cd|提交日期|
|%cr|提交日期（距今多长时间）|
|%s|提交说明|

## merge

```shell
# 先切换到 master 分支，再合并 dev 分支到 master 分支
git checkout master
git merge dev
# 删除分支
git branch -d dev

# 冲突时（俩分支对同一文件的同一部分做了不同的修改，解决冲突需要合并或者二选一
# <<<<<<<< HEAD:index.html
# xxxxxxxxxx part1
# =========
# xxxxxxxxxx part2
# >>>>>>>> iss53:index.html
```

## rebase

变基：整合来自不同分支的修改

将提交到某一分支上(dev)的所有修改在另一分支(master)上重新引用一次

```shell
git checkout dev
git rebase master

# 快进合并 master 分支，使之包含来自 dev 分支的修改
# git rebase <basebranch> <topicbranch>
git rebase master dev
```

## reset

```shell
# --soft 仅仅恢复头指针，其他的不变
git reset --soft

# --mixed 默认 重置暂存区的文件与上一次的提交(commit)保持一致，工作区文件内容保持不变
git reset --mixed

# --hard 一切全部恢复（重置了工作目录和暂存区）
# 每次提交包含一个指向上次提交对象(父对象)的指针
# HEAD 指向当前分支最新提交的引用
git reset --hard HEAD^2
```

## restore

```shell
# 取消暂存的文件 a.txt
git restore --staged a.txt
```

## show

```shell
# 查看标签信息和与之对应的提交信息
git show v2.0
```

## tag

```shell
# 查看 --list
git tag -l "v1.8.5*"
# 后期打标签
git tag -a v3.0 9fceb02
```
