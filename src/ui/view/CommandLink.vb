Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class CommandLink
    Inherits Button

    Public Sub New()
        MyBase.New()
        MyBase.FlatStyle = FlatStyle.System
        Me.UpdateCommandLink()
    End Sub

    Private _commandLinkNote As String

    <Category("Appearance")>
    <DefaultValue("")>
    Public Property CommandLinkNote As String
        Get
            Return _commandLinkNote
        End Get
        Set
            _commandLinkNote = value
            Me.UpdateCommandLink()
        End Set
    End Property

#Region "P/Invoke Stuff"

    Private Const BS_COMMANDLINK As Integer = &HE
    Private Const BCM_SETNOTE As Integer = &H1609

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As IntPtr
    End Function

    Private Sub UpdateCommandLink()
        Me.RecreateHandle()
        SendMessage(Me.Handle, BCM_SETNOTE, IntPtr.Zero, _commandLinkNote)
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.Style = cp.Style Or BS_COMMANDLINK
            Return cp
        End Get
    End Property

#End Region
End Class