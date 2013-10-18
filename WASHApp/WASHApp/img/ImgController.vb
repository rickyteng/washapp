Imports System.Web.Http
Imports System.Net.Http
Imports System.Net.Http.Headers

Public Class ImgController
    Inherits ApiController
    Dim accept_type As Dictionary(Of String, String) = New Dictionary(Of String, String) From {{".jpg", "jpg"}, {".png", "png"}, {".gif", "gif"}}
    <HttpGet(), HttpPost()> Public Function g(ByVal name As String) As HttpResponseMessage
        Dim content As New StreamContent(New System.IO.FileStream(Request.RequestUri.LocalPath.Substring(1), IO.FileMode.Open))
        Dim ext As String = System.IO.Path.GetExtension(Request.RequestUri.LocalPath).ToLower()
        If accept_type.ContainsKey(ext) Then
            content.Headers.ContentType = New MediaTypeHeaderValue("image/" & ext)
        End If
        Dim response = New HttpResponseMessage() With {.Content = content}
        'response.Headers.CacheControl = New CacheControlHeaderValue() With {.MaxAge = New TimeSpan(1, 0, 0)}
        Return response
    End Function
End Class