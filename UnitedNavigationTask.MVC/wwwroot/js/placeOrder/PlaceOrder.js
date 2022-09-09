$(document).ready(function () {
    var table = $('#orders').DataTable();

    $('#orders tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');
    });

    $('#button').click(function () {
        //dataRecords = table.rows('.selected').data().map((row) => {
        //    var csvDto = {
        //        OrderNumber: row[0],
        //        ConsignmentNumber: row[1],
        //        ParcelCode: row[2],
        //        ConsigneeName: row[3],
        //        AddressOne: row[4],
        //        AddressTwo: row[5],
        //        City: row[6],
        //        CountryCode: row[7],
        //        ItemQuantity: row[8],
        //        ItemValue: row[9],
        //        ItemWeight: row[10],
        //        ItemDesciption: row[11],
        //        ItemCurrency: row[12]
        //    }
        //    console.log(row);
        //    console.log(row.toString());
        //    console.log(JSON.stringify(row));
        //    csvDto = JSON.stringify(csvDto);
        //});

        var dataArray = table.rows('.selected').data().toArray();

        var data = new Array();

        $.each(dataArray, function (index, value) {

            var csvDto = {};
            csvDto.OrderNumber = value[0];
            csvDto.ConsignmentNumber = value[1];
            csvDto.ParcelCode = value[2];
            csvDto.ConsigneeName = value[3];
            csvDto.AddressOne = value[4];
            csvDto.AddressTwo = value[5];
            csvDto.City = value[6];
            csvDto.CountryCode = value[7];
            csvDto.ItemQuantity = value[8];
            csvDto.ItemValue = value[9];
            csvDto.ItemWeight = value[10];
            csvDto.ItemDesciption = value[11];
            csvDto.ItemCurrency = value[12];
            data.push(csvDto);
        });

        $.ajax({
            type: 'POST',
            url: '/Home/PlaceOrder',
            data: JSON.stringify(data),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                alert("success");
            },
            error: function (response) {
                alert("error");
            }
        });

        //let postData = {
        //    EmpId: $("#txtId").val(),
        //    EmpName: $("#txtName").val(),
        //    EmpSalary: $("#txtSalary").val()
        //}

        //var promises = GetAjaxDataPromise('@Url.Action("PlaceOrder", "Home")', postData);
        //promises.done(function (response) {
        //    debugger;
        //    alert("Hello");
        //});
    });
});

//function GetAjaxDataPromise(url, postData) {
//    debugger;
//    var promise = $.post(url, postData, function (promise, status) {
//    });
//    return promise;
//};