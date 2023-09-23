# document

## branch

```shell
# HEAD 指向当前所在的分支

# 创建一个可以移动的新的指针
get branch testing
```

## checkout

```shell
# 切换分支 main
git checkout main

# 创建并切换分支 dev
# 等效于 git branch dev, git checkout dev
git checkout -b dev

# 删除分支 dev
git branch -d dev

# 撤销之前所作的修改
# 就是用最近一次提交版本去覆盖
git checkout -- a.txt
```

## clone

```shell
# git 克隆的服务器的默认名称: origin
# -v 显示读写远程仓库使用的 Git 保存的简写与其对应的 URL
# git remote show <remote> 查看远程仓库的更多信息
# git remote rename <old> <new>
# git remote remove <name>
git remote

# 添加远程仓库 git remote add <shortname> <url>
git remote add shortname https://github.com/fewbadboy/development.git

# 命令中就可以用 shortname 代替整个 URL
# 从远程仓库下载最新的提交、分支和标签，但它并不会自动合并或更新本地分支
git fetch shortname

# 包含了两个操作：git fetch和git merge。它从远程仓库下载最新提交，并自动将其合并到当前分支。
git pull shortname

# 推送到上游 git push <remote> <branch>
git push origin master
```

## commit

```shell
git add a.txt
git commit --amend # 遗漏 a.txt 文件没添加，或提交信息写错了
```

## diff

```shell
git diff # 显示尚未暂存的改动
git diff --cached # 已暂存和最后一次提交的文件差异
git diff master origin/master # 已提交，未推送 与 远程仓库的文件差异
```

## grep

方便地从提交历史、工作目录、甚至索引中查找一个字符串或者正则表达式。

## log

```shell
git log -p -2 # --patch 最近 2 次提交
git log --stat # 简略统计信息
git log --pretty=formate:"%h  - %an : %s" --graph # --pretty=oneline
git log --since=2.weeks --author='any' --committer='some' --grep='匹配的提交说明' # since/after until/before 
```

|选项|说明|
|:----:|:----|
|%H|提交的完整哈希值|
|%h|提交的简写哈希值||
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
# 合并dev分支 到现在分支上
git merge origin/dev
```

## rebase

变基

提交到 dev 分支上的所有修改都移至 master 分支上

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

# 取消暂存的文件 a.txt
git reset HEAD a.txt
```

## show

```shell
# 查看标签信息和与之对应的提交信息
git show v2.0
```

## tag

```shell
# 后期打标签
git tag -a v3.0 9fceb02
```
