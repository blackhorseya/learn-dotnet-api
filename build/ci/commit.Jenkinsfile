#!/usr/bin/env groovy

pipeline {
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
          sh '''
          dotnet test --no-build --no-restore
          '''
        }
      }
    }
  }
}