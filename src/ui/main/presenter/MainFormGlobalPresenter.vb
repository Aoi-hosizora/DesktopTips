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

End Class
