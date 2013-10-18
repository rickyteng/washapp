Imports System.Web.Http
Imports System.Net.Http
Imports System.Net.Http.Headers

Public Class JsController
    Inherits ApiController
    <HttpGet(), HttpPost()> Public Function g(ByVal name As String) As HttpResponseMessage
        Dim content As New StreamContent(New System.IO.FileStream(Request.RequestUri.LocalPath.Substring(1), IO.FileMode.Open))
        content.Headers.ContentType = New MediaTypeHeaderValue("text/javascript")
        Dim response = New HttpResponseMessage() With {.Content = content}
        'response.Headers.CacheControl = New CacheControlHeaderValue() With {.MaxAge = New TimeSpan(1, 0, 0)}
        Return response
    End Function
End Class