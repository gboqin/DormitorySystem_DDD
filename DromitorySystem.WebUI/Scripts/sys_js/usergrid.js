var dlg_user, dlg_user_form;

function Initialization(dlgid,gridid,strurl) {
    dlg_user = $(dlgid).dialog({
        closed: true,
        modal:true
    });
    dlg_user_form = dlg_user.find('form');
    getgridData(gridid, strurl);
}

function getgridData(gridid, strurl) {
    $(gridid).datagrid({
        method: 'GET',
        url: strurl,
        pageSize: 15,
        pageList: [5, 10, 15],
        idField: 'Id',
        loadMsg: '数据加载中请稍后……',
        rownumbers: true,
        animate: true,
        collapsible: true,
        nowrap: true,
        pagination: true,
        singleSelect: true,
        //--测试查询条件queryParams: {code: ''},
        columns: [[
            { field: "Id", hidden: true },
            { field: "usr_lev_Id", hidden: true },
            { title: '工号', field: 'usr_Code', width: 180 },
            { title: '名称', field: 'usr_Name', width: 180 },
            { title: '密码', field: 'usr_Password', formatter: formatPassword, width: 180 },
            { title: '角色', field: 'usr_Level', width: 180 }
        ]]
    });
}

function getcomboData(cbid, levid, strurl) {
    $(cbid).combobox({
        method: 'GET',
        url: strurl,
        editable: false,
        valueField: 'Id',
        textField: 'lev_Text',
        panelHeight: "auto",
        onSelect: function (record) {
            $(levid).val(record.lev_Text);
        }
    });
}

function formatPassword(val,row) {
    return '<input style="border:none;background:transparent;" type="password" class="easyui-validatebox" value='+val+' readonly />';
}

function addUser(dlgid,gridid,strurl) {
    dlg_user_form.form('clear');
    dlg_user = $(dlgid).dialog({
        title: '添加用户',
        toolbar: [
            {
                text: '保存',
                iconCls: 'icon-save',
                handler: function () {
                    var validate = dlg_user_form.form('validate');
                    if (validate == false) { alert("数据有误，请检查！"); return false; }
                    $.ajax({
                        url: strurl,
                        data: dlg_user_form.serialize(),
                        dataType: 'json',
                        type: 'POST',
                        cache: false,
                        success: function (data) {
                            $(gridid).datagrid('appendRow', {
                                Id: data.Id,
                                usr_lev_Id:data.usr_lev_Id,
                                usr_Code:data.usr_Code,
                                usr_Name:data.usr_Name,
                                usr_Password:data.usr_Password,
                                usr_Level:data.usr_Level
                            });
                            dlg_user.dialog('close');
                        }
                    });
                }
            }, '-',
            {
                text: '关闭',
                iconCls: 'icon-no',
                handler: function () {
                    dlg_user.dialog('close');
                }
            }
        ]
    });
    dlg_user.dialog('open');
    dlg_user.window('open').window('resize', { top: $(document).scrollTop() + ($(window).height() - 250) * 0.5 });
}

function edtUser(dlgid, gridid, strurl) {
    var erow = $(gridid).datagrid('getSelected');
    if (erow == null) {
        alert('请选择要修改的记录');
        return false;
    }
    var rowIndex = $(gridid).datagrid('getRowIndex', erow);
    dlg_user_form.form('clear');
    dlg_user = $(dlgid).dialog({
        title: '修改用户',
        toolbar: [
            {
                text: '保存',
                iconCls: 'icon-save',
                handler: function () {
                    var validate = dlg_user_form.form('validate');
                    if (validate == false) { alert("数据有误！"); return false; }
                    $.ajax({
                        url: strurl,
                        data: dlg_user_form.serialize(),
                        type: 'PUT',
                        cache: false,
                        dataType: 'json',
                        success: function (data) {
                            $(gridid).datagrid('updateRow', {
                                index: rowIndex,
                                row: {
                                    usr_lev_Id: data.usr_lev_Id,
                                    usr_Code: data.usr_Code,
                                    usr_Name: data.usr_Name,
                                    usr_Password: data.usr_Password,
                                    usr_Level: data.usr_Level
                                }
                            });

                            dlg_user.dialog('close');
                        },
                        error: function () {
                            alert('数据有误，保存失败！');
                        }
                    });
                }
            }, '-',
            {
                text: '关闭',
                iconCls: 'icon-no',
                handler: function () {
                    dlg_user.dialog('close');
                }
            }
        ]
    });
    dlg_user_form.form('load', erow);
    dlg_user.dialog('open');
    dlg_user.window('open').window('resize', { top: $(document).scrollTop() + ($(window).height() - 250) * 0.5 });
}

function delUser(gridid, strurl) {
    var delrow = $(gridid).datagrid('getSelected');
    if (delrow == null) {
        alert('请选择要删除的记录！');
        return false;
    }
    var rowIndex = $(gridid).datagrid('getRowIndex', delrow);
    if (!confirm("确定要删除吗？")) { return false; }
    $.ajax({
        method: 'PUT',
        url: strurl,
        dataType: 'json',
        data: $.parseJSON(JSON.stringify(delrow)),
        success:function(data) {
            $(gridid).datagrid('deleteRow', rowIndex);
        }
    });
}
