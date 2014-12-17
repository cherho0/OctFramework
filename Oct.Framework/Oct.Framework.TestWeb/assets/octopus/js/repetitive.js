;(function() {
    var UserList = new List();
    UserList.url = "/user/repetitive";
    UserList.tplUrl = "/tpl?name=repetitive";
    UserList.actions = {
        "title"  : ["修改", "转换用户"],
        "icon"   : ["edit", "exchange"],
        "modal"  : ['j-modal', ''],
        "action" : ['edit', 'transform']
    };
    $("#SearchSubmitBtn").bind("click", function() {
        UserList.Query();
    }).click();
})();

$("body").on("click", ".j-modal", function(e) {
    var ch = document.documentElement.clientHeight;
    var src = $(this).attr("href");
    var title = $(this).attr("title");
    $("#Modal .modal-title").text(title);
    $('#Modal').on('shown.bs.modal', function(e) {
        // $("#Modal .modal-body").height(ch - $("#Modal .modal-header").outerHeight(true) - $("#Modal .modal-footer").outerHeight(true) - 100);
    }).modal('show');
    $('#Modal iframe').attr("src", src);
    return false;
});

/*
 * 角色权限树形菜单
 */
;(function() {
    var tree = $("#RoleTree");
    if ( !tree.length ) return;
    $.ajax({
        url: "/assets/octopus/_tree.json",
        dataType: "text",
        success: function(result) {
            tree.on('changed.jstree', function(e, data) {
                var i, j, r = [];
                for (i = 0, j = data.selected.length; i < j; i++) {
                    r.push(data.instance.get_node(data.selected[i]).id);
                }
                $('#event_result').html('Selected: ' + r.join(', '));
            }).jstree({
                'plugins': ["wholerow", "checkbox"],
                'core': {
                    "check_callback": true,
                    'data': eval('(' + result + ')')
                }
            });
        }
    });
})();


/*
 * 转换用户
 */
;(function() {
    $("#DataTableWrapper .js-repetitive").on("click", ".js-action a", function(e) {
        var $this = $(this);
        if ($this.data("action") != "transform") return;
        e.preventDefault();

        $.ajax({
            url      : $this.attr("href"),
            type     : "POST",
            dataType : "json",
            success  : function(result) {
                if (result.succeed.toString() == "true") {
                    $this.closest("tr").fadeOut('fast', function() {
                        $(this).remove();
                    });
                } else {
                    alert(result.msg);
                }
            },
            error: function() {
                alert("转换失败，请联系管理员。")
            }
        });
    });
})();
