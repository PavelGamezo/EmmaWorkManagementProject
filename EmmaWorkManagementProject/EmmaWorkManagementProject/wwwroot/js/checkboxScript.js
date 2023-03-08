function checkboxScript(parameters) {
    const id = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');

    if (id === undefined || url === undefined) {
        alert('Somthing is wrong...')
    }

    $.ajax({
        type: 'Get',
        url: url,
        data: { "id": id },
        success: function (response) {
            location.reload();
        },
        failure: function () {
            location.reload();
        },
        error: function (response) {
            location.reload();
        }
    });
}