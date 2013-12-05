Imports System.Web.Http
Imports System.Web.Http.SelfHost

Public Class MainForm
    Dim config As HttpSelfHostConfiguration
    Dim httpserver As HttpSelfHostServer

    Sub hostinit(ByVal port As Integer)
        config = New HttpSelfHostConfiguration("http://localhost:" + port.ToString)

        config.Routes.MapHttpRoute("plugins", "plugins/{*pathInfo}", New With {.controller = "Plugins", .action = "g"})
        config.Routes.MapHttpRoute("static", "static/{*pathInfo}", New With {.controller = "Static", .action = "g"})
        config.Routes.MapHttpRoute("img", "img/{name}", New With {.controller = "Img", .action = "g"})
        config.Routes.MapHttpRoute("css", "css/{name}", New With {.controller = "Css", .action = "g"})
        config.Routes.MapHttpRoute("js", "js/{name}", New With {.controller = "Js", .action = "g"})
        config.Routes.MapHttpRoute("API", "{controller}/{action}/{id}", New With {.id = RouteParameter.Optional}) ' vs2010 just can not IntelliSense.... 
        config.Routes.MapHttpRoute("default", "", New With {.controller = "Home", .action = "g"})

        httpserver = New HttpSelfHostServer(config)

        'http://blog2.darkthread.net/post-2013-06-04-self-host-web-api.aspx
        httpserver.OpenAsync.Wait()

        'netsh http add urlacl url=http://+:port_number/ user=machine\username

        WebBrowser1.Navigate("http://localhost:" + port.ToString)
    End Sub

    Public Sub New()

        ' 此為設計工具所需的呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入任何初始設定。
        Dim configjsonstring As String
        Using x As New System.IO.StreamReader("config.json")
            configjsonstring = x.ReadToEnd()
        End Using
        Dim configjson As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(configjsonstring)
        Me.Width = configjson("window.w")
        Me.Height = configjson("window.h")
        If Not configjson("port") Is Nothing Then
            Dim port As Integer = configjson("port")
            hostinit(port)
        Else
            hostinit(32767)
        End If
        

    End Sub

    Protected Overrides Sub Finalize()
        httpserver.CloseAsync.Wait()
        MyBase.Finalize()
    End Sub

    Private Sub WebBrowser1_DocumentTitleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles WebBrowser1.DocumentTitleChanged
        Me.Text = WebBrowser1.DocumentTitle
    End Sub
End Class
