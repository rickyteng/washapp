Imports System.Web.Http
Imports System.Net.Http
Imports System.Net.Http.Headers

Public Class PluginsController
    Inherits ApiController
    Dim accept_type As Dictionary(Of String, String)
    <HttpGet(), HttpPost()> Public Function g(ByVal pathInfo As String) As HttpResponseMessage
        Dim configjsonstring As String
        Using x As New System.IO.StreamReader("config.json")
            configjsonstring = x.ReadToEnd()
        End Using
        Dim configjson As Hashtable = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Hashtable)(configjsonstring)
        accept_type = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(configjson("plugins").ToString())
        pathInfo = "plugins/" & pathInfo
        Dim ext As String = System.IO.Path.GetExtension(pathInfo)
        Dim filename As String = System.IO.Path.GetFileName(pathInfo)
        If accept_type.ContainsKey(filename) Then
            If System.IO.File.Exists(pathInfo) Then
                Dim p As New System.Diagnostics.Process
                'p.StartInfo.UseShellExecute = false
                'p.StartInfo.RedirectStandardOutput = false
                p.StartInfo.WorkingDirectory = System.IO.Path.GetFullPath("plugins/")
                p.StartInfo.FileName = System.IO.Path.GetFullPath(pathInfo)
                p.Start()
                If accept_type.Item(filename) = "1" Then
                    p.WaitForExit()
                Else
                    'do nothing
                End If

                Dim content As New StreamContent(New System.IO.MemoryStream(System.Text.ASCIIEncoding.UTF8.GetBytes("[""OK""]")))
                content.Headers.ContentType = New MediaTypeHeaderValue("application/json")
                Dim response = New HttpResponseMessage() With {.Content = content}
                response.Headers.CacheControl = New CacheControlHeaderValue() With {.MaxAge = New TimeSpan(0, 0, 5)}
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
