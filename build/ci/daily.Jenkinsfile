#!/usr/bin/env groovy

pipeline {
  environment {
    PATH = "/root/.dotnet/tools:$PATH"
    APP_NAME = 'learn-dotnet'
    VERSION = '1.0.0'
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
        container('dotnet-sdk') {
            sh 'dotnet tool install --global coverlet.console'
            sh 'dotnet tool install --global dotnet-sonarscanner'
            sh 'apk add --no-cache openjdk8'
        }
        sh 'printenv'
      }
    }

    stage('Build') {
      steps {
        container('dotnet-sdk') {
          sh 'dotnet build -c Release -o ./publish'
        }
      }
    }

    stage('Test') {
      steps {
        container('dotnet-sdk') {
          echo "perform dotnet test and generate test and coverage results"
          sh '''
          dotnet test /p:CollectCoverage=true \
          /p:CoverletOutputFormat=opencover \
          /p:CoverletOutput=$(pwd)/TestResults/ \
          --logger trx \
          -r ./TestResults \
          -o ./publish \
          --no-build --no-restore
          '''
        }
      }
    }

    stage('Static Code Analysis') {
      steps {
        container('dotnet-sdk') {
//             sh 'dotnet sonarscanner begin'
            echo "perform static code analysis"
            echo "push coverage and test results to sonarqube"
        }
      }
    }

    stage('Build and push docker image') {
      steps {
        environment {
            DOCKERHUB = credentials('docker-hub-credential')
        }
        echo "${DOCKERHUB_USR}:${DOCKERHUB_PSW}"
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