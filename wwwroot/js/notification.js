window.ShowToastr = function (type, message) {
    switch (type) {
        case "success":
            toastr.success(message);
            break;

        case "error":
            toastr.error(message);
            break;

        default:
            toastr.info(message);
            break;
    }
}