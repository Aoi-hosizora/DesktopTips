Public Class SettingUtil

    Private Const AppName As String = "DesktopTips"

    Private Const PosSection As String = "PosSize"
    Private Const FormSection As String = "FormSize"
    Private Const NetSection As String = "NetRecord"
    Private Const KeySection As String = "HotKey"

    Public Structure AppSetting
        ' PosSize
        Public Top As Integer
        Public Left As Integer
        Public Height As Integer
        Public Width As Integer

        Public SaveTop As Integer
        Public SaveLeft As Integer

        ' FormSize
        Public MaxOpacity As Double
        Public TopMost As Boolean
        Public IsFold As Boolean
        Public HighLightColor As Color

        ' NetRecord
        Public LastMobileIP As String
        Public LastLocalPort As String

        ' HotKey
        Public IsUseHotKey As Boolean
        Public HotKey As Integer
    End Structure

    Public Shared Sub SaveAppSettings(ByVal appSetting As AppSetting)
        SaveSetting(AppName, PosSection, "Top", appSetting.Top)
        SaveSetting(AppName, PosSection, "Left", appSetting.Left)
        SaveSetting(AppName, PosSection, "Height", appSetting.Height)
        SaveSetting(AppName, PosSection, "Width", appSetting.Width)

        appSetting.SaveTop = If(appSetting.SaveTop = 0, -1, appSetting.SaveTop)
        appSetting.SaveLeft = If(appSetting.SaveLeft = 0, -1, appSetting.SaveLeft)
        SaveSetting(AppName, PosSection, "SaveTop", appSetting.SaveTop)
        SaveSetting(AppName, PosSection, "SaveLeft", appSetting.SaveLeft)

        SaveSetting(AppName, FormSection, "Opacity", appSetting.MaxOpacity)
        SaveSetting(AppName, FormSection, "TopMost", appSetting.TopMost)
        SaveSetting(AppName, FormSection, "IsFold", appSetting.IsFold)
        SaveSetting(AppName, FormSection, "HighLight", ColorTranslator.ToHtml(appSetting.HighLightColor))

        SaveSetting(AppName, NetSection, "LastMobileIP", appSetting.LastMobileIP)
        SaveSetting(AppName, NetSection, "LastLocalPort", appSetting.LastLocalPort)

        SaveSetting(AppName, KeySection, "IsUseHotKey", appSetting.IsUseHotKey)
        SaveSetting(AppName, KeySection, "HotKey", appSetting.HotKey)
    End Sub

    Public Shared Function LoadAppSettings() As AppSetting
        Dim setting As AppSetting
        setting.Top = GetSetting(AppName, PosSection, "Top", 20)
        setting.Left = GetSetting(AppName, PosSection, "Left", 20)
        setting.Height = GetSetting(AppName, PosSection, "Height", 163)
        setting.Width = GetSetting(AppName, PosSection, "Width", 200)

        setting.SaveTop = GetSetting(AppName, PosSection, "SaveTop", -1)
        setting.SaveLeft = GetSetting(AppName, PosSection, "SaveLeft", -1)

        setting.MaxOpacity = GetSetting(AppName, FormSection, "Opacity", 0.6)
        setting.TopMost = GetSetting(AppName, FormSection, "TopMost", False)
        setting.IsFold = GetSetting(AppName, FormSection, "IsFold", True)
        setting.HighLightColor = ColorTranslator.FromHtml(GetSetting(AppName, FormSection, "HighLight", "#FF0000"))

        setting.LastMobileIP = GetSetting(AppName, NetSection, "LastMobileIP", "127.0.0.1:8776")
        setting.LastLocalPort = GetSetting(AppName, NetSection, "LastLocalPort", "8776")

        setting.IsUseHotKey = GetSetting(AppName, KeySection, "IsUseHotKey", True)
        setting.HotKey = GetSetting(AppName, KeySection, "HotKey", Keys.F4)

        Return setting
    End Function
End Class
