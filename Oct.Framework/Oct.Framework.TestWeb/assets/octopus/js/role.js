;(function() {
    var RoleList = new List();
    RoleList.url = "/membership/role";
    RoleList.tplUrl = "/tpl?name=role";
    RoleList.actions = {
        "title": ["修改", "删除", "编辑权限"],
        "icon": ["edit", "trash-o", "key"]
    };
    $("#SearchSubmitBtn").bind("click", function() {
        RoleList.Query();
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
    var roleIfm = $("#RolePrivilegeEdit");
    $.ajax({
        url: "/membership/ModulePrivilegesData/",
        type: "POST",
        dataType: "text",
        success: function(result) {
            var treedata = eval('(' + result + ')');
            if (!treedata.data.length) return;
            tree.on('changed.jstree', function(e, data) {
                var type;
                for(i = 0, j = data.selected.length; i < j; i++) {
                    type = data.instance.get_node(data.selected[i]).li_attr.type;
                    if (type == "0") {
                        roleIfm.attr("src", "/Membership/ModulePrivilegesEdit/" + data.instance.get_node(data.selected[i]).id);
                    } else {
                        roleIfm.attr("src", "/Membership/PrivilegeEdit/" + data.instance.get_node(data.selected[i]).id);
                    }
                }
            }).jstree({
                'plugins': ["wholerow"],
                'core': {
                    "check_callback": true,
                    'data': treedata.data
                }
            }).jstree("open_all");
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
    roleIfm.load(function() {
        roleIfm.height( roleIfm.contents().height() );
    }).attr("src", roleIfm.data("src"));
})();
