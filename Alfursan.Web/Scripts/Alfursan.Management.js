var AlfursanManagement = {
    OpenCreateUserModel: function () {
        $(".modal-body").load("/Management/_CreateUserView/0", null, function () {
            $("#AlfursanModal").modal("show");
        });
    },
    GetUserList: function (userType) {
        var table = $('#grid-users').DataTable();
        $('#grid-users tbody').on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                table.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });
        $('#grid-users tbody').on('dblclick', 'tr', function () {
            var id = $(this).attr("data");
            $(".modal-body").load("/Management/_CreateUserView/" + id, null, function () {
                $("#AlfursanModal").modal("show");
            });
            
        });
    }
};

