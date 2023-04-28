# Install

## JDK

[archive](https://www.oracle.com/java/technologies/downloads/archive/)

## Linux

```shell
tar -zxvf jdk-8u241-linux-x64.tar.gz
mv jdk1.8.0_241  jdk1.8
vi /etc/profile

export JAVA_HOME=/usr/local/src/jdk/jdk1.8
export PATH=$PATH:$JAVA_HOME/bin
export CLASSPATH=.:$JAVA_HOME/lib/dt.jar:$JAVA_HOME/lib/tools.jar

source /etc/profile
java -version
```

## 防火墙

```shell
# centos
systemctl status firewalld # 状态

#（--permanent永久生效，没有此参数重启后失效）
#注：可以是一个端口范围，如1000-2000/tcp
firewall-cmd --zone=public --add-port=80/tcp --permanent
firewall-cmd --reload
firewall-cmd --query-port=80/tcp
firewall-cmd --zone=public --remove-port=80/tcp --permanent # 移除端口
firewall-cmd --list-port
```
