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
    image: blackhorseya/dotnet-builder:3.1-alpine
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
      always {
        script {
          def prefixIcon = currentBuild.currentResult == 'SUCCESS' ? ':white_check_mark:' : ':x:'
          def blocks = [
            [
              "type": "section",
              "text": [
                "type": "mrkdwn",
                "text": "${prefixIcon} *<${BUILD_URL}|${JOB_NAME} #${VERSION}>*"
              ]
            ],
            [
              "type": "divider"
            ],
            [
              "type": "section",
              "fields": [
                [
                  "type": "mrkdwn",
                  "text": "*:star: Build Status:*\n${currentBuild.currentResult}"
                ],
                [
                  "type": "mrkdwn",
                  "text": "*:star: Elapsed:*\n${currentBuild.durationString}"
                ],
                [
                  "type": "mrkdwn",
                  "text": "*:star: Job:*\n<${JOB_URL}|${JOB_NAME}>"
                ],
                [
                  "type": "mrkdwn",
                  "text": "*:star: Project:*\n<${GIT_URL}|Github>"
                ],
                [
                  "type": "mrkdwn",
                  "text": "*:star: Build Image:*\n<https://hub.docker.com/r/${DOCKERHUB_USR}/${APP_NAME}/tags|Docker hub>"
                ]
              ]
            ]
          ]
          slackSend(blocks: blocks)
        }
      }
  }
}