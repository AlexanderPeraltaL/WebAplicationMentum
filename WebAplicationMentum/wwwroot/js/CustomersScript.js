

$("#formCustomers").submit(function (e) {
    e.preventDefault();
    debugger;
    var isValid = true;
    $('#formCustomers input').each(function () {
        if ($(this).val() == "") {
            isValid = false;
            return false;
        }
    });

    if (isValid) {
        var data = {};
        $('#formCustomers input').each(function () {
            data[$(this).attr('id')] = $(this).val();
        });

        $.ajax({
            type: "POST",
            url: "../Customers/Create",
            data: data,
            success: function (response) {
                console.log(response);
                if (response >= 1) {
                    alert("Se guardo correctamente");
                    $("#formCustomers")[0].reset();
                    window.location.href = "/App";
                }
                else {
                    alert("Algo paso, revise el log de eventos del servidor");
                }
            },
            error: function (response) {
                alert("Algo paso");
            }
        });
    } else {
        alert("Por favor, llene todos los campos");
    }
});


$("#formCustomerUpdate").submit(function (e) {


    var isValid = true;
    $('#formCustomerUpdate input').each(function () {
        if ($(this).val() == "") {
            isValid = false;
            return false;
        }
    });
    if (isValid) {
        debugger;
        e.preventDefault();
        var data = {};
        $('#formCustomerUpdate input').each(function () {
            data[$(this).attr('id')] = $(this).val();
        });

        $.ajax({
            type: "PUT",
            url: "../Customers/Update",
            data: data,
            success: function (response) {
                console.log(response);
                if (response >= 1) {
                    alert("Se guardo correctamente");
                    location.reload(true);
                }
                else {
                    alert("Algo paso, revise el log de eventos del servidor");
                }
            },
            error: function (response) {
                alert("Algo paso");
            }
        });
    } else {
        alert("Por favor, llene todos los campos");
    }
});
    



window.addEventListener("DOMContentLoaded", function () {
    
    $.ajax({
        type: "get",
        url: "../Customers/GetList",
        success: function (response) {
            console.log(response);
            var tbody = $('.table-border-bottom-0');
            for (var i = 0; i < response.length; i++) {
                var row = buildRow(response[i]);
                tbody.append(row);
            }
        }
    });
});

function buildRow(data) {
    var tr = $('<tr>');
    tr.append($('<td>').text(data.id));
    tr.append($('<td>').text(data.fullName));
    tr.append($('<td>').text(data.address));
    tr.append($('<td>').text(data.numberPhone));
    tr.append($('<td>').text(data.dateCreation));
    tr.append($('<td>').append($('<button>').addClass('btn btn-primary').attr('onClick', 'viewModal(' + data.id + ')').text('Edit')));
    tr.append($('<td>').append($('<a>').attr('href', 'App/CustomersContacts?id=' + data.id).text('Ver contactos')));
    return tr;
}

function viewModal(id)
{
    $("#basicModal").modal("show");
    $("#modalheader").html("Editar cliente " + id);

    $.ajax({
        type: "get",
        url: "../Customers/GetById?Id="+id,
        success: function (customer) {
            $("#Id").val(customer.id);
            $("#FullName").val(customer.fullName);
            $("#Address").val(customer.address);
            $("#NumberPhone").val(customer.numberPhone);

        }
    });
}


$('th').click(function () {
    var table = $(this).parents('table').eq(0)
    var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()))
    this.asc = !this.asc
    if (!this.asc) {
        rows = rows.reverse()
    }
    for (var i = 0; i < rows.length; i++) {
        table.append(rows[i])
    }
    setIcon($(this), this.asc);
})

function comparer(index) {
    return function (a, b) {
        var valA = getCellValue(a, index),
            valB = getCellValue(b, index)
        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.localeCompare(valB)
    }
}

function getCellValue(row, index) {
    return $(row).children('td').eq(index).html()
}

function setIcon(element, asc) {
    $("th").each(function (index) {
        $(this).removeClass("sorting");
        $(this).removeClass("asc");
        $(this).removeClass("desc");
    });
    element.addClass("sorting");
    if (asc) element.addClass("asc");
    else element.addClass("desc");
}


