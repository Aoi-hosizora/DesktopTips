Imports System.Runtime.CompilerServices
Imports System.Net.Sockets
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports System.IO

Public Class SyncData

    ''' <summary>
    ''' 本地客户端发送至远程服务端
    ''' 同步到移动端
    ''' </summary>
    Public Shared sendClientSocket As Socket = Nothing
    Public Delegate Sub SendTabsCb(ByVal ok As Exception)

    ''' <summary>
    ''' 同步远程 (本地 C -> 安卓 S)
    ''' </summary>
    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub SendTabs(ByVal Ip As String, ByVal Port As Integer, ByVal cb As SendTabsCb)
        Dim ret As New Exception

        ' ソケットが接続されていないか、sendto 呼び出しを使ってデータグラム ソケットで送信するときにアドレスが指定されていないため、データの送受信を要求することは禁じられています。
        If sendClientSocket IsNot Nothing Then
            Try
                sendClientSocket.Close()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
            sendClientSocket = Nothing
        End If

        ' Clear Connect
        Try
            sendClientSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            If sendClientSocket Is Nothing Then
                Throw New Exception("Client socket can't connect to server, sendClientSocket is nothing.")
            End If
            sendClientSocket.Connect(New IPEndPoint(IPAddress.Parse(Ip), Port))

            ' Connect Success
            Dim json As String = JsonConvert.SerializeObject(GlobalModel.Tabs, Formatting.Indented)

            If Not String.IsNullOrWhiteSpace(json) Then
                ' Json Parse Success
                sendClientSocket.Send(Encoding.UTF8.GetBytes(json.ToArray()))

                sendClientSocket.Close()
                sendClientSocket = Nothing

                ' Handle Success
                ret = Nothing
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            ret = ex
        End Try

        If sendClientSocket IsNot Nothing Then
            sendClientSocket.Close()
            sendClientSocket = Nothing
        End If

        cb(ret)
    End Sub

    ''' <summary>
    ''' 监听本地服务端接收远程客户端数据
    ''' 从移动端同步
    ''' </summary>
    Public Shared rcvServerSocket As TcpListener = Nothing
    Public Delegate Sub ReceiveTabsCb(ByVal ret As String, ByVal ok As Exception)

    ''' <summary>
    ''' 同步本地 (安卓 C -> 本地 S)
    ''' </summary>
    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub ReceiveTabs(ByVal Port As Integer, ByVal cb As ReceiveTabsCb)

        ' 待っている状態からスレッドが中断されました。
        Dim ret As String = "", ok As New Exception

        If rcvServerSocket IsNot Nothing Then
            Try
                rcvServerSocket.Stop()
            Catch ex As Exception
                Console.WriteLine("0：" + ex.Message)
            End Try
            rcvServerSocket = Nothing
        End If

        ' Clear Connect
        Try
            rcvServerSocket = New TcpListener(New IPEndPoint(IPAddress.Parse("0.0.0.0"), Port))
            If rcvServerSocket Is Nothing Then
                Throw New Exception("Server socket can't listen local, rcvServerSocket is nothing.")
            End If
            rcvServerSocket.Start()
        Catch ex As Exception
            ' アクセス許可で禁じられた方法でソケットにアクセスしようとしました。 (80)
            Console.WriteLine("1：" + ex.Message)
            ok = ex
            GoTo eof
        End Try

        ' Connect Success
        While True
            Dim client As TcpClient
            Try
                client = rcvServerSocket.AcceptTcpClient()
            Catch ex As Exception
                ' ブロック操作は WSACancelBlockingCall の呼び出しに割り込まれました。
                ' (rcvServerSocket.Stop()) System.Net.Sockets.SocketException
                Console.WriteLine("2：" + ex.Message)
                ok = ex
                Exit While ' goto eof
            End Try

            ' Get Client
            ' 算術演算の結果オーバーフローが発生しました。
            Try
                Dim stream As NetworkStream = client.GetStream()
                Dim data As String = ""

                ' 读取流
                SyncLock stream
                    Dim reader As New StreamReader(stream)
                    data = reader.ReadToEnd()
                End SyncLock

                ok = Nothing
                ret = data

                stream.Close()
                client.Close()

                Exit While ' goto eof
            Catch ex As Exception
                Console.Write("3：" + ex.Message)
                ok = ex
                Exit While ' goto eof
            End Try
        End While

eof:
        If rcvServerSocket IsNot Nothing Then
            rcvServerSocket.Stop()
            rcvServerSocket = Nothing
        End If

        cb(ret, ok)
    End Sub

    ''' <summary>
    ''' 获取局域网内 IP
    ''' </summary>
    Public Shared Function GetLanIP() As String
        Dim ip As String = String.Empty
        For Each ipa As IPAddress In Dns.GetHostAddresses(Dns.GetHostName())
            If ipa.AddressFamily = AddressFamily.InterNetwork Then
                ip = ipa.ToString()
            End If
        Next
        Return ip
    End Function

End Class
