$(function () {
    getBrandLst();
});


function searchGo() {
    $.ajax({
        url: "http://api.hyde.com/product/GetSaleProductList",//?PageIndex=1&PageSize=10
        dataType: 'json',
        type: "post",
        //data: { productids: 0, genderid: 0, brandid: 0, categoryid: 0 },
        success: function (data) {
            if (data.err_code == 0) {
                if (data.entity.TotalItem > 0) {
                    var dataHtml = "";
                    for (var i = 0; i < data.entity.PageList.length; i++) {
                        var item = data.entity.PageList[i];
                        dataHtml += getProductHtml(item);
                    }
                    $(".panel-body").html(dataHtml);
                }
            } else
                alert(data.err_info);
        },
        error: function (err) {
            alert(err);
        }
    });
    $.ajax({
        url: "http://api.hyde.com/product/GetProduct?code=E14920",//code=E14920
        dataType: 'json',
        type: "get",
        success: function (data) {
            if (data.err_code == 0) {
                if (data.entity) {
                    for (var i = 0; i < data.entity.length; i++) {
                        var item = data.entity[i];
                        //$(".dropdown-menu").append(" <li>" + item.name + "</li>");
                    }
                }
            } else
                alert(data.err_info);
        },
        error: function (err) {
            alert(err);
        }
    });
}
function getProductHtml(pitem) {
    var html = "<div class='row'>";
    html += "<table class='table' style='border-bottom:solid #808080'> <tr>";
    html += "<td style='width: 10%'><img src='" + (pitem.imgpath == null ? "image/default.png" : pitem.imgpath) + "' alt='...'/></td>";
    html += "<td colspan='2'>";
    html += "<div style='float: left;color:red'>￥" + pitem.saleprice + "</div>";
    html += "<div style='float: right'>吊牌价：" + pitem.unitprice + "</div>";
    html += "<div style='margin-top: 20px;'>" + pitem.brandname + "</div></td>";
    html += "</tr></table>";
    html += " <div class='panel-heading' style='margin-top: -20px'>鞋码</div>";
    html += "<table class='table'><tr> <td>尺码</td>";
    var stockHtml = "<tr><td>数量</td>";
    for (var i = 0; i < pitem.details.length; i++) {
        var ditem = pitem.details[i];
        html += "<td>" + ditem.size + "</td>";
        stockHtml += "<td>" + ditem.stockquantity + "</td>";
    }
    html += stockHtml;
    html += "</table></div>";
    return html;
}

/***
*
*
*
**/
function getBrandLst() {
    $.ajax({
        url: "http://api.hyde.com/BaseData/GetBrandList?shutout=true",
        dataType: 'json',
        //jsonp: "jsoncallback",
        //contentType: "application/x-www-form-urlencoded",
        //username: "hk013",
        //password: "123",
        //data: { username: loginName, password: pwd },
        success: function (data) {
            if (data.err_code == 0) {
                if (data.entity) {
                    for (var i = 0; i < data.entity.length; i++) {
                        var item = data.entity[i];
                        $(".dropdown-menu").append(" <li><a href=''>" + item.name + "</a></li>");
                    }
                }
            } else
                alert(data.err_info);
        },
        error: function (err) {
            alert(err);
        }
    });
}