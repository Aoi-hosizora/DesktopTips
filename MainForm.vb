Imports System.IO
Imports System.Text

Public Class MainForm

    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Private Declare Function SetCursorPos Lib "user32" (ByVal x As Long, ByVal y As Long) As Long
    ' Private Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)

    Enum MouseEvent
        MOUSEEVENTF_LEFTDOWN = &H2      '左键按下
        MOUSEEVENTF_LEFTUP = &H4        '左键释放
        MOUSEEVENTF_MIDDLEDOWN = &H20   '中键按下
        MOUSEEVENTF_MIDDLEUP = &H40     '中键释放
        MOUSEEVENTF_RIGHTDOWN = &H8     '右键按下
        MOUSEEVENTF_RIGHTUP = &H10      '右键释放
        MOUSEEVENTF_MOVE = &H1          '指针移动
    End Enum

#Region "PosMove"

    Private PushDownMouseInScreen As Point
    Private PushDownWindowPos As Point
    Private PushDownWindowSize As Point
    Private IsMouseDown As Boolean
    Private IsChangeSize As Boolean

    Private Sub ListView_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            IsMouseDown = True
        End If
        PushDownMouseInScreen = Cursor.Position
        PushDownWindowPos = New Point(Me.Left, Me.Top)
        PushDownWindowSize = New Point(Me.Width, Me.Height)
    End Sub

    Private Sub ListView_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseMove
        If e.X > sender.Width - 25 Or IsChangeSize = True Then
            Me.Cursor = Cursors.SizeWE
            If IsMouseDown Then
                IsChangeSize = True
                Me.Width = PushDownWindowSize.X + Cursor.Position.X - PushDownMouseInScreen.X
            End If
        Else
            Me.Cursor = Cursors.Default
            If IsMouseDown And Not IsChangeSize Then
                Me.Top = PushDownWindowPos.Y + Cursor.Position.Y - PushDownMouseInScreen.Y
                Me.Left = PushDownWindowPos.X + Cursor.Position.X - PushDownMouseInScreen.X
            End If
        End If
    End Sub

    Private Sub ListView_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseUp
        IsMouseDown = False
        IsChangeSize = False
    End Sub

#End Region

#Region "Show End Act DAct"

    ''' <summary>
    ''' 透明度
    ''' </summary>
    Private MaxOpacity As Double = 0.75
    ''' <summary>
    ''' 透明速度
    ''' </summary>
    Private Const OpacitySpeed As Double = 0.08

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''' Load / End Form ''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub TimerShowForm_Tick(sender As System.Object, e As System.EventArgs) Handles TimerShowForm.Tick
        Me.Opacity += OpacitySpeed
        Me.Top += 1
        If Me.Opacity >= MaxOpacity Then
            TimerShowForm.Enabled = False
            Me.Opacity = MaxOpacity
        End If
    End Sub

    Private Sub TimerEndForm_Tick(sender As System.Object, e As System.EventArgs) Handles TimerEndForm.Tick
        Me.Opacity -= OpacitySpeed
        Me.Top -= 1
        If Me.Opacity <= 0 Then
            TimerEndForm.Enabled = False
            Me.Close()
        End If
        If TimerShowForm.Enabled = True Then
            TimerShowForm.Enabled = False
        End If
    End Sub

    Private Sub ButtonCloseForm_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCloseForm.Click, PopMenuButtonExit.Click
        TimerEndForm.Enabled = True
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''' Act / DAct Form ''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub TimerMouseIn_Tick(sender As System.Object, e As System.EventArgs) Handles TimerMouseIn.Tick
        Me.Opacity += 0.02
        If Me.Opacity >= 1 Then
            TimerMouseIn.Stop()
            TimerMouseIn.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseOut_Tick(sender As System.Object, e As System.EventArgs) Handles TimerMouseOut.Tick
        Me.Opacity -= 0.02
        If Me.Opacity <= MaxOpacity Then
            Me.Opacity = MaxOpacity
            TimerMouseOut.Stop()
            TimerMouseOut.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 设置移入移出事件
    ''' </summary>
    Private Sub SetupMouseEnterLeave()
        AddHandler Me.MouseMove, AddressOf FormMouseMove
        AddHandler Me.MouseLeave, AddressOf FormMouseLeave
        For Each Ctrl As Control In Me.Controls
            AddHandler Ctrl.MouseMove, AddressOf FormMouseMove
            AddHandler Ctrl.MouseLeave, AddressOf FormMouseLeave
        Next
    End Sub

    ''' <summary>
    ''' 鼠标移动
    ''' </summary>
    Private Sub FormMouseMove(sender As Object, e As System.EventArgs)
        If Cursor.Position.X >= Me.Left And Cursor.Position.X <= Me.Right And _
            Cursor.Position.Y >= Me.Top And Cursor.Position.Y <= Me.Bottom Then
            TimerMouseOut.Stop()
            TimerMouseOut.Enabled = False
            TimerMouseIn.Enabled = True
            TimerMouseIn.Start()
        End If
    End Sub

    ''' <summary>
    ''' 鼠标移出，并且没有popup
    ''' </summary>
    Private Sub FormMouseLeave(sender As Object, e As System.EventArgs)
        If ListPopMenu.PopupControl Is Nothing Then
            TimerMouseIn.Stop()
            TimerMouseIn.Enabled = False
            TimerMouseOut.Enabled = True
            TimerMouseOut.Start()
        End If
    End Sub

    ''' <summary>
    ''' Popup 关闭，不能使用 Close
    ''' </summary>
    Private Sub ListPopMenu_PopupFinalized(sender As Object, e As System.EventArgs) Handles ListPopMenu.PopupFinalized
        FormMouseLeave(sender, e)
    End Sub

