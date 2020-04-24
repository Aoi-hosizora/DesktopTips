Public Interface TempFormContract

    Public Interface IView
        Function GetMe() As TempForm
    End Interface

    Public Interface IGlobalPresenter
        Sub LoadList()                                                                      ' 加载列表文件
        Sub SaveList()                                                                      ' 保存列表文件
        Sub OpenFileDir()                                                                   ' 打开文件所在位置
        Sub SetupHotKey(handle As IntPtr, id As Integer)                                    ' 设置快捷键
        Function RegisterShotcut(handle As IntPtr, key As Keys, id As Integer) As Boolean   ' 注册快捷键
        Sub UnregisterShotcut(handle As IntPtr, id As Integer)                              ' 注销快捷键
    End Interface

End Interface
