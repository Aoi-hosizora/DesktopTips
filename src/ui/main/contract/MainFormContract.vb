Public Interface MainFormContract

    Public Interface IView
        Function GetMe() As MainForm
    End Interface

    Public Interface IGlobalPresenter
        Function LoadSetting() As SettingUtil.AppSetting    ' 加载设置
        Sub SaveSetting(setting As SettingUtil.AppSetting)  ' 保存设置
        Sub LoadList()                                      ' 加载列表文件
        Sub SaveList()                                      ' 保存列表文件
        Sub OpenFileDir()                                   ' 打开文件所在位置
    End Interface

    Public Interface IListPresenter
        'Sub Insert()        ' 插入
        'Sub Delete()        ' 删除
        'Sub Update()        ' 更新
        'Sub Highlight()     ' 高亮
        'Sub PinTop()        ' 置顶
        'Sub PinBottom()     ' 置底
        'Sub MoveUp()        ' 上移
        'Sub MoveDown()      ' 下移
        'Sub Copy()          ' 复制
        'Sub Paste()         ' 粘贴插入
        'Sub SelectAll()     ' 全选
        'Sub Search()        ' 搜索
        'Sub OpenLinks()     ' 打开链接
        'Sub ViewFile()      ' 浏览当前分组内容
    End Interface

    Public Interface IGroupPresenter
        'Sub Insert()    ' 插入
        'Sub Delete()    ' 删除
        'Sub Update()    ' 更新
        'Sub MoveGroup() ' 移动分组顺序
        'Sub MoveItems() ' 移动记录到分组
    End Interface

End Interface
