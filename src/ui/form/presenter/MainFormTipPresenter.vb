Imports System.Text

Public Class MainFormTipPresenter
    Implements MainFormContract.ITipPresenter

    Private ReadOnly _view As MainFormContract.IView
    Private ReadOnly _globalPresenter As MainFormContract.IGlobalPresenter

    Public Sub New(view As MainFormContract.IView)
        _view = view
        _globalPresenter = New MainFormGlobalPresenter(_view)
    End Sub

    Public Function Insert() As Boolean Implements MainFormContract.ITipPresenter.Insert
        Dim msg As String = InputBox("新的标签：", "添加").Trim()
        If msg <> "" Then
            Dim tip As New TipItem(msg)
            GlobalModel.CurrentTab.Tips.Add(tip)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function Delete(items As IEnumerable(Of TipItem)) As Boolean Implements MainFormContract.ITipPresenter.Delete
        Dim tipItems As IEnumerable(Of TipItem) = If(TryCast(items, TipItem()), items.ToArray())
        Dim sb As New StringBuilder
        For Each item As TipItem In tipItems
            sb.AppendLine(item.Content)
        Next
        Dim ok = MessageBoxEx.Show($"确定删除以下 {items.Count} 个标签吗？{vbNewLine}{vbNewLine}{sb.ToString()}",
            "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
            _view.GetMe())
        If ok = vbOK Then
            For Each item As TipItem In tipItems
                GlobalModel.CurrentTab.Tips.Remove(item)
            Next
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function Update(item As TipItem) As Boolean Implements MainFormContract.ITipPresenter.Update
        Dim newStr As String = InputBox($"修改标签 ""{item.Content}"" 为：", "修改", item.Content).Trim()
        If newStr <> "" And newStr <> item.Content Then
            item.Content = newStr
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Sub Copy(items As IEnumerable(Of TipItem)) Implements MainFormContract.ITipPresenter.Copy
        Dim sb As New StringBuilder
        For Each item As TipItem In items
            sb.AppendLine(item.Content)
        Next
        Clipboard.SetText(sb.ToString())
    End Sub

    Public Function Paste(item As TipItem) As Boolean Implements MainFormContract.ITipPresenter.Paste
        Dim clip As String = Clipboard.GetText().Trim()
        If Not String.IsNullOrWhiteSpace(clip) Then
            Dim ok = MessageBoxEx.Show($"是否向当前标签项 ""{item.Content}"" 末尾添加剪贴板内容 ""{clip}""？",
                "附加内容", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                _view.GetMe(), New String() {"添加空格", "添加逗号", "不添加"})
            If ok = vbYes Then
                item.Content += " " & clip
                _globalPresenter.SaveFile()
                Return True
            ElseIf ok = vbNo Then
                item.Content += ", " & clip
                _globalPresenter.SaveFile()
                Return True
            End If
        End If
        Return False
    End Function

    Public Function MoveUp(item As TipItem) As Boolean Implements MainFormContract.ITipPresenter.MoveUp
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        If idx >= 1 Then
            GlobalModel.CurrentTab.Tips.RemoveAt(idx)
            GlobalModel.CurrentTab.Tips.Insert(idx - 1, item)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function MoveDown(item As TipItem) As Boolean Implements MainFormContract.ITipPresenter.MoveDown
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        If idx <= GlobalModel.CurrentTab.Tips.Count - 2 Then
            GlobalModel.CurrentTab.Tips.RemoveAt(idx)
            GlobalModel.CurrentTab.Tips.Insert(idx + 1, item)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function MoveTop(item As TipItem) As Boolean Implements MainFormContract.ITipPresenter.MoveTop
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        GlobalModel.CurrentTab.Tips.RemoveAt(idx)
        GlobalModel.CurrentTab.Tips.Insert(0, item)
        _globalPresenter.SaveFile()
        Return True
    End Function

    Public Function MoveBottom(item As TipItem) As Boolean Implements MainFormContract.ITipPresenter.MoveBottom
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        GlobalModel.CurrentTab.Tips.RemoveAt(idx)
        GlobalModel.CurrentTab.Tips.Add(item)
        _globalPresenter.SaveFile()
        Return True
    End Function

    Public Sub Search() Implements MainFormContract.ITipPresenter.Search
        Dim results As New List(Of Tuple(Of Integer, Integer))
        Dim text As String = InputBox("请输入搜索内容：", "搜索").Trim()
        If text = "" Then
            Return
        End If
        For Each tab As Tab In GlobalModel.Tabs
            For Each tip As TipItem In tab.Tips
                If tip.Content.ToLower().Contains(text.ToLower()) Then
                    results.Add(New Tuple(Of Integer, Integer)(GlobalModel.Tabs.IndexOf(tab), tab.Tips.IndexOf(tip)))
                End If
            Next
        Next
        SearchDialog.Close()
        If results.Count = 0 Then
            MessageBoxEx.Show($"未找到 ""{text}"" 。",
                "查找", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                _view.GetMe())
        Else
            SearchDialog.SearchText = text
            SearchDialog.GetFunc = Function() results
            SearchDialog.SearchFunc = Sub() Search()
            SearchDialog.HighlightFunc = Sub(tabIndex As Integer, tipIndex As Integer)
                _view.GetMe().Focus()
                _view.GetMe().FormOpacityUp()
                _view.FocusItem(tabIndex, tipIndex)
            End Sub
            SearchDialog.Show(_view.GetMe())
        End If
    End Sub

    Public Sub ViewCurrentList(items As IEnumerable(Of TipItem)) Implements MainFormContract.ITipPresenter.ViewCurrentList
        Dim sb As New StringBuilder
        For Each item As TipItem In items.Cast(Of TipItem)()
            sb.AppendLine(item.Content & If(item.IsHighLight, $" [高亮 {item.Color.Name}]", ""))
        Next
        _view.ShowTextForm($"浏览文件 (共 {items.Count} 项)", sb.ToString(), Color.Black)
    End Sub

    Public Function GetLinks(items As IEnumerable(Of TipItem)) As List(Of String) Implements MainFormContract.ITipPresenter.GetLinks
        Dim res As New List(Of String)
        For Each item As TipItem In items
            For Each link As String In item.Content.Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                If link.StartsWith("http://") Or link.StartsWith("https://") Then
                    res.Add(link)
                End If
            Next
        Next
        Return res
    End Function

    Public Shared Sub OpenInDefaultBrowser(links As IEnumerable(Of String), inNew As Boolean)
        If inNew Then
            Dim browser As String = CommonUtil.GetDefaultBrowserPath().ToLower()
            If browser.Contains("chrome") Then
                Dim p As New Process()
                With p.StartInfo
                    .FileName = browser
                    .Arguments = "--new-window " & String.Join(" ", links)
                    .WindowStyle = ProcessWindowStyle.Minimized
                End With
                p.Start()
                Return
            End If
        End If
        For Each link As String In links
            Process.Start(link)
        Next
    End Sub

    Public Sub OpenAllLinks(items As IEnumerable(Of TipItem), inNew As Boolean) Implements MainFormContract.ITipPresenter.OpenAllLinks
        Dim links As List(Of String) = getLinks(items)
        If links.Count = 0 Then
            MessageBoxEx.Show("所选项不包含任何链接。", "打开链接", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim linksString As String = String.Join(vbNewLine, links)
            Dim ok = MessageBoxEx.Show($"是否打开以下 {links.Count} 个链接：{vbNewLine}{vbNewLine}{linksString}",
                "打开链接", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If ok = vbOK Then
                OpenInDefaultBrowser(links, inNew)
            End If
        End If
    End Sub

    Public Sub ViewAllLinks(items As IEnumerable(Of TipItem), inNew As Boolean) Implements MainFormContract.ITipPresenter.ViewAllLinks
        Dim links As List(Of String) = getLinks(items)
        If links.Count = 0 Then
            MessageBoxEx.Show("所选项不包含任何链接。", "打开链接", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            LinkDialog.Close()
            LinkDialog.GetFunc = Function() links
            LinkDialog.OpenBrowserFunc = Sub(l As IEnumerable(Of String))
                OpenInDefaultBrowser(l, inNew)
            End Sub
            LinkDialog.Show(_view.GetMe())
        End If
    End Sub

    Public Sub SetupHighlightColor(cb As Action) Implements MainFormContract.ITipPresenter.SetupHighlightColor
        ColorDialog.SaveFunc = Sub() cb()
        ColorDialog.ShowDialog(_view.GetMe())
    End Sub
End Class
