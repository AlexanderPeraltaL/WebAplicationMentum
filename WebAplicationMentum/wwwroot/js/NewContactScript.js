window.addEventListener("DOMContentLoaded", function () {
    let urlParams = new URLSearchParams(window.location.search);
    let customerId = urlParams.get('id');
    $("#CustomerId").val(customerId);
});

$("#formContact").submit(function (e) {
    e.preventDefault();

    var isValid = true;
    $('#formContact input').each(function () {
        if ($(this).val() == "") {
            isValid = false;
            return false;
        }
    });
    if (isValid) {

        var data = {};
        $('#formContact input').each(function () {
            data[$(this).attr('id')] = $(this).val();
        });
        data["CustomersModel"] = { id: $("#CustomerId").val() };
        id = data["CustomersModel"]["id"];

        $.ajax({
            type: "POST",
            url: "../Contacts/Create",
            data: data,
            success: function (response) {
                console.log(response);
                if (response >= 1) {
                    alert("Se guardo correctamente");
                    $("#formContact")[0].reset();
                    window.location.href = "/App/CustomersContacts?id=" + id;
                }
                else {
                    alert("Algo paso, revise el log de eventos del servidor");
                }
            },
            error: function (response) {
                console.log(response)
                alert("Algo paso");
            }
        });
    } else {
        alert("Por favor, llene todos los campos");
    }
});
