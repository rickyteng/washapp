washapp
=======

Web Api Self Host App

This template program is written in VB.Net.
You can just change the content as you need.

Main page is html/first.html.
Css files should be put in css/ folder.
Javascript files should be put in js/ folder.
Images files(.jpg, .png, .gif) should be put in img/ folder.
Plugins files should be put in plugins/ folder.

Have fun!


Change
=======
1. Add StaticController to serve static file at any folder.
2. Add PluginsController to serve plug-in functionality.
3. Server read port setting from config.json.


Introduction
=======
#### Http Port
WASHApp will serve http function at port 32767 by default or read setting from config.json.

#### Security
If you run under win7 or win server 2008, following command need to be executed to allow port to be 

opened.

  netsh http add urlacl url=http://+:32767/ user=machine\username

#### Configuration
config.json give simplest configuration that can define window width and height, static file type, and plugin function.

    {
      "window.w":800,
      "window.h":400,
      "port":32767,
      "static_support":{
        ".txt":"text/text",
        ".log":"text/text",
        ".js":"text/javascript",
        ".png":"image/png",
        ".jpg":"image/jpg",
        ".gif":"image/gif"
      },
      "plugins":{
        "plugins_test.py":"1"
      }
    }

#### Static file
Because some reason, path can not support ".." as parent folder. I walked around to serve file in other folder by assign special string. For example:

    static/p/plugins/list_parent_dir.txt

is point to:

    static/../plugins/list_parent_dir.txt

#### Plug-in

Plugin functionality is provided by execute an executable file like .exe or .py.
In order to do this, config.json should add a section plugins as following:

    "plugins":{
        "plugins_test.py":"1"
    }
    
Inside plugins, It should be a name/value pair list. Name is executable name that washapp will call by using another process. Value is indicate whether washapp wait execution complete or not. 1 means wait while 0 means no wait.

Typically, call executable and get result like following code:

    function get_plugins_test() {
        $.get("static/p/plugins/list_parent_dir.txt", function (data) {
        
            var y = "";
            data = data.split('\n');
            for (x in data) {
                y += x + ' ** ' + data[x] + '<br />';
            }
            $('#display').html(y);
        }, 'text');
    }
    
    $.get("plugins/plugins_test.py", function (data) {
        get_plugins_test();
    }, 'json');
    
You can refer the js/cmd.js
