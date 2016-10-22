function ajaxGet() {
    $.ajax({
        url: "api/Chat",
        success: function (data) {
            $(data).each(function (index, item) {
                createMessage(item.Name, item.Message)
            });
        }
    });
}

function ajaxMessage(message) {
    var name = $("#displayname").val();

    //$.post(
    //    "api/Chat",
    //    {
    //        Name: name,
    //        Message: message
    //    },
    //    function (data) {
    //        //alert('succeed');
    //    }
    //)
}