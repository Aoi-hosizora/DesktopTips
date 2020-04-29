Imports System.Text

Public Class MainFormListPresenter
    Implements MainFormContract.IListPresenter

    Private ReadOnly _view As MainFormContract.IView

    Public Sub New(view As MainFormContract.IView)
        _view = view
    End Sub

    Public Function Insert() As TipItem Implements MainFormContract.IListPresenter.Insert
        Dim msg As String = InputBox("新的提醒标签：", "添加")
        If msg <> "" Then
            Return New TipItem(msg.Trim())
        End If
        Return Nothing
    End Function

    Public Function Delete(items As IEnumerable(Of TipItem)) As Boolean Implements MainFormContract.IListPresenter.Delete
        Dim sb As New StringBuilder
        For Each item As TipItem In items
            sb.AppendLine(item.Content)
        Next
        Dim ok As Integer = MessageBoxEx.Show(
            "确定删除以下 " & items.Count & " 个提醒标签吗？" & Chr(10) & Chr(10) & sb.ToString,
            "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
            _view.GetMe())
        If ok = vbOK Then
            Return True
        End If
        Return False
    End Function

    Public Function Update(ByRef item As TipItem) As Boolean Implements MainFormContract.IListPresenter.Update
        Dim newstr As String = InputBox("修改提醒标签 """ & item.Content & """ 为：", "修改", item.Content)
        If newstr <> "" And newstr <> item.Content Then
            item.Content = newstr.Trim()
            Return True
        End If
        Return False
    End Function

    Public Sub Copy(items As IEnumerable(Of TipItem)) Implements MainFormContract.IListPresenter.Copy
        Dim sb As New StringBuilder
        For Each item As TipItem In items
            sb.AppendLine(item.Content)
        Next
        Clipboard.SetText(sb.ToString())
    End Sub

    Public Function Paste(ByRef item As TipItem) As Boolean Implements MainFormContract.IListPresenter.Paste
        Dim clip As String = Clipboard.GetText
        If Not String.IsNullOrWhiteSpace(clip) Then
            Dim ok As DialogResult = MessageBoxEx.Show(
               "是否向当前选中项 """ & item.Content & """ 末尾添加剪贴板内容 """ & clip & """？",
               "附加内容", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
               _view.GetMe())

            If ok = vbOK Then
                item.Content += " " & Clipboard.GetText().Trim()
                Return True
            End If
        End If
        Return False
    End Function

    Public Sub Search() Implements MainFormContract.IListPresenter.Search
        Dim result As New List(Of Tuple(Of Integer, Integer))
        Dim text As String = InputBox("请输入查找的文字：", "查找").Trim()
        If text = "" Then
            Exit Sub
        End If

        For Each tab As Tab In GlobalModel.Tabs
            For Each tip As TipItem In tab.Tips
                If tip.Content.ToLower.Contains(text.ToLower) Then
                    result.Add(New Tuple(Of Integer, Integer)(GlobalModel.Tabs.IndexOf(tab), tab.Tips.IndexOf(tip)))
                End If
            Next
        Next

        SearchDialog.Close()
        If result.Count = 0 Then
            MessageBoxEx.Show("未找到 """ & text & """ 。",
                "查找", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                _view.GetMe())
        Else
            SearchDialog.SearchText = text
            ' SearchDialog.SearchResult = result
            SearchDialog.Show(_view.GetMe())
        End If
    End Sub

    Public Sub ViewCurrentList(items As IEnumerable(Of TipItem)) Implements MainFormContract.IListPresenter.ViewCurrentList
        Dim sb As New StringBuilder
        For Each Item As TipItem In items.Cast(Of TipItem)()
            sb.AppendLine(Item.Content & If(Item.Content, " [高亮]", ""))
        Next
        _view.ShowTextForm("浏览文件 (共 " & items.Count & " 项)", sb.ToString(), Color.Black)
    End Sub

    ''' <summary>
    ''' 获取当前列表所选中的所有链接
    ''' </summary>
    Private Function getLinks(items As IEnumerable(Of TipItem)) As List(Of String)
        Dim res As New List(Of String)
        For Each item In items
            Dim sp As String() = item.Content.Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
            For Each link In sp
                If link.StartsWith("http://") Or link.StartsWith("https://") Then
                    res.Add(link)
                End If
            Next
        Next
        Return res
    End Function

    Public Sub OpenAllLinks(items As IEnumerable(Of TipItem)) Implements MainFormContract.IListPresenter.OpenAllLinks
        Dim links As List(Of String) = getLinks(items)
        If links.Count = 0 Then
            MessageBox.Show("所选项不包含任何链接。", "打开链接", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim ok As MessageBoxButtons = MessageBox.Show(
                "是否打开以下 " & links.Count & " 个链接：" + Chr(10) + Chr(10) + String.Join(Chr(10), links),
                "打开链接", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If ok = vbOK Then
                CommonUtil.OpenWebsInDefaultBrowser(links)
            End If
        End If
    End Sub

    Public Sub OpenSomeLinks(items As IEnumerable(Of TipItem)) Implements MainFormContract.IListPresenter.OpenSomeLinks
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

    Public Sub ViewHighlightList(items As IEnumerable(Of TipItem), color As Color) Implements MainFormContract.IListPresenter.ViewHighlightList
        Dim sb As New StringBuilder
        Dim idx As Integer = 0
        For Each Item As TipItem In items.Cast(Of TipItem)()
            If Item.IsHighLight Then
                sb.AppendLine(Item.Content)
                idx += 1
            End If
        Next
        _view.ShowTextForm("查看高亮 (共 " & idx & " 项)", sb.ToString, color)
    End Sub
End Class
