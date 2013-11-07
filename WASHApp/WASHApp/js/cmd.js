function bind_event() {
    $('#btn_plugins_test').click(function () {
        plugins_test();
    });
}

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

function plugins_test() {
    $.get("plugins/plugins_test.py", function (data) {
        get_plugins_test();
    }, 'json');
}

$(document).ready(function () {
    bind_event();
})