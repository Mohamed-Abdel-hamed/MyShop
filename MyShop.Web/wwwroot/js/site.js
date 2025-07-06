$(function () {
    UploadImage();
    DeleteItem();
});

function DeleteItem() {
    $('body').on('click', '.js-delete-item', function () {
        var btn = $(this);
        var row = btn.closest('tr');
        bootbox.confirm({
            message: "do you want to delete this item ?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                }, 
                cancel: {
                    label: 'No',
                    className: 'btn-primary'
                }
            },
            callback: function (result) {

                if (result)
                {
                    $.post({
                        url: btn.data('url'),
                        success: function () {
                            row.remove();
                        }
                    });
                }
            }
        });

    });
}
function UploadImage() {
    $('#ImageUpload').on('change', function () {
        $('#ImagePreview').attr('src', window.URL.createObjectURL(this.files[0]));
    });
}