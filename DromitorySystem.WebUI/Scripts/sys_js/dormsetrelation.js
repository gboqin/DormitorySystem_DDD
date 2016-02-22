var dlg, dlgform;

function Initialization(dlgid, treeid, treeurl, gridid, gridurl) {
    dlg = $(dlgid).dialog({
        closed: true,
        modal: true
    });
    dlgform = dlg.find('form');
    gettreedata(treeid, treeurl, gridid, gridurl);
    getgriddata(gridid, gridurl, -1);
}

function gettreedata(treeid, treeurl, gridid, gridurl) {
    $(treeid).tree({
        method: 'GET',
        url: treeurl + '?id',
        checkbox: false,
        loadMsg: '数据加载中，请稍后……',
        valueField: 'id',
        textField: 'text',
        onBeforeExpand: function (node) {
            $(treeid).tree('options').url = treeurl + '?id=' + node.id;
        },
        onClick: function (node) {
            $(gridid).datagrid({
                url: gridurl,
                queryParams: {
                    dormId: node.id
                }
            });
            //getgriddata(gridid, gridurl, node.id);
        }
    });
}

function getgriddata(gridid, gridurl, dormid) {
    $(gridid).datagrid({
        method: 'GET',
        url: gridurl,
        fit: true,
        pageSize: 15,
        pageList: [5, 10, 15],
        idField: 'Id',
        loadMsg: '数据加载中，请稍后……',
        rownumbers: true,
        animate: true,
        collapsible: true,
        nowrap: true,
        pagination: true,
        singleSelect: true,
        queryParams: { dormId: dormid },
        columns: [[
            { field: "Id", hidden: true },
            { field: "dsr_DormId", hidden: true },
            { field: "dsr_SetTypeId", hidden: true },
            { title: "选项类别", field: "SetType", width: 120 },
            { field: "dsr_DormSetId", hidden: true },
            { title: "选项内容", field: "SetContent", width: 120 },
            { title: "特定", field: "dsr_Private", formatter: function (value) { return boolformatyesorno(value); }, width: 60 },
            //{ title: "使用状态", field: "dsr_State", formatter: function (value) { return boolformatonoroff(value); }, width: 60 },
            { title: "启用日期", field: "dsr_Enable", formatter: function (value) { return formatdate(value); }, width: 120 },
            //{ title: "停用日期", field: "dsr_unEnable", formatter: function (value) { return formatdate(value); }, width: 120 }
        ]]
    });
}

function getcomboboxdata(comboboxid, strurl, subcombobox, suburl) {
    //联动
    $(comboboxid).combobox({
        method: 'GET',
        url: strurl,
        editable: false,
        valueField: 'Id',
        textField: 'Name',
        onSelect: function (node) {
            subcombobox.combobox({
                method: 'GET',
                url: suburl + '?typeId=' + node.Id,
                editable: false
            }).combobox('clear');
        }
    });
    var subcombobox = $(subcombobox).combobox({
        editable: false
    });
}

function doAdd(dlgid, treeid, gridid, coverid, strurl) {
    var treeSelectedItem = $(treeid).tree('getSelected');
    if (treeSelectedItem == null) {
        alert('请选择将要设置的宿舍单元');
        return false;
    }
    $(coverid).show();
    dlgform.form('clear');
    $('#dsr_DormId').val(treeSelectedItem.id);
    dlg = $(dlgid).dialog({
        title: '添加选项设置',
        toolbar: [
            {
                text: '保存',
                iconCls: 'icon-save',
                handler: function () {
                    var validate = dlgform.form('validate');
                    if (validate == false) { alert("数据有误，请检查！"); return false; }
                    $.ajax({
                        url: strurl,
                        data: dlgform.serialize(),
                        dataType: 'json',
                        type: 'POST',
                        cache: false,
                        success: function (data) {
                            $(gridid).datagrid('appendRow', {
                                Id: data.Id,
                                dsr_DormId: data.dsr_DormId,
                                dsr_SetTypeId: data.dsr_SetTypeId,
                                SetType: data.SetType,
                                dsr_DormSetId: data.dsr_DormSetId,
                                SetContent: data.SetContent,
                                dsr_Private: data.dsr_Private,
                                dsr_State: data.dsr_State,
                                dsr_Enable: data.dsr_Enable,
                                dsr_unEnable: data.dsr_unEnable
                            });
                            dlg.dialog('close');
                        },
                        error: function (data) {
                            alert(data.responseJSON.ModelState.error);
                        }
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

function doEdit(dlgid, gridid,subcomboid,coverid, strurl,combourl) {
    var item = $(gridid).datagrid('getSelected');
    if (item == null) { alert("请选择将要修改的记录！"); return false; }
    dlgform.form('clear');
    var itemid = $(gridid).datagrid('getRowIndex', item);
    dlg = $(dlgid).dialog({
        title: '修改选项设置',
        toolbar: [
            {
                text: '保存',
                iconCls: 'icon-save',
                handler: function () {
                    var validate = dlgform.form('validate');
                    if (validate == false) { alert("数据有误！"); return false; }
                    $.ajax({
                        url: strurl,
                        data: dlgform.serialize(),
                        type: 'PUT',
                        cache: false,
                        dataType: 'json',
                        success: function (data) {
                            $(gridid).datagrid('updateRow', {
                                index: itemid,
                                row: {
                                    Id: data.Id,
                                    dsr_DormId: data.dsr_DormId,
                                    dsr_SetTypeId: data.dsr_SetTypeId,
                                    SetType: data.SetType,
                                    dsr_DormSetId: data.dsr_DormSetId,
                                    SetContent: data.SetContent,
                                    dsr_Private: data.dsr_Private,
                                    dsr_State: data.dsr_State,
                                    dsr_Enable: data.dsr_Enable,
                                    dsr_unEnable: data.dsr_unEnable
                                }
                            });

                            dlg.dialog('close');
                        },
                        error: function (data) {
                            alert(data.responseJSON.ModelState.error);
                        }
                    });
                }
            }, '-', {
                text: '取消',
                iconCls: 'icon-no',
                handler: function () {
                    dlg.dialog('close');
                }
            }
        ]
    });
    $(subcomboid).combobox({
        method: 'GET',
        url: combourl + '?typeId=' + item.dsr_SetTypeId
    });
    $(coverid).hide();
    dlgform.form('load', item);
    dlg.window('open').window('resize', { top: $(document).scrollTop() + ($(window).height() - 250) * 0.5 });
    //dlg.dialog('open');
    //dlg.dialog("move", { top: $(document).scrollTop() + ($(window).height() - 250) * 0.5 });
}

function doDelete(gridid, strurl) {
    var item = $(gridid).datagrid('getSelected');
    if (item == null) { alert("请选择将要删除的记录！"); return false; }
    var rowIndex = $(gridid).datagrid('getRowIndex', item);
    if (!confirm("确定要删除吗？")) { return false; }
    $.ajax({
        url: strurl,
        data: $.parseJSON(JSON.stringify(item)),
        type: 'PUT',
        dataType: 'json',
        cache: false,
        success: function () {
            $(gridid).datagrid('deleteRow', rowIndex);
        },
        errow: function () { alert("删除失败！"); }
    });
}