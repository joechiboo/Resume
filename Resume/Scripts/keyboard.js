//键盘操作
$(document).keyup(function (event) {
    switch (event.keyCode) {
        case 13:
            console.log('Enter');
            $("#sendmessage").click();
            break;
        case 37:
            console.log('方向键-左');
            break;
        case 39:
            console.log('方向键-右');
            break;
    };
    return false;
});