/**
 * OctopusCRS公用方法库
 *
 * @author Xiaozj { bladecamper (at) GMAIL (dot) com }
 * @Copyright 888ly.CN
 * @return {Object} jQuery Object
 */


/*==========================
 * bootstrap-datepicker汉化
 * @param
 * @return
 */
;
(function($) {

    if (jQuery().datepicker) {
        $.fn.datepicker.dates['en'] = {
            days: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日"],
            daysShort: ["周日", "周一", "周二", "周三", "周四", "周五", "周六", "周日"],
            daysMin: ["日", "一", "二", "三", "四", "五", "六", "日"],
            months: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            monthsShort: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            today: "今日",
            clear: "清空",
            format: "yyyy年mm月dd日",
            weekStart: 1
        };
    };

    if (jQuery().datepicker) {
        $('.input-daterange, .date-picker').datepicker({
            autoclose: true,
            clearBtn: true
        });
    }
}(jQuery));

/*==========================
 * 列表页数据呈现
 * @param
 * @return
 */
var List = function() {
    return {
        type:"post",
        dataBody: $("#DataTableBody"),
        pagination: $("#Pagination"),
        paginationInfo: $("#PaginationInfo"),
        searchForm: $("#SearchForm"),
        searchBtn: $("#SearchSubmitBtn"),
        pagesize: 10,
        totalRecord: 0,
        url: null,
        minDisplayActions: 4,
        tplUrl: null,
        tpl: null,
        helper: {},
        /*json数据排序*/
        formatJsonoOptions: {
            baseKey: "",
            baseValue: ""
        },
        callback: function() {},
        readyHandler: function() {
            this.dataBody.empty();
            var loading = this.loading = this.loading ? this.loading.show() : $("<tr><td colspan='" + this.dataBody.siblings("thead").find("th").size() + "'>" +
                "<div class=\"col-xs-6 col-xs-offset-3\"><div class=\"progress progress-striped active\">" +
                "<div class=\"progress-bar progress-bar-success\" role=\"progressbar\" aria-valuenow=\"100\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: 100%\">" +
                "</div>" +
                "</div></div>" +
                "</td></tr>").appendTo(this.dataBody);
        },
        successHandler: function(page, result) {
            var T = this;
            /*先查看是否有T.tplUrl属性，就取该属性；否则就自己生成tpl。*/ 
            if (!T.tpl) {
                $.get(T.tplUrl, function(tplResult) {
                    T.tpl = tplResult;
                    T.renderHandler(page, T.tpl, result);
                });
            } else {
                T.renderHandler(page, T.tpl, result);
            }
        },
        renderHandler: function(page, tpl, result) {
            var T = this;
            var render = template.compile(tpl);
            var helper = T.helper;
            $.each(helper, function(key, value) {
                template.helper(key, value);
            });

            var html = render(result);
            T.loading.hide();
            T.dataBody.append(html).find("input").uniform();

            T.callback(result);

            if (page == 1) T.paginationHandler(result);
        },
        paginationHandler: function(result) {
            var T = this;
            /*如果没有total属性就去data的长度*/
            var dataLength = totalRecord = ( result.total || result.data.length );
            var maxPage = Math.ceil(dataLength / T.pagesize);

            T.pagination.toggle(maxPage > 1);
            T.paginationInfo.text("总计" + dataLength + "条记录");
            T.pagination.bootpag({
                next: '<i class="fa fa-angle-right"></i>',
                prev: '<i class="fa fa-angle-left"></i>',
                total: maxPage,
                maxVisible: Math.min(maxPage, 10),
                page: 1,
            }).off("page").on("page", function(event, num) {
                T.Query(num);
            });
        },
        Query: function(page) {
            var T = this;
            if (!T.url || !T.dataBody.length) return;

            if (!page) page = 1;
            if(T.searchForm){
                var queryString = "page=" + page + "&ps=" + T.pagesize + "&" + T.searchForm.serialize();
            }

            T.searchBtn.addClass("disabled").attr("disabled", "disabled");

            /*先查看是否有dataBody属性（#DataTableBody），如果没有就自己生成一个表格；如果有就在dataBody里插入数据*/ 
            $.ajax({
                url: T.url,
                type: T.type,
                data: queryString,
                dataType: "json",
                beforeSend: function() {
                    T.readyHandler()
                },
                complete: function() {
                    T.searchBtn.removeClass("disabled").removeAttr("disabled");
                },
                success: function(result) {

                    /*将result排个序，如果是特殊值，均排在最后*/
                    T.forMatJson(T.formatJsonoOptions, result);
                    T.successHandler(page, result);
                }
            });
        },

        /*
        =======================================================
            这个方法用来json排序
            params:{baseKey:"",baseValue:""},data
            return:
        =======================================================
        */
        forMatJson: function(options, result) {
            var newJsonArray = [];
            var baseKey = options["baseKey"] || "";
            var baseValue = options["baseValue"];

            if (baseKey === "") {
                return;
            }

            var oldDataArray = result["data"] || [];
            /*先将不特殊的复制到新的数组*/
            for (var i = 0; i < oldDataArray.length; i++) {
                if (oldDataArray[i][baseKey] !== baseValue) {
                    newJsonArray.push(oldDataArray[i]);
                }
            }
            /*将特殊的复制到新的数组*/
            for (var i = 0; i < oldDataArray.length; i++) {
                if (oldDataArray[i][baseKey] === baseValue) {
                    newJsonArray.push(oldDataArray[i]);
                }
            }
            /*result的data属性迭代*/
            result["data"] = newJsonArray;
        }
    };
};

