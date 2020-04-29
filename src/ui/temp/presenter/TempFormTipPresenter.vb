Imports System.Text

Public Class TempFormTipPresenter
    Implements TempFormContract.ITipPresenter

    Private ReadOnly _view As TempFormContract.IView
    Private ReadOnly _globalPresenter As TempFormContract.IGlobalPresenter

    Public Sub New(view As TempFormContract.IView)
        _view = view
        _globalPresenter = New TempFormGlobalPresenter(_view)
    End Sub

    Public Function Insert() As Boolean Implements TempFormContract.ITipPresenter.Insert
        Dim msg As String = InputBox("新的标签：", "添加").Trim()
        If msg <> "" Then
            Dim tip As New TipItem(msg)
            GlobalModel.CurrentTab.Tips.Add(tip)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function Delete(items As IEnumerable(Of TipItem)) As Boolean Implements TempFormContract.ITipPresenter.Delete
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

    Public Function Update(item As TipItem) As Boolean Implements TempFormContract.ITipPresenter.Update
        Dim newStr As String = InputBox($"修改标签 ""{item.Content}"" 为：", "修改", item.Content).Trim()
        If newStr <> "" And newStr <> item.Content Then
            item.Content = newStr
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Sub Copy(items As IEnumerable(Of TipItem)) Implements TempFormContract.ITipPresenter.Copy
        Dim sb As New StringBuilder
        For Each item As TipItem In items
            sb.AppendLine(item.Content)
        Next
        Clipboard.SetText(sb.ToString())
    End Sub

    Public Function Paste(item As TipItem) As Boolean Implements TempFormContract.ITipPresenter.Paste
        Dim clip As String = Clipboard.GetText().Trim()
        If Not String.IsNullOrWhiteSpace(clip) Then
            Dim ok = MessageBoxEx.Show($"是否向当前标签项 ""{item.Content}"" 末尾添加剪贴板内容 ""{clip}""？",
                "附加内容", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                _view.GetMe(), {"添加空格", "添加逗号", "不添加"})
            If ok = vbYes Then
                item.Content += " " & clip
                _globalPresenter.SaveFile()
                Return True
            Else If ok = vbNo Then
                item.Content += ", " & clip
                _globalPresenter.SaveFile()
                Return True
            End If
        End If
        Return False
    End Function

    Public Function MoveUp(item As TipItem) As Boolean Implements TempFormContract.ITipPresenter.MoveUp
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        If idx >= 1 Then
            GlobalModel.CurrentTab.Tips.RemoveAt(idx)
            GlobalModel.CurrentTab.Tips.Insert(idx - 1, item)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function MoveDown(item As TipItem) As Boolean Implements TempFormContract.ITipPresenter.MoveDown
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        If idx <= GlobalModel.CurrentTab.Tips.Count - 2 Then
            GlobalModel.CurrentTab.Tips.RemoveAt(idx)
            GlobalModel.CurrentTab.Tips.Insert(idx + 1, item)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function MoveTop(item As TipItem) As Boolean Implements TempFormContract.ITipPresenter.MoveTop
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        GlobalModel.CurrentTab.Tips.RemoveAt(idx)
        GlobalModel.CurrentTab.Tips.Insert(0, item)
        _globalPresenter.SaveFile()
        Return True
    End Function

    Public Function MoveBottom(item As TipItem) As Boolean Implements TempFormContract.ITipPresenter.MoveBottom
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        GlobalModel.CurrentTab.Tips.RemoveAt(idx)
        GlobalModel.CurrentTab.Tips.Add(item)
        _globalPresenter.SaveFile()
        Return True
    End Function

    Public Sub Search() Implements TempFormContract.ITipPresenter.Search
        Dim result As New List(Of Tuple(Of Integer, Integer))
        Dim text As String = InputBox("请输入搜索内容：", "搜索").Trim()
        If text = "" Then
            Return
        End If

        For Each tab As Tab In GlobalModel.Tabs
            For Each tip As TipItem In tab.Tips
                If tip.Content.ToLower().Contains(text.ToLower()) Then
                    result.Add(New Tuple(Of Integer, Integer)(GlobalModel.Tabs.IndexOf(tab), tab.Tips.IndexOf(tip)))
                End If
            Next
        Next
        SearchDialog.Close()
        If result.Count = 0 Then
            MessageBoxEx.Show($"未找到 ""{text}"" 。",
                "查找", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                _view.GetMe())
        Else
            SearchDialog.SearchText = text
            SearchDialog.SearchResult = result
            SearchDialog.Show(_view.GetMe())
        End If
    End Sub

    Public Sub ViewCurrentList(items As IEnumerable(Of TipItem)) Implements TempFormContract.ITipPresenter.ViewCurrentList
        Dim sb As New StringBuilder
        For Each item As TipItem In items.Cast(Of TipItem)()
            sb.AppendLine(item.Content & If(item.IsHighLight, $" [高亮 {item.Color.Name}]", ""))
        Next
        _view.ShowTextForm($"浏览文件 (共 {items.Count} 项)", sb.ToString(), Color.Black)
    End Sub

    Public Function GetLinks(items As IEnumerable(Of TipItem)) As List(Of String) Implements TempFormContract.ITipPresenter.GetLinks
        Dim res As New List(Of String)
        For Each item As TipItem In items
            For Each link As String In item.Content.Split(New Char() {" ", ",", ".", ";"}, StringSplitOptions.RemoveEmptyEntries)
                If link.StartsWith("http://") Or link.StartsWith("https://") Then
                    res.Add(link)
                End If
            Next
        Next
        Return res
    End Function

    Public Sub OpenAllLinks(items As IEnumerable(Of TipItem)) Implements TempFormContract.ITipPresenter.OpenAllLinks
        Dim links As List(Of String) = getLinks(items)
        If links.Count = 0 Then
            MessageBox.Show("所选项不包含任何链接。", "打开链接", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim linksString As String = String.Join(vbNewLine, links)
            Dim ok = MessageBox.Show($"是否打开以下 {links.Count} 个链接：{vbNewLine}{vbNewLine}{linksString}",
                "打开链接", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If ok = vbOK Then
                CommonUtil.OpenWebsInDefaultBrowser(links)
            End If
        End If
    End Sub

    Public Sub ViewAllLinks(items As IEnumerable(Of TipItem)) Implements TempFormContract.ITipPresenter.ViewAllLinks
        Dim links As List(Of String) = getLinks(items)
        If links.Count = 0 Then
            MessageBox.Show("所选项不包含任何链接。", "打开链接", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            LinkDialog.ListView.Items.Clear()
            For Each link As String In links
                LinkDialog.ListView.Items.Add(link)
            Next
            LinkDialog.Show(_view.GetMe())
        End If
    End Sub
End Class
