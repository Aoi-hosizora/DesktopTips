Imports System.IO

Public Class MainFormGlobalPresenter
    Implements MainFormContract.IGlobalPresenter

    Private ReadOnly _view As MainFormContract.IView

    Public Sub New(view As MainFormContract.IView)
        _view = view
    End Sub

    Public Sub LoadFile() Implements MainFormContract.IGlobalPresenter.LoadFile
        Try
            GlobalModel.LoadAllData()
        Catch ex As FileLoadException
            Dim ok = MessageBoxEx.Show($"错误：{ex.Message}{vbNewLine}是否打开文件位置检查文件？",
                "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                _view.GetMe(), {"開く", "キャンセル"})
            If ok = vbYes Then
                OpenFileDir()
            End If
            Application.Exit()
        End Try
    End Sub

    Public Sub SaveFile() Implements MainFormContract.IGlobalPresenter.SaveFile
        GlobalModel.SaveAllData()
    End Sub

    Public Sub OpenFileDir() Implements MainFormContract.IGlobalPresenter.OpenFileDir
        'C:\Users\Windows 10\AppData\Roaming\DesktopTips
        Process.Start("explorer.exe", $"/select,""{GlobalModel.STORAGE_FILENAME}""")
    End Sub

    Public Function RegisterHotKey(handle As IntPtr, key As Keys, id As Integer) As Boolean Implements MainFormContract.IGlobalPresenter.RegisterHotKey
        If Not NativeMethod.RegisterHotKey(
            handle, id, CommonUtil.GetNativeModifiers(CommonUtil.GetModifiersFromKey(key)), CommonUtil.GetKeyCodeFromKey(key)) Then
            MessageBox.Show("快捷键已被占用，请重新设置", "快捷键", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Public Sub UnregisterHotKey(handle As IntPtr, id As Integer) Implements MainFormContract.IGlobalPresenter.UnregisterHotKey
        NativeMethod.UnregisterHotKey(handle, id)
    End Sub

    Public Sub SetupHotKey(handle As IntPtr, id As Integer) Implements MainFormContract.IGlobalPresenter.SetupHotKey
        HotKeyDialog.HotkeyEditBox.CurrentKey = My.Settings.HotKey
        HotKeyDialog.CheckBoxIsValid.Checked = My.Settings.IsUseHotKey
        HotKeyDialog.DoCallback =
            Sub(key As Keys, use As Boolean)
                UnregisterHotKey(handle, id)
                If Not use OrElse RegisterHotKey(handle, key, id) Then
                    My.Settings.IsUseHotKey = use
                    My.Settings.HotKey = key
                    My.Settings.Save()
                End If
            End Sub
        HotKeyDialog.ShowDialog(_view.GetMe())
    End Sub
End Class
