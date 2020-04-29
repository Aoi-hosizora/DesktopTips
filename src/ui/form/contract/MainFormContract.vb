﻿Public Interface MainFormContract
    Public Interface IView
        Function GetMe() As MainForm                                                        ' 获得 this 指针
        Sub ShowTextForm(title As String, content As String, textColor As Color)            ' 显示文本对话框
        Sub FocusItem(tabIndex As Integer, tipIndex As Integer)                             ' 选择某一搜索结果
    End Interface

    Public Interface IGlobalPresenter
        Sub LoadFile()                                                                      ' 加载文件
        Sub SaveFile()                                                                      ' 保存文件
        Sub OpenFileDir()                                                                   ' 打开文件所在位置
        Sub SetupHotKey(handle As IntPtr, id As Integer)                                    ' 设置快捷键
        Function RegisterHotKey(handle As IntPtr, key As Keys, id As Integer) As Boolean    ' 注册快捷键
        Sub UnregisterHotKey(handle As IntPtr, id As Integer)                               ' 注销快捷键
    End Interface

    Public Interface ITipPresenter
        Function Insert() As Boolean                                                ' 插入
        Function Delete(items As IEnumerable(Of TipItem)) As Boolean            ' 删除
        Function Update(item As TipItem) As Boolean                             ' 更新
        Sub Copy(items As IEnumerable(Of TipItem))                              ' 复制
        Function Paste(item As TipItem) As Boolean                              ' 粘贴插入
        Function MoveUp(item As TipItem) As Boolean                             ' 上移
        Function MoveDown(item As TipItem) As Boolean                           ' 下移
        Function MoveTop(item As TipItem) As Boolean                            ' 置顶
        Function MoveBottom(item As TipItem) As Boolean                         ' 置底
        Sub Search()                                                            ' 搜索
        Sub ViewCurrentList(items As IEnumerable(Of TipItem))                   ' 浏览当前列表
        Function GetLinks(items As IEnumerable(Of TipItem)) As List(Of String)  ' 打开所有链接
        Sub OpenAllLinks(items As IEnumerable(Of TipItem), inNew As Boolean)    ' 打开所有链接
        Sub ViewAllLinks(items As IEnumerable(Of TipItem), inNew As Boolean)    ' 浏览所有链接
    End Interface

    Public Interface ITabPresenter
        Function Insert() As Boolean                                                                ' 插入
        Function Delete(tab As Tab) As Boolean                                                      ' 删除
        Function Update(tab As Tab) As Boolean                                                      ' 修改
        Function MoveItems(items As IEnumerable(Of TipItem), src As Tab, dest As Tab) As Boolean    ' 修改
    End Interface
End Interface