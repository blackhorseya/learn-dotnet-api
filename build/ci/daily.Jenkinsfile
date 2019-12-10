#!/usr/bin/env groovy

pipeline {
  environment {
    PATH = "/root/.dotnet/tools:$PATH"
    APP_NAME = 'learn-dotnet'
    VERSION = "1.0.0.${BUILD_ID}"
    DOCKERHUB = credentials('docker-hub-credential')
    IMAGE_NAME = "${DOCKERHUB_USR}/${APP_NAME}"
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
    image: docker:latest
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
            sh 'docker version'
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
            container('docker') {
                echo """
IMAGE_NAME: ${IMAGE_NAME}
"""

                sh "docker build -t ${IMAGE_NAME}:latest -f Dockerfile --network bridge ."
                sh "docker login --username ${DOCKERHUB_USR} --password ${DOCKERHUB_PSW}"
                sh """
                docker push ${IMAGE_NAME}:latest && \
                docker tag ${IMAGE_NAME}:latest ${IMAGE_NAME}:${VERSION} && \
                docker push ${IMAGE_NAME}:${VERSION}
                """
                sh "docker images --filter=reference='${IMAGE_NAME}:*'"
            }
        }
    }

    stage('Deploy to dev') {
      steps {
        echo "deploy to dev for latest version"
      }
    }
  }

  post {
      success {
        script {
          def blocks = [
            [
              "type": "section",
              "text": [
                "type": "mrkdwn",
                "text": ":white_check_mark: *<${BUILD_URL}|${JOB_NAME} #${VERSION}>*"
              ]
            ],
            [
              "type": "divider"
            ],
            [
              "type": "section",
              "text": [
                "type": "mrkdwn",
                "text": ":white_check_mark: *${currentBuild.currentResult}*\n:white_check_mark: Elapsed: ${currentBuild.durationString}"
              ]
            ],
            [
              "type": "section",
              "text": [
                "type": "mrkdwn",
                "text": ":white_check_mark: Job: <${JOB_URL}|${JOB_NAME}>\n :white_check_mark: Project: <${GIT_URL}|Github>\n :white_check_mark: Image: <https://hub.docker.com/r/${DOCKERHUB_USR}/${APP_NAME}/tags|Docker hub>"
              ]
            ],
            [
              "type": "divider"
            ]
          ]
          slackSend(blocks: blocks)
        }
      }
      failure {
          slackSend color: 'danger', message: 'build failed.'
      }
  }
}