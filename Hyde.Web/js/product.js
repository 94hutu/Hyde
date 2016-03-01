$(function () {
    getBrandLst();
});

function getBrandLst() {
    $.ajax({
        url: "http://api.hyde.com/BaseData/GetBrandList",
        dataType: 'json',
        //jsonp: "jsoncallback",
        //contentType: "application/x-www-form-urlencoded",
        //username: "hk013",
        //password: "123",
        //data: { username: loginName, password: pwd },
        success: function (data) {
            alert(data.err_code);
            //window.location.href = "index.html";
        },
        error: function (err) {
            alert(err);
        }
    });
}