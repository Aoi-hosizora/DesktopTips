Imports System.Text

Public Class TempFormListPresenter
    Implements TempFormContract.IListPresenter

    Private ReadOnly _view As TempFormContract.IView

    Public Sub New(view As TempFormContract.IView)
        _view = view
    End Sub

    Public Sub Insert() Implements TempFormContract.IListPresenter.Insert
        Dim msg As String = InputBox("新的提醒标签：", "添加")
        msg = msg.Trim()
        If String.IsNullOrWhiteSpace(msg) Then
            Dim tip As TipItem = New TipItem(msg)
            GlobalModel.CurrentTab.Tips.Add(tip)
        End If
    End Sub

    Public Sub Delete(items As IEnumerable(Of TipItem)) Implements TempFormContract.IListPresenter.Delete
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
    End Sub

    Public Sub Update(ByRef item As TipItem) Implements TempFormContract.IListPresenter.Update
        Dim newstr As String = InputBox("修改提醒标签 """ & item.Content & """ 为：", "修改", item.Content)
        newstr = newstr.Trim()
        If newstr <> "" And newstr <> item.Content Then
            item.Content = newstr
        End If
    End Sub

    Public Sub Copy(items As IEnumerable(Of TipItem)) Implements TempFormContract.IListPresenter.Copy
        Dim sb As New StringBuilder
        For Each item As TipItem In items
            sb.AppendLine(item.Content)
        Next
        Clipboard.SetText(sb.ToString())
    End Sub

    Public Sub Paste(ByRef item As TipItem) Implements TempFormContract.IListPresenter.Paste
        Dim clip As String = Clipboard.GetText().Trim()
        If Not String.IsNullOrWhiteSpace(clip) Then
            Dim ok As DialogResult = MessageBoxEx.Show("是否向当前选中项 """ & item.Content & """ 末尾添加剪贴板内容 """ & clip & """？",
               "附加内容", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, _view.GetMe())
            If ok = vbOK Then
                item.Content += " " & clip
            End If
        End If
    End Sub
End Class