/*处理查询按钮*/
;+function(){
    var list = new List();

    $.fn.extend({
        dataToTable: function(callback){
            /*判断*/
            if( !$(this).hasClass("J_SearchButton") ){return;} 

            /*处理list*/ 
            list.searchBtn= $(this);
            list.dataBody= $( $(this).data("tablebody") );
            list.pagination= $( $(this).data("pagination") );
            list.paginationInfo= $( $(this).data("tablepaginationinfo") );
            list.searchForm= $( $(this).data("tablesearchform") );
            list.url= $(this).data("tableresource");
            list.tplUrl= $(this).data("tplurl");
            list.pagesize = $(this).data("limit");
            list.type=$(this).data("tableajaxtype");
            list.pagesize = $(this).data("tabledatalimit") || 10;

            /*开个口，给用户自定义list*/
            callback(list);

            list.Query();
        }
    });
}(); 


/*==========================
 * 日期拾取
 * @param
 * @return
 */
if (jQuery().datepicker) {
    $('.date-picker').datepicker({
        rtl: Metronic.isRTL(),
        autoclose: true
    });
    $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal
}

/*==========================
 * 刷新父窗口
 * @param
 * @return
 */
function RefreshParentWindow() {
    if (window.parent && window.parent != window) {
        window.parent.location.reload();
    } else {
        window.location.reload();
    }
    //window.location.reload(true); // 设为true强制从服务器加载，避免firefox的重复post
}



/*==========================
 * 弹出层
 * @param
 * @return
 */
;
(function() {
    $("body").on("click", ".j-modal", function(e) {

        /*bootstrap modal的一个bug*/
        $("#Modal").css("display", "block");

        var _ = $(this);
        var ch = document.documentElement.clientHeight;
        var src = _.attr("href");
        var target = $(_.data("target"));
        var title = _.attr("title");
        var ifm = $('<iframe src="about:blank" frameborder="0" style="width: 100%; height: 100%;" name="modal-iframe"></iframe>');
        var header = $("#Modal .modal-header");
        var body = $("#Modal .modal-body");
        var wrapper = $("#Modal .modal-dialog");
        var padding, margin, maxHeight;

        ifm.one("load", function() {
            var _ = $(this);

            var contentHeight = _.contents().find("body").outerHeight(true);
            ifm.animate({
                height: Math.min(maxHeight, contentHeight)
            }, 'fast');
        });

        $("#Modal .modal-title").text(title);

        $('#Modal').one('show.bs.modal', function(e) {
            if (target.length) {
                /*target出现*/
                target.show().appendTo(body);
            } else {
                padding = parseInt(body.css("paddingTop")) + parseInt(body.css("paddingBottom"));
                margin = parseInt(wrapper.css("marginTop")) + parseInt(wrapper.css("marginBottom"));
                maxHeight = ch - header.outerHeight() - padding - margin;

                /*先设置src再插入dom中，chrome触发两次的bug*/
                ifm.attr("src", src).appendTo(body);
            }
        }).one('hidden.bs.modal', function(e) {
            if (target.length) {
                /*target循环使用*/
                target.appendTo("body").hide();
            }
            body.empty();
        }).modal("show");

        return false;
    });

    $("body").on("click", ".J_CloseModal", function(e) {
        e.preventDefault();
        $("#Modal .close", window.parent.document).click();
    });

})();



