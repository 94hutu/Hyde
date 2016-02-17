$(function () {
    $("#btnLogin").click(login);
});

function login() {
    var loginName = $("#txtLoginName").val();
    if (loginName == "" || loginName.length <= 0) {
        $("#txtLoginName").focus();
        return;
    }
    var pwd = $("#txtPwd").val();
    if (pwd == "" || pwd.length < 0) {
        $("#txtPwd").focus();
        return;
    }
    $.ajax({
        url: "http://171.221.240.159:10000/api/ProductController/GetProduct",
        dataType: 'jsonp',
        jsonp: "jsoncallback",
        //contentType: "application/x-www-form-urlencoded",
        username: "hk013",
        password:"123",
        data: { code: loginName },
        success: function (data) {
            alert(data);
            window.location.href = "index.html";
        },
        error: function (err) {
            alert(err);
        }
    });
}