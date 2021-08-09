Imports System.Text
Imports System.Text.RegularExpressions

Public Class MainFormTipPresenter
    Implements MainFormContract.ITipPresenter

    Private ReadOnly _view As MainFormContract.IView
    Private ReadOnly _globalPresenter As MainFormContract.IGlobalPresenter

    Public Sub New(view As MainFormContract.IView)
        _view = view
        _globalPresenter = New MainFormGlobalPresenter(_view)
    End Sub

    Public Function Insert() As Boolean Implements MainFormContract.ITipPresenter.Insert
        Dim markdown = False
        Dim msg As String = TipEditDialog.ShowDialog("新的标签：", "添加", markdown:=markdown)
        msg = CommonUtil.FormatText(msg)
        If msg <> "" Then
            Dim now = DateTime.Now
            Dim tip As New TipItem(msg) With {.Markdown = markdown, .CreatedAt = now, .UpdatedAt = now}
            GlobalModel.CurrentTab.Tips.Add(tip)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function Delete(items As IEnumerable(Of TipItem)) As Boolean Implements MainFormContract.ITipPresenter.Delete
        items = items.ToList()
        Dim sb As New StringBuilder
        Dim toMany = False
        For i = 0 To items.Count - 1
            Dim content = items(i).Content.Replace(vbNewLine, "↴")
            If content.Length > 35 Then
                content = content.Substring(0, 33) & "..."
            End If
            If i <= 25 Then
                sb.AppendLine(content)
            Else
                toMany = True
                Continue For
            End If
        Next i
        If toMany Then
            sb.AppendLine("......")
        End If

        Dim tipsString = sb.ToString()
        Dim ok = MessageBoxEx.Show($"确定删除以下 {items.Count} 个标签吗？{vbNewLine}{vbNewLine}{tipsString}", "删除",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question, _view.GetMe())
        If ok = vbOK Then
            For Each item As TipItem In items
                GlobalModel.CurrentTab.Tips.Remove(item)
            Next
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function Update(item As TipItem) As Boolean Implements MainFormContract.ITipPresenter.Update
        Dim content = item.Content
        If content.Length > 600 Then
            content = content.Substring(0, 600) + "..."
        End If
        Dim saveCallback = Sub(text As String)
                               If text <> "" And text <> item.Content Then
                                   item.Content = text
                                   item.UpdatedAt = DateTime.Now
                                   _globalPresenter.SaveFile()
                               End If
                           End Sub
        Dim markdown = item.Markdown
        Dim newStr As String = TipEditDialog.ShowDialog($"修改标签为：", "修改", item.Content, markdown:=markdown, saveCallback:=saveCallback)
        newStr = CommonUtil.FormatText(newStr)
        If newStr <> "" And (newStr <> item.Content OrElse markdown <> item.Markdown) Then
            item.Content = newStr
            item.Markdown = markdown
            item.UpdatedAt = DateTime.Now
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Sub Copy(items As IEnumerable(Of TipItem)) Implements MainFormContract.ITipPresenter.Copy
        Dim tipString As String = String.Join(vbNewLine, items.Select(Function(t) t.Content))
        Clipboard.SetText(tipString)
    End Sub

    Public Function Paste(item As TipItem) As Boolean Implements MainFormContract.ITipPresenter.Paste
        Dim clip As String = Clipboard.GetText().Trim()
        If clip <> "" Then
            Dim content = item.Content
            If content.Length > 600 Then
                content = content.Substring(0, 600) + "..."
            End If
            Dim ok = MessageBoxEx.Show($"是否向当前标签项 ""{content}"" 末尾添加剪贴板内容 ""{clip}""？", "粘贴",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, _view.GetMe(), {"添加空格", "添加回车", "取消"})
            If ok = vbYes Then
                item.Content &= " " & clip
                item.UpdatedAt = DateTime.Now
                _globalPresenter.SaveFile()
                Return True
            ElseIf ok = vbNo Then
                item.Content &= vbNewLine & clip
                item.UpdatedAt = DateTime.Now
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
        Dim text As String = InputBox("请输入搜索内容 (使用 || 和 &&&& 分隔关键字)：", "搜索").Trim().ToLower()
        If text = "" Then Return

        Dim ors = text.Split({"||"}, StringSplitOptions.RemoveEmptyEntries).Select(
            Function(s)
                Return s.Split({"&&"}, StringSplitOptions.RemoveEmptyEntries).Select(Function(w) w.Trim()).ToList()
            End Function).ToList()
        Dim results As List(Of Tuple(Of Integer, Integer)) = GlobalModel.Tabs.SelectMany(
            Function(tab)
                Return tab.Tips.
                Where(Function(tip)
                          Dim content = tip.Content.ToLower()
                          Return ors.Any(Function(ands) ands.All(Function(a) content.Contains(a))) ' 先 || 后 &&
                      End Function).
                Select(Function(tip)
                           Return New Tuple(Of Integer, Integer)(GlobalModel.Tabs.IndexOf(tab), tab.Tips.IndexOf(tip)) ' tabIdx, tipIdx
                       End Function)
            End Function).ToList()

        If results.Count = 0 Then
            MessageBoxEx.Show($"未找到 ""{text}"" 。", "查找", MessageBoxButtons.OK, MessageBoxIcon.Information, _view.GetMe())
        Else
            SearchDialog.SearchText = text
            SearchDialog.SearchResult = results
            SearchDialog.NewSearchCallback = Sub()
                                                 Search()
                                             End Sub
            SearchDialog.SelectCallback = Sub(tabIndex As Integer, tipIndex As Integer)
                                              _view.GetMe().Focus()
                                              _view.GetMe().FormOpacityUp()
                                              _view.FocusItem(tabIndex, tipIndex)
                                          End Sub
            SearchDialog.ShowDialog()
        End If
    End Sub

    Public Function HighlightTips(items As IEnumerable(Of TipItem), color As TipColor) As Boolean Implements MainFormContract.ITipPresenter.HighlightTips
        Dim tipItems = items.ToList()
        Dim newColor = color
        If tipItems.Count = 1 Then
            If tipItems.First().IsHighLight AndAlso tipItems.First().ColorId = color.Id Then ' 已经高亮并且是当前颜色
                newColor = Nothing
            End If
        ElseIf tipItems.Count > 1 Then
            If tipItems.Where(Function(i) i.ColorId = color.Id).Count = tipItems.Count Then ' 所有选择项都是同种颜色
                newColor = Nothing
            End If
        End If

        Dim newColorId = If(newColor?.Id, -1)
        Dim now = DateTime.Now
        For Each item In tipItems
            item.ColorId = newColorId
            item.UpdatedAt = now
        Next
        _globalPresenter.SaveFile()
        Return True
    End Function

    Public Function CheckTipsDone(items As IEnumerable(Of TipItem)) As Boolean Implements MainFormContract.ITipPresenter.CheckTipsDone
        Dim tipItems = items.ToList()
        Dim toDone = Not tipItems.All(Function(item) item.Done)
        Dim now = DateTime.Now
        For Each item In tipItems
            item.Done = toDone
            item.UpdatedAt = now
        Next
        _globalPresenter.SaveFile()
        Return toDone
    End Function

    Public Sub ViewList(items As IEnumerable(Of TipItem), highlight As Boolean) Implements MainFormContract.ITipPresenter.ViewList
        If highlight Then
            items = items.Where(Function(t) t.IsHighLight)
        End If

        Dim contents As New List(Of Tuple(Of String, Color))
        For Each item In items
            Dim content = item.Content.Replace(vbNewLine, "↴") & If(item.IsHighLight, $" [{item.Color.Name}]", "")
            contents.Add(New Tuple(Of String, Color)(content, If(item.Color?.Color, Color.Black)))
        Next

        If Not highlight Then
            _view.ShowTextForm($"浏览列表 (共 {items.Count} 项)", contents)
        Else
            _view.ShowTextForm($"浏览高亮 (共 {items.Count} 项)", contents)
        End If
    End Sub

    Private Sub OpenInDefaultBrowser(links As IEnumerable(Of String), inNew As Boolean)
        If inNew Then
            Dim browser As String = CommonUtil.GetDefaultBrowserPath().ToLower()
            If browser.Contains("chrome") Then
                Dim p As New Process() With {.StartInfo = New ProcessStartInfo(browser, "--new-window " & String.Join(" ", links))}
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
                p.Start()
                Return
            End If
        End If
        links.ToList().ForEach(Sub(link) Process.Start(link))
    End Sub

    Public Function GetLinks(items As IEnumerable(Of TipItem)) As IEnumerable(Of String) Implements MainFormContract.ITipPresenter.GetLinks
        Return items.SelectMany(Function(t) t.Content.Split(New Char() {" ", vbTab, vbCrLf, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)).
            Where(Function(s) s.StartsWith("http://") Or s.StartsWith("https://"))
    End Function

    Public Sub ViewAllLinks(items As IEnumerable(Of TipItem)) Implements MainFormContract.ITipPresenter.ViewAllLinks
        Dim itemList = items.ToList()
        Dim links As List(Of String) = GetLinks(itemList).ToList()
        If links.Count = 0 Then
            MessageBoxEx.Show($"所选 {itemList.Count} 个标签内不包含任何链接。", "浏览链接", MessageBoxButtons.OK, MessageBoxIcon.Error, _view.GetMe())
        Else
            LinkDialog.Close()
            LinkDialog.Text = "浏览链接"
            LinkDialog.Message = $"所选 {itemList.Count} 个标签包含了 {links.Count} 个链接："
            LinkDialog.Links = links
            LinkDialog.CheckBoxText = "在新窗口打开浏览器"
            LinkDialog.CheckBoxChecked = My.Settings.OpenInNewBrowser
            LinkDialog.CheckBoxChangedCallback = Sub(c)
                                                     My.Settings.OpenInNewBrowser = c
                                                     My.Settings.Save()
                                                 End Sub
            LinkDialog.OkCallback = Sub(l As IEnumerable(Of String), inNew As Boolean) OpenInDefaultBrowser(l, inNew)
            LinkDialog.ShowDialog(_view.GetMe())
        End If
    End Sub

    Public Sub SetupHighlightColor(cb As Action) Implements MainFormContract.ITipPresenter.SetupHighlightColor
        ColorDialog.SaveCallback = Sub()
                                       _globalPresenter.SaveFile()
                                       cb()
                                   End Sub
        ColorDialog.ShowDialog(_view.GetMe())
    End Sub

    Public Function GetTipsLabel(items As IEnumerable(Of TipItem), font As Font, size As Integer) As String Implements MainFormContract.ITipPresenter.GetTipsLabel
        Dim itemList = items.ToList()
        Dim result = ""
        For i = 0 To itemList.Count - 1
            Dim item = itemList.ElementAt(i)
            If i >= 15 Then
                result &= vbNewLine & $"...... (剩下 {itemList.Count - 15} 项)"
                Exit For
            End If
            If result.Length > 0 Then result &= vbNewLine

            Dim content = item.Content.Replace(vbNewLine, "↴")
            result &= CommonUtil.TrimForEllipsis(content, font, size)
        Next
        Return result
    End Function
End Class
