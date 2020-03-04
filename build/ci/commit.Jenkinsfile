#!/usr/bin/env groovy

@Library("jenkins-shared-libraries") _

pipeline {
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
"""
        }
    }
    stages {
        stage('Prepare') {
            steps {
                echo "branch name: ${env.GIT_BRANCH}"
                sh label: "print all environment variable", script: "printenv | sort"
                
                container('builder') {
                    script {
                        dotnet.printInfo()
                    }
                }
            }
        }

        stage('Build') {
            steps {
                container('builder') {
                    script {
                        dotnet.build(useCache: true)
                    }
                }
            }
        }

        stage('Test') {
            steps {
                container('builder') {
                    script {
                        dotnet.test(genReport: false, genCoverage: false)
                    }
                }
            }
        }
    }

    post {
        always {
            script {
                def changes = common.getChanges()
                echo "${changes}"

                def authorEmail = common.getAuthorEmail()
                echo "${authorEmail}"
            }
        }
    }
}