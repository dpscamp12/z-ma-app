﻿/*
 *   Handling of Employees Details/View Logic
 */

// Define namespace 'zmapp'
if (typeof zmapp == 'undefined') {
    zmapp = {};
}

$(function() {
    zmapp.employees.init();
});

// Do everything in namespace 'zmapp.employees'
// Don't polute global namespace.
zmapp.employees = (function () {

    // enum definitions
    var displayMode = {
        NoEmployeeSelected: 0,
        DisplayEmployeeDetails: 1,
        EditEmployeeDetails: 2
    };
    
    // local variables
    var currentDisplayMode = displayMode.NoEmployeeSelected;
    var isNewEmployee = false;

    return {
        // public methods
        init: function () {
            setDisplayMode(displayMode.NoEmployeeSelected);
            loadEmployeeList();
            registerEventHandlers();
        }
    };

    // private methods   
    function registerEventHandlers()
    {
        $('#addEmployeeButton').click(function() {
            var newEmployee = {
                Id: Math.floor(Math.random() * 1000),
                FirstName: '',
                LastName: '',
                CareerLevel: 1
            };
            isNewEmployee = true;
            initEmployeeDetails(newEmployee);
            setDisplayMode(displayMode.EditEmployeeDetails);
        });

        $('#editButton').click(function() {
            setDisplayMode(displayMode.EditEmployeeDetails);
        });

        $('#cancelButton').click(function () {
            if (isNewEmployee) {
                isNewEmployee = false;
                setDisplayMode(displayMode.NoEmployeeSelected);
            }
            else {
                // reload existing employee
                var employeeId = $('#employeeId').text();
                loadEmployee(employeeId);
                setDisplayMode(displayMode.DisplayEmployeeDetails);
            }
        });

        $('#saveButton').click(function () {
            isNewEmployee = false;
            saveEmployee();
            setDisplayMode(displayMode.DisplayEmployeeDetails);
        });

        $('#addReservationButton').click(function() {
            appendReservationToTable(new Date(2013, 0, 1), new Date(2013, 11, 31), ""); // Javascript month start with 0 == Jan
        });
    }

    function loadEmployeeList() {
        $.getJSON("/Employee/Identifiers", { 'seed': new Date().getTime() })  // date avoids that call only hits cache... AUTSCH.
            .success(function(employees) {
                initEmployeeTable(employees);
            })
            .error(function() {
                alert("Error loading employee list");
            });
    }
        
    function initEmployeeTable(employees) {
        clearEmployeeTable();
            
        for (var i = 0; i < employees.length; i++) {
            appendEmployeeToEmployeeTable(employees[i].Id, employees[i].FirstName + ' ' + employees[i].LastName);
        }
    }
       
    function clearEmployeeTable() {
        $('#employeeTable tr').each(function (index, row) {
            if (index > 0) { // index 0 is header row
                $(row).remove();
            }
        });
    }
        
    function appendEmployeeToEmployeeTable(id, name) {
        var htmlRow = '<tr>' +
            '<td>' + id + '</td>' +
            '<td>' + name + '</td>' +
            '<td><button id="' + id + '" data-action="show" type="submit">Show Details</button></td>' +
            '<td><button id="' + id + '" data-action="delete" type="submit">Delete</button></td>' +
            '</tr>';
            
        $('#employeeTable tr:last').after(htmlRow);
            
        // 'show'-button handling 
        $('#employeeTable button[data-action=show]:last').click(function () {
            var employeeId = $(this).attr('id');
            loadEmployee(employeeId);
            setDisplayMode(displayMode.DisplayEmployeeDetails);
        });

        // 'delete'-button handling 
        $('#employeeTable button[data-action=delete]:last').click(function () {
            setDisplayMode(displayMode.NoEmployeeSelected);

            var employeeId = $(this).attr('id');
            var jsonText = window.JSON.stringify({ id: employeeId });
            $.ajax({
                type: 'DELETE',
                url: "/Employee/DeleteEmployee",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: jsonText
            })
            .success(function (e) {
                loadEmployeeList();
            })
            .error(function (xhr, ajaxOptions, thrownError) {
                alert('Error deleting employee [Status-code:' + xhr.status + ']');
            });
        });
    }
        
    function setDisplayMode(mode) {
        currentDisplayMode = mode;
            
        if (mode == displayMode.NoEmployeeSelected) {
            $('#details').hide();
        }
        else {
            $('#details').show();
            if (mode == displayMode.DisplayEmployeeDetails) {
                $('#editButton').show();
                $('#saveButton').hide();
                $('#cancelButton').hide();
            }
            else if (mode == displayMode.EditEmployeeDetails) {
                $('#editButton').hide();
                $('#saveButton').show();
                $('#cancelButton').show();
            }
                
            var isReadonly = (mode == displayMode.DisplayEmployeeDetails);
            $('#details input').attr('readonly', isReadonly);
            $('#careerLevel option').attr('disabled', isReadonly);
            $('#skills input').attr('disabled', isReadonly);
            $('#careerLevel').attr('disabled', isReadonly);

            updateReservationsForCurrentDisplayMode();
        }
    }
        
    function updateReservationsForCurrentDisplayMode() {
        if (currentDisplayMode == displayMode.EditEmployeeDetails) {
            $('#reservations button').show();
                
            $('#reservationsTable input').attr('disabled', false);
        }
        else {
            $('#reservations button').hide();
            $('#reservationsTable input').attr('disabled', true);
        }
    }
        
    function setCareerLevel(careerLevel) {
        $('#careerLevel').val(careerLevel);
    }
        
    function getCareerLevel() {
        return parseInt($('#careerLevel').val());
    }
        
    function setSkills(skills) {
        $("#skills input:checkbox").each(function () {
            var checkboxId = parseInt($(this).val());
            var found = $.inArray(checkboxId, skills);
            $(this).attr('checked', found != -1);
        });
    }
        
    function getSkills() {
        var skills = new Array();
        $("#skills input:checked").each(function () {
            var skillId = parseInt($(this).val());
            skills.push(skillId);
        });
        return skills;
    }

    function clearReservations() {
        $('#reservationsTable tr').each(function (index, row) {
            if (index > 0) { // index 0 is header row
                $(row).remove();
            }
        });
    }
        
    function setReservations(reservations) {
        clearReservations();
            
        if (reservations) {                
            for (var i = 0; i < reservations.length; i++) {
                var start = new Date(parseInt(reservations[i].Start.substr(6)));
                var end = new Date(parseInt(reservations[i].End.substr(6)));
                var customerName = reservations[i].CustomerName;
                appendReservationToTable(start, end, customerName);
            }
        }

        updateReservationsForCurrentDisplayMode();
    }
        
    function appendReservationToTable(startDate, endDate, customerName) {

        var htmlRow = '<tr>' +
                '<td><input type="date" name="start" value="' + getRfc3339FormattedStringFromDate(startDate) + '"/></td>' +
                '<td><input type="date" name="end" value="' + getRfc3339FormattedStringFromDate(endDate) + '"/></td>' +
                '<td><input type="text" name="customer" value="' + customerName + '"/></td>' +
                '<td><button>Delete</button></td>';
            
        $('#reservationsTable tr:last').after(htmlRow);
            
        // button handling in row
        $('#reservationsTable button:last').click(function () {
            $(this).parent().parent().remove();
        });
    }
        
    function getReservations() {
        var reservations = new Array();
        $('#reservationsTable tr').each(function(index, row) {
            if (index > 0) {  // index 0 is header row
                var reservation = { };
                $('input', row).each(function (i, input) {
                    if (i == 0) {
                        reservation['Start'] = getDateFromRfc3339FormattedString($(input).val());
                    } else if (i == 1) {
                        reservation['End'] = getDateFromRfc3339FormattedString($(input).val());
                    } else if (i == 2) {
                        reservation['CustomerName'] = $(input).val();
                    }
                });
                reservations.push(reservation);
            }
        });
        return reservations;
    }
        
    function getRfc3339FormattedStringFromDate(date) {
        var month = date.getMonth() + 1;
        if (month < 10) {
            month = '0' + month;
        } 
        var day = date.getDate();
        if (day < 10) {
            day = '0' + day;
        }

        return date.getFullYear() + '-' + month + '-' + day;
    }

    function getDateFromRfc3339FormattedString(text) {
        var parts = text.split('-');
        return new Date(parts[0], parts[1] - 1, parts[2]);
    }

    function loadEmployee(employeeId) {
        $.getJSON("/Employee/Employee", { 'id': employeeId, 'seed': new Date().getTime() })  // date avoids that call only hits cache... AUTSCH.
                .success(function (data) {
                    initEmployeeDetails(data);
                })
                .error(function () {
                    alert("Error loading data for employee with ID " + employeeId);
                });
    };
        
    function initEmployeeDetails(employee) {
        $('#employeeId').html(employee.Id);
        $('#firstName').val(employee.FirstName);
        $('#lastName').val(employee.LastName);
        $('#street').val(employee.Street);
        $('#city').val(employee.City);
        $('#zipCode').val(employee.ZipCode);
        setCareerLevel(employee.CareerLevel);
        setSkills(employee.Skills);
        setReservations(employee.Reservations);
    }
        
    function saveEmployee() {

        var employee = {
            "id": $('#employeeId').text(),
            "FirstName": $('#firstName').val(),
            "LastName": $('#lastName').val(),
            "Street": $('#street').val(),
            "City": $('#city').val(),
            "ZipCode": $('#zipCode').val(),
            "Skills": getSkills(),
            "CareerLevel": getCareerLevel(),
            "Reservations": getReservations()
        };
        var employeeJsonText = window.JSON.stringify(employee);

        $.ajax({
            type: 'POST',
            url: "/Employee/Employee",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: employeeJsonText
        })
            .success(function (e) {
                loadEmployeeList();
                loadEmployee(e.Id);
            })
            .error(function(xhr, ajaxOptions, thrownError) {
                alert('Error saving data [Status-code:' + xhr.status + ']');
            });
    }
})();