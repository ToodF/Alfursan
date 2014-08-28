var AlfursanManagement = {
    CustomerProfileId: 3,

    UserListInit: function () {
        AlfursanManagement.GetUserList();

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
            AlfursanManagement.DeleteUserById($("#UserId").val());
        });

        $(".modal-footer .btn-primary").click(function () {
            var form = $(".modal-body form");
            var isValid = form.valid();
            if (!isValid) {
                return false;
            }
            AlfursanAjax.Request(form.attr('action'), form.attr('method'), null, null, function (result) {
                if (result.ReturnCode == "4") {
                    $("#AlfursanModal").modal("hide");
                    AlfursanManagement.GetUserList();
                }
            });

        });
    },
    EditProfileRoleInit: function () {
        $("#ProfileId").change(function () {
            AlfursanManagement.GetRolesByProfileId($(this).val());
        });
    },

    GetUserList: function () {
        $("#grid-container").css("display", "none");
        $("#message-delete-user").css("display", "none");

        var url = "/Management/_UserList";
        AlfursanAjax.Request(url, "post", null, "body", function (result) {
            $("#grid-container").html(result);
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

            $("input[name='change-status-passive']").click(function () {
                AlfursanManagement.ChangeStatusById($(this).attr("data"), false);
            });

            $("input:button[name='change-status-active']").click(function () {
                AlfursanManagement.ChangeStatusById($(this).attr("data"), true);
            });
        });
    },

    DeleteUserById: function (id) {
        var url = "/api/UserApi/" + id;
        AlfursanAjax.Request(url, "post", null, null, function () {
            AlfursanManagement.GetUserList();
        });
    },

    ChangeStatusById: function (id, status) {
        var url = "/api/UserApi/" + id + "/" + status;
        AlfursanAjax.Request(url, "Post", null, null, function () {
            AlfursanManagement.GetUserList();
        });
    },

    BindUser: function (userId) {
        if (userId > 0) {
            var url = "/api/UserApi/" + userId;
            AlfursanAjax.Request(url, 'Get', null, ".modal-body", function (result) {
                AlfursanUser.SetEntity(result.Data);
                AlfursanUser.BindModel();
                $("#AlfursanModal").modal("show");
            });
        } else {
            AlfursanUser.BindModel();
            $("#AlfursanModal").modal("show");
        }
    },

    GetRolesByProfileId: function (profileId) {
        var url = "/Management/_Roles/" + profileId;
        AlfursanAjax.Request(url, 'Get', null, "#contaner-roles", function (result) {
            $("#contaner-roles").html(result);
        });
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
    ProfileId: 3,
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
            this.ProfileId = 3;
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
            this.ProfileId = entity.ProfileId;
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
        $('input:radio[name="ProfileId"][value=' + this.ProfileId + ']').prop('checked', true);
        if (this.ProfileId == 3) {
            $("#form-item-customerofficer").css("display", "block");
        } else {
            $("#form-item-customerofficer").css("display", "none");
        }
    }
};



