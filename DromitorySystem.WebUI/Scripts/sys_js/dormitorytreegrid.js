var dlg, dlgform;

function Initialization(dlgid, treegridid, strurl) {
    dlg = $(dlgid).dialog({
        closed: true,
        modal:true
    });
    dlgform = $(dlgid).find('form');
    gettreegridData(treegridid, strurl);
}

function gettreegridData(treegridid, strurl) {
    $(treegridid).treegrid({
        method: 'GET',
        url: strurl + '?id',
        idField: 'id',
        treeField: 'text',
        rownumbers: true,
        animate: true,
        loadMsg: '数据加载中，请稍后……',
        collapsible: true,
        columns: [[
            { field: 'id', hidden: true },
            { title: '编号', field: 'text', width: 180 },
            {title:'名称',field:'level',formatter: formatLevel,width:180}
        ]],
        onBeforeExpand: function (row) {
            $(treegridid).treegrid('options').url = strurl;
        }
    });
}

function formatLevel(value) {
    if (value) {
        switch (value) {
            case 1:
                return '楼';
                break;
            case 2:
                return '层';
                break;
            case 3:
                return '房';
                break;
            case 4:
                return '床';
                break;
        }
    }
}

function doAdd(dlgid, treegridid, strurl) {
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
                        dataType: 'json',
                        type: 'POST',
                        cacha: false,
                        success: function (data) {
                            $(treegridid).treegrid('append', {
                                parent: null,
                                data: [{
                                    id: data.id,
                                    text: data.text,
                                    state: data.state,
                                    level: data.level
                                }]
                            });
                            dlg.dialog('close');
                        },
                        error: function () { alert("新增失败！"); return false; }
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

function doDelete(treegridid, strurl) {
    var drow = $(treegridid).treegrid('getSelected');
    if (drow == null) { alert("请选择将要删除的记录！"); return false; }
    if (!confirm("确定要删除吗？")) { return false; }
    $.ajax({
        url: strurl,
        data: $.parseJSON(JSON.stringify(drow)),
        type: 'PUT',
        dataType: 'json',
        cache: false,
        success: function () {
            $(treegridid).treegrid('remove', drow.id);
        }
    });
}