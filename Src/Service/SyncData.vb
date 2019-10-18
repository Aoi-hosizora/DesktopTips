Imports System.Runtime.CompilerServices
Imports System.Net.Sockets
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json

Public Class SyncData

    ''' <summary>
    ''' 同步远程 (本地 C -> 安卓 S)
    ''' 远程监听地址 -> 确定远程地址 -> 本地发送数据 -> 等待 ACK
    ''' </summary>
    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Function SendTabs(ByVal Ip As String, ByVal Port As Integer) As Boolean
        Try
            Dim socket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            socket.Bind(New IPEndPoint(IPAddress.Parse(Ip), Port))

            Dim json As String = JsonConvert.SerializeObject(GlobalModel.Tabs, Formatting.Indented)
            If Not String.IsNullOrWhiteSpace(json) Then
                socket.Send(Encoding.UTF8.GetBytes(json.ToArray()))
                Return True
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' 同步本地 (安卓 C -> 本地 S)
    ''' 确定端口 -> 监听本地地址 -> 电脑端发送 -> 本地接受处理
    ''' </summary>
    ''' <returns>Error -> ""</returns>
    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Shared Function ReceiveTabs(ByVal Port As Integer) As String
        Dim server As New TcpListener(New IPEndPoint(IPAddress.Parse("0.0.0.0"), Port))
        Try
            server.Start()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return ""
        End Try
        While True
            Dim client As TcpClient
            Try
                client = server.AcceptTcpClient()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Return ""
            End Try

            Try
                Dim stream As NetworkStream = client.GetStream()
                Dim srcBufList As New List(Of Byte)
                Dim bt As Byte = stream.ReadByte()
                While bt <> -1
                    srcBufList.Add(bt)
                    bt = stream.ReadByte()
                End While
                stream.Close()
                client.Close()
                server.Stop()
                Return Encoding.UTF8.GetString(srcBufList.ToArray())
            Catch ex As Exception
                Console.Write(ex.Message)
            End Try
        End While
        Return ""
    End Function

End Class
