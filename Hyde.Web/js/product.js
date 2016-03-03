var myScroll,
	pullDownEl, pullDownOffset,
	pullUpEl, pullUpOffset,
	generatedCount = 0;
$(function () {
    getBrandLst();
    //初始化绑定iScroll控件 
    document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);

    setTimeout(loaded, 200);
});
/**
 * 将数值四舍五入(保留2位小数)后格式化成金额形式
 *
 * @param num 数值(Number或者String)
 * @return 金额格式的字符串,如'1,234,567.45'
 * @type String
 */
function formatCurrency(num) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
        num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}

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
    html += "<div style='float: left;color:red'>￥" + formatCurrency(pitem.saleprice) + "</div>";
    html += "<div style='float: right'>吊牌价：" + formatCurrency(pitem.unitprice) + "</div>";
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


/**
 * 下拉刷新 （自定义实现此方法）
 * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
 */
function pullDownAction() {
    setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
        searchGo();
        myScroll.refresh();		//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
    }, 1000);	// <-- Simulate network congestion, remove setTimeout from production!
}

/**
 * 滚动翻页 （自定义实现此方法）
 * myScroll.refresh();		// 数据加载完成后，调用界面更新方法
 */
function pullUpAction() {
    setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
        //var el, li, i;
        //el = document.getElementById('thelist');

        //for (i = 0; i < 3; i++) {
        //    li = document.createElement('li');
        //    li.innerText = 'Generated row ' + (++generatedCount);
        //    el.appendChild(li, el.childNodes[0]);
        //}

        myScroll.refresh();		// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
    }, 1000);	// <-- Simulate network congestion, remove setTimeout from production!
}
/**
 * 初始化iScroll控件
 */
function loaded() {
    pullDownEl = document.getElementById('pullDown');
    pullDownOffset = pullDownEl.offsetHeight;
    pullUpEl = document.getElementById('pullUp');
    pullUpOffset = pullUpEl.offsetHeight;

    myScroll = new iScroll('wrapper', {
        useTransition: true,
        topOffset: pullDownOffset,
        onRefresh: function () {
            if (pullDownEl.className.match('loading')) {
                pullDownEl.className = '';
                pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Pull down to refresh...';
            } else if (pullUpEl.className.match('loading')) {
                pullUpEl.className = '';
                pullUpEl.querySelector('.pullUpLabel').innerHTML = 'Pull up to load more...';
            }
        },
        onScrollMove: function () {
            if (this.y > 5 && !pullDownEl.className.match('flip')) {
                pullDownEl.className = 'flip';
                pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Release to refresh...';
                this.minScrollY = 0;
            } else if (this.y < 5 && pullDownEl.className.match('flip')) {
                pullDownEl.className = '';
                pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Pull down to refresh...';
                this.minScrollY = -pullDownOffset;
            } else if (this.y < (this.maxScrollY - 5) && !pullUpEl.className.match('flip')) {
                pullUpEl.className = 'flip';
                pullUpEl.querySelector('.pullUpLabel').innerHTML = 'Release to refresh...';
                this.maxScrollY = this.maxScrollY;
            } else if (this.y > (this.maxScrollY + 5) && pullUpEl.className.match('flip')) {
                pullUpEl.className = '';
                pullUpEl.querySelector('.pullUpLabel').innerHTML = 'Pull up to load more...';
                this.maxScrollY = pullUpOffset;
            }
        },
        onScrollEnd: function () {
            if (pullDownEl.className.match('flip')) {
                pullDownEl.className = 'loading';
                pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Loading...';
                pullDownAction();	// Execute custom function (ajax call?)
            } else if (pullUpEl.className.match('flip')) {
                pullUpEl.className = 'loading';
                pullUpEl.querySelector('.pullUpLabel').innerHTML = 'Loading...';
                pullUpAction();	// Execute custom function (ajax call?)
            }
        }
    });

    setTimeout(function () { document.getElementById('wrapper').style.left = '0'; }, 800);
}