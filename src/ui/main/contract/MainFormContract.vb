﻿Public Interface MainFormContract

    Public Interface IView
        Function GetMe() As MainForm
        Sub ShowTextForm(title As String, content As String, textColor As Color)
    End Interface

    Public Interface IGlobalPresenter
        Function LoadSetting() As SettingUtil.AppSetting    ' 加载设置
        Sub SaveSetting(setting As SettingUtil.AppSetting)  ' 保存设置
        Sub LoadList()                                      ' 加载列表文件
        Sub SaveList(items As ListBox.ObjectCollection)     ' 保存列表文件
        Sub OpenFileDir()                                   ' 打开文件所在位置
    End Interface

    Public Interface IListPresenter
        Function Insert() As TipItem                                        ' 插入
        Function Delete(items As List(Of TipItem)) As Boolean               ' 删除
        Function Update(ByRef item As TipItem) As Boolean                   ' 更新
        Sub Copy(items As List(Of TipItem))                                 ' 复制
        Function Paste(ByRef item As TipItem) As Boolean                    ' 粘贴插入
        Sub Search()                                                        ' 搜索
        Sub OpenAllLinks(items As List(Of TipItem))                         ' 打开所有链接
        Sub OpenSomeLinks(items As List(Of TipItem))                        ' 打开部分链接
        Sub ViewCurrentList(items As List(Of TipItem))                      ' 浏览当前列表
        Sub ViewHighlightList(items As List(Of TipItem), color As Color)    ' 浏览列表高亮
    End Interface

    Public Interface IGroupPresenter
        'Sub Insert()    ' 插入
        'Sub Delete()    ' 删除
        'Sub Update()    ' 更新
        'Sub MoveGroup() ' 移动分组顺序
        'Sub MoveItems() ' 移动记录到分组
    End Interface

End Interface