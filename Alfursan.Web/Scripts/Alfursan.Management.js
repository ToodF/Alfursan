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

        $("#AlfursanModal .modal-footer .btn-primary").click(function () {
            var form = $("#AlfursanModal .modal-body form");
            var isValid = form.valid();
            if (!isValid) {
                return false;
            }
            AlfursanAjax.Request(form.attr('action'), form.attr('method'), form.serialize(), ".modal-body", function (result) {
                if (result.ReturnCode == "1") {
                    $("#AlfursanModal").modal("hide");
                    AlfursanManagement.GetUserList();
                }
            });

        });

        $("#ChangePasswordModal .modal-footer .btn-primary").click(function () {
            var form = $("#ChangePasswordModal .modal-body form");
            var isValid = form.valid();
            if (!isValid) {
                return false;
            }
            AlfursanAjax.Request(form.attr('action'), form.attr('method'), form.serialize(), ".modal-body", function (result) {
                if (result.ReturnCode == "1") {
                    $("#ChangePasswordModal").modal("hide");
                }
            });

        });

        $("#delete-user-cancel").click(function () {
            $("#message-delete-user").hide(1000);
        });

        $("#delete-user-ok").click(function () {
            $("#message-delete-user").hide(1000);
            AlfursanManagement.DeleteUserById($("#UserId").val());
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
        AlfursanAjax.Request(url, "get", null, "body", function (result) {
            $("#grid-container").html(result);

            var table = $('#grid-users').DataTable({
                "order": [[1, "asc"]],
                "language": {
                    "url": "/Plugins/Datatable/lang/en.txt"
                }
            });

            $("#grid-container").css("display", "block");

            $("img[name='change-status-passive']").click(function () {
                AlfursanManagement.ChangeStatusById($(this).attr("data"), false);
            });

            $("img[name='change-status-active']").click(function () {
                AlfursanManagement.ChangeStatusById($(this).attr("data"), true);
            });
            
            $(".edit-user").click(function () {
                var id = $(this).attr("data");
                AlfursanManagement.BindUser(id);
            });

            $(".delete-user").click(function () {
                var id = $(this).attr("data");
                var email = $(this).attr("email");
                $("#user-email").html(email);
                $("#UserId").val(id);
                $("#new-user-button").focus();
                $("#message-delete-user").show("slow");
            });

            $(".btn-change-pass").click(function () {
                var email = $(this).attr("email");
                AlfursanManagement.OpenChangePassModal(email);
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
        $(".modal-body .alert").remove();
        if (userId > 0) {
            var url = "/api/UserApi/" + userId;
            AlfursanAjax.Request(url, 'Get', null, ".modal-body", function (result) {
                AlfursanUser.SetEntity(result.Data);
                AlfursanUser.BindModel();
                $("#AlfursanModal").modal("show");
            });
        } else {
            AlfursanUser.SetEntity(null);
            AlfursanUser.BindModel();
            $("#AlfursanModal").modal("show");
        }
    },

    GetRolesByProfileId: function (profileId) {
        var url = "/Management/_Roles/" + profileId;
        AlfursanAjax.Request(url, 'Get', null, "#contaner-roles", function (result) {
            $("#contaner-roles").html(result);
        });
    },

    OpenChangePassModal: function (email) {
        AlfursanUser.BindModelForChangePass(email);
        $("#ChangePasswordModal").modal("show");
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
    CustomOfficerId: "",

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
            this.CountryId = "0";
            this.Phone = "";
            this.Address = "";
            this.ProfileId = 3;
            this.CustomOfficerId = "";
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
            this.CustomOfficerId = entity.CustomOfficerId;
        }
    },
   
    BindModelForChangePass: function (email) {
        $("#ChangePasswordModal #Email").val(email);
        $("#email-for-changepass").text(email);
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
        $("#CustomOfficerId").val(this.CustomOfficerId);
    }
};



