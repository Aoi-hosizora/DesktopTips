Imports System.IO

Public Class MainFormGlobalPresenter
    Implements MainFormContract.IGlobalPresenter

    Private ReadOnly _view As MainFormContract.IView

    Public Sub New(view As MainFormContract.IView)
        _view = view
    End Sub

    ''' <summary>
    ''' 加载设置
    ''' </summary>
    Public Function LoadSetting() As SettingUtil.AppSetting Implements MainFormContract.IGlobalPresenter.LoadSetting
        Return SettingUtil.LoadAppSettings()
    End Function

    ''' <summary>
    ''' 保存设置
    ''' </summary>
    Public Sub SaveSetting(setting As SettingUtil.AppSetting) Implements MainFormContract.IGlobalPresenter.SaveSetting
        SettingUtil.SaveAppSettings(setting)
    End Sub

    ''' <summary>
    ''' 加载列表文件
    ''' </summary>
    Public Sub LoadList() Implements MainFormContract.IGlobalPresenter.LoadList
        Try
            GlobalModel.LoadAllData()
        Catch ex As FileLoadException
            Dim ok As MsgBoxResult = MessageBoxEx.Show(
                "错误：" & ex.Message & Chr(10) & "是否打开文件位置检查文件？",
                "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                _view.GetMe(), {"開く", "キャンセル"})
            If ok = vbYes Then
                OpenFileDir()
            End If
            Application.Exit()
            Return
        End Try
    End Sub

    ''' <summary>
    ''' 保存列表文件
    ''' </summary>
    Public Sub SaveList(items As ListBox.ObjectCollection) Implements MainFormContract.IGlobalPresenter.SaveList
        Dim Tips As New List(Of TipItem)
        For Each Tip As TipItem In items.Cast(Of TipItem)()
            Tips.Add(Tip)
        Next
        GlobalModel.CurrentTab.Tips = New List(Of TipItem)(Tips) ' 当前列表
        GlobalModel.SaveAllData()
    End Sub

    ''' <summary>
    ''' 打开文件所在位置
    ''' </summary>
    Public Sub OpenFileDir() Implements MainFormContract.IGlobalPresenter.OpenFileDir
        'C:\Users\Windows 10\AppData\Roaming\DesktopTips
        Process.Start("explorer.exe", "/select,""" & GlobalModel.STORAGE_FILENAME & """")
    End Sub

    ''' <summary>
    ''' 注册快捷键
    ''' </summary>
    Public Function RegisterShotcut(handle As IntPtr, key As Keys, id As Integer) As Boolean Implements MainFormContract.IGlobalPresenter.RegisterShotcut
        If Not NativeMethod.RegisterHotKey(handle, id, CommonUtil.GetNativeModifiers(CommonUtil.GetModifiersFromKey(key)), CommonUtil.GetKeyCodeFromKey(key)) Then
            MessageBox.Show("快捷键已被占用，请重新设置", "快捷键", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 注销快捷键
    ''' </summary>
    Public Sub UnregisterShotcut(handle As IntPtr, id As Integer) Implements MainFormContract.IGlobalPresenter.UnregisterShotcut
        NativeMethod.UnregisterHotKey(handle, id)
    End Sub

    ''' <summary>
    ''' 设置快捷键
    ''' </summary>
    Public Sub SetupHotKey(handle As IntPtr, id As Integer) Implements MainFormContract.IGlobalPresenter.SetupHotKey
        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()
        HotKeyDialog.HotkeyEditBox.CurrentKey = setting.HotKey
        HotKeyDialog.CheckBoxIsValid.Checked = setting.IsUseHotKey
        HotKeyDialog.OkCallback = Sub(key As Keys, use As Boolean)
                                      UnregisterShotcut(handle, id)
                                      If Not use OrElse RegisterShotcut(handle, key, id) Then
                                          setting.IsUseHotKey = use
                                          setting.HotKey = key
                                          SettingUtil.SaveAppSettings(setting)
                                      End If
                                  End Sub
        HotKeyDialog.ShowDialog(_view.GetMe())
    End Sub
End Class
