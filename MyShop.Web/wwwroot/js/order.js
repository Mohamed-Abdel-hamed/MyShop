$(function () {
    $('#order').DataTable({
        serverSide: true,
        processing: true,
        stateSave: true,
        ajax:
        {
            url: '/Admin/Orders/GetOrders',
            type: 'POST'
        },
        order: [[1, 'asc']],
        language: {
            loadingRecords: "Loading...",
            processing: `
                <div class="spinner-border text-secondary" role="status">
                            <span class="sr-only"></span>
                           </div>`
        },
        columnDefs: [{
            targets: [0],
            visible: false,
            searchable: false
        }],
        columns: [
            { "data": "id", "name": "Id" },
            { "data": "userName", "name": "UserName" },
            { "data": "phoneNumber", "name": "PhoneNumber" },
            { "data": "orderStatus", "name": "OrderStatus" },
            { "data": "totalPrice", "name": "TotalPrice" },
            {
                orderable: false, "render": function (data, type, row) {
                    return `
                      <a href="/Admin/Orders/Details/${row.id}" class="btn btn-warning">Details</a>
                    `
                }
            }
        ]
    });
});