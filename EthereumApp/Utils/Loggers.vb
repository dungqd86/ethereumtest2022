Imports System.Configuration
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports NLog
Imports NLog.Config
Imports NLog.Targets

Public Class Loggers
    Private Shared Sub Init()
        Dim configuration As LoggingConfiguration = New LoggingConfiguration()

        Dim target As FileTarget = New FileTarget()
        configuration.AddTarget("file", target)
        target.FileName = LogPath + "${date:format=yyyyMMdd}/TraceLog.txt"
        target.Layout = "${message}"
        Dim item As LoggingRule = New LoggingRule("*", target)
        item.EnableLoggingForLevel(LogLevel.Trace)
        configuration.LoggingRules.Add(item)

        Dim target2 As FileTarget = New FileTarget()
        configuration.AddTarget("file", target2)
        target2.FileName = LogPath + "${date:format=yyyyMMdd}/DebugLog.txt"
        target2.Layout = "${message}"
        Dim rule2 As LoggingRule = New LoggingRule("*", target2)
        rule2.EnableLoggingForLevel(LogLevel.Debug)
        configuration.LoggingRules.Add(rule2)

        Dim target3 As FileTarget = New FileTarget()
        configuration.AddTarget("file", target3)
        target3.FileName = LogPath + "${date:format=yyyyMMdd}/InfoLog.txt"
        target3.Layout = "${message}"
        Dim rule3 As LoggingRule = New LoggingRule("*", target3)
        rule3.EnableLoggingForLevel(LogLevel.Info)
        configuration.LoggingRules.Add(rule3)

        Dim target4 As FileTarget = New FileTarget()
        configuration.AddTarget("file", target4)
        target4.FileName = LogPath + "${date:format=yyyyMMdd}/WarnLog.txt"
        target4.Layout = "${message}"
        Dim rule4 As LoggingRule = New LoggingRule("*", target4)
        rule4.EnableLoggingForLevel(LogLevel.Warn)
        configuration.LoggingRules.Add(rule4)

        Dim target5 As FileTarget = New FileTarget()
        configuration.AddTarget("file", target5)
        target5.FileName = LogPath + "${date:format=yyyyMMdd}/ErrorLog.txt"
        target5.Layout = "${message}"
        Dim rule5 As LoggingRule = New LoggingRule("*", target5)
        rule5.EnableLoggingForLevel(LogLevel.Error)
        configuration.LoggingRules.Add(rule5)

        Dim target6 As FileTarget = New FileTarget()
        configuration.AddTarget("file", target6)
        target6.FileName = LogPath + "${date:format=yyyyMMdd}/FatalLog.txt"
        target6.Layout = "${message}"
        Dim rule6 As LoggingRule = New LoggingRule("*", target6)
        rule6.EnableLoggingForLevel(LogLevel.Fatal)
        configuration.LoggingRules.Add(rule6)

        LogManager.Configuration = configuration
    End Sub

    Public Shared Sub TraceLog(ByVal content As Object)
        If LogManager.Configuration Is Nothing Then
            Init()
        End If
        Dim logger As NLog.Logger = LogManager.GetLogger(New StackFrame(1).GetMethod().Name)
        logger.Trace(content)
    End Sub

    Public Shared Sub ErrorLog(ByVal content As Object)
        If LogManager.Configuration Is Nothing Then
            Init()
        End If
        Dim logger As NLog.Logger = LogManager.GetLogger(New StackFrame(1).GetMethod().Name)
        logger.Error(content)
    End Sub

    Public Shared Sub WriteLog(ByVal logType As LogType, ByVal content As Object)
        If LogManager.Configuration Is Nothing Then
            Init()
        End If

        Dim logger As NLog.Logger = LogManager.GetLogger(New StackFrame(1).GetMethod().Name)
        Select Case logType
            Case LogType.Trace
                logger.Trace(content)
                Return

            Case LogType.Debug
                logger.Debug(content)
                Return

            Case LogType.Warning
                logger.Warn(content)
                Return

            Case LogType.Error
                logger.Error(content)
                Return

            Case LogType.Fatal
                logger.Fatal(content)
                Return
        End Select
        logger.Info(content)
    End Sub

    Private Shared ReadOnly Property LogPath() As String
        Get
            Dim absolutePath As String = New Uri(Assembly.GetExecutingAssembly().EscapedCodeBase).AbsolutePath

            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("LogPath")) Then
                absolutePath = ConfigurationManager.AppSettings("LogPath")
            Else
                absolutePath = (absolutePath.Substring(0, absolutePath.IndexOf("/bin") + 1) + "Logs/")
            End If

            Try
                If Not Directory.Exists(absolutePath) Then
                    Directory.CreateDirectory(absolutePath)
                End If
            Catch

            End Try

            Return absolutePath
        End Get
    End Property

    Public Enum LogType
        Trace
        Debug
        Info
        Warning
        [Error]
        Fatal
        TraceOveride
    End Enum


End Class