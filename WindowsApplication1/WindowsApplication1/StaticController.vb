Imports System.Web.Http
Imports System.Net.Http
Imports System.Net.Http.Headers

Public Class StaticController
    Inherits ApiController
    Dim accept_type As Dictionary(Of String, String) ' = New Dictionary(Of String, String) From {{".txt", "text/text"}}
    <HttpGet()> Public Function g(ByVal pathInfo As String) As HttpResponseMessage
        Dim configjsonstring As String
        Using x As New System.IO.StreamReader("config.json")
            configjsonstring = x.ReadToEnd()
        End Using
        Dim configjson As Hashtable = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Hashtable)(configjsonstring)
        accept_type = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(configjson("static_support").ToString())
        Dim ext As String = System.IO.Path.GetExtension(pathInfo)
        If accept_type.ContainsKey(ext) Then
            If System.IO.File.Exists(pathInfo) Then
                Dim content As New StreamContent(New System.IO.FileStream(pathInfo, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite, 1024, True))
                content.Headers.ContentType = New MediaTypeHeaderValue(accept_type(ext))
                Dim response = New HttpResponseMessage() With {.Content = content}
                'response.Headers.CacheControl = New CacheControlHeaderValue() With {.MaxAge = New TimeSpan(1, 0, 0)}
                Return response
            Else
                Dim response = New HttpResponseMessage() With {.StatusCode = Net.HttpStatusCode.NotFound}
                Return response
            End If
        Else
            Dim response = New HttpResponseMessage() With {.StatusCode = Net.HttpStatusCode.NotFound}
            Return response
        End If

    End Function
End Class