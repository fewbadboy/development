# pipeline

```groovy
pipeline {
  agent {
    docker {
      image 'node: 22.12.0-alpine'
      args '-p 3000:3000'
    }
  }

  environment {
    DB_ENGINE 'sqlite'
  }

  stages {
    stage('Example') {

      environment {
        // Manage Jenkins > Credentials > (选择作用域，例如 "Global") > Add Credentials
        // 设置 Username, Password
        // ID 设置 my-predefined 供脚本引用
        CREDENTIALS = credentials('my-predefined')
      }

      parameters {
        string(name: 'PERSON', defaultValue: 'Mr Jenkins', description: 'Who should I say hello to?')
      }

      // 触发器 MINUTE HOUR DAY MONTH DAY_OF_WEEK
      triggers {
        // H */4 * * * 每 4 小时触发一次
        // 工作日 每 30分支触发一次
        cron('H/30 * * * 1-5')
      }

      steps {
        sh 'node --version'
        sh 'echo "Service user is $CREDENTIALS_USR"'
        sh 'echo "Service password is $CREDENTIALS_PSW"'
        sh 'git clone https://${CREDENTIALS_USR}:${CREDENTIALS_PSW}@<git-server>/<owner>/<repo>.git'
        // NODE_NAME 构建节点名称  对 main 节点为 main
        echo "Running URL: $JENKINS_URL"
        echo "Hello: ${params.PERSON}"
      }
    }

    stage("retry") {
      // Jenkins运行构建的分支名匹配时执行
      when {
        branch 'development'
      }

      stages{
        retry(3) {
          sh 'echo "retry..."'
        }
        timeout(time: 3, unit: 'MINUTES') {
          sh 'echo "timeout..."'
        }
      }
    }

    stage("A") {
      steps {
        echo "========executing A========"
      }
      post {
        always {
          echo "========always========"
        }
        success {
          echo "========A executed successfully========"
        }
        failure {
          echo "========A execution failed========"
        }
      }
    }
  }

  post {
    always {
      echo "========always========"
    }
    success {
      echo "========pipeline executed successfully ========"
    }
    failure {
      echo "========pipeline execution failed========"
    }
  }
}
```
