﻿var dlg, dlgform;

function Initialization(dlgid,containerid,gridid,toolid,strurl) {
    dlg = $(dlgid).dialog({
        closed: true,
        modal:true
    });
    dlgform = $(dlgid).find('form');
    $(containerid).hide();
    getgriddata(containerid, gridid, toolid, strurl);
}

function getgriddata(containerid, gridid, toolid, strurl) {
    $(gridid).datagrid({
        method: 'GET',
        url: strurl,
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
        toolbar: toolid,
        queryParams: { search: 'no', name: '' },
        columns: [[
            { field: 'Id', hidden: true },
            { title: '选项类别',field:'Name',width:180 },
            { title: '选项描述',field:'Decription',width:360 }
        ]]
    });  
}

function doSearch(gridid, containerid, strurl) {
    $(containerid).show();
    $(gridid).datagrid({
        url: strurl,
        queryParams:{search:'do',name:$('#sname').val()}
    });
    $(gridid).datagrid('clearSelections');
}

function doClean(gridid, containerid, strurl) {
    $('#sname').val("");
    $(gridid).datagrid({
        url: strurl,
        queryParams: { Search: 'no', name: '' }
    });
    $(gridid).datagrid('clearSelections');
    $(containerid).hide();
}

function doAdd(dlgid, gridid, strurl) {
    dlgform.form('clear');
    dlg = $(dlgid).dialog({
        title: '添加',
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
                        type: 'POST',
                        dataType: 'json',
                        cache: false,
                        success: function (data) {
                            $(gridid).datagrid('appendRow', {
                                Id: data.Id,
                                Name: data.Name,
                                Decription:data.Decription
                            });
                            dlg.dialog('close');
                        },
                        error: function () { alert("添加失败！"); return false; }
                    });
                }
            }, '-',
            {
                text: '取消',
                iconCls: 'icon-no',
                handler: function () {
                    dlg.dialog('close');
                }
            }]
    });
    dlg.dialog('open');
    dlg.window('open').window('resize', { top: $(document).scrollTop() + ($(window).height() - 250) * 0.5 });
}

function doEdit(dlgid, gridid, strurl) {
    var erow = $(gridid).datagrid('getSelected');
    if (erow == null) { alert("请选择要修改的记录！"); return false; }
    var rowIndex = $(gridid).datagrid('getRowIndex', erow);
    dlg = $(dlgid).dialog({
        title: '修改',
        toolbar: [
            {
                text: '保存',
                iconCls: 'icon-save',
                handler: function () {
                    $.ajax({
                        url: strurl,
                        data: dlgform.serialize(),
                        type: 'PUT',
                        dataType: 'json',
                        cache: false,
                        success: function (data) {
                            $(gridid).datagrid('updateRow', {
                                index: rowIndex,
                                row: {
                                    Name: data.Name,
                                    Decription:data.Decription
                                }
                            });
                        }
                    });
                    dlg.dialog('close');
                }
            }, '-',
            {
                text: '取消',
                iconCls: 'icon-no',
                handler: function () { dlg.dialog('close');}
            }]
    });
    dlgform.form('load', erow);
    dlg.dialog('open');
    dlg.window('open').window('resize', { top: $(document).scrollTop() + ($(window).height() - 250) * 0.5 });
}

function doDelete(gridid, strurl) {
    var drow = $(gridid).datagrid('getSelected');
    if (drow == null) { alert("请选择将要删除的记录！"); return false };
    var rowIndex = $(gridid).datagrid('getRowIndex',drow);
    if (!confirm("确定要删除吗？")) { return false; }
    $.ajax({
        url: strurl,
        data: $.parseJSON(JSON.stringify(drow)),
        type: 'PUT',
        dataType: 'json',
        cache: false,
        success: function () {
            $(gridid).datagrid('deleteRow', rowIndex);
        },
        errow: function () { alert("删除失败！");}
    });
}