# Build

仅在Docker Swarm 模式下使用: Docker secret 创建密码（避免明文显示密码）

```shell

echo "123456" | docker secret create my-password -

docker build . -t secure-app-secrets:1.0 --secret id=npmrc,src=$HOME/.npmrc
```