/*==========================
 * 全选/反选
 * @param
 * @return
 */
$("body").on("click", ".j-checkall", function() {
    $($(this).data("target")).find("input[type=checkbox]").prop("checked", true).uniform();
    return false;
}).on("click", ".j-checkreverse", function() {
    $($(this).data("target")).find("input[type=checkbox]").prop("checked", function(i, val) {
        return !val;
    }).uniform();
    return false;
});

/*==========================
 * 接受系统广播消息
 * @param
 * @return
 */
// ;
// (function() {
//     var pushserver;

//     // Proxy created on the fly
//     if (!$.connection) return;
//     pushserver = $.connection.pushHub;
//     // Declare a function on the chat hub so the server can invoke it
//     pushserver.client.showMessage = function(from, title, message) {
//         writeEvent(message, title, from);
//     };
//     // Start the connection
//     $.connection.hub.start();

//     //A function to write events to the page
//     function writeEvent(message, title, from) {
//         var now = new Date();
//         var nowStr = now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();

//         toastr.options = {
//             "closeButton": true,
//             "positionClass": "toast-bottom-right",
//             "showDuration": "1000",
//             "timeOut": "60000",
//             "extendedTimeOut": "1000",
//         }
//         toastr.info("<b>" + title + "</b><br/>" + message + "<br/>" + nowStr, "系统消息 来自：" + from);
//         getmsg();
//     }

//     //获取消息数  msgcount
//     getmsg();

//     function getmsg() {
//         $.get('/home/MsgCount', function(d) {
//             $('#msgcount').html(d);
//         });
//     }
// })();

/*==========================
 * 处理结果提示
 * @param
 * @return
 */
function Result(status, msg) {
    toastr.options = {
        "closeButton": true,
        "positionClass": "toast-top-center",
        "showDuration": "1000",
        "timeOut": "10000",
        "extendedTimeOut": "1000",
    }
    if (status == "success") {
        toastr.success(msg);
    } else {
        toastr.error(msg);
    }
}

function getArgs(strParame) {
    var args = new Object();
    var query = location.search.substring(1); // Get query string
    var pairs = query.split("&"); // Break at ampersand

    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('='); // Look for "name=value"

        if (pos == -1)
            continue; // If not found, skip

        var argname = pairs[i].substring(0, pos); // Extract the name
        var value = pairs[i].substring(pos + 1); // Extract the value

        value = decodeURIComponent(value); // Decode it, if needed
        args[argname] = value; // Store as a property
    }

    return args[strParame]; // Return the object
}

/*==========================
 * 表单验证
 * @param
 * @return
 */
if (jQuery.validator) {
    /*默认的验证*/
    jQuery.extend(jQuery.validator.messages, {
        number: "请输入合法的数字",
        required: "该项必须填写",
        email: "E-Mail格式不正确",
        url: "请输入合法的网址",
        digits: "只能输入整数",
        date: "输入正确的日期",
        maxlength: jQuery.validator.format("最长可输入 {0} 个字符"),
        minlength: jQuery.validator.format("最少需要输入 {0} 个字符"),
        rangelength: jQuery.validator.format("请输入 {0} 到 {1} 个字符"),
        range: jQuery.validator.format("请输入一个介于 {0} 和 {1} 之间的值"),
        max: jQuery.validator.format("请输入一个最大为{0} 的值"),
        min: jQuery.validator.format("请输入一个最小为{0} 的值"),
        isPhone: "输入正确的手机号码",
        equalTo: "密码前后必须一致"
    });
    $(".J_FormValidate").validate({
        errorElement: "span",
        errorPlacement: function(error, element) { //错误信息位置设置方法
            error.appendTo(element.closest('.form-group').find(".J_ValidateMsg")); //这里的element是录入数据的对象
        },
        highlight: function(element) { // hightlight error inputs
            $(element).closest('.form-group').addClass('has-error'); // set error class to the control group
        },
        unhighlight: function(element) { // revert the change done by hightlight
            $(element).closest('.form-group').removeClass('has-error'); // set error class to the control group
        },
        success: function(label) {
            label.closest('.form-group').removeClass('has-error'); // set success class to the control group
        }
    });

    /*自定义的方法*/
    jQuery.validator.addMethod("isPhone",function(value,element){
        return this.optional(element) || /(86)*0*13\d{9}/.test(value);
    });
}




