var AlfursanManagement = {
    OpenCreateUserModel: function () {
        $(".modal-body").load("/Management/_CreateUserView",null,function() {
            $("#AlfursanModal").modal("show");
        });
    },
    GetUserList : function(userType) {
        $.ajax({
            type: "POST",
            url: url,
            data: {userType:userType},
            success: success,
            dataType: dataType
        });
    }
}