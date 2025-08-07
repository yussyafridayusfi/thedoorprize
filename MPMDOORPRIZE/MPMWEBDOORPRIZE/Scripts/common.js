let loadPanel
let toast
const toastTypes = {
    SUCCESS: "success",
    ERROR: "error",
    WARNING: "warning",
    INFO: "info"
}

$(document).ready(function () {
    loadPanel = $('.loadpanel').dxLoadPanel({
        shadingColor: 'rgba(0,0,0,0.4)',
        visible: false,
        showIndicator: true,
        showPane: true,
        shading: true,
        hideOnOutsideClick: false,
    }).dxLoadPanel('instance');

    toast = $('#toast').dxToast({
        displayTime: 1000, width: 400
    }).dxToast('instance');
})

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

function getFstDayOfMonFnc() {
    var date = new Date();
    return new Date(date.getFullYear(), date.getMonth(), 1)
}

function getUrl(url) {
    let finalUrl = null
    $('.url').each(function () {
        if ($(this).data(url)) finalUrl = $(this).data(url)
    })
    return finalUrl
}

