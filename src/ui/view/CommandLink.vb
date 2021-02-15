Imports System.Windows.Forms

''' <summary>
''' CommandLink 按钮，通过 BS_COMMANDLINK 创建
''' </summary>
Public Class CommandLink
    Inherits Button

    Public Sub New()
        MyBase.New()
        FlatStyle = FlatStyle.System
        RefreshCommandLinkUi()
    End Sub

#Region "属性"

    Private _commandLinkNote As String

    ''' <summary>
    ''' CommandLink 的 Note 内容
    ''' </summary>
    Public Property CommandLinkNote As String
        Get
            Return _commandLinkNote
        End Get
        Set
            _commandLinkNote = ""
            RefreshCommandLinkUi()
            _commandLinkNote = value
            RefreshCommandLinkUi()
        End Set
    End Property

#End Region

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.Style = cp.Style Or NativeMethod.BS_COMMANDLINK
            Return cp
        End Get
    End Property

    ''' <summary>
    ''' 刷新 CommandLink UI
    ''' </summary>
    Private Sub RefreshCommandLinkUi()
        RecreateHandle()
        NativeMethod.SendMessage(Handle, NativeMethod.BCM_SETNOTE, IntPtr.Zero, _commandLinkNote)
    End Sub
End Class
