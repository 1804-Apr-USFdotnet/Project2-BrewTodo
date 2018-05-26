node {
    stage('import') {
        try {
            checkout scm
        } catch(exc) {
            slackError('import')
            throw exc
        }
    }
    stage('build') {
        try {
            slackStatus('build')
            dir('BrewTodoServer') {
                bat 'nuget restore'
                bat 'msbuild /p:MvCBuildViews=true'
            }
        } catch(exc) {
            slackError('build')
            throw exc
        }
    }
    stage('test') {
        try {
            slackStatus('test')
            dir('BrewTodoServer') {
                bat "VSTest.Console.exe BrewTodoServerTests\\bin\\Debug\\BrewTodoServerTests.dll"
            }
        } catch(exc) {
            slackError('test')
            throw exc
        } 
    }
    stage('analyze') {
        try {
            slackStatus('analyze')
            dir('BrewTodoServer') {
                bat 'SonarQube.Scanner.MSBuild.exe begin /k:\"brewtodo-server-key\" /d:sonar.organization=\"brewtodo-group\" /d:sonar.host.url=\"https://sonarcloud.io\"'
                bat 'msbuild /t:rebuild'
                bat 'SonarQube.Scanner.MSBuild.exe end'
            }
        } catch(exc) {
            slackError('analyze')
            throw exc
        }
    }
    stage('package') {
        try {
            slackStatus('package')
            dir('BrewTodoServer') {
                bat 'msbuild BrewTodoServer /t:package'
            }
        } catch(exc) {
            slackError('package')
            throw exc
        }
    }
    stage('deploy (server)') {
        try {
            slackStatus("deploy (server)")
            dir('BrewTodoServer\\BrewTodoServer\\obj\\Debug\\Package') {
                bat "\"C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe\" -source:package='C:\\Program Files (x86)\\Jenkins\\workspace\\BrewTodo\\BrewTodoServer\\BrewTodoServer\\obj\\Debug\\Package\\BrewTodoServer.zip' -dest:auto,computerName=\"${env.server_address}/msdeploy.axd\",userName=\"Administrator\",password=\"${env.server_password}\",authtype=\"basic\",includeAcls=\"False\" -verb:sync -disableLink:AppPoolExtension -disableLink:ContentExtension -disableLink:CertificateExtension -setParamFile:\"C:\\Program Files (x86)\\Jenkins\\workspace\\BrewTodo\\BrewTodoServer\\BrewTodoServer\\obj\\Debug\\Package\\BrewTodoServer.SetParameters.xml\" -AllowUntrusted=True"
            }
        } catch(exc) {
            slackError("deploy (server)")
            throw exc
        }
    }
    stage('success') {
        slackSend color: 'good', message: "Pipeline end reached. [<${JOB_URL}|${env.JOB_NAME}> <${env.BUILD_URL}console|${env.BUILD_DISPLAY_NAME}>] [${currentBuild.durationString[0..-14]}]"
    }
}

def slackStatus(stageName) {
    slackSend message: "${stageName} stage reached. [<${JOB_URL}|${env.JOB_NAME}> <${env.BUILD_URL}console|${env.BUILD_DISPLAY_NAME}>] [${currentBuild.durationString[0..-14]}]"
}

def slackError(stageName) {
    slackSend color: 'danger', message: "${stageName} stage failed. [<${JOB_URL}|${env.JOB_NAME}> <${env.BUILD_URL}console|${env.BUILD_DISPLAY_NAME}>] [${currentBuild.durationString[0..-14]}]"
}
