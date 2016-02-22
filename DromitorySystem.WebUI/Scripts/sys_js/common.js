function boolformatyesorno(value) {
    if (value) { return "是"; }
    return "否";
}

function boolformatonoroff(value) {
    if (value) { return "启用中"; }
    return "停用";
}

function formatdate(value) {
    if (value != null) {
        var date = (new Date(value));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate ;
        //return "<lable class='text1'>" + date.getFullYear() + "-" + month + "-" + currentDate + "</lable>";
    }
}