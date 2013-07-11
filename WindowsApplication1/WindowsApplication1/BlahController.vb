Imports System.Web.Http
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection

Public Class BlahController
    Inherits ApiController
    <HttpGet()> Public Function getDate() As String
        Return System.DateTime.Today.ToString("yyyy/MM/dd")
    End Function
End Class

Public Class HtmlController
    Inherits ApiController
    <HttpGet()> Public Function gethtml(ByVal id As String) As String
        Return id
    End Function
    <HttpGet()> Public Function getvar() As String
        Return Request.Properties("id").ToString()
    End Function
End Class


