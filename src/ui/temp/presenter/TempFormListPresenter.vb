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
    Public Sub Insert() Implements TempFormContract.IListPresenter.Insert
        Dim msg As String = InputBox("新的提醒标签：", "添加")
        msg = msg.Trim()
        If String.IsNullOrWhiteSpace(msg) Then
            Dim tip As TipItem = New TipItem(msg)
            GlobalModel.CurrentTab.Tips.Add(tip)
            _globalPresenter.SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 删除标签
    ''' </summary>
    Public Sub Delete(ByRef items As IEnumerable(Of TipItem)) Implements TempFormContract.IListPresenter.Delete
        Dim sb As New StringBuilder
        For Each item As TipItem In items
            sb.AppendLine(item.Content)
        Next
        Dim ok As Integer = MessageBoxEx.Show("确定删除以下 " & items.Count & " 个提醒标签吗？" & Chr(10) & Chr(10) & sb.ToString,
            "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, _view.GetMe())
        If ok = vbOK Then
            For Each item In items
                GlobalModel.CurrentTab.Tips.Remove(item)
            Next
        End If
        _globalPresenter.SaveList()
    End Sub

    ''' <summary>
    ''' 修改标签
    ''' </summary>
    Public Sub Update(ByRef item As TipItem) Implements TempFormContract.IListPresenter.Update
        Dim newstr As String = InputBox("修改提醒标签 """ & item.Content & """ 为：", "修改", item.Content)
        newstr = newstr.Trim()
        If newstr <> "" And newstr <> item.Content Then
            item.Content = newstr
            _globalPresenter.SaveList()
        End If
    End Sub

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
    Public Sub Paste(ByRef item As TipItem) Implements TempFormContract.IListPresenter.Paste
        Dim clip As String = Clipboard.GetText().Trim()
        If Not String.IsNullOrWhiteSpace(clip) Then
            Dim ok As DialogResult = MessageBoxEx.Show("是否向当前选中项 """ & item.Content & """ 末尾添加剪贴板内容 """ & clip & """？",
               "附加内容", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, _view.GetMe())
            If ok = vbOK Then
                item.Content += " " & clip
                _globalPresenter.SaveList()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 上移
    ''' </summary>
    Public Sub MoveUp(item As TipItem) Implements TempFormContract.IListPresenter.MoveUp
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        If idx >= 1 Then
            GlobalModel.CurrentTab.Tips.RemoveAt(idx)
            GlobalModel.CurrentTab.Tips.Insert(idx - 1, item)
            _globalPresenter.SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 下移
    ''' </summary>
    Public Sub MoveDown(item As TipItem) Implements TempFormContract.IListPresenter.MoveDown
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        If idx <= GlobalModel.CurrentTab.Tips.Count - 2 Then
            GlobalModel.CurrentTab.Tips.RemoveAt(idx)
            GlobalModel.CurrentTab.Tips.Insert(idx + 1, item)
            _globalPresenter.SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 置顶
    ''' </summary>
    Public Sub MoveTop(item As TipItem) Implements TempFormContract.IListPresenter.MoveTop
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        GlobalModel.CurrentTab.Tips.RemoveAt(idx)
        GlobalModel.CurrentTab.Tips.Insert(0, item)
        _globalPresenter.SaveList()
    End Sub

    ''' <summary>
    ''' 置底
    ''' </summary>
    Public Sub MoveBottom(item As TipItem) Implements TempFormContract.IListPresenter.MoveBottom
        Dim idx = GlobalModel.CurrentTab.Tips.IndexOf(item)
        GlobalModel.CurrentTab.Tips.RemoveAt(idx)
        GlobalModel.CurrentTab.Tips.Insert(GlobalModel.CurrentTab.Tips.Count - 1, item)
        _globalPresenter.SaveList()
    End Sub
End Class
