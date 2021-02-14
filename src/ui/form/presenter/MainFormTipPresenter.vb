Public Class MainFormTipPresenter
    Implements MainFormContract.ITipPresenter

    Private ReadOnly _view As MainFormContract.IView
    Private ReadOnly _globalPresenter As MainFormContract.IGlobalPresenter

    Public Sub New(view As MainFormContract.IView)
        _view = view
        _globalPresenter = New MainFormGlobalPresenter(_view)
    End Sub

    Public Function Insert() As Boolean Implements MainFormContract.ITipPresenter.Insert
        Dim msg As String = TipsEditDialog.ShowDialog("新的标签：", "添加").Trim()
        If msg <> "" Then
            Dim tip As New TipItem(GlobalModel.CurrentTab.Tips.Count, msg)
            GlobalModel.CurrentTab.Tips.Add(tip)
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function

    Public Function Delete(items As IEnumerable(Of TipItem)) As Boolean Implements MainFormContract.ITipPresenter.Delete
        items = items.ToList()
        Dim tipString As String = String.Join(vbNewLine, items.Select(Function(t) t.Content))
        Dim ok = MessageBoxEx.Show($"确定删除以下 {items.Count} 个标签吗？{vbNewLine}{vbNewLine}{tipString}", "删除",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question, _view.GetMe())
        If ok = vbOK Then
            For Each item As TipItem In items
                GlobalModel.CurrentTab.Tips.Remove(item)
                ' GlobalModel.CurrentTab.Tips.RemoveAll(Function(t) t.Id = item.Id)
            Next
            GlobalModel.ReorderTips(GlobalModel.CurrentTab.Tips)
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
        Dim newStr As String = TipsEditDialog.ShowDialog($"修改标签 ""{content}"" 为：", "修改", item.Content).Trim()
        If newStr <> "" And newStr <> item.Content Then
            item.Content = newStr
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
            Dim ok = MessageBoxEx.Show($"是否向当前标签项 ""{item.Content}"" 末尾添加剪贴板内容 ""{clip}""？", "粘贴",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, _view.GetMe(), {"添加空格", "添加逗号", "不添加"})
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
        Dim text As String = InputBox("请输入搜索内容：", "搜索").Trim()
        If text = "" Then
            Return
        End If
        Dim results As List(Of Tuple(Of Integer, Integer)) = GlobalModel.Tabs.SelectMany(Function(tab)
            Return tab.Tips.Where(Function(tip) tip.Content.ToLower().Contains(text.ToLower())).ToList().Select(Function(tip)
                Return New Tuple(Of Integer, Integer)(GlobalModel.Tabs.IndexOf(tab), tab.Tips.IndexOf(tip))
            End Function)
        End Function).ToList()

        SearchDialog.Close()
        If results.Count = 0 Then
            MessageBoxEx.Show($"未找到 ""{text}"" 。", "查找", MessageBoxButtons.OK, MessageBoxIcon.Information, _view.GetMe())
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

    Public Function HighlightTips(items As IEnumerable(Of TipItem), color As TipColor) As Boolean Implements MainFormContract.ITipPresenter.HighlightTips
        If color Is Nothing Then
            For Each item In items
                item.ColorId = -1 ' UnHighlight
            Next
        Else
            For Each item In items
                item.ColorId = color.Id
            Next
        End If
        _globalPresenter.SaveFile()
        Return True
    End Function

    Public Sub ViewList(items As IEnumerable(Of TipItem), highlight As Boolean) Implements MainFormContract.ITipPresenter.ViewList
        If Not highlight Then
            Dim tipString = String.Join(vbNewLine, items.Select(Function(t) t.Content & If(t.IsHighLight, $" [高亮 {t.Color.Name}]", "")))
            _view.ShowTextForm($"浏览列表 (共 {items.Count} 项)", tipString.ToString(), Color.Black)
        Else
            Dim highLightItems = items.Where(Function(t) t.IsHighLight).Select(Function(t) $"{t.Content} [{t.Color.Name}]")
            Dim tipString = String.Join(vbNewLine, highLightItems)
            _view.ShowTextForm($"浏览高亮 (共 {highLightItems.Count} 项)", tipString.ToString(), Color.Black)
        End If
    End Sub

    Public Function GetLinks(items As IEnumerable(Of TipItem)) As IEnumerable(Of String) Implements MainFormContract.ITipPresenter.GetLinks
        Return items.SelectMany(Function(t) t.Content.Split(New Char() {" ", vbCrLf, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)).
            Where(Function(s) s.StartsWith("http://") Or s.StartsWith("https://"))
    End Function

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

    Public Sub ViewAllLinks(items As IEnumerable(Of TipItem)) Implements MainFormContract.ITipPresenter.ViewAllLinks
        Dim links As List(Of String) = GetLinks(items).ToList()
        If links.Count = 0 Then
            MessageBoxEx.Show("所选项不包含任何链接。", "打开链接", MessageBoxButtons.OK, MessageBoxIcon.Error, _view.GetMe())
        Else
            LinkDialog.Close()
            LinkDialog.GetFunc = Function() links
            LinkDialog.OpenBrowserFunc = Sub(l As IEnumerable(Of String), inNew As Boolean) OpenInDefaultBrowser(l, inNew)
            LinkDialog.Show(_view.GetMe())
        End If
    End Sub

    Public Sub SetupHighlightColor(cb As Action) Implements MainFormContract.ITipPresenter.SetupHighlightColor
        ColorDialog.SaveCallback = Sub()
            _globalPresenter.SaveFile()
            cb()
        End Sub
        ColorDialog.ShowDialog(_view.GetMe())
    End Sub
End Class
