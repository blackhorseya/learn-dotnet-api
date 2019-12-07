#!/usr/bin/env groovy

pipeline {
  triggers {
    pollSCM( env.BRANCH_NAME.equals('develop') ? 'TZ=Asia/Taipei\nH H(4-5) * * *' : '')
  }
  agent {
    kubernetes {
      yaml """
apiVersion: v1
kind: Pod
spec:
  containers:
  - name: dotnet-sdk
    image: mcr.microsoft.com/dotnet/core/sdk:3.1-alpine
    command:
    - cat
    tty: true
"""
    }
  }
  stages {
    stage('Prepare') {
      steps {
        echo "branch name: ${env.GIT_BRANCH}"
        sh 'ls -al'
        sh 'printenv'
      }
    }

    stage('Build') {
      steps {
        container('dotnet-sdk') {
          sh '''
          dotnet build -c Release -o ./publish
          '''
        }
      }
    }

    stage('Test') {
      steps {
        container('dotnet-sdk') {
          echo "perform dotnet test and generate test and coverage results"
          sh '''
          dotnet test --no-build --no-restore
          '''
        }
      }
    }

    stage('Static Code Analysis') {
      steps {
        echo "perform static code analysis"
        echo "push coverage and test results to sornacube"
      }
    }

    stage('Build and push docker image') {
      steps {
        echo "build docker image..."
        echo "push the image to harbor..."
      }
    }

    stage('Deploy to dev') {
      steps {
        echo "deploy to dev for latest version"
      }
    }
  }
}