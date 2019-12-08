#!/usr/bin/env groovy

pipeline {
  environment {
    PATH = "/root/.dotnet/tools:$PATH"
    APP_NAME = 'learn-dotnet'
    VERSION = "1.0.0.${BUILD_ID}"
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
  - name: docker
    image: docker:1.11
    command: ['cat']
    tty: true
    volumeMounts:
    - name: dockersock
      mountPath: /var/run/docker.sock
  volumes:
  - name: dockersock
    hostPath:
      path: /var/run/docker.sock
"""
    }
  }
  stages {
    stage('Prepare') {
      steps {
        echo """
Perform ${JOB_NAME} for
Repo: ${env.GIT_URL}
Branch: ${env.GIT_BRANCH}
Application: ${APP_NAME}:${VERSION}
"""
        
        container('dotnet-sdk') {
            sh '''dotnet tool install --global coverlet.console
            dotnet tool install --global dotnet-sonarscanner
            apk add --no-cache openjdk8'''
            
            sh 'dotnet --info'
        }
        
        container('docker') {
            sh 'docker info'
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
        environment {
            DOCKERHUB = credentials('docker-hub-credential')
            IMAGE_TAG = "${DOCKERHUB_USR}/${APP_NAME}:${VERSION}"
        }
        steps {
            container('docker') {
                echo """
IMAGE_TAG: ${IMAGE_TAG}
"""

                sh "docker build -t ${IMAGE_TAG} -f Dockerfile --network=bridge ."
                sh "docker images --filter=reference='${DOCKERHUB_USR}/${APP_NAME}:*'"
                echo "tag images"
                echo "push the image to harbor..."
            }
        }
    }

    stage('Deploy to dev') {
      steps {
        echo "deploy to dev for latest version"
      }
    }
  }
}