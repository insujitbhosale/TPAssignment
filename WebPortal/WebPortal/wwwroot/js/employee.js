
$(document).ready(function () {

    ValidateEmpModal();

});

function ValidateEmpModal() {

    $('#empForm').validate({
        rules: {
            name: 'required',
            address: 'required',
            userID: {
                required: true,
                minlength: 5
            },
            password: {
                required: true,
                minlength: 5
            }
        },
        messages: {
            name: 'This field is required',
            address: 'This field is required',
            userID: 'This field is required',
            password: {
                minlength: 'Password must be at least 5 characters long'
            }
        },
        submitHandler: function (form) {

        }
    });

}

$('#btnEmpMdCreate').on("click", function (eve) {
    $('#btnEmpUpdate').hide();
    $('#btnEmpCreate').show();
    $('#EmployeeModal').modal('show');
});

$('#btnEmpCreate').on("click", function (eve) {

    var form = $("#empForm");
    form.validate();
    if (!form.valid()) { return; }

    let departmentId = $('#departmentList').val();
    let Name = $('#name').val();
    let Address = $('#address').val();
    let UserName = $('#userID').val();
    let Password = $('#password').val();

    let employee = { "name": Name, "address": Address, "deptID": departmentId };
    var auth = CreateAuthEncodeString(UserName, Password);

    $.ajax(({
        url: employeeURL,
        dataType: "JSON",
        type: "POST",
        contentType: 'application/json',
        data: JSON.stringify(employee),
        beforeSend: function (req) {
            req.setRequestHeader('Authorization', auth);
        },
        success: function (result) {
            if (result.statusCode) {
                swal("Good job!", "Data Inserted !", "success");
                clearControlswithLoadEmployees();
            }
        },
        error: function (jqXHR, status, err) {
            ThrowError(jqXHR, status, err);
        }
    }));

    $('#EmployeeModal').modal('hide');
});

let Id;
$('#btnEmpMdUpdate').on("click", function (eve) {

    $('#btnEmpCreate').hide();
    $('#btnEmpUpdate').show();

    var $table = $('#EmployeeData')
    let selectedRow = $table.bootstrapTable('getSelections')[0];

    if (selectedRow == null || selectedRow == undefined) {
        swal("Oh!", "Please select row from table", "error");
        return;
    }
    $('#departmentList').val(selectedRow.department.deptID);
    $('#name').val(selectedRow.name);
    $('#address').val(selectedRow.address);
    Id = selectedRow.id;
    $('#EmployeeModal').modal('show');
});

$('#btnEmpUpdate').on("click", function (eve) {

    var form = $("#empForm");
    form.validate();
    if (!form.valid()) { return; }


    let departmentId = $('#departmentList').val();
    let Name = $('#name').val();
    let Address = $('#address').val();
    let UserName = $('#userID').val();
    let Password = $('#password').val();

    let employee = { "name": Name, "address": Address, "deptID": departmentId };
    var auth = CreateAuthEncodeString(UserName, Password);

    $.ajax(({
        url: employeeURL + Id,
        dataType: "JSON",
        type: "PUT",
        contentType: 'application/json',
        data: JSON.stringify(employee),
        beforeSend: function (req) {
            req.setRequestHeader('Authorization', auth);
        },
        success: function (result) {
            if (result.statusCode) {
                swal("Good job!", "Data Updated !", "success");
                clearControlswithLoadEmployees();
            }
        },
        error: function (jqXHR, status, err) {
            ThrowError(jqXHR, status, err);
        }
    }));
    $('#EmployeeModal').modal('hide');
});

$('#btnEmpDelete').on("click", function (eve) {

    var $table = $('#EmployeeData')
    let selectedRow = $table.bootstrapTable('getSelections')[0];
    if (selectedRow == null || selectedRow == undefined) {
        swal("Oh!", "Please select row from table", "error");
        return;
    }

    Id = selectedRow.id;

    let UserName = $('#userID').val();
    let Password = $('#password').val();

    var auth = CreateAuthEncodeString(UserName, Password);

    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this Employee : " + selectedRow.name,
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {

                $.ajax(({
                    url: employeeURL + Id,
                    dataType: "JSON",
                    type: "DELETE",
                    contentType: 'application/json',
                    beforeSend: function (req) {
                        req.setRequestHeader('Authorization', auth);
                    },
                    success: function (result) {
                        if (result.statusCode) {
                            clearControlswithLoadEmployees();
                            swal("Employee has been deleted!", {
                                icon: "success",
                            });
                        }
                    },
                    error: function (jqXHR, status, err) {
                        ThrowError(jqXHR, status, err);
                    }
                }));
            } else {
                swal("Your Employee is safe!");
            }
        });
});

function clearControlswithLoadEmployees() {
    $('#name').val("");
    $('#address').val("");
    LoadEmployees();
    $('.ajax-loader').hide();
}

function ThrowError(jqXHR, status, err) {
    let errorMessage = " ";
    if (jqXHR.responseText != null && jqXHR.responseText != undefined && jqXHR.responseText != "") {
        errorMessage = "-" + JSON.parse(jqXHR.responseText).ErrorMessage;
    }
    swal("Oh!", err + errorMessage, "error");
}