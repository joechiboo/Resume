//$(document).ready(function () {
//    $("#NameCard").hide();

//    //rotateYDIV(2); // 2 : 轉一圈

//    $("#NameCard").click(function () {
//        rotateYDIV(1); // 1 : 轉半圈
//    });
//});

var ny = 0, rotYINT;
function rotateYDIV(circle) {
    clearInterval(rotYINT);
    rotYINT = setInterval("startYRotate(" + circle + ")", 10);
}

function startYRotate(circle) {
    var y = document.getElementById("NameCard");
    ny = ny + 1;
    y.style.transform = "rotateY(" + ny + "deg)";

    //y.style.webkitTransform = "rotateY(" + ny + "deg)"
    //y.style.OTransform = "rotateY(" + ny + "deg)"
    //y.style.MozTransform = "rotateY(" + ny + "deg)"
    if ((circle == 1 && ny == 180) || ny >= 360) {
        clearInterval(rotYINT);
        if (ny >= 360) {
            ny = 0;
        }
    }
}
