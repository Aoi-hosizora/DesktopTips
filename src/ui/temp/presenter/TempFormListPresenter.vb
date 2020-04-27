Imports System.Text

Public Class TempFormListPresenter
    Implements TempFormContract.IListPresenter

    Private ReadOnly _view As TempFormContract.IView
    Private ReadOnly _globalPresenter As TempFormContract.IGlobalPresenter

    Public Sub New(view As TempFormContract.IView)
        _view = view
        _globalPresenter = New TempFormGlobalPresenter(_view)
    End Sub

    ''' <summary>
    ''' 插入标签
    ''' </summary>
    Public Function Insert() As Boolean Implements TempFormContract.IListPresenter.Insert
        Dim msg As String = InputBox("新的提醒标签：", "添加").Trim()
        If msg <> "" Then
            Dim tip As New TipItem(msg)
            GlobalModel.CurrentTab.Tips.Add(tip)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 删除标签
    ''' </summary>
    Public Function Delete(ByRef items As IEnumerable(Of TipItem)) As Boolean Implements TempFormContract.IListPresenter.Delete
        Dim sb As New StringBuilder
        For Each item As TipItem In items
            sb.AppendLine(item.Content)
        Next
        Dim ok = MessageBoxEx.Show($"确定删除以下 {items.Count} 个提醒标签吗？{vbNewLine}{vbNewLine}{sb.ToString()}",
            "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
            _view.GetMe())
        If ok = vbOK Then
            For Each item In items
                GlobalModel.CurrentTab.Tips.Remove(item)
            Next
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 修改标签
    ''' </summary>
    Public Function Update(ByRef item As TipItem) As Boolean Implements TempFormContract.IListPresenter.Update
        Dim newStr As String = InputBox($"修改提醒标签 ""{item.Content}"" 为：", "修改", item.Content).Trim()
        If newStr <> "" And newStr <> item.Content Then
            item.Content = newStr
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 复制标签
    ''' </summary>
    Public Sub Copy(items As IEnumerable(Of TipItem)) Implements TempFormContract.IListPresenter.Copy
        Dim sb As New StringBuilder
        For Each item As TipItem In items
            sb.AppendLine(item.Content)
        Next
        Clipboard.SetText(sb.ToString())
    End Sub

    ''' <summary>
    ''' 粘贴到最后
    ''' </summary>
    Public Function Paste(ByRef item As TipItem) As Boolean Implements TempFormContract.IListPresenter.Paste
        Dim clip As String = Clipboard.GetText().Trim()
        If Not String.IsNullOrWhiteSpace(clip) Then
            Dim ok = MessageBoxEx.Show($"是否向当前选中项 ""{item.Content}"" 末尾添加剪贴板内容 ""{clip}""？",
                "附加内容", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                _view.GetMe())
            If ok = vbOK Then
                item.Content += " " & clip
                _globalPresenter.SaveFile()
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 上移
    ''' </summary>
    Public Function MoveUp(item As TipItem) As Boolean Implements TempFormContract.IListPresenter.MoveUp
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        If idx >= 1 Then
            GlobalModel.CurrentTab.Tips.RemoveAt(idx)
            GlobalModel.CurrentTab.Tips.Insert(idx - 1, item)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 下移
    ''' </summary>
    Public Function MoveDown(item As TipItem) As Boolean Implements TempFormContract.IListPresenter.MoveDown
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        If idx <= GlobalModel.CurrentTab.Tips.Count - 2 Then
            GlobalModel.CurrentTab.Tips.RemoveAt(idx)
            GlobalModel.CurrentTab.Tips.Insert(idx + 1, item)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 置顶
    ''' </summary>
    Public Function MoveTop(item As TipItem) As Boolean Implements TempFormContract.IListPresenter.MoveTop
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        GlobalModel.CurrentTab.Tips.RemoveAt(idx)
        GlobalModel.CurrentTab.Tips.Insert(0, item)
        _globalPresenter.SaveFile()
        Return True
    End Function

    ''' <summary>
    ''' 置底
    ''' </summary>
    Public Function MoveBottom(item As TipItem) As Boolean Implements TempFormContract.IListPresenter.MoveBottom
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        GlobalModel.CurrentTab.Tips.RemoveAt(idx)
        GlobalModel.CurrentTab.Tips.Add(item)
        _globalPresenter.SaveFile()
        Return True
    End Function

    ''' <summary>
    ''' 搜索
    ''' </summary>
    Public Sub Search() Implements TempFormContract.IListPresenter.Search
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

    ''' <summary>
    ''' 浏览当前列表
    ''' </summary>
    Public Sub ViewCurrentList(items As IEnumerable(Of TipItem)) Implements TempFormContract.IListPresenter.ViewCurrentList
        Dim sb As New StringBuilder
        For Each item As TipItem In items.Cast(Of TipItem)()
            sb.AppendLine(item.Content & If(item.IsHighLight, " [高亮]", ""))
        Next
        _view.ShowTextForm($"浏览文件 (共 {items.Count} 项)", sb.ToString(), Color.Black)
    End Sub

    ''' <summary>
    ''' 获取当前列表所选中的所有链接
    ''' </summary>
    Private Function getLinks(items As IEnumerable(Of TipItem)) As List(Of String)
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

    ''' <summary>
    ''' 打开所有链接
    ''' </summary>
    Public Sub OpenAllLinks(items As IEnumerable(Of TipItem)) Implements TempFormContract.IListPresenter.OpenAllLinks
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

    ''' <summary>
    ''' 浏览所有链接
    ''' </summary>
    Public Sub ViewAllLinks(items As IEnumerable(Of TipItem)) Implements TempFormContract.IListPresenter.ViewAllLinks
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
