# 生成 SSH 公钥

```shell
cd ~/.ssh

# 生成公钥
ssh-keygen -o
```

## 签署工作

验证提交是不是来自可信来源，可通过 GPG 来签署和验证工作的方式

```shell
# 配置 GPG 和安装个人密钥
gpg --gen-key

# 获取公钥
gpg --list-key
# /Users/web/.gnupg/pubring.gpg
# ---------------------------------
# pub   2048R/0A46826A 2014-06-04

# 签署私钥
git config --global user.signingkey 0A46826A
# 现在 Git 默认使用你的密钥来签署标签与提交

# 签署新的标签, 使用 -s 代替 -a 即可
git tag -s v1.5 -m 'my signed 1.5 tag'
```
