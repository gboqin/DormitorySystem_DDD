var dlg, dlgForm;
function Initialization(dlgid,treeid, cobtreeid, strurl) {
    dlg = $(dlgid).dialog({
        closed: true,
        modal:true
    });
    dlgForm = dlg.find('form');
    getTreeData(treeid, strurl);
    getCobTreeData(cobtreeid, strurl);
}

function getTreeData(treeid, strurl) {
    $(treeid).tree({
        method: 'GET',
        url: strurl,
        checkbox: false,
        loadMsg: '数据加载中请稍后……',
        valueField: 'id',
        textField: 'text'
    });
}

function getCobTreeData(cobtreeid, strurl) {
    $(cobtreeid).combotree({
        method: 'GET',
        url: strurl,
        checkbox: false,
        editable: false,
        valueField: 'id',
        textField: 'text',
        onLoadSuccess: function (node, data) {
            $(cobtreeid).combotree('tree').tree("collapseAll");
        }
    });
}

function doAdd(dlgid, treeid, cobtreeid, strurl) {
    $(cobtreeid).combotree('enable');
    dlgForm.form('clear');
    dlg = $(dlgid).dialog({
        title: '添加部门',
        toolbar: [
            {
                text: '保存',
                iconCls:'icon-save',
                handler: function () {
                    var validate = dlgForm.form('validate');
                    if (validate == false) { alert("数据有误！"); return false; }
                    $.ajax({
                        url: strurl,
                        data: dlgForm.serialize(),
                        type: 'POST',
                        cache: false,
                        dataType: 'json',
                        success: function (data) {
                            if (data) {
                                var treenode = $(treeid).tree('find', data.pid);
                                $(treeid).tree('append', {
                                    parent: treenode ? treenode.target : null,
                                    data: [{
                                        id: data.id,
                                        text: data.text,
                                        state:data.state
                                    }]
                                });

                                var cobtreenode = $(cobtreeid).combotree('tree').tree('find', data.pid);
                                $(cobtreeid).combotree('tree').tree('append', {
                                    parenta: cobtreenode ? cobtreenode.target : null,
                                    data: [{
                                        id: data.id,
                                        text: data.text,
                                        state:data.state
                                    }]
                                });
                                dlg.dialog('close');
                            }
                        },
                        error: function () {alert('数据有误，保存失败！');}
                    });
                }
            }, '-',
            {
                text: '取消',
                iconCls: 'icon-no',
                handler: function () {
                    dlg.dialog('close');
                }
            }
        ]
    });
    dlg.dialog('open');
    dlg.window('open').window('resize', { top: $(document).scrollTop() + ($(window).height() - 250) * 0.5 });
}

function doEdit(dlgid, treeid, cobtreeid, strurl) {
    var enode = $(treeid).tree('getSelected');
    if (enode == null) { alert("请选择要修改的记录！") }
    $(cobtreeid).combotree("disable");
    dlgForm.form('clear');
    dlg = $(dlgid).dialog({
        title: '修改部门',
        toolbar: [
            {
                text: '保存',
                iconCls: 'icon-save',
                handler: function () {
                    var validate = dlgForm.form('validate');
                    if (validate == false) { alert("数据有误！"); return false; }
                    $.ajax({
                        url: strurl,
                        data: dlgForm.serialize(),
                        type: 'PUT',
                        cache: false,
                        dataType: 'json',
                        success: function (data) {
                            $(treeid).tree('update', {
                                target: enode.target,
                                text: data.text,
                                order: data.order
                            });
                            var cobnode = $(cobtreeid).combotree('tree').tree('find', data.id);
                            $(cobtreeid).combotree('tree').tree('update', {
                                target: cobnode.target,
                                text: data.text,
                                order: data.order
                            });
                            dlg.dialog('close');
                        },
                        error: function () { alert('数据有误，保存失败！'); return false;}
                    });
                }
            }, '-',
            {
                text: '取消',
                iconCls: 'icon-no',
                handler: function () {
                    dlg.dialog("close");
                }
            }
        ]
    });

    //为form绑定数据
    $.ajax({
        type: 'GET',
        url: '../api/apiDept/GetNoteById?id=' + enode.id,
        dataType: 'json',
        success: function (data) {
            dlgForm.form('load', data);
            //$(cobtreeid).combotree("setText", data.text);
        }
    });
    dlg.dialog('open');
    dlg.window('open').window('resize', { top: $(document).scrollTop() + ($(window).height() - 250) * 0.5 });
}

function doDelete(treeid, cobtreeid, strurl) {
    var dnode = $(treeid).tree('getSelected');
    if (dnode == null) { alert("请选择要删除的记录！"); return false; }
    if (!confirm("确定要删除吗？")) { return false; }
    //var Data = {};
    //Data.id = dnode.id;
    //Data.text = dnode.text;
    //Data.pid = null;
    //Data.order = null;
    $.ajax({
        url: strurl,
        //data: $.parseJSON(JSON.stringify(Data)),
        data: $.parseJSON(JSON.stringify(dnode)),
        type: 'PUT',
        dataType: 'json',
        success: function (data) {
            $(treeid).tree('remove', dnode.target);
            var dcobtree = $(cobtreeid).combotree('tree').tree('find', dnode.id);
            $(cobtreeid).combotree('tree').tree('remove', dcobtree.target);
        },
        error: function () { alert('删除失败！'); return false; }
    });
}