;(function() {
    var CompanyList = new List();
    CompanyList.url = "/organization/company/";
    CompanyList.tplUrl = "/tpl?name=company";
    CompanyList.actions = {
        "title"  : ["修改", "删除", "日志"],
        "icon"   : ["edit", "trash-o", "files-o"],
        "action" : ["edit", "delete", "log"]
    };
    $("#SearchSubmitBtn").bind("click", function() {
        CompanyList.Query();
    }).click();
})();

$("body").on("click", ".j-modal", function(e) {
    var ch = document.documentElement.clientHeight;
    var src = $(this).attr("href");
    var title = $(this).attr("title");
    $("#Modal .modal-title").text(title);
    $('#Modal').on('shown.bs.modal', function(e) {
        $("#Modal .modal-body").height(ch - $("#Modal .modal-header").outerHeight(true) - $("#Modal .modal-footer").outerHeight(true) - 100);
    }).modal({
        show: true
    });
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
 * 删除公司
 */
;(function() {
    $("#DataTableWrapper .js-companylist").on("click", ".js-action a", function(e) {
        var $this = $(this);
        if ($this.data("action") != "delete") return;
        e.preventDefault();

        if (window.confirm("确定删除吗？")) {
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
                    alert("删除失败，请联系管理员。")
                }
            });
        }
    })
})();
