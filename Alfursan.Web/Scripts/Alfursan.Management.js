var AlfursanManagement = {
    OpenCreateUserModel: function () {
        $(".modal-body").load("/Management/_CreateUserView", null, function () {
            $("#AlfursanModal").modal("show");
        });
    },
    GetUserList: function (userType) {
        $('#grid-users').dataTable();
    }
};

