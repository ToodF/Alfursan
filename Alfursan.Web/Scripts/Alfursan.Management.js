var AlfursanManagement = {
    CustomerProfileId: 3,

    UserListInit: function () {
        AlfursanManagement.GetUserList();

        $("#MailBody").htmlarea();

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
                    AlfursanManagement.BindCustomOfficers();
                }
            });
        });

        $("#ChangePasswordModal .modal-footer .btn-primary").click(function () {
            var form = $("#ChangePasswordModal .modal-body form");
            var isValid = form.valid();
            if (!isValid) {
                return false;
            }
            AlfursanAjax.Request(form.attr('action'), form.attr('method'), form.serialize(), ".modal-body");
        });

        $("#SendMailModal .modal-footer .btn-primary").click(function () {
            var form = $("#SendMailModal .modal-body form");
            var isValid = form.valid();
            if (!isValid) {
                return false;
            }
            AlfursanAjax.Request(form.attr('action'), form.attr('method'), form.serialize(), ".modal-body");
        });


        $("#delete-user-cancel").click(function () {
            $("#message-delete-user").hide(1000);
        });

        $("#delete-user-ok").click(function () {
            $("#message-delete-user").hide(1000);
            AlfursanManagement.DeleteUserById($("#UserId").val());
        });

        $("#send-bulk-mail").click(function () {
            var emails = "";
            $("input[type=checkbox]:checked").each(function () {
                emails += $(this).attr("data") + ";";
            });
            if (emails.length > 0) {
                $("#email-for-sendMail").text(emails);
                $("#mailTo").val(emails);
                $("#SendMailModal").modal("show");
            } else {
                alert("Kullanıcı seçmelisiniz.");
            }
        });
    },

    ArchiveInit: function () {

        $("#MailBody").htmlarea();

        var sendMail = { Files: [], Users: [], Subject: '', Emails: '', MailBody: '' };

        $('#sendMail').click(function () {
            var ids = [];
            $(".error-select-user").css("display", "none");
            $("input[type=checkbox][checked]").each(function () {
                ids.push($(this).attr("data"));

                sendMail.Files.push({
                    FileId: $(this).attr("data"),
                    FileName: $(this).attr("data-filename"),
                    RelatedFileName: $(this).attr("data-filepath")
                });

                $('#fileIds').val(ids);
            });

            if (ids.length > 0) {
                AlfursanManagement.OpenSendMailModal();
            } else {
                $(".error-select-user").css("display", "block");
            }

        });

        $('.list-group-item').each(function () {


            // Settings
            var $widget = $(this),
                $checkbox = $('<input type="checkbox" class="hidden" />'),
                color = ($widget.data('color') ? $widget.data('color') : "primary"),
                style = ($widget.data('style') == "button" ? "btn-" : "list-group-item-"),
                settings = {
                    on: {
                        icon: 'glyphicon glyphicon-check'
                    },
                    off: {
                        icon: 'glyphicon glyphicon-unchecked'
                    }
                };

            $widget.css('cursor', 'pointer');
            $widget.append($checkbox);

            // Event Handlers
            $widget.on('click', function () {
                $checkbox.prop('checked', !$checkbox.is(':checked'));
                $checkbox.triggerHandler('change');
                updateDisplay();
            });
            $checkbox.on('change', function () {
                updateDisplay();
            });


            // Actions
            function updateDisplay() {
                var isChecked = $checkbox.is(':checked');

                // Set the button's state
                $widget.data('state', (isChecked) ? "on" : "off");

                // Set the button's icon
                $widget.find('.state-icon')
                    .removeClass()
                    .addClass('state-icon ' + settings[$widget.data('state')].icon);

                // Update the button's color
                if (isChecked) {
                    $widget.addClass(style + color + ' active');
                } else {
                    $widget.removeClass(style + color + ' active');
                }
            }

            // Initialization
            function init() {

                if ($widget.data('checked') == true) {
                    $checkbox.prop('checked', !$checkbox.is(':checked'));
                }

                updateDisplay();

                // Inject the icon if applicable
                if ($widget.find('.state-icon').length == 0) {
                    $widget.prepend('<span class="state-icon ' + settings[$widget.data('state')].icon + '"></span>');
                }
            }
            init();
        });

        $('#get-checked-data').on('click', function (event) {
            event.preventDefault();

            var form = $("#SendMailModal .modal-body form");
            var isValid = form.valid();
            if (!isValid) {
                return false;
            }

            $("#check-list-box li.active").each(function (idx, li) {
                sendMail.Users.push({ UserId: $(li).attr("data"), Name: $(li).text(), Email: $(li).attr("data-email") });
            });
            sendMail.Subject = $("#Subject").val();
            sendMail.Emails = $("#Emails").val();
            sendMail.MailBody = $("#MailBody").val();

            if (sendMail.Users.length > 0 || sendMail.Emails.length > 0) {
                AlfursanAjax.Request("/Archive/SendMail", "Post", JSON.stringify(sendMail, null, '\t'), ".modal-body", function () { });
            } else {
                alert("Mail gönderim için mail adresi eklemelisiniz.");
            }

        });

        $('input[name="search-customer"]').keyup(function () {

            var searchText = $(this).val().toLowerCase();

            $('#check-list-box > li').each(function () {

                var currentLiText = $(this).text(),
                    showCurrentLi = currentLiText.toLowerCase().indexOf(searchText) !== -1;

                $(this).toggle(showCurrentLi);

            });
        });

    },

    OpenSendMailModal: function () {
        $("#SendMailModal").modal("show");
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
                "order": [[2, "asc"]],
                "language": {
                    "url": "/Plugins/Datatable/lang/en.txt"
                }, responsive: true
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

            $(".btn-send-mail").click(function () {
                var email = $(this).attr("email");
                $("#email-for-sendMail").text(email);
                $("#mailTo").val(email);
                $("#SendMailModal").modal("show");
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
    },

    BindCustomOfficers: function () {
        var url = "/Management/GetCustomOfficers";
        AlfursanAjax.Request(url, "GET", null, null, function (result) {
            $("#CustomOfficerId").find('option').remove();
            $.each(result.Data, function (key, value) {
                $("#CustomOfficerId").append($("<option></option>").val
                (value.Value).html(value.Text));
            });
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
    CustomOfficerId: 0,

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
            this.CustomOfficerId = 0;
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
        if (this.ProfileId == 1) {
            $(".profile-container").css("display", "none");
        } else {
            $(".profile-container").css("display", "block");
        }
        $("#CustomOfficerId").val(this.CustomOfficerId);
    }
};



