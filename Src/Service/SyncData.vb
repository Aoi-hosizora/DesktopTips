Imports System.Runtime.CompilerServices
Imports System.Net.Sockets
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports System.IO

Public Class SyncData

    Public Delegate Sub SendTabsCb(ByVal ok As Exception)


    Public Shared sendClientSocket As Socket = Nothing

    ''' <summary>
    ''' 同步远程 (本地 C -> 安卓 S)
    ''' 远程监听地址 -> 确定远程地址 -> 本地发送数据 -> 等待 ACK
    ''' </summary>
    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub SendTabs(ByVal Ip As String, ByVal Port As Integer, ByVal cb As SendTabsCb)
        Dim ret As New Exception

        ' ソケットが接続されていないか、sendto 呼び出しを使ってデータグラム ソケットで送信するときにアドレスが指定されていないため、データの送受信を要求することは禁じられています。
        If sendClientSocket IsNot Nothing Then
            sendClientSocket.Close()
        End If
        sendClientSocket = Nothing
        Try
            sendClientSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            If sendClientSocket Is Nothing Then
                Throw New Exception()
            End If
            sendClientSocket.Connect(New IPEndPoint(IPAddress.Parse(Ip), Port))

            Dim json As String = JsonConvert.SerializeObject(GlobalModel.Tabs, Formatting.Indented)
            Console.WriteLine(json.Length)
            If Not String.IsNullOrWhiteSpace(json) Then
                sendClientSocket.Send(Encoding.UTF8.GetBytes(json.ToArray()))
                sendClientSocket.Close()
                ret = Nothing
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            ret = ex
        End Try

        If sendClientSocket IsNot Nothing Then
            sendClientSocket.Close()
        End If
        cb(ret)
    End Sub

    Public Shared rcvServerSocket As TcpListener = Nothing

    Public Delegate Sub ReceiveTabsCb(ByVal ret As String, ByVal ok As Exception)

    ''' <summary>
    ''' 同步本地 (安卓 C -> 本地 S)
    ''' 确定端口 -> 监听本地地址 -> 电脑端发送 -> 本地接受处理
    ''' </summary>
    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Sub ReceiveTabs(ByVal Port As Integer, ByVal cb As ReceiveTabsCb)

        ' 待っている状態からスレッドが中断されました。

        Dim ret As String = ""
        Dim ok As New Exception

        rcvServerSocket = New TcpListener(New IPEndPoint(IPAddress.Parse("0.0.0.0"), Port))

        Try
            rcvServerSocket.Start()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            ok = ex
            GoTo eof
        End Try

        While True
            Dim client As TcpClient
            Try
                client = rcvServerSocket.AcceptTcpClient()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                ok = ex
                GoTo eof
            End Try

            ' 算術演算の結果オーバーフローが発生しました。
            Try
                Dim stream As NetworkStream = client.GetStream()
                Dim data As String

                ' 读取流
                SyncLock stream
                    Dim reader As New StreamReader(stream)
                    data = reader.ReadToEnd()
                End SyncLock

                ok = Nothing
                ret = data

                stream.Close()
                client.Close()
                GoTo eof
            Catch ex As Exception
                Console.Write(ex.Message)
                ok = ex
                GoTo eof
            Finally
                If rcvServerSocket IsNot Nothing Then
                    rcvServerSocket.Stop()
                End If
            End Try
        End While

eof:
        If rcvServerSocket IsNot Nothing Then
            rcvServerSocket.Stop()
        End If
        cb(ret, ok)
    End Sub

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
