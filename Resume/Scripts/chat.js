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

function createMessage(name, message) {
    var log = $("#chatlog");
    var length = $(log).find("li").length;

    $("#chatlog").prepend('<li data-icon="delete"><a href="#">'+ name +':'+ message +'</a></li>');
    if (length >= 5) {
        $(log).find("li").eq(5).slideUp();
    }
    $('#chatlog').listview('refresh');
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