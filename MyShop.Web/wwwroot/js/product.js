$(function () { 
    $('#product').DataTable({
        serverSide: true,
        processing: true,
        stateSave:true,
        ajax:
        {
            url: '/Admin/Products/GetProducts',
           type:'POST'
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
            searchable:false
        }],
        columns: [
            { "data": "id", "name": "Id","className":"d-none"},
            { "data": "name", "name": "Name" },
            { "data": "description", "name": "Description" },
            { "data": "price", "name": "Price" },
            { "data": "categoryName", "name": "Category" }, 
            { "data": "createTime", "name": "CreatedDate" },
            {
                orderable: false, "render": function (data, type, row) {
                    return `
                      <a href="/Admin/Products/Edit/${row.id}" class="btn btn-warning">Edit</a>
                        <a href="javascript:;" class="btn btn-danger js-delete-item m-1" data-url="/Admin/Products/Delete/${row.id}">Delete</a>
                    `
                }
            }
        ]
    });
});