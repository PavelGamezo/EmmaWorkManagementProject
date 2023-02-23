function openAddModal(parameters) {
    const url = parameters.url;
    const modal = $('#modal');

    if (url === undefined) {
        alert('Somthing is wrong...')
    }

    $.ajax({
        type: 'Get',
        url: url,
        success: function (response) {
            modal.find(".modal-body").html(response);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText)
        }
    });
}
