Imports System.Web.Http
Imports System.Net.Http
Imports System.Net.Http.Headers
Public Class HomeController
    Inherits ApiController
    <HttpGet()> Public Function g() As HttpResponseMessage

        Dim content As New StreamContent(New System.IO.FileStream("html/first.html", IO.FileMode.Open))
        content.Headers.ContentType = New MediaTypeHeaderValue("text/html")
        Dim response As New HttpResponseMessage() With {.Content = content}
        'response.Headers.Add("Access-Control-Allow-Origin", "*") ' This line may be used in someday...
        Return response
    End Function
End Class
