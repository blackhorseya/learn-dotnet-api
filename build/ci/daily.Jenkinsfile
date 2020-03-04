#!/usr/bin/env groovy

@Library("jenkins-shared-libraries") _

pipeline {
    environment {
        // application settings
        APP_NAME = "learn-dotnet"
        VERSION = "1.0.1"
        FULL_VERSION = "${VERSION}.${BUILD_ID}"
        IMAGE_NAME = "${DOCKER_REGISTRY_CRED_USR}/${APP_NAME}"

        // docker credentials
        DOCKER_REGISTRY_URL = "https://registry.hub.docker.com/"
        DOCKER_REGISTRY_ID = "docker-hub-credential"
        DOCKER_REGISTRY_CRED = credentials("${DOCKER_REGISTRY_ID}")

        // sonarqube settings
        SONARQUBE_HOST_URL = "https://sonar.blackhorseya.com"
        SONARQUBE_TOKEN = credentials('sonarqube-token')

        // kubernetes settings
        KUBE_CONFIG_ID = "kube-config"

        // git settings
        GIT_CREDENTIAL_ID = "github-ssh"
    }
    agent {
        kubernetes {
            yaml """
apiVersion: v1
kind: Pod
spec:
  containers:
  - name: builder
    image: blackhorseya/dotnet-builder:3.1-alpine
    command: ['cat']
    tty: true
  - name: docker
    image: docker:latest
    command: ['cat']
    tty: true
    volumeMounts:
    - name: dockersock
      mountPath: /var/run/docker.sock
  - name: helm
    image: alpine/helm:3.1.0
    command: ['cat']
    tty: true
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
                script {
                    DEPLOY_TO = common.getTargetEnv("${GIT_BRANCH}")
                }
                echo """
branch name: ${env.GIT_BRANCH}
target env: ${DEPLOY_TO}
"""
                sh label: "print all environment variable", script: "printenv | sort"

                container('builder') {
                    script {
                        dotnet.printInfo()
                    }
                }

                container('docker') {
                    sh label: "print docker info and version", script: """
                    docker info
                    docker version
                    """
                }

                container('helm') {
                    script {
                        deploy.helmInfo()
                        deploy.copyConfig("${KUBE_CONFIG_ID}")
                    }
                }
            }
        }

        stage('Build') {
            steps {
                container('builder') {
                    script {
                        dotnet.scannerBegin(
                            projectKey: "${APP_NAME}",
                            version: "${FULL_VERSION}",
                            hostUrl: "${SONARQUBE_HOST_URL}",
                            token: "${SONARQUBE_TOKEN}"
                        )
                        dotnet.build(useCache: true)
                    }
                }
            }
        }

        stage('Test') {
            steps {
                container('builder') {
                    script {
                        dotnet.test(
                            genCoverage: true,
                            genReport: true
                        )
                    }
                }
            }
        }

        stage('Static Code Analysis') {
            steps {
                container('builder') {
                    script {
                        dotnet.scannerEnd(
                            token: "${SONARQUBE_TOKEN}"
                        )
                    }
                }
            }
        }

        stage('Build and push docker image') {
            steps {
                container('docker') {
                    script {
                        docker.withRegistry("${DOCKER_REGISTRY_URL}", "${DOCKER_REGISTRY_ID}") {
                            def image = docker.build("${IMAGE_NAME}:${FULL_VERSION}", "--network host .")
                            image.push()
                            image.push('latest')
                        }
                    }
                }
            }
        }

        stage('Deploy') {
            steps {
                container('helm') {
                    script {
                        deploy.helmListWithEnv("${DEPLOY_TO}")
                        deploy.helmUpgrade(
                            appName: "${APP_NAME}",
                            version: "${FULL_VERSION}",
                            imageName: "${IMAGE_NAME}",
                            env: "${DEPLOY_TO}"
                        )
                    }
                }

                script {
                    common.gitAddTag("${GIT_CREDENTIAL_ID}", "${DEPLOY_TO}", "${VERSION}")
                }
            }
        }
    }

    post {
        always {
            script {
                notify.sendSlack()
            }
        }
    }
}