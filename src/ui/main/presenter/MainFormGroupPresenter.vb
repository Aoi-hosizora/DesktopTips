Imports System.Text

Public Class MainFormGroupPresenter
    Implements MainFormContract.IGroupPresenter

    Private ReadOnly _view As MainFormContract.IView

    Public Sub New(view As MainFormContract.IView)
        _view = view
    End Sub

    Public Function Insert() As Tab Implements MainFormContract.IGroupPresenter.Insert
        Dim tabName As String = InputBox("请输入新分组的标题: ", "新建", "分组")
        tabName = tabName.Trim()
        If tabName <> "" Then
            If Tab.CheckDuplicateTab(tabName, GlobalModel.Tabs) IsNot Nothing Then
                MessageBoxEx.Show("分组标题 """ & tabName & """ 已存在。",
                                  "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                                  _view.GetMe())
            Else
                Dim t As New Tab(tabName)
                GlobalModel.Tabs.Add(t)
                GlobalModel.SaveAllData()
                Return t
            End If
        End If
        Return Nothing
    End Function

    Public Function Delete(title As String) As Boolean Implements MainFormContract.IGroupPresenter.Delete
        Dim tab As Tab = tab.GetTabFromTitle(title)
        If tab.Tips.Count <> 0 Then
            MessageBoxEx.Show("分组内存在 " & tab.Tips.Count & " 条记录无法删除，请先将记录移动到别的分组。",
                              "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                              _view.GetMe())
        Else
            Dim ok = MessageBoxEx.Show("是否删除分组 """ + title + """？",
                                       "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                                       _view.GetMe())
            If ok = vbOK Then
                GlobalModel.Tabs.RemoveAt(GlobalModel.Tabs.IndexOf(tab))
                GlobalModel.SaveAllData()
                Return True
            End If
        End If
        Return False
    End Function

    Public Function Update(oldName As String) As String Implements MainFormContract.IGroupPresenter.Update
        Dim newName As String = InputBox("重命名分组 """ & oldName & """ 为: ", "重命名", oldName)
        newName = newName.Trim()
        If newName <> "" Then
            If Tab.CheckDuplicateTab(newName, GlobalModel.Tabs) IsNot Nothing Then
                MessageBoxEx.Show("分组标题 """ & newName & """ 已存在。",
                                  "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                                  _view.GetMe())
            Else
                Tab.GetTabFromTitle(oldName).Title = newName
                GlobalModel.SaveAllData()
                Return newName
            End If
        End If
        Return ""
    End Function

    Public Function MoveItems(isAll As Boolean, items As IEnumerable(Of TipItem), src As String, dest As String) As Boolean Implements MainFormContract.IGroupPresenter.MoveItems
        Dim flag As String
        If isAll Then
            flag = "确定将当前分组 """ & src & """ 的全部内容 (共 " & items.Count & " 项) 移动至分组 """ & dest & """ 吗？"
        Else
            Dim sb As New StringBuilder
            For Each Item As TipItem In items
                sb.AppendLine(Item.TipContent)
            Next
            flag = "确定将当前分组 """ & src & """ 所选内容 (共 " & items.Count & " 项) 移动至分组 """ & dest & """ 吗？" & Chr(10) & Chr(10) & sb.ToString()
        End If

        Dim ok = MessageBoxEx.Show(flag, "移动至分组...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, _view.GetMe())
        If ok = vbOK Then
            For Each item As TipItem In items
                Tab.GetTabFromTitle(dest).Tips.Add(item)
                Tab.GetTabFromTitle(src).Tips.Remove(item)
            Next
            GlobalModel.SaveAllData()
            Return True
        End If
        Return False
    End Function
End Class