/*
========================================================================
    根据class以及setting创造一个自定义的selector，可以使用
    param : className
    return :
========================================================================
*/
function makeCustomSelector(className) {

    /*
    ========================================================================
        返回一个setting对象，以供使用
        param : $
        return :
    ========================================================================
    */
    function makeSettingObj() {
        return {
            id: function (e) {
                return e.value;
            },
            placeholder: "请输入至少2个字搜索",
            minimumInputLength: 2,
            allowClear: true,
            ajax: {
                url: "temp/select2.json",
                dataType: 'json',
                data: function (term, page) {
                    return {
                        ComName: term,
                        ps: 20,
                        page: page
                    };
                },
                type: "GET",
                results: function (data, page) {
                    var more = (page * 10) < data.total;
                    var d = data.data;
                    var newData = [];
                    $.each(d, function (i, v) {
                        var o = {};
                        o.value = v.Id;
                        o.text = v.CompanyName;
                        newData.push(o);
                    });
                    return {
                        results: newData,
                        more: more
                    };
                },
                formatSelection: function (company) {
                    return company.text;
                }
            },
            initSelection: function (element, callback) {
                callback({
                    text: customSelector.data("name") || "",
                    value: $(element).val()
                });
            }
        };
    }

    var customSelector = $(className);
    if (customSelector.length) { 
        /*添加setting属性*/
        customSelector.setting = makeSettingObj();
    }

    return customSelector;
}



/*已自定义的className*/
var customSelectors = {
    ".J_CompanySelector": function () {
        $(document).ready(function () {
            var customSelector = makeCustomSelector(".J_CompanySelector");
            if (!customSelector.length) { return; }
            customSelector.select2(customSelector.setting);
        });
        
    },
    ".J_CardSelector": function (companyId) {
        /*这个函数用来修改setting，并且执行select2方法*/
        var customSelector = makeCustomSelector(".J_CardSelector");
        if (!customSelector.length) { return; }

        var setting = customSelector.setting;

        setting.placeholder = "请输入至少1个字搜索";
        setting.minimumInputLength = 1;
        setting.ajax.url = "/Common/GetCardNum";
        setting.ajax.data = function (term, page) {
            return {
                num: term,
                ps: 20,
                page: page
            };
        };
        
        setting.ajax.results = function (data, page) {
            var more = (page * 10) < data.length;
            var d = data;
            var newData = [];
            $.each(d, function (i, v) {
                var o = {};
                o.value = v.CardNumber;
                o.text = v.CardNumber;
                o.CompanyName = v.CompanyName;
                o.BankName = v.BankName;
                o.OwnerName = v.OwnerName;
                o.CompanyId = v.CompanyId;
                newData.push(o);
            });
            return {
                results: newData,
                more: more
            };
        };

        setting.initSelection = function (element, callback) {
            callback({
                text: customSelector.val() || "",
                value: $(element).val()
            });
        };

        customSelector.select2(customSelector.setting).change(function (event) {
            $("#PayCompanyId").val(event.added.CompanyId);
            $("#select2-chosen-1").text(event.added.CompanyName);
            $("#BankName").val(event.added.BankName);
            $("#LinkManUser").val(event.added.OwnerName);
        });

    },
    ".J_BillNoSelector": function (companyId) {
        var customSelector = makeCustomSelector(".J_BillNoSelector");
        if (!customSelector.length) { return; }
        var setting = customSelector.setting;

        setting.placeholder = "请输入至少1个字搜索";
        setting.minimumInputLength = 1;
        setting.ajax.url = "/common/GetBillNo";
        setting.ajax.data = function(term, page) {
            return {
                billno: term,
                ps: 20,
                page: page,
                companyId: companyId
            };
        };
        setting.ajax.results = function (data, page) {
            var more = (page * 10) < data.length;
            var d = data;
            var newData = [];
            $.each(d, function (i, v) {
                var o = {};
                o.BeginDate = v.BeginDate;
                o.EndDate = v.EndDate;
                o.text = v.BillNo;
                o.value = v.BillNo;
                newData.push(o);
            });
            return {
                results: newData,
                more: more
            };
        };

        customSelector.select2(customSelector.setting).change(function (event) {
            $("#DateBegin").val(event.added.BeginDate);
            $("#EndDate").val(event.added.EndDate);
        });
    }
};



