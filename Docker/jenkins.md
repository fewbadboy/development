# jenkins

## 配置明确的本地 volume

所有 Jenkins 数据都位于工作区存储在 /var/jenkins_home 中,包括插件和配置

```shell
# 明确配置(方便管理它并附加到另一个容器进行升级)
docker -v jenkins_home:/var/jenkins_home ...
```

## 启动容器

1. Jenkins 初始化
2. 安装插件(错误就重试)
3. 创建管理员
4. GitLab, NodeJS，SSH Agent 等插件安装
5. 系统管理
   - 全局工具配置
     - 设置 Node 环境
     - 凭据管理 - 域(下拉按钮处添加 SSH 凭据添加私钥)
