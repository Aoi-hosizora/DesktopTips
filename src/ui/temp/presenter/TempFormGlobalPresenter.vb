Imports System.IO

Public Class TempFormGlobalPresenter
    Implements TempFormContract.IGlobalPresenter

    Private ReadOnly _view As TempFormContract.IView

    Public Sub New(view As TempFormContract.IView)
        _view = view
    End Sub

    ''' <summary>
    ''' 加载列表文件
    ''' </summary>
    Public Sub LoadFile() Implements TempFormContract.IGlobalPresenter.LoadFile
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

    ''' <summary>
    ''' 保存列表文件
    ''' </summary>
    Public Sub SaveFile() Implements TempFormContract.IGlobalPresenter.SaveFile
        GlobalModel.SaveAllData()
    End Sub

    ''' <summary>
    ''' 打开文件所在位置
    ''' </summary>
    Public Sub OpenFileDir() Implements TempFormContract.IGlobalPresenter.OpenFileDir
        'C:\Users\Windows 10\AppData\Roaming\DesktopTips
        Process.Start("explorer.exe", $"/select,""{GlobalModel.STORAGE_FILENAME}""")
    End Sub

    ''' <summary>
    ''' 注册快捷键
    ''' </summary>
    Public Function RegisterHotKey(handle As IntPtr, key As Keys, id As Integer) As Boolean Implements TempFormContract.IGlobalPresenter.RegisterHotKey
        If Not NativeMethod.RegisterHotKey(handle, id, CommonUtil.GetNativeModifiers(CommonUtil.GetModifiersFromKey(key)), CommonUtil.GetKeyCodeFromKey(key)) Then
            MessageBox.Show("快捷键已被占用，请重新设置", "快捷键", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 注销快捷键
    ''' </summary>
    Public Sub UnregisterHotKey(handle As IntPtr, id As Integer) Implements TempFormContract.IGlobalPresenter.UnregisterHotKey
        NativeMethod.UnregisterHotKey(handle, id)
    End Sub

    ''' <summary>
    ''' 设置快捷键
    ''' </summary>
    Public Sub SetupHotKey(handle As IntPtr, id As Integer) Implements TempFormContract.IGlobalPresenter.SetupHotKey
        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()
        HotKeyDialog.HotKeyEditBox.CurrentKey = setting.HotKey
        HotKeyDialog.CheckBoxIsValid.Checked = setting.IsUseHotKey
        HotKeyDialog.OkCallback =
            Sub(key As Keys, use As Boolean)
                UnregisterHotKey(handle, id)
                If Not use OrElse RegisterHotKey(handle, key, id) Then
                    setting.IsUseHotKey = use
                    setting.HotKey = key
                    SettingUtil.SaveAppSettings(setting)
                End If
            End Sub
        HotKeyDialog.ShowDialog(_view.GetMe())
    End Sub
End Class
