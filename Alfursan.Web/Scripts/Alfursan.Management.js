var AlfursanManagement = {

    CustomerProfileId: 3,

    Init: function () {
        AlfursanManagement.GetUserList("sd");

        $("input[name='ProfileId']").change(function () {
            if ($(this).val() == AlfursanManagement.CustomerProfileId) {
                $("#form-item-customerofficer").css("display", "block");
            } else {
                $("#form-item-customerofficer").css("display", "none");
            }
        });

        $("#create-user").click(function () {
            AlfursanManagement.BindUser(0);
        });

        $("#delete-user-ok").click(function () {
            $("#message-delete-user").css("display", "none");
            AlfursanManagement.DeleteUserById( $("#UserId").val());
        });

        $(".modal-footer .btn-primary").click(function () {
            var form = $(".modal-body form");
            var isValid = form.valid();
            if (!isValid) {
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
    },

    GetUserList: function (userType) {
        $("#grid-container").css("display", "none");
        $("#message-delete-user").css("display", "none");
        $("#grid-container").load("/Management/_UserList", null, function () {
            var table = $('#grid-users').DataTable();
            $("#grid-container").css("display", "block");
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
                AlfursanManagement.BindUser(id);
            });

            $("input[name='delete-user']").click(function () {
                var id = $(this).attr("data");
                var container = $("#message-delete-user");
                $("#user-email").html(id);
                $("#UserId").val(id);
                $(container).css("display", "block");
                $("#delete-user-cancel").click(function () {
                    $(container).css("display", "none");
                });
            });

        });
    },

    DeleteUserById: function (id) {
        $.ajax({
            type: 'Post',
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
    },

    BindUser: function (userId) {
        if (userId > 0) {
            $.ajax({
                type: 'Get',
                url: "/api/UserApi/" + userId,
                data: null
            }).done(function (result) {
                var messageCode = "alert-success";
                if (result.ReturnCode == "2") {
                    messageCode = "alert-danger";
                } else if (result.ReturnCode == "3") {
                    messageCode = "alert-warning";
                } else if (result.ReturnCode == "4") {
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
                AlfursanUser.SetEntity(result.Data);
                AlfursanUser.BindModel();
                $("#AlfursanModal").modal("show");
            }).fail(function () {
                alert("hata");
            });
        } else {
            AlfursanUser.BindModel();
            $("#AlfursanModal").modal("show");
        }
    }
};

var AlfursanUser = {
    UserId: 0,
    UserName: "",
    Email: "",
    Name: "",
    Surname: "",
    Password: "",
    ConfirmPassword: "",
    CompanyName: "",
    CountryId: 0,
    Phone: "",
    Address: "",

    SetEntity: function (entity) {
        if (entity == null) {
            this.UserId = 0;
            this.UserName = "";
            this.Email = "";
            this.Password = "";
            this.ConfirmPassword = "";
            this.Name = "";
            this.Surname = "";
            this.CompanyName = "";
            this.CountryId = "";
            this.Phone = "";
            this.Address = "";
        } else {
            this.UserId = entity.UserId;
            this.UserName = entity.UserName;
            this.Email = entity.Email;
            this.Password = entity.Password;
            this.ConfirmPassword = entity.ConfirmPassword;
            this.Name = entity.Name;
            this.Surname = entity.Surname;
            this.CompanyName = entity.CompanyName;
            this.CountryId = entity.CountryId;
            this.Phone = entity.Phone;
            this.Address = entity.Address;
        }
    },

    BindModel: function () {
        $("#UserId").val(this.UserId);
        $("#UserName").val(this.UserName);
        $("#Email").val(this.Email);
        $("#Password").val(this.Password);
        $("#ConfirmPassword").val(this.ConfirmPassword);
        if (this.UserId > 0) {
            $("#container-pass").css("display", "none");
            $("#container-confirmpassword").css("display", "none");
        } else {
            $("#container-pass").css("display", "block");
            $("#container-confirmpassword").css("display", "block");
        }
        $("#Name").val(this.Name);
        $("#Surname").val(this.Surname);
        $("#CompanyName").val(this.CompanyName);
        $("#CountryId").val(this.CountryId);
        $("#Phone").val(this.Phone);
        $("#Address").val(this.Address);
    }
};


$(document).ready(function () {
    AlfursanManagement.Init();
});

