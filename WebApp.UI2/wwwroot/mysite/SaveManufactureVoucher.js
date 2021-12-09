$('#voucherDate').datetimepicker({
    /* format: 'L',*/
    format: 'YYYY/MM/DD'
});


var addManufactureRequest = {
    financialYearId: "string",
    data: "string",
    data2: "string",
    voucherNumber: "string",
    date: "2021-11-26T04:26:32.023Z"
}

function AddJournalEntries(e) {
    // let confirmAction = confirm("Are you sure to delete this product?");
    performPostRequest(e);
}

function performPostRequest(e) {
    e.preventDefault();
    let financialYearId = document.getElementById('financialYearId').value;
    let voucherNumber = document.getElementById('Input_VoucherNumber').value;   
    let date = $('#voucherDate').val();
    let userId = document.getElementById('userId');
    let userAccessToken = userId.value;
    let apiUrl = document.getElementById('apiurl').value;

    let OrderDetl = [];
    $('#tbodyitems tr').each(function () {
        let text = $(this).find("td").eq(4).html();
        if (text !== "Total") {

            let Name = $(this).find("td").eq(1).text().trim();
            let id = $(this).find(".hdnappendId").val();
            let variantId = $(this).find(".hdnappendVarientId").val();
            let storeProductId = $(this).find(".hdnappendStoreProductId").val();
            let ItemAmount = $(this).find(".hdnappendAmount").val();
            let MRPPerUnit = $(this).find(".hdnappendMRP").val();
            let Quantity = $(this).find(".hdnappendQuantity").val();
            let consumedQuantity = $(this).find(".hdnappendConsumedQuantity").val();
            let status = $(this).find(".hdnappendStatus").val();

            if (id === "/") {
                id = "00000000-0000-0000-0000-000000000000"
            }
            OrderDetl.push({
                'Name': Name,
                'StockItemId': storeProductId,
                'Id': id,
                "type": "Input",
                'Rate': MRPPerUnit,
                'Quantity': Quantity,
                'ConsumedQuantity': consumedQuantity,
                'ItemAmount': ItemAmount,
                'Status': status
            });
        }
    });

    alert(JSON.stringify(OrderDetl) + '  ' + OrderDetl.length)

    let OrderOutput = [];
    $('#tbodyitemsOutput tr').each(function () {
        let text = $(this).find("td").eq(4).html();
        if (text !== "Total") {

            let Name = $(this).find("td").eq(1).text().trim();
            let id = $(this).find(".hdnappendIdOutput").val();
            let variantId = $(this).find(".hdnappendVarientIdOutput").val();
            let storeVariantId = $(this).find(".hdnappendProductIdOutput").val();
            let ItemAmount = $(this).find(".hdnappendAmountOutput").val();
            let MRPPerUnit = $(this).find(".hdnappendMRPOutput").val();
            let Quantity = $(this).find(".hdnappendQuantityOutput").val();
            let ProductId = $(this).find(".hdnappendProductIdOutput").val();
            if (id === "/") {
                id = "00000000-0000-0000-0000-000000000000"
            }
            OrderOutput.push({
                'Name': Name,
                'storeVariantId': storeVariantId,
                'variantId': variantId,
                'Id': id,
                "type": "Output",
                'Rate': MRPPerUnit,
                'Quantity': Quantity,
                'ItemAmount': ItemAmount
            });
        }
    });

    addManufactureRequest.financialYearId = financialYearId;
    addManufactureRequest.voucherNumber = voucherNumber;
    addManufactureRequest.date = date;
    addManufactureRequest.data = JSON.stringify(OrderDetl);
    addManufactureRequest.data2 = JSON.stringify(OrderOutput);

    axios.post(`${apiUrl}/api/Manufacture/AddManufactureVoucher/`, this.addManufactureRequest, {
        headers: {
            Authorization: "Bearer " + userAccessToken
        }
    })
        .then(function (response) {
            console.log(response);
            if (response.data.success) {
                toastr.success(response.data.description)
                window.location.href = '/MyBooks/VoucherWithItems/index'
            } else {
                toastr.error(response.data.description)
            }
        })
        .catch(function (response) {
            console.log(response);
            toastr.error(response)
        });
};