Module EngineWrapper
    ''' <summary>
    ''' EngineWrapper를 초기화합니다.
    ''' </summary>
    ''' <param name="engineFucntion">엔진이 initialization 함수의 매개변수로 넘긴 engineFucntion 딕셔너리</param>
    ''' <param name="engineArgs">엔진이 initialization 함수의 매개변수로 넘긴 engineArgs 배열</param>
    Public Sub Init(ByRef engineFucntion As Dictionary(Of String, [Delegate]), ByRef engineArgs As Object())
        EngineFunction.EFUNC_EngineShutdown = engineFucntion("EFUNC_EngineShutdown")
        EngineFunction.EFUNC_LogWrite = engineFucntion("EFUNC_LogWrite")
        EngineFunction.EFUNC_LogWriteP = engineFucntion("EFUNC_LogWriteP")
        EngineFunction.EFUNC_SetMainForm = engineFucntion("EFUNC_SetMainForm")
        EngineFunction.EFUNC_ShowWarningMsg = engineFucntion("EFUNC_ShowWarningMsg")
        EngineFunction.EFUNC_ShowWarningMsgE = engineFucntion("EFUNC_ShowWarningMsgE")
        EngineFunction.EFUNC_ShowWarningMsgS = engineFucntion("EFUNC_ShowWarningMsgS")
        EngineFunction.EFUNC_ShowErrorMsg = engineFucntion("EFUNC_ShowErrorMsg")
        EngineFunction.EFUNC_ShowErrorMsgE = engineFucntion("EFUNC_ShowErrorMsgE")
        EngineFunction.EFUNC_ShowErrorMsgS = engineFucntion("EFUNC_ShowErrorMsgS")
        EngineFunction.EFUNC_GetEngineIcon = engineFucntion("EFUNC_GetEngineIcon")
        EngineArgument.ApplicationStartupPath = engineArgs(0).ToString
        EngineArgument.Version = engineArgs(1)
    End Sub

    Public Class EngineFunction
        Public Shared EFUNC_EngineShutdown As [Delegate]
        Public Shared EFUNC_LogWrite As [Delegate]
        Public Shared EFUNC_LogWriteP As [Delegate]
        Public Shared EFUNC_SetMainForm As [Delegate]
        Public Shared EFUNC_ShowWarningMsg As [Delegate]
        Public Shared EFUNC_ShowWarningMsgE As [Delegate]
        Public Shared EFUNC_ShowWarningMsgS As [Delegate]
        Public Shared EFUNC_ShowErrorMsg As [Delegate]
        Public Shared EFUNC_ShowErrorMsgE As [Delegate]
        Public Shared EFUNC_ShowErrorMsgS As [Delegate]
        Public Shared EFUNC_GetEngineIcon As [Delegate]
    End Class

    Public Class EngineArgument
        Public Shared ApplicationStartupPath As String
        Public Shared Version As Object
    End Class
End Module

Public Class CommonComponet
    Public Class HamsterVersion '버전정보를 담기위한 클래스
        Private StrVersion As String '문자열 버젼
        Private MajorVersion, MinorVersion, BuildDateNumber, BuildCountNumber As Integer '메이저버젼,마이너버전,빌드번호,수정번호
        Private ModuleName As String '모듈이름

        Private setStatus As Boolean = False '초기화 되지 않았거나, 정의하지 않았거나, 비포함의 경우 FALSE
        Private setStatusEXP As Byte = 0 'setStatus가 false일경우, 0=초기화 되지않음, 1=정의하지 않음, 2=포함하지 않음


        Public Sub New(ModuleNam As String, MajorVer As Byte, MinorVer As Byte, BuildNum As Integer, ChangeNum As Integer)
            '버젼을 초기화 합니다         
            setStatus = True

            ModuleName = ModuleNam
            MajorVersion = MajorVer
            MinorVersion = MinorVer
            BuildDateNumber = BuildNum
            BuildCountNumber = ChangeNum
        End Sub


        Public Sub SetVersion(NotDefine As Boolean)
            '정의하지 않았거나, 포함하지 않았을 경우 FALSE를 넘겨주면 비포함, TRUE는 정의되지 않음 입니다
            MajorVersion = 0
            MinorVersion = 0
            BuildDateNumber = 0
            BuildCountNumber = 0

            If NotDefine Then
                setStatus = False
                setStatusEXP = 1
            Else
                setStatus = False
                setStatusEXP = 2
            End If
        End Sub

        Public Function GetVersion(returnString As Boolean) As Object
            'returnString이 TRUE 일경우 문자열로 버젼을 반환하며, FALSE 일 경우 정수 배열로 반환합니다
            If setStatusEXP = 0 And setStatus = False Then
                Throw New VersionNotSetException
            End If

            If returnString Then
                Dim ver As String = MajorVersion & "." & MinorVersion & "." & BuildDateNumber & "." & BuildCountNumber
                Return ver
            Else
                Dim ver As Integer() = {MajorVersion, MinorVersion, BuildDateNumber, BuildCountNumber}
                Return ver
            End If
        End Function

        Public Function GetName() As String
            Return ModuleName
        End Function

    End Class

    Public Class VersionNotSetException
        Inherits Exception

        Public Const expMessage As String = "[햄스터 엔진 내부 예외]버젼이 초기화되지 않았습니다, 초기화되지 않은 버젼객체에 엑세스하려고 했습니다"

        Sub New()
            MyBase.New(expMessage)
        End Sub
    End Class
End Class