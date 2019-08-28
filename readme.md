# DesktopTips
+ `VB.net` 编写的桌面备忘录工具
+ (学了两年 SE 了还在用以前学的 `VB.net` 写工具)

### 环境
+ `MSVB` 2010
+ `.NET Framework` 4.0
+ `Dotnetbar` 10.8 (习惯)

### 功能 (v2.0)
+ [x] 存储提醒，支持高亮显示
+ [x] 分组分类，支持分组内移动
+ [ ] ...

### 待改
+ [ ] ...

### 说明
+ 注册表设置
    + `\HKEY_USERS\S-x-x-x-x\Software\VB and VBA Program Settings\DesktopTips\`
    + `FormSize` , `PosSize`

```vb
' Util\SettingUtil.vb
SaveSetting(AppName, PosSection, "Top", Me.Top)
SaveSetting(AppName, FormSection, "Opacity", MaxOpacity)
```

+ 文件访问
	+ 文件系统：`AppData\Roaming`
    + `*.dat` 为分组内容文件 (二进制)
    + `Tabs.dip` 为分组标题文件 (二进制)

```vb
Public Shared StorageFileDir As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DesktopTips"
Private Shared StorageTabsInfo As String = StorageFileDir & "\Tabs.dip"

' C:\Users\xxx\AppData\Roaming\DesktopTips\xxx

Dim StorageTipsName As String = StorageFileDir & "\" & Tab.TabTitle & ".dat"
SaveBinary(StorageTipItems.Item(TabTips.GetTabTipsIndexFromTabTitle(Tab.TabTitle, StorageTipItems)), StorageTipsName)
SaveBinary(StorageTabs, StorageTabsInfo)
```

### 截图
![ScreenShot](./assets/ScreenShot.jpg)