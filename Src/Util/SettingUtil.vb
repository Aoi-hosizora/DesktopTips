Public Class SettingUtil

    Private Const AppName As String = "DesktopTips"
    Private Const PosSection As String = "PosSize"
    Private Const FormSection As String = "FormSize"

    Public Structure AppSetting
        Public Top As Integer
        Public Left As Integer
        Public Height As Integer
        Public Width As Integer

        Public MaxOpacity As Double
        Public TopMost As Boolean

        Public IsFold As Boolean

        Public HighLightColor As Color
    End Structure

    Public Shared Sub SaveAppSettings(ByVal appSetting As AppSetting)
        SaveSetting(AppName, PosSection, "Top", appSetting.Top)
        SaveSetting(AppName, PosSection, "Left", appSetting.Left)
        SaveSetting(AppName, PosSection, "Height", appSetting.Height)
        SaveSetting(AppName, PosSection, "Width", appSetting.Width)
        SaveSetting(AppName, FormSection, "Opacity", appSetting.MaxOpacity)
        SaveSetting(AppName, FormSection, "TopMost", appSetting.TopMost)
        SaveSetting(AppName, FormSection, "IsFold", appSetting.IsFold)
        SaveSetting(AppName, FormSection, "HighLight", ColorTranslator.ToHtml(appSetting.HighLightColor))
    End Sub

    Public Shared Function LoadAppSettings() As AppSetting
        Dim setting As AppSetting
        setting.Top = GetSetting(AppName, PosSection, "Top", 20)
        setting.Left = GetSetting(AppName, PosSection, "Left", 20)
        setting.Height = GetSetting(AppName, PosSection, "Height", 163)
        setting.Width = GetSetting(AppName, PosSection, "Width", 200)
        setting.MaxOpacity = GetSetting(AppName, FormSection, "Opacity", 0.6)
        setting.TopMost = GetSetting(AppName, FormSection, "TopMost", False)
        setting.IsFold = GetSetting(AppName, FormSection, "IsFold", True)
        setting.HighLightColor = ColorTranslator.FromHtml(GetSetting(AppName, FormSection, "HighLight", "#FF0000"))
        Return setting
    End Function
End Class
