'Hamster Engine v2.0용 Project Adapter
Public Class Project
    Public Shared Version As New CommonComponet.HamsterVersion("Hamster Engine Default", 2, 0, 0, 0) '이 부분을 수정해 프로젝트의 버전을 수정하세요.
    '("개체 식별자 문자열", {"어셈블리 경로"})
    Private Shared LoadObject_PROJ_Info As New Dictionary(Of String, Object())

    Public Sub initialization(ByRef engineAssembly As Dictionary(Of String, Object), ByRef engineFucntion As Dictionary(Of String, [Delegate]), ByRef engineProperty As Dictionary(Of String, Boolean), ByVal engineArgs As Object(), ByRef projectAssembly As Dictionary(Of String, Object()), ByRef projectReference As Dictionary(Of String, Object()))
        EngineWrapper.Init(engineFucntion, engineArgs)
        MsgBox("Project Initialization")
    End Sub

    Public Sub main(ProjsideAsm As Dictionary(Of String, Object))
        MsgBox("Project Main")
    End Sub
End Class