/*没有参数的就先执行了*/
$(document).ready(function () {
    $.each(customSelectors, function (key, value) {
        if (key != ".J_BillNoSelector") {
            value();
        }
        
    });
});



/*
    ============================================================
    用于重新计算modal的高度
    param:option{tagString:""}
    return:
    ============================================================
*/
function changeModalHeight(option) {
    var tagString = option ? option.tagString : "[name=modal-iframe]";

    var $modal = $(tagString, window.parent.document);

    /*如果不存在就退出*/
    if (!$modal.length) return;

    var body = $("#Modal .modal-body", window.parent.document);
    var wrapper = $("#Modal .modal-dialog", window.parent.document);
    var header = $("#Modal .modal-header", window.parent.document);

    var ch = window.parent.document.documentElement.clientHeight;

    var contentHeight = $modal.contents().find("body").outerHeight(true);

    var padding = parseInt(body.css("paddingTop")) + parseInt(body.css("paddingBottom"));
    var margin = parseInt(wrapper.css("marginTop")) + parseInt(wrapper.css("marginBottom"));
    var maxHeight = ch - header.outerHeight() - padding - margin;

    $modal.animate({
        height: Math.min(maxHeight, contentHeight)
    }, 'fast');
}



/*
==================================================
简易的渲染模板的方法
params:option{dataBody:$,json:{},tplUrl:""}
==================================================
*/

function renderModel(params) {
    if (!params) { return; }

    var dataBody = params["dataBody"];
    var json = params["json"];
    var tplUrl = params["tplUrl"];
    /*先初始化dataBody*/
    dataBody.empty();

    $.get(tplUrl, function (tplStr) {
        /*退出计划*/
        if (!tplStr) {
            return;
        }
        /*渲染模板*/
        var render = template.compile(tplStr);
        var tempHtml = render(json);
        /*添加模板*/
        dataBody.append(tempHtml);
    });
}



