/*
* 角色权限树形菜单
*/

; (function () {
    var companyId = getArgs("companyId");

    var tree = $("#Tree");
    if (!tree.length) return;
    var roleIfm = $("#TreeNodeDetail");
    $.ajax({
        url: "/views/org/getjson.json",
        data: { companyId: companyId },
        dataType: "json",
        success: function (result) {
            var treedata = result;
            if (!treedata.data.length) return;
            tree.on('changed.jstree', function (e, data) {
                for (i = 0, j = data.selected.length; i < j; i++) {
                    roleIfm.attr("src", "/org/user2orgmgt?companyId=" + companyId + "&orgId=" + data.instance.get_node(data.selected[i]).id);
                }
            }).jstree({
                "core": {
                    "themes": {
                        "responsive": false
                    },
                    'data': treedata.data
                },
                "types": {
                    "default": {
                        "icon": "fa fa-folder icon-state-warning icon-lg"
                    }
                },
                "plugins": ["types"]
            }).jstree("open_all");
        }
    });
    $("#ExpandAll").click(function () {
        tree.jstree('open_all');
        return false;
    });
    $("#CollapseAll").click(function () {
        tree.jstree('close_all');
        return false;
    });


    roleIfm.load(function () {
        roleIfm.height(roleIfm.contents().height());
    }).attr("src", roleIfm.data("src"));

})();
