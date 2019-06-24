
let departmentData;

let employeeURL = "http://localhost:51873/api/Employee/";

let departmentURL = "http://localhost:51873/api/Departments/";

$(document).ready(function () {   
    LoadEmployees();
    LoadDepartments();
});

function LoadDepartments() {
    $.ajax(({
        url: departmentURL,
        dataType: "json",
        type: "get",
        success: function (result) {
            if (result.data) {
                var $table = $('#DepartmentData')
                departmentData = result.data;
                $table.bootstrapTable('destroy');
                $table.bootstrapTable({ data: departmentData })
                $('#departmentList').html("");
                $.each(departmentData, function (index, value) {
                    $('#departmentList').append('<option value="' + value.deptID + '">' + value.deptName + '</option>');
                });
            }
        }
    }));
}

function LoadEmployees() {
    $.ajax(({
        url: employeeURL,
        dataType: "json",
        type: "get",
        success: function (result) {
            if (result.data) {
                var $table = $('#EmployeeData')
                $table.bootstrapTable('destroy');
                $table.bootstrapTable({ data: result.data })
            }
        }
    }));
}

function CreateAuthEncodeString(user, password) {
    var tok = user + ':' + password;
    var hash = Base64.encode(tok);
    return "Basic " + hash;
}

