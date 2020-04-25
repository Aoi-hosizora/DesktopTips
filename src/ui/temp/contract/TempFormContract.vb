Public Interface TempFormContract

    Public Interface IView
        Function GetMe() As TempForm                                                        ' 获得 this 指针
        Sub ShowTextForm(title As String, content As String, textColor As Color)            ' 显示文本对话框
    End Interface

    Public Interface IGlobalPresenter
        Sub LoadList()                                                                      ' 加载列表文件
        Sub SaveList()                                                                      ' 保存列表文件
        Sub OpenFileDir()                                                                   ' 打开文件所在位置
        Sub SetupHotKey(handle As IntPtr, id As Integer)                                    ' 设置快捷键
        Function RegisterShotcut(handle As IntPtr, key As Keys, id As Integer) As Boolean   ' 注册快捷键
        Sub UnregisterShotcut(handle As IntPtr, id As Integer)                              ' 注销快捷键
    End Interface

    Public Interface IListPresenter
        Sub Insert()                                            ' 插入
        Sub Delete(ByRef items As IEnumerable(Of TipItem))      ' 删除
        Sub Update(ByRef item As TipItem)                       ' 更新
        Sub Copy(items As IEnumerable(Of TipItem))              ' 复制
        Sub Paste(ByRef item As TipItem)                        ' 粘贴插入
        Sub MoveUp(item As TipItem)                             ' 上移
        Sub MoveDown(item As TipItem)                           ' 下移
        Sub MoveTop(item As TipItem)                            ' 置顶
        Sub MoveBottom(item As TipItem)                         ' 置底
        Sub Search()                                            ' 搜索
        Sub OpenAllLinks(items As IEnumerable(Of TipItem))      ' 打开所有链接
        Sub OpenSomeLinks(items As IEnumerable(Of TipItem))     ' 打开部分链接
        Sub ViewCurrentList(items As IEnumerable(Of TipItem))   ' 浏览当前列表
    End Interface

End Interface
