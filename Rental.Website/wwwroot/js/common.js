"use strict";

function GetDate(valDate) {
    var date = new Date(valDate),
        yr = date.getFullYear(),
        month = date.getMonth() < 10 ? '0' + date.getMonth() : date.getMonth(),
        day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate(),
        newDate = yr + '-' + month + '-' + day;
    return newDate; 
}

function StatusResult(status) {
    return status == 'Y' ? 'Active' : 'InActive';
}

function DateToTimeStamp(data) {
    var splitData = data.split('-'), 
     newDate = splitData[1] + "/" + splitData[2] + "/" + splitData[0];
  return new Date(newDate).getTime();
}

function DeleteRow(url) {
    return HttpRequestAsync('', url, 'Delete', true)
}

function HttpRequestAsync(rowData, reqUrl, opType, asyncMethod) {
    $.ajax({
        type: opType,
        data: rowData,
        url: reqUrl,
        async: asyncMethod,
        success: function (response) {
            debugger;
            return response;
        },
    });
}