var AlfursanManagement = {
    OpenCreateUserModel: function () {
        $(".modal-body").load("/Management/_CreateUserView/0", null, function () {
            $("#AlfursanModal").modal("show");
            
            //$(".modal-body form").validate({
            //    rules: {
            //        ProfileId: { required: true },
            //        UserName: { required: true, minlength: 4 },
            //        Email: { required: true }
            //    },
            //    messages: {
            //        Email: "Article title is required (at least 4 chars).",
            //        UserName: "Magazine name is required is required (at least 4 chars)."
            //    },
            //    errorContainer: "#validationSummary",
            //    errorLabelContainer: "#validationSummary ul",
            //    wrapper: "li",
            //    submitHandler: function (form) {
            //        alert("sf");
            //    }
            //});

            $(".modal-footer .btn-primary").click(function () {
                var form = $(".modal-body form");
                if (form.validate()) {

                } else {
                    alert("notvalid");
                    return false;
                }
              
                $.ajax({
                    type: form.attr('method'),
                    url: form.attr('action'),
                    data: form.serialize()
                }).done(function (result) {
                    var messageCode = "alert-success";
                    if (result.ReturnCode == "2") {
                        messageCode = "alert-danger";
                    }
                    else if (result.ReturnCode == "3") {
                        messageCode = "alert-warning";
                    }
                    else if (result.ReturnCode == "4") {
                        messageCode = "alert-info";
                    }
                    var alertDiv = $(".alert");
                    if (alertDiv.size() > 0) {
                        alertDiv.removeAttr("class");
                        alertDiv.addClass("alert");
                        alertDiv.addClass(messageCode);
                        alertDiv.html(result.ResponseMessage);
                    } else {
                        $(".modal-body").append('<div class="alert ' + messageCode + '" role="alert">' + result.ResponseMessage + '</div>');
                       
                    }
                    if (result.ReturnCode == "4") {
                        $("#AlfursanModal").modal("hide");
                        AlfursanManagement.GetUserList("sd");
                    }
                  
                }).fail(function () {
                    alert("hata");
                });
            });


        });
    },
    GetUserList: function (userType) {
        $("#grid-container").load("/Management/_UserList", null, function () {
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
        });
    },
    DeleteUserById: function (id) {
        $(".modal-body").html.empty();
        $(".modal-body").html("Bu kullanıcıyı silmek istiyor musunuz.");
        $("#AlfursanModal").modal("show");

        $(".modal-footer .btn-primary").click(function () {
            $.ajax({
                type: 'DELETE',
                url: "/api/UserApi/" + id,
                data: null
            }).done(function (result) {
                var messageCode = "alert-success";
                if (result.ReturnCode == "2") {
                    messageCode = "alert-danger";
                }
                else if (result.ReturnCode == "3") {
                    messageCode = "alert-warning";
                }
                else if (result.ReturnCode == "4") {
                    messageCode = "alert-info";
                }
                var alertDiv = $(".alert");
                if (alertDiv.size() > 0) {
                    alertDiv.removeAttr("class");
                    alertDiv.addClass("alert");
                    alertDiv.addClass(messageCode);
                    alertDiv.html(result.ResponseMessage);
                } else {
                    $(".modal-body").append('<div class="alert ' + messageCode + '" role="alert">' + result.ResponseMessage + '</div>');
                    $("#AlfursanModal").modal("hide");
                }
                AlfursanManagement.GetUserList("sd");
            }).fail(function () {
                alert("hata");
            });
        });
    }
};

