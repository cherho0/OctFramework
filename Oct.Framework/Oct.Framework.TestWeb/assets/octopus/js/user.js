;(function() {
    var UserList = new List();
    UserList.url = "/user/list";
    UserList.tplUrl = "/tpl?name=user";
    UserList.actions = {
        "title"  : ["修改", "删除", "日志"],
        "icon"   : ["edit", "trash-o", "files-o"],
        "action" : ["edit", "delete", "log"]
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
        url: "/membership/RolePrivilegeData/",
        type: "POST",
        dataType: "json",
        data : {
            "id" : $("#roleId").val()
        },
        success: function(result) {
            tree.on('changed.jstree', function(e, data) {
                var node, i, j, r = [];
                for (i = 0, j = data.selected.length; i < j; i++) {
                    node = data.instance.get_node(data.selected[i]);
                    if ( node.li_attr.type == "1" ) {
                        r.push(node.id);
                    }
                }
                $("#SelectedPrivilegeID").val(r.join(','));
            }).jstree({
                'plugins': ["wholerow", "checkbox"],
                'core': {
                    "check_callback": true,
                    'data': result.data
                }
            }).jstree('open_all');
        }
    });
    $("#ExpandAll").click(function() {
        tree.jstree('open_all');
        return false;
    });
    $("#CollapseAll").click(function() {
        tree.jstree('close_all');
        return false;
    });
})();

/*
 * 编辑用户-角色下拉选择
 */
;(function() {
    var roleSelector = $("#RoleList");
    if (roleSelector.length) roleSelector.select2({});
})();


/*
 * 删除用户
 */
;(function() {
    $("#DataTableWrapper .js-userlist").on("click", ".js-action a", function(e) {
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

/*
 * 编辑用户-选择公司
 */
;(function() {
    var companySelector = $("#CompanySelector");
    if ( !companySelector.length ) return;
    companySelector.select2({
        id: function(e) { return e.value; },
        placeholder: "请输入至少2个字搜索",
        minimumInputLength: 2,
        allowClear : true,
        ajax: {
            url: "/Organization/SearchCompany/",
            dataType: 'json',
            data: function (term, page) {
                return {
                    name: term
                };
            },
            type: "POST",
            results: function (data, page) {
                return {results: data.data};
            },
            formatSelection: selected
        },
        initSelection: function(element, callback) {
            callback({
                text : companySelector.data("name"),
                value : $(element).val()
            });
        }
    });
    function selected(company) {
        return company.text;
    }
})();
