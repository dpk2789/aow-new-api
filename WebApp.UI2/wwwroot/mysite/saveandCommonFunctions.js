var loading = $("#loading");
$(document).on({
    ajaxStart: function () {
        loading.show();
    },
    ajaxStop: function () {
        loading.hide();
    }
});

function JsonDate(jsonDate) {
    if (!jsonDate) {
        return jsonDate;
    }
    //    var yojsonDate = (new Date()).toJSON();
    var date = new Date(parseInt(jsonDate.substr(6)));
    var hours = date.getHours();
    var ampm = hours >= 12 ? 'pm' : 'am';
    var month = date.getMonth() + 1;
    return date.getFullYear() + '/' + month + '/' + date.getDate() + " " + date.getHours() + ":" + date.getMinutes() + " " + ampm;
    //return yojsonDate;
}

$("#Input_LedgerName").autocomplete({
    minLength: 1,
    source: function (request, response) {
        var url = $(this.element).data("url");
        $.getJSON(url,
            { term: request.term },
            function (data) {
                response(data)
            });
    },
    appendTo: $(".modal-body ui-autocomplete-input"),
    select: function (event, ui) {
        $("#Input_LedgerId").val(ui.item.id);
        $("#Input_LedgerName").val(ui.item.Name);
        $('#Input_VoucherNumber').focus()
    },
    change: function (event, ui) {
        if (!ui.item) {
            $(event.target).val("");
        }
    }

});

var addVoucherWithItemsRequest = {
    id: "string",
    financialYearId: "string",
    voucherName: "string",
    data: "string",
    data2: "string",
    invoice: "string",
    termsDays: 0,
    accountId: "string",
    total: "string",
    note: "string",
    buttonValue: "string",
    date: "string",
}

function AddInvoice(e) {
    // let confirmAction = confirm("Are you sure to delete this product?");
    let url = "AddVoucherInvoice";
    let voucherId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
    performPostRequest(e, url, voucherId);
}

function UpdateInvoice(e) {
    // let confirmAction = confirm("Are you sure to delete this product?");
    let url = "UpdateVoucherInvoice";
    let voucherId = document.getElementById('voucherId').value;
    performPostRequest(e, url, voucherId);
}

function performPostRequest(e, url, voucherId) {
    e.preventDefault();
    let buttonValue = $(this).closest("input");
    let apiUrl = document.getElementById('apiurl').value;
    let date = $('#voucherDate').val();
    let userId = document.getElementById('userId');
    let financialYearId = document.getElementById('financialYearId').value;
    let userAccessToken = userId.value;
    let voucherName = $('#VoucherName').val();
    $(".SaveOrder").attr("class", "btn btn-primary SaveOrder disabled");
    let form = $('#addPurchaseBill');
    let token = $('input[name="__RequestVerificationToken"]', form).val();
    let Invoice = $('#Input_VoucherNumber').val();
    let AccountId = $('#Input_LedgerId').val();
    let termsDays = $('#terms').val();
    let Note = $('#editor1').val();
    let Total = $('#Input_Total').val();

    if (date == null || date == "") {
        alert("Please Enter Date");
        return false;
    }

    let OrderDetl = [];
    $('#tbodyitems tr').each(function () {
        let text = $(this).find("td").eq(4).html();
        if (text !== "Total") {
            let Name = $(this).find("td").eq(1).text().trim();
            //let Quantity = parseFloat($(this).find("td").eq(3).html()).toFixed(2);;
            let ItemAmount = $(this).find(".hdnappendAmount").val();
            let MRPPerUnit = $(this).find(".hdnappendMRP").val();
            let Quantity = $(this).find(".hdnappendQuantity").val();
            let ProductId = $(this).find(".hdnappendProductId").val();
            let ItemId = $(this).find(".hdnappendProductAccountId").val();
            if (ItemId === "/") {
                ItemId ="00000000-0000-0000-0000-000000000000"
            }
            let hdnAccountCategoryName = $(this).find(".hdnappendhdnAccountCategoryName").val();
            let hdnappendhdnItemType = $(this).find(".hdnappendhdnItemType").val();
            OrderDetl.push({
                'Name': Name,
                'ProductId': ProductId,
                'Id': ItemId,
                'MRPPerUnit': MRPPerUnit,
                'Quantity': Quantity,
                'ItemAmount': ItemAmount,
                'AccountCategoryName': hdnAccountCategoryName,
                'ItemType': hdnappendhdnItemType
            });
        }
    });

    let sundryItems = [];
    $('#tbodysundryitems tr').each(function () {
        let text = $(this).find("td").eq(1).html();
        if (text != "Total") {
            let sundtryItemId = $(this).find(".hdnappendSundryProductId").val();
            let ledgerId = $(this).find(".hdnappendSundryAccountId").val();
            let SundryAmount = $(this).find(".hdnappendSundryAmount").val();
            let sundryPercent = $(this).find(".hdnappendSundryPercent").val();

            sundryItems.push({
                'ProductId': sundtryItemId,
                'LedgerId': ledgerId,
                'ItemAmount': SundryAmount,
                'Percent': sundryPercent
            });
        }
    });

   // alert(JSON.stringify(sundryItems) + '  ' + sundryItems.length)

    addVoucherWithItemsRequest.id = voucherId;
    addVoucherWithItemsRequest.financialYearId = financialYearId;
    addVoucherWithItemsRequest.accountId = AccountId;
    addVoucherWithItemsRequest.invoice = Invoice;
    addVoucherWithItemsRequest.date = date;
    addVoucherWithItemsRequest.voucherName = voucherName;
    addVoucherWithItemsRequest.note = Note;
    addVoucherWithItemsRequest.total = Total;
    addVoucherWithItemsRequest.termsDays = termsDays;
    addVoucherWithItemsRequest.data = JSON.stringify(OrderDetl);
    addVoucherWithItemsRequest.data2 = JSON.stringify(sundryItems);
    axios.post(`${apiUrl}/api/VoucherInvoice/` + url, this.addVoucherWithItemsRequest, {
        headers: {
            Authorization: "Bearer " + userAccessToken
        }
    })
        .then(function (response) {
            console.log(response);
            if (response.data.success) {
                toastr.success(response.data.description)
                window.location.href = '/MyBooks/VoucherWithItems/index?voucherName=' + voucherName
            } else {
                toastr.error(response.data.description)
            }
        })
        .catch(function (response) {
            console.log(response);
            toastr.error(response)
        });
};