#End Region

#Region "Setting"

    Private Const AppName As String = "DesktopTips"
    Private Const Section As String = "FormPosSize"

    Private Sub MainForm_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        SaveList()

        SaveSetting(AppName, Section, "Top", Me.Top)
        SaveSetting(AppName, Section, "Left", Me.Left)
        SaveSetting(AppName, Section, "Height", Me.Height)
        SaveSetting(AppName, Section, "Width", Me.Width)
        SaveSetting(AppName, Section, "Opacity", MaxOpacity)
    End Sub

    Private Sub MainForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = GetSetting(AppName, Section, "Top", 20)
        Me.Left = GetSetting(AppName, Section, "Left", 20)
        Me.Height = GetSetting(AppName, Section, "Height", 163)
        Me.Width = GetSetting(AppName, Section, "Width", 200)
        MaxOpacity = GetSetting(AppName, Section, "Opacity", 0.6)

        NumericUpDownListCnt.Value = (Me.Height - 27) \ 17
        ButtonRemoveItem.Enabled = False

        SetupMouseEnterLeave()
        LoadList()
        FormOpacity_Load()
    End Sub

#End Region

#Region "FileIO"

    ' 文件 IO
    Private FileDir As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DesktopTips"
    Private FileName As String = FileDir & "\SavedItem.dat"
    Private HighItems As List(Of String) = New List(Of String)

    Private MagicSign As String = ",,,"
    Private HighLightedSign As String = MagicSign & "1"
    Private UnHighLightSign As String = MagicSign & "0"


    Private Sub SaveList()
        Dim Buf As StringBuilder = New StringBuilder
        Buf.Append(ListView.Items.Count)
        For Each item As String In ListView.Items
            If HighItems.Contains(item) Then
                Buf.Append(vbNewLine & item.ToString() & HighLightedSign)
            Else
                Buf.Append(vbNewLine & item.ToString() & UnHighLightSign)
            End If
        Next
        If Not Directory.Exists(FileDir) Then
            Directory.CreateDirectory(FileDir)
        End If
        Dim Myw As New FileStream(FileName, FileMode.Create)
        Dim MyBytes As Byte() = New UTF8Encoding().GetBytes(Buf.ToString())
        Dim MyB_Write As BinaryWriter = New BinaryWriter(Myw)
        MyB_Write.Write(MyBytes, 0, MyBytes.Length)
        Myw.Close()
    End Sub

    Private Sub LoadList()
        If File.Exists(FileName) Then
            Dim reader As TextReader = File.OpenText(FileName)
            Dim Count As Integer = Convert.ToInt32(reader.ReadLine())
            For i = 1 To Count
                Dim OneLine As String = reader.ReadLine()
                Dim ItemStrs As String() = OneLine.Split(MagicSign)
                ' Console.WriteLine(OneLine & "|" & ItemStrs.Length & "|" & ItemStrs(0) & "|" & ItemStrs(1) & "|" & ItemStrs(2) & "|" & ItemStrs(3))
                ListView.Items.Add(ItemStrs(0))
                If ItemStrs.Length >= 2 Then ' 不短路
                    ' TODO 3??? split
                    If ItemStrs(3).Equals("1") Then
                        HighItems.Add(ItemStrs(0))
                    End If
                End If
            Next
            reader.Close()
        End If
    End Sub

#End Region

#Region "List Sel"

    Dim SelectItem As Integer

    ' 焦点
    Private Sub MainForm_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate
        SelectItem = ListView.SelectedIndex
        ListView.ClearSelected()
    End Sub

    ' 焦点
    Private Sub ListView_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseClick
        If ListView.IndexFromPoint(e.X, e.Y) = -1 Then
            ListView.ClearSelected()
        End If
    End Sub

    Private Sub ListView_RightMouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseUp
        ' 实现右键同时选择
        Dim idx As Integer = ListView.IndexFromPoint(e.X, e.Y)
        If e.Button = Windows.Forms.MouseButtons.Right And idx <> -1 Then
            ListView.SetSelected(idx, True)
        End If
    End Sub

    Private Sub ButtonFocus_Handle(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ButtonAddItem.MouseDown, ButtonRemoveItem.MouseDown, ButtonCloseForm.MouseDown, ButtonChangeHeight.MouseDown
        LabelFocus.Select()
    End Sub

#End Region

    ' 增
    Private Sub ButtonAddItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAddItem.Click, PopMenuButtonAddItem.Click
        Dim msg As String = InputBox("新的提醒标签：", "添加")
        While msg.Contains(MagicSign)
            MsgBox("字符错误，不允许使用 """ + MagicSign + """ 内置字符。", MsgBoxStyle.Critical, "错误")
            msg = InputBox("新的提醒标签：", "添加")
        End While
        If msg <> "" Then
            ListView.Items.Add(msg.Trim())
            SaveList()
        End If
    End Sub

    ' 删
    Private Sub ButtonRemoveItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonRemoveItem.Click, PopMenuButtonRemoveItem.Click
        If (ListView.SelectedItem IsNot Nothing) Then
            Dim ok As Integer = MsgBox("确定删除提醒标签 """ & ListView.SelectedItem & """ 吗？", MsgBoxStyle.OkCancel, "删除")
            If (ok = vbOK) Then

                If HighItems.Contains(ListView.Items(SelectItem)) Then
                    HighItems.Remove(ListView.Items(SelectItem))
                End If

                ListView.Items.RemoveAt(SelectItem)
                SaveList()
            End If
        End If
    End Sub

    ' 改
    Private Sub ListView_DoubleClick(sender As Object, e As System.EventArgs) Handles ListView.DoubleClick, PopMenuButtonEditItem.Click
        If (ListView.SelectedItem IsNot Nothing) Then
            Dim newstr As String = InputBox("修改提醒标签 """ & ListView.SelectedItem & """ 为：", "修改", ListView.SelectedItem)
            If newstr.Contains(MagicSign) Then
                MsgBox("字符错误，不允许使用 """ + MagicSign + """ 内置字符。", MsgBoxStyle.Critical, "错误")
                Return
            End If
            If newstr <> "" Then

                If HighItems.Contains(ListView.Items(SelectItem)) Then
                    HighItems.Remove(ListView.Items(SelectItem))
                    HighItems.Add(newstr)
                End If

                ListView.Items(SelectItem) = newstr.Trim()
                SaveList()
            End If
        End If
    End Sub

    ' 选择
    Private Sub ListView_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListView.SelectedIndexChanged
        ButtonRemoveItem.Enabled = ListView.SelectedItem IsNot Nothing
        If HighItems.Contains(ListView.SelectedItem) Then
            PopMenuButtonHighLight.Checked = True
        Else
            PopMenuButtonHighLight.Checked = False
        End If
        ListView.Refresh()
    End Sub

    ' 大小
    Private Sub NumericUpDown1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDownListCnt.ValueChanged
        Dim MoveY As Integer
        If Me.Height < NumericUpDownListCnt.Value * 17 + 27 Then
            MoveY = 10
        ElseIf Me.Height > NumericUpDownListCnt.Value * 17 + 27 Then
            MoveY = -10
        End If
        Me.Height = NumericUpDownListCnt.Value * 17 + 27
        mouse_event(MouseEvent.MOUSEEVENTF_MOVE, 0, MoveY, 0, 0)
    End Sub

    ' 显示大小
    Private Sub ButtonChangeHeight_Click(sender As System.Object, e As System.EventArgs) Handles ButtonChangeHeight.Click
        ButtonChangeHeight.Checked = Not ButtonChangeHeight.Checked
        NumericUpDownListCnt.Visible = ButtonChangeHeight.Checked
    End Sub

    Private Sub ButtonChangeHeight_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ButtonChangeHeight.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            'DevComponents.DotNetBar.MessageBoxEx.Show
            Dim msg As String = "文件保存在： " & vbCr & """" & FileName & """ 内，" & vbCr & "是否打开该文件夹？"
            Dim re As Integer = _
                MessageBox.Show(msg, "提醒", _
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

            If re = vbOK Then
                PopMenuButtonOpenFile_Click(Me.PopMenuButtonOpenFile, New System.EventArgs)
            End If
        End If
    End Sub

    ' 打开文件所在位置
    Private Sub PopMenuButtonOpenFile_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonOpenFile.Click
        'Dim path As String = FileDir
        'Process.Start(path)
        'C:\Users\Windows 10\AppData\Roaming\DesktopTips\SavedItem.dat

        System.Diagnostics.Process.Start("explorer.exe", "/select,""" & FileName & """")
    End Sub

    Private Sub MainForm_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        'Console.WriteLine(e.X & ", " & e.Y)
    End Sub

    ' 置顶
    Private Sub PopMenuButtonMoveTop_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonMoveTop.Click
        Dim currIdx As Integer = ListView.SelectedIndex
        Dim currItem As Object = ListView.SelectedItem
        ListView.Items.Remove(currItem)
        ListView.Items.Insert(0, currItem)
        ListView.SetSelected(0, True)
        SaveList()
    End Sub

    ' 上移
    Private Sub PopMenuButtonMoveUp_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonMoveUp.Click
        Dim currIdx As Integer = ListView.SelectedIndex
        If currIdx >= 1 Then
            Dim currItem As Object = ListView.SelectedItem
            ListView.Items.Remove(currItem)
            ListView.Items.Insert(currIdx - 1, currItem)
            ListView.SetSelected(currIdx - 1, True)
            SaveList()
        End If
    End Sub

    ' 下移
    Private Sub PopMenuButtonMoveDown_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonMoveDown.Click
        Dim currIdx As Integer = ListView.SelectedIndex
        If currIdx <= ListView.Items.Count() - 2 Then
            Dim currItem As Object = ListView.SelectedItem
            ListView.Items.Remove(currItem)
            ListView.Items.Insert(currIdx + 1, currItem)
            ListView.SetSelected(currIdx + 1, True)
            SaveList()
        End If
    End Sub

    ' TopMost
    Private Sub PopMenuButtonWinTop_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonWinTop.Click
        sender.checked = Not sender.checked
        Me.TopMost = sender.checked
    End Sub

    ' 显示文本窗体
    Private Sub ShowForm(ByVal Title As String, ByVal Content As String)
        Dim WinSize As Size = New Size(500, 300)
        Dim TextSize As Size = New Size(WinSize.Width - 16, WinSize.Height - 39)

        Dim TextBox As New TextBox With {.Text = Content, .ReadOnly = True, .Multiline = True, .ScrollBars = ScrollBars.Both, .WordWrap = False, _
                                         .Size = TextSize, .BackColor = Color.White, .Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!), _
                                         .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top}

        Dim Win As New Form With {.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable, .Text = Title, .Size = WinSize, .TopMost = True}
        Win.Controls.Add(TextBox)
        Win.Show(Me)

        Win.Top = Me.Top
        Win.Left = Me.Left - WinSize.Width - 15
        TextBox.Select(0, 0)
    End Sub

    ' 浏览文件
    Private Sub PopMenuButtonViewFile_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonViewFile.Click
        If File.Exists(FileName) Then
            Dim reader As TextReader = File.OpenText(FileName)
            Dim Content As String = reader.ReadToEnd()
            reader.Close()

            ' 清除 ,,,
            Content = Content.Replace(HighLightedSign, "")
            Content = Content.Replace(UnHighLightSign, "")
            ShowForm("浏览文件", Content)
        End If
    End Sub

    ' 高亮
    Private Sub PopMenuButtonHighLight_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonHighLight.Click
        If HighItems.Contains(ListView.SelectedItem) Then
            HighItems.Remove(ListView.SelectedItem)
        Else
            HighItems.Add(ListView.SelectedItem)
        End If
        SaveList()
        ListView.Refresh()
    End Sub

    ' 重写绘制
    Private Sub ListView_DrawItem(sender As Object, e As System.Windows.Forms.DrawItemEventArgs) Handles ListView.DrawItem

        e.DrawBackground()
        e.DrawFocusRectangle()
        Dim NowItem As String = ListView.Items(e.Index).ToString
        Dim ColoredBrush As SolidBrush = New SolidBrush(Color.Black)

        If HighItems.Contains(NowItem) Then
            ColoredBrush = New SolidBrush(Color.Red)
        Else
            ColoredBrush = New SolidBrush(e.ForeColor)
        End If

        e.Graphics.DrawString(NowItem, e.Font, ColoredBrush, e.Bounds, StringFormat.GenericDefault)

    End Sub

    ' 显示高亮部分
    Private Sub PopMenuButtonHighLightList_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonHighLightList.Click
        Dim Content As New StringBuilder
        For Each Item As String In HighItems
            Content.AppendLine(Item)
        Next
        ShowForm("查看高亮", Content.ToString)
    End Sub

#Region "Opacity"

    Dim ops() As Double = {0.2, 0.4, 0.6, 0.7, 0.8, 1.0}
    Dim opBtns(ops.Length - 1) As DevComponents.DotNetBar.ButtonItem

    ''' <summary>
    ''' 动态添加透明度
    ''' </summary>
    Private Sub FormOpacity_Load()
        For i = 0 To ops.Length - 1
            opBtns(i) = New DevComponents.DotNetBar.ButtonItem With { _
                .Name = "PopMenuButtonOpacity" & CInt(ops(i) * 100), _
                .Tag = ops(i), _
                .Text = CInt(ops(i) * 100) & "%"
            }

            AddHandler opBtns(i).Click, AddressOf PopMenuButtonOpacity_Click
            Me.PopMenuButtonOpacity.SubItems.Add(opBtns(i))

            If MaxOpacity = opBtns(i).Tag Then
                opBtns(i).Checked = True
            End If
        Next
    End Sub

    Private Sub PopMenuButtonOpacity_Click(sender As System.Object, e As System.EventArgs)

        Dim btn As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)

        MaxOpacity = CDbl(btn.Tag())

        For Each btnS In opBtns
            btnS.Checked = False
        Next

        btn.Checked = True

        TimerMouseOut.Enabled = True
        TimerMouseOut.Start()
    End Sub

#End Region

End Class
