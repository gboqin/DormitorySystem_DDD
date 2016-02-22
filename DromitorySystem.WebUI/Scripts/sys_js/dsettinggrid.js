var dlg, dlgform;

function Initialization(scomboid, dlgid, cdlgid, gridid, toolid, comboid, typename, combourl, gridurl) {
    dlg = $(dlgid).dialog({
        closed: true,
        modal:true
    });
    dlgform = dlg.find('form');
    $(cdlgid).hide();
    getcomboData(scomboid, combourl);
    getcomboDataOnselect(comboid, typename, combourl);
    getgriddata(gridid, toolid, gridurl);   
}

function getcomboData(comboid, strurl) {
    $(comboid).combobox({
        method: 'GET',
        url: strurl,
        editable: false,
        valueField: 'Id',
        textField: 'Name',
        panelHeight:'auto'
    });
}

function getcomboDataOnselect(comboid, typename,strurl) {
    $(comboid).combobox({
        method: 'GET',
        url: strurl,
        editable: false,
        valueField: 'Id',
        textField: 'Name',
        panelHeight:'auto',
        onSelect: function (record) {
            $(typename).val(record.Name);
        }
    });
}

function getgriddata(gridid, toolid,strurl) {
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
        queryParams: { search: 'no', content: '', typeid: '' },
        columns:[[
            {field:'Id',hidden:true},
            {field:'TypeId',hidden:true},
            { title: '选项名称', field: 'Content', width: 320 },
            { title: '选项类别',field:'TypeName',width:180 }
        ]]
    });
}

function doSearch(cdlgid, contentid, typeid, gridid, strurl) {
    $(cdlgid).show();
    $(gridid).datagrid({
        queryParams: { search: 'do', content: $(contentid).val(), typeid: $(typeid).combobox('getValue') },
        url: strurl
    });
    $(gridid).datagrid('clearSelections');
}

function doClean(cdlgid, contentid, typeid, gridid, strurl) {
    $(contentid).val('');
    $(typeid).combobox('clear');
    $(gridid).datagrid({
        queryParams: { search: 'do', content: '', typeid: '' },
        url: strurl
    });
    $(gridid).datagrid('clearSelections');
    $(cdlgid).hide();
}

function doAdd(dlgid, gridid, strurl) {
    dlgform.form('clear');
    dlg = $(dlgid).dialog({
        title: '新增',
        toolbar: [
            {
                text: '保存',
                iconCls: 'icon-save',
                handler: function () {
                    $.ajax({
                        url: strurl,
                        data: dlgform.serialize(),
                        type: 'POST',
                        dataType: 'json',
                        cache: false,
                        success: function (data) {
                            $(gridid).datagrid('appendRow', {
                                Id: data.Id,
                                TypeId:data.TypeId,
                                Content: data.Content,
                                TypeName:data.TypeName
                            })
                            dlg.dialog('close');
                        },
                        error: function () {
                            alert("新添失败！");return false;
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

function doEdit(dlgid, gridid, strurl) {
    var erow=$(gridid).datagrid('getSelected');
    if (erow == null) { alert("请选择将要修改的记录！"); return false; }
    var rowIndex = $(gridid).datagrid('getRowIndex',erow);
    dlgform.form('clear');
    dlg = $(dlgid).dialog({
        title: '修改',
        toolbar: [
            {
                text: '保存',
                iconCls: 'icon-save',
                handler: function () {
                    $.ajax({
                        url:strurl,
                        data:dlgform.serialize(),
                        type:'PUT',
                        dataType:'json',
                        cache:false,
                        success: function (data) {
                            $(gridid).datagrid('updateRow',{
                                index:rowIndex,
                                row:{
                                    TypeId:data.TypeId,
                                    Content: data.Content,
                                    TypeName:data.TypeName
                                }
                            });
                            dlg.dialog('close');
                        },
                        error:function(){alert("修改失败！");return false; }
                    });
                }
            }, '-',
            {
                text: '取消',
                iconCls: 'icon-no',
                handler:function(){
                    dlg.dialog('close');
                }
            }
        ]
    });
    dlgform.form('load', erow);
    dlg.dialog('open');
    dlg.window('open').window('resize', { top: $(document).scrollTop() + ($(window).height() - 250) * 0.5 });
}

function doDelete(gridid, strurl) {
    var drow = $(gridid).datagrid('getSelected');
    if (drow == null) { alert("请选择将要删除的记录！"); return false; }
    if (!confirm("确定要删除吗？")) { return false; }
    var rowIndex = $(gridid).datagrid('getRowIndex', drow);
    $.ajax({
        url: strurl,
        data: $.parseJSON(JSON.stringify(drow)),
        type: 'PUT',
        dataType: 'json',
        cache: false,
        success: function () {
            $(gridid).datagrid('deleteRow', rowIndex);
        },
        errow: function () { alert("删除失败！"); }
    });
}