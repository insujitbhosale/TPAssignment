
$(document).ready(function () {
    ValidateDeptModal();
});

function ValidateDeptModal() {

    $('#deptForm').validate({
        rules: {
            deptName: 'required',
            depuserID: {
                required: true,
                minlength: 5
            },
            depuserID: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            deptName: 'This field is required',
            depuserID: 'This field is required',
            depuserID: {
                minlength: 'Password must be at least 5 characters long'
            }
        },
        submitHandler: function (form) {

        }
    });

}

$('#btnDepMdCreate').on("click", function (eve) {
    $('#btnDepUpdate').hide();
    $('#btnDepCreate').show();
    $('#departmentModel').modal('show');
});

$('#btnDepCreate').on("click", function (eve) {

    var form = $("#deptForm");
    form.validate();
    if (!form.valid()) { return; }

    let Name = $('#deptName').val();
    let UserName = $('#depuserID').val();
    let Password = $('#deppassword').val();

    let department = { "deptName": Name };
    var auth = CreateAuthEncodeString(UserName, Password);

    $.ajax(({
        url: departmentURL,
        dataType: "JSON",
        type: "POST",
        contentType: 'application/json',
        data: JSON.stringify(department),
        beforeSend: function (req) {
            req.setRequestHeader('Authorization', auth);
        },
        success: function (result) {
            if (result.statusCode) {
                swal("Good job!", "Data Inserted !", "success");
                clearControlsWithLoadDept();
            }
        },
        error: function (jqXHR, status, err) {
            ThrowError(jqXHR, status, err);
        }
    }));
    $('#departmentModel').modal('hide');
});

$('#btnDepMdUpdate').on("click", function (eve) {

    $('#btnDepCreate').hide();
    $('#btnDepUpdate').show();

    var $table = $('#DepartmentData');
    let selectedRow = $table.bootstrapTable('getSelections')[0];

    if (selectedRow == null || selectedRow == undefined) {
        swal("Oh!", "Please select row from table", "error");
        return;
    }

    $('#deptName').val(selectedRow.deptName);
    Id = selectedRow.deptID;
    $('#departmentModel').modal('show');
});

$('#btnDepUpdate').on("click", function (eve) {

    var form = $("#deptForm");
    form.validate();
    if (!form.valid()) { return; }


    let Name = $('#deptName').val();
    let UserName = $('#depuserID').val();
    let Password = $('#deppassword').val();

    let department = { "deptName": Name };
    var auth = CreateAuthEncodeString(UserName, Password);

    $.ajax(({
        url: departmentURL + Id,
        dataType: "JSON",
        type: "PUT",
        contentType: 'application/json',
        data: JSON.stringify(department),
        beforeSend: function (req) {
            req.setRequestHeader('Authorization', auth);
        },
        success: function (result) {
            if (result.statusCode) {
                clearControlsWithLoadDept();
                swal("Good job!", "Data Updated !", "success");                
            }
        },
        error: function (jqXHR, status, err) {
            ThrowError(jqXHR, status, err);
        }
    }));
    $('#departmentModel').modal('hide');
});

$('#btnDepDelete').on("click", function (eve) {

    var $table = $('#DepartmentData')
    let selectedRow = $table.bootstrapTable('getSelections')[0];

    if (selectedRow == null || selectedRow == undefined) {
        swal("Oh!", "Please select row from table", "error");
        return;
    }

    Id = selectedRow.deptID;

    let UserName = $('#depuserID').val();
    let Password = $('#deppassword').val();

    var auth = CreateAuthEncodeString(UserName, Password);

    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this department : " + selectedRow.deptName,
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {

                $.ajax(({
                    url: departmentURL + Id,
                    dataType: "JSON",
                    type: "DELETE",
                    contentType: 'application/json',
                    beforeSend: function (req) {
                        req.setRequestHeader('Authorization', auth);
                    },
                    success: function (result) {
                        if (result.statusCode) {
                            clearControlsWithLoadDept();
                            swal("department has been deleted!", {
                                icon: "success",
                            });
                        }
                    },
                    error: function (jqXHR, status, err) {
                        ThrowError(jqXHR, status, err);
                    }
                }));
            } else {
                swal("Your department is safe!");
            }
        });
});

function clearControlsWithLoadDept() {    
    LoadDepartments();
    $('#deptName').val("");
    $('.ajax-loader').hide();
}