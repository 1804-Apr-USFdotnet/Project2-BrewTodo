node {
    stage('import') {
        try {
            git 'https://github.com/1804-Apr-USFdotnet/Project2-BrewTodo.git'
        } catch(exc) {
            slackError('import')
            throw exc
        }
    }
    stage('build') {
        try {
            bat 'nuget restore'
            bat 'msbuild /p:MvCBuildViews=true'
        } catch(exc) {
            slackError('build')
            throw exc
        }
    }
    stage('test') {
        try {
            bat "VSTest.Console.exe BrewTodo.Tests\\bin\\Debug\\BrewTodo.Tests.dll"
        } catch(exc) {
            slackError('test')
            throw exc
        } 
    }
    stage('analyze') {
        try {
//            bat 'SonarQube.Scanner.MSBuild.exe begin /k:\"kchoi-project1\" /d:sonar.organization=\"darthyoshi-github\" /d:sonar.host.url=\"https://sonarcloud.io\"'
//            bat 'msbuild /t:rebuild'
//            bat 'SonarQube.Scanner.MSBuild.exe end'
        } catch(exc) {
            slackError('analyze')
            throw exc
        }
    }
    stage('package') {
        try {
            bat 'msbuild BrewTodo /t:package'
        } catch(exc) {
            slackError('package')
            throw exc
        }
    }
    stage('deploy') {
        try {
            dir('BrewTodo\\obj\\Debug\\Package') {
                bat "\"C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe\" -source:package='C:\\Program Files (x86)\\Jenkins\\workspace\\BrewTodo\\BrewTodo\\obj\\Debug\\Package\\BrewTodo.zip' -dest:auto,computerName=\"https://ec2-18-217-26-31.us-east-2.compute.amazonaws.com:8172/msdeploy.axd\",userName=\"Administrator\",password=\"${env.deploy_password}\",authtype=\"basic\",includeAcls=\"False\" -verb:sync -disableLink:AppPoolExtension -disableLink:ContentExtension -disableLink:CertificateExtension -setParamFile:\"C:\\Program Files (x86)\\Jenkins\\workspace\\BrewTodo\\BrewTodo\\obj\\Debug\\Package\\BrewTodo.SetParameters.xml\" -AllowUntrusted=True"
            }
        } catch(exc) {
            slackError('deploy')
            throw exc
        }
    }
}

def slackError(stageName) {
    slackSend color: 'danger', message: "${stageName} stage failed. [<${JOB_URL}|${env.JOB_NAME}> <${env.BUILD_URL}console|${env.BUILD_DISPLAY_NAME}>] [${currentBuild.durationString[0..-14]}]"
}
