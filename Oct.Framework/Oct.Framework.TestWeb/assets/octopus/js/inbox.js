;(function () {
    var list = new List();
    list.url = "@Url.Action("MyMsgJson")";
    list.tplUrl = "/tpl?name=MyMsg";
    list.helper = {
        readStatus: function (read) {
            return (read ? 'text-danger' : '');
        }
    };
    list.callback = function() {

    };
    list.Query();
})();
