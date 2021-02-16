Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json

Public Class SmmsUtil
    Private Const UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36"
    Private Const Host = "sm.ms"

    ''' <summary>
    ''' 表示任务执行结果，包括成功失败，具体结果
    ''' </summary>
    Public Class TaskResult(Of T)
        Public Property Success As Boolean
        Public Property Result As T
        Public Property Ex As Exception

        Public Sub New(success As Boolean, result As T, Optional ex As Exception = Nothing)
            Me.Success = success
            Me.Result = result
            Me.Ex = ex
        End Sub
    End Class

    ''' <summary>
    ''' 检查 token 是否有效，返回值表示 (是否异常，是否有效)
    ''' </summary>
    Public Shared Async Function CheckToken(token As String) As Task(Of TaskResult(Of Boolean))
        Using client As New HttpClient
            ' Request
            Dim req As New HttpRequestMessage(HttpMethod.Post, "https://sm.ms/api/v2/profile")
            req.Headers.Add("User-Agent", UserAgent)
            req.Headers.Add("Host", Host)
            req.Headers.Add("Authorization", token)

            ' Response
            Try
                Dim resp = Await client.SendAsync(req)
                Dim json = Await resp.Content.ReadAsStringAsync()
                Dim r = JsonConvert.DeserializeObject(Of SmmsResult)(json)
                Return New TaskResult(Of Boolean)(True, r.Success) ' 是否有效
            Catch ex As Exception
                Return New TaskResult(Of Boolean)(False, False, ex)' 异常
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 上传文件，返回值表示 (是否异常，(文件名，新链接))
    ''' </summary>
    Public Shared Async Function UploadImage(filename As String, token As String) As Task(Of TaskResult(Of Tuple(Of String, String)))
        Using client As New HttpClient, multipart As New MultipartFormDataContent
            ' Request
            Dim req As New HttpRequestMessage(HttpMethod.Post, "https://sm.ms/api/v2/upload")
            req.Headers.Add("User-Agent", UserAgent)
            req.Headers.Add("Host", Host)
            req.Headers.Add("Authorization", token)

            Try
                ' Form
                Dim info As New FileInfo(filename)
                Dim formdataBs = Encoding.UTF8.GetBytes(String.Format("form-data; name=""{0}""; filename=""{1}""", "smfile", info.Name))
                Dim formdataSb As New StringBuilder
                Array.ForEach(formdataBs, Sub(b) formdataSb.Append(ChrW(b)))

                Dim content As New StreamContent(File.OpenRead(info.FullName))
                content.Headers.Add("Content-Disposition", formdataSb.ToString())
                multipart.Add(content)
                req.Content = multipart

                ' Response
                Dim resp = Await client.SendAsync(req)
                Dim json = Await resp.Content.ReadAsStringAsync()
                Dim r = JsonConvert.DeserializeObject(Of SmmsResultWithData)(json)
                If r.Success OrElse r.Message.Contains("upload repeated limit") Then
                    Dim name = "", url = ""
                    If r.Success Then
                        name = r.Data.Filename
                        url = r.Data.Url
                    Else
                        url = r.Message.Split({"exists at: "}, StringSplitOptions.RemoveEmptyEntries).Last
                        name = url.Split("/").Last
                    End If
                    Return New TaskResult(Of Tuple(Of String, String))(True, New Tuple(Of String, String)(name, url))
                Else
                    Return New TaskResult(Of Tuple(Of String, String))(False, Nothing, New Exception(r.Message))
                End If
            Catch ex As Exception
                Return New TaskResult(Of Tuple(Of String, String))(False, Nothing, ex)
            End Try
        End Using
    End Function

    <Serializable, JsonObject>
    Private Class SmmsResult
        <JsonProperty("success")>
        Public Property Success As Boolean
    End Class

    <Serializable, JsonObject>
    Private Class SmmsResultWithData
        <JsonProperty("success")>
        Public Property Success As Boolean

        <JsonProperty("message")>
        Public Property Message As String

        <JsonProperty("data")>
        Public Property Data As SmmsResponse
    End Class

    <Serializable, JsonObject>
    Private Class SmmsResponse
        <JsonProperty("filename")>
        Public Property Filename As String

        <JsonProperty("storename")>
        Public Property Storename As String

        <JsonProperty("url")>
        Public Property Url As String
    End Class
End Class
