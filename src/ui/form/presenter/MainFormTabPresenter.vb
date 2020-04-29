Imports System.Text

Public Class MainFormTabPresenter
    Implements MainFormContract.ITabPresenter

    Private ReadOnly _view As MainFormContract.IView
    Private ReadOnly _globalPresenter As MainFormContract.IGlobalPresenter

    Public Sub New(view As MainFormContract.IView)
        _view = view
        _globalPresenter = New MainFormGlobalPresenter(_view)
    End Sub

    Public Function Insert() As Boolean Implements MainFormContract.ITabPresenter.Insert
        Dim tabName As String = InputBox("新分组的标题：", "新建", "新建分组").Trim()
        If tabName <> "" Then
            If GlobalModel.CheckDuplicateTab(tabName, GlobalModel.Tabs) Then
                MessageBoxEx.Show("分组标题 """ & tabName & """ 已存在。",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, _view.GetMe())
            Else
                Dim tab As New Tab(tabName)
                GlobalModel.Tabs.Add(tab)
                _globalPresenter.SaveFile()
                Return True
            End If
        End If
        Return False
    End Function

    Public Function Delete(tab As Tab) As Boolean Implements MainFormContract.ITabPresenter.Delete
        If tab.Tips.Count <> 0 Then
            MessageBoxEx.Show($"分组内存在 {tab.Tips.Count} 条记录，请先将标签移动到别的分组。",
                "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, _view.GetMe())
        Else
            Dim ok = MessageBoxEx.Show($"是否删除分组 ""{tab.Title}""？",
                "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, _view.GetMe())
            If ok = vbOK Then
                GlobalModel.Tabs.Remove(tab)
                _globalPresenter.SaveFile()
                Return True
            End If
        End If
        Return False
    End Function

    Public Function Update(tab As Tab) As Boolean Implements MainFormContract.ITabPresenter.Update
        Dim newName As String = InputBox($"重命名分组 ""{tab.Title}"" 为: ", "重命名", tab.Title).Trim()
        If newName <> "" Then
            If GlobalModel.CheckDuplicateTab(newName, GlobalModel.Tabs) Then
                MessageBoxEx.Show($"分组标题 ""{newName}"" 已存在。",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, _view.GetMe())
            Else
                tab.Title = newName
                _globalPresenter.SaveFile()
                Return True
            End If
        End If
        Return False
    End Function

    Public Function MoveItems(items As IEnumerable(Of TipItem), src As Tab, dest As Tab) As Boolean Implements MainFormContract.ITabPresenter.MoveItems
        Dim tipItems As IEnumerable(Of TipItem) = If(TryCast(items, TipItem()), items.ToArray())
        Dim flag As String
        If src.Tips.Count = items.Count Then
            flag = $"确定将当前分组 ""{src.Title}"" 的全部内容 (共 {src.Tips.Count} 项) 移动至分组 ""{dest.Title}"" 吗？"
        Else
            Dim sb As New StringBuilder
            For Each item As TipItem In tipItems
                sb.AppendLine(item.Content)
            Next
            flag = $"确定将当前分组 ""{src.Title}"" 所选内容 (共 {items.Count} 项) 移动至分组 ""{dest.Title}"" 吗？{vbNewLine}{vbNewLine}{sb.ToString()}"
        End If

        Dim ok = MessageBoxEx.Show(flag,
            "移动至分组...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, _view.GetMe())
        If ok = vbOK Then
            For Each item As TipItem In tipItems
                dest.Tips.Add(item)
                src.Tips.Remove(item)
            Next
            _globalPresenter.SaveFile()
            Return True
        End If
        Return False
    End Function
End Class
