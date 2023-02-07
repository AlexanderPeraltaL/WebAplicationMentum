$(document).ready(function () {
    $("#formCustomers").validate({
        rules: {
            FullName: {
                required: true,
                minlength: 3
            },
            Adress: {
                required: true,
                minlength: 5
            },
            NumberPhone: {
                required: true
            }
        },
        messages: {
            FullName: {
                required: "Por favor ingrese su nombre completo",
                minlength: "El nombre debe tener al menos 3 caracteres"
            },
            Adress: {
                required: "Por favor ingrese su dirección",
                minlength: "La dirección debe tener al menos 5 caracteres"
            },
            NumberPhone: {
                required: "Por favor ingrese su número de teléfono"
            }
        }
    });

    let customerId = new URLSearchParams(window.location.search).get('id');

    fetchContacts(customerId);
});


function fetchContacts(id) {

    $.ajax({
        type: "get",
        url: `/Contacts/GetList/${id}`,
        success: function (response) {
            console.log(response);
            const tbody = $(".table-border-bottom-0");
            response.forEach((data) => {
                tbody.append(buildRow(data));
            });
            $("#divNewContact").append(
                `<button class='btn btn-primary' onClick='newContact(${id})';>Agregar Contacto</button>`,
                '<div></div>',
                `<a class='btn btn-primary' href="/">Lista clientes</a>`
            );
        }
    });
}

$("#formCustomerUpdate").submit(function (e) {
    e.preventDefault();
    debugger;
    var isValid = true;
    $('#formCustomerUpdate input').each(function () {
        if ($(this).val() == "") {
            isValid = false;
            return false;
        }
    });
    if (isValid) {
        e.preventDefault();
        var data = {};
        $('#formCustomerUpdate input').each(function () {
            data[$(this).attr('id')] = $(this).val();
        });
        $.ajax({
            type: "PUT",
            url: "../Contacts/Update",
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
    }
});


function buildRow(data) {
    console.log(data.birthday);
    const tr = $("<tr>");
    tr.append($("<td>").text(data.id));
    tr.append($("<td>").text(data.fullName));
    tr.append($("<td>").text(data.address));
    tr.append($("<td>").text(data.numberPhone));
    tr.append($("<td>").text(data.customersModel.id));
    tr.append($("<td>").text(data.birthday));
    tr.append(
        $(`<td><button class='btn btn-primary' onClick='viewModal(${data.id})'>Edit</button></td>`)
    );
    tr.append(
        $(`<td><button class='btn btn-primary' onClick='deleteContact(${data.id})'>Delete</button></td>`)
    );
    return tr;
}

function newContact(id) {
    window.location.href = `/App/NewContact?id=${id}`;
}

function viewModal(id) {
    $("#basicModal").modal("show");
    $("#modalheader").html(`Editar cliente ${id}`);
    $.ajax({
        type: "get",
        url: `/Contacts/GetById/${id}`,
        success: function (customer) {
            console.log(customer)
            var birthday = new Date(customer.birthday);
            $("#Id").val(customer.id);
            $("#FullName").val(customer.fullName);
            $("#Address").val(customer.address);
            $("#NumberPhone").val(customer.numberPhone);
            $("#Birthday").val(birthday.toISOString().substring(0, 10));

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

function deleteContact(id) {
    debugger;
    $.ajax({
        type: "delete",
        url: `/Contacts/Delete/${id}`,
        success: function (customer) {
            $("#Id").val(customer.id);
            $("#FullName").val(customer.fullName);
            $("#Address").val(customer.address);
            $("#NumberPhone").val(customer.numberPhone);
            location.reload(true);
        }
    });
}