/*select的多级联动*/
; +function () {
    /*
    ===============================================================
    一个制造多级联动selecter的方法
    param:
    return:$
    ===============================================================
    */
    function multiSelecterFactory() {
        
        /*
        ===================================================================
        这个方法生成子select对象
        params:index:""
        return:
        ===================================================================
        */
        function Selecter() {
            /*如果有人以方法的形式调用这个方法，那么就把this替换成一个新对象*/
            if (this == window) { return new Selecter(); };

            this.multiLevelSelects = document.querySelectorAll(".J_MultiLevelSelect") || [];

            if (!this.multiLevelSelects.length) { return; }

            for (var i = 0; i < this.multiLevelSelects.length; i++) {
                var tempSelecters = {}
                tempSelecters.element = this.multiLevelSelects[i].querySelectorAll("select");

                for (var key in tempSelecters.element) {
                    +function (key, element) {
                        element["index"] = parseInt(key);
                    }(key, tempSelecters.element[key]);
                }

                this[i] = tempSelecters;
                this.length = i + 1;
            }
            

            return this;
        }

        
        /*
        ===================================================================
        这个方法根据json数据改变select的option
        params:{json:{},element:$,defaultText: "",defaultValue:"",textName:"",valueName:""}
        {subElement:{},accordingTo:"",jsonUrl:"",defaultText: "",defaultValue:"",textName:"",valueName:"",paramKey:""}
        return:
        ===================================================================
        */
        function renderSubSelect(option) {
            var json = option["json"] || [];
            var element = option["element"] || $();
            var defaultText = option["defaultText"];
            var defaultValue = option["defaultValue"];
            var textName = option["textName"];
            var valueName = option["valueName"];

            if (!element.length) { return; };

            /*先清空element的选项*/
            element.empty();
            element.append($("<option>" + defaultText + "</option>").val(defaultValue));

            /*如果为空，到此为止*/
            if (!json.length) { return;}

            $.each(json, function (key, value) {
                element.append($("<option>" + value[textName] + "</option>").val(value[valueName]));
            });
        };



        /*
        ===================================================================
        这个方法就是用来查找子select
        params:{index:int}
        return:
        ===================================================================
        */
        Selecter.prototype.getSubSelectByIndex = function (i,index) {
            var subSelects = $(this[i].element);
            /*如果是第一个，没有accordingToDom*/
            if (index < 0) { return null;};
            return subSelects.eq(index);
        };


        

        /*
        ===================================================================
        这个方法就是用来将每个子select元素element变成多级联动的元素
        params:dom
        return:
        ===================================================================
        */
        Selecter.prototype.multiSelecter = function (i,element) {

            var tempOption = {};

            tempOption.element = $(element);
            tempOption.accordingTo = $(element).data("accordingto") || "";
            tempOption.jsonUrl = $(element).data("jsonurl") || "";
            tempOption.defaultText = $(element).data("defaulttext") || "请选择";
            tempOption.defaultValue = $(element).data("defaultvalue") || "";
            tempOption.textName = $(element).data("textname") || "";
            tempOption.valueName = $(element).data("valuename") || "";
            tempOption.paramKey = $(element).data("paramkey") || "";

            var jsonUrl = tempOption["jsonUrl"];
            var accordingToDom = !tempOption.accordingTo ? $(this.getSubSelectByIndex(i, element.index - 1)) : $(accordingTo);


            


            /*绑定change事件*/
            if (accordingToDom.length) {
                
                /*初始值*/
                var tempData = {};
                tempData[tempOption.paramKey] = accordingToDom.find("option:selected").val();
                $.getJSON(jsonUrl, tempData, function (data) {

                    tempOption.json = data;
                    renderSubSelect(tempOption);
                });

                accordingToDom.on("change",function () {

                    if ($(this).find("option:selected").text() === $(this).data("defaultText") && $(this).find("option:selected").val() === $(this).data("defaultValue")) {
                        tempOption.json = [];
                        return;
                    };

                    var tempData = {};
                    tempData[tempOption.paramKey] = accordingToDom.find("option:selected").val();

                    $.getJSON(jsonUrl, tempData, function (data) {

                        tempOption.json = data;
                        renderSubSelect(tempOption);
                    });
                });
            }
        };


        /*
        ===================================================================
        这个方法就是用来将每个子select元素element变成多级联动的元素
        params:
        return:
        ===================================================================
        */
        Selecter.prototype.customSelecter = function (i) {
            
            for (var i = 0; i < this.length; i++) {

                var element = this[i].element || [];

                for (var j = 0; j < element.length; j++) {
                    this.multiSelecter(i,element[j]);
                };
            }
        };


        var tempSelecter = Selecter();
        /*如果不存在就不用执行以下代码了*/
        if (!tempSelecter.length) { return tempSelecter; }

        tempSelecter.customSelecter();
        
    };

    multiSelecterFactory();
    
}();



/*
* 角色权限树形菜单
*/
; (function () {

    function renderTree(tree){
        $.ajax({
            url: tree.data("treeresource"),
            type: tree.data("treeajaxtype") || "get",
            dataType: "json",
            data: tree.data("treeajaxparams") || "",
            success: function(data){
                tree.jstree({
                    "core": {
                        "themes": {
                            "responsive": false
                        },
                        'data': data
                    },
                    "types": {
                        "default": {
                            "icon": tree.data("treeiconstyle")
                        }
                    },
                    "plugins": ["types"]
                })

                /*是否默认全部展开*/
                if( tree.data("treeopenall") ){
                    return tree.data("treeopenall") === "false" ? 
                    ( 
                        typeof tree.data("treeopenall") === "boolean" ? ( tree.jstree("open_all") ) : void 0
                    ) 
                    : 
                    ( tree.jstree("open_all") );
                }
            }
        });
    };

    var trees = $(".J_tree");
    if (!trees.length) return;

    $.each(trees,function(key,value){
        renderTree( $(value) );
    });


    //trees.on('changed.jstree', function (e, data) {
    //    /* change事件 */ 
    //})

    $("#ExpandAll").click(function () {
        trees.jstree('open_all');
        return false;
    });
    $("#CollapseAll").click(function () {
        trees.jstree('close_all');
        return false;
    });
})();