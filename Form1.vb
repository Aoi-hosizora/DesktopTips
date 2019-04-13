Imports System.IO
Imports System.Text

Public Class Form1

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

        If e.X > Me.Width - 10 Or IsChangeSize = True Then
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

    Private Sub ButtonFocus_Handle(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ButtonAddItem.MouseDown, ButtonRemoveItem.MouseDown, ButtonCloseForm.MouseDown, ButtonChangeHeight.MouseDown
        LabelFocus.Select()
    End Sub
#End Region

#Region "TimerShow"

    Private Const MaxOpacity As Double = 0.75
    Private Sub TimerShowForm_Tick(sender As System.Object, e As System.EventArgs) Handles TimerShowForm.Tick
        Me.Opacity += 0.08
        Me.Top += 1
        If Me.Opacity >= MaxOpacity Then
            TimerShowForm.Enabled = False
            Me.Opacity = MaxOpacity
        End If
    End Sub

    Private Sub TimerEndForm_Tick(sender As System.Object, e As System.EventArgs) Handles TimerEndForm.Tick
        Me.Opacity -= 0.08
        Me.Top -= 1
        If Me.Opacity <= 0 Then
            TimerEndForm.Enabled = False
            Me.Close()
        End If
        If TimerShowForm.Enabled = True Then
            TimerShowForm.Enabled = False
        End If
    End Sub

    Private Sub ButtonCloseForm_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCloseForm.Click
        TimerEndForm.Enabled = True
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub TimerMouseIn_Tick(sender As System.Object, e As System.EventArgs) Handles TimerMouseIn.Tick
        Me.Opacity += 0.02
        If Me.Opacity >= 1 Then
            TimerMouseIn.Enabled = False
        End If
        If TimerMouseOut.Enabled = True Then
            TimerMouseOut.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseOut_Tick(sender As System.Object, e As System.EventArgs) Handles TimerMouseOut.Tick
        Me.Opacity -= 0.02
        If Me.Opacity <= MaxOpacity Then
            TimerMouseOut.Enabled = False
            Me.Opacity = MaxOpacity
        End If
        If TimerMouseIn.Enabled = True Then
            TimerMouseIn.Enabled = False
        End If
    End Sub

    Private Sub ListView_MouseEnter(sender As Object, e As System.EventArgs) Handles ListView.MouseEnter
        TimerMouseIn.Enabled = True
    End Sub

    Private MouseLeaveCnt As Integer
    Private Sub ListView_MouseLeave(sender As Object, e As System.EventArgs) Handles ListView.MouseLeave
        MouseLeaveCnt = 0
        TimerMouseOut.Enabled = True
    End Sub

#End Region

#Region "Setting"

    Private Const AppName As String = "DesktopTips"
    Private Const Section As String = "FormPosSize"

    Private Sub Form1_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        SaveList()

        SaveSetting(AppName, Section, "Top", Me.Top)
        SaveSetting(AppName, Section, "Left", Me.Left)
        SaveSetting(AppName, Section, "Height", Me.Height)
        SaveSetting(AppName, Section, "Width", Me.Width)
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Top = GetSetting(AppName, Section, "Top", 20)
        Me.Left = GetSetting(AppName, Section, "Left", 20)
        Me.Height = GetSetting(AppName, Section, "Height", 163)
        Me.Width = GetSetting(AppName, Section, "Width", 200)
        NumericUpDown1.Value = (Me.Height - 27) \ 17
        ButtonRemoveItem.Enabled = False

        LoadList()
    End Sub

#End Region

    ' 增
    Private Sub ButtonAddItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAddItem.Click
        Dim msg As String = InputBox("新的提醒标签：", "添加")
        If msg <> "" Then
            ListView.Items.Add(msg.Trim())
            SaveList()
        End If
    End Sub

    Dim SelectItem As Integer
    ' 删
    Private Sub ButtonRemoveItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonRemoveItem.Click
        If (ListView.SelectedItem IsNot Nothing) Then
            Dim ok As Integer = MsgBox("确定删除提醒标签 """ & ListView.SelectedItem & """ 吗？", MsgBoxStyle.OkCancel, "删除")
            If (ok = vbOK) Then
                ListView.Items.RemoveAt(SelectItem)
                SaveList()
            End If
        End If
    End Sub

    ' 改
    Private Sub ListView_DoubleClick(sender As Object, e As System.EventArgs) Handles ListView.DoubleClick
        If (ListView.SelectedItem IsNot Nothing) Then
            Dim newstr As String = InputBox("修改提醒标签 """ & ListView.SelectedItem & """ 为：", "修改", ListView.SelectedItem)
            If newstr <> "" Then
                ListView.Items(SelectItem) = newstr.Trim()
                SaveList()
            End If
        End If
    End Sub

    ' 选择
    Private Sub ListView_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListView.SelectedIndexChanged
        ButtonRemoveItem.Enabled = ListView.SelectedItem IsNot Nothing
    End Sub

    ' 焦点
    Private Sub Form1_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate
        SelectItem = ListView.SelectedIndex
        ListView.ClearSelected()
    End Sub

    ' 焦点
    Private Sub ListView_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseClick
        If ListView.IndexFromPoint(e.X, e.Y) = -1 Then
            ListView.ClearSelected()
        End If
    End Sub

    ' 大小
    Private Sub NumericUpDown1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        Dim MoveY As Integer
        If Me.Height < NumericUpDown1.Value * 17 + 27 Then
            MoveY = 10
        ElseIf Me.Height > NumericUpDown1.Value * 17 + 27 Then
            MoveY = -10
        End If
        Me.Height = NumericUpDown1.Value * 17 + 27
        mouse_event(MouseEvent.MOUSEEVENTF_MOVE, 0, MoveY, 0, 0)
    End Sub

    ' 显示大小
    Private Sub ButtonChangeHeight_Click(sender As System.Object, e As System.EventArgs) Handles ButtonChangeHeight.Click
        ButtonChangeHeight.Checked = Not ButtonChangeHeight.Checked
        NumericUpDown1.Visible = ButtonChangeHeight.Checked
    End Sub

    ' 文件 IO
    Private FileDir As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "/DesktopTips"
    Private FileName As String = FileDir & "/SavedItem.dat"

    Private Sub SaveList()
        Dim Buf As StringBuilder = New StringBuilder
        Buf.Append(ListView.Items.Count)
        For Each item As String In ListView.Items
            Buf.Append(vbNewLine & item.ToString())
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
                ListView.Items.Add(reader.ReadLine())
            Next
            reader.Close()
        End If
    End Sub


    Private Sub ButtonChangeHeight_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ButtonChangeHeight.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            MsgBox("文件保存在： %userprofile%\AppData\Roaming\DesktopTips\SavedItem.dat 内。", MsgBoxStyle.Information, "提醒")
        End If
    End Sub
End Class
