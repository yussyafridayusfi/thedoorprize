var Hadiah = []
const base_url = `${base_url_home}App/`

$(document).ready(function () {

  console.log('ready')
  getHadiah()

  $("#btnSaveAttachment").click(() => {

      if ($("#fileAttachment").val() != "") {
        const fi = $("#fileAttachment")[0].files[0]
        let filesize = fi['size']

        //console.log(fi)
        if (filesize > 5000000) {
          $("#errorFile").removeClass('d-none').text('file yang diupload terlalu besar')

        } else {
          if (
            fi['type'] !== 'application/vnd.ms-excel' && // .xls
            fi['type'] !== 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' // .xlsx
          ) {
            $("#errorFile").removeClass('d-none').text('File yang diupload salah')
          } else {

            if (typeof (FileReader) != "undefined") {
              loadPanel.show();

              var formData = new FormData();
              formData.append('infile', fi);

              //const file = document.getElementById('excelFile').files[0];
              if (!fi) return alert('Please select a file.');

              const reader = new FileReader();
              reader.onload = function (e) {
                const data = new Uint8Array(e.target.result);
                const workbook = XLSX.read(data, { type: 'array' });

                const firstSheetName = workbook.SheetNames[0];
                const worksheet = workbook.Sheets[firstSheetName];
                const jsonData = XLSX.utils.sheet_to_json(worksheet, { defval: "" });

                // Convert numeric strings to actual numbers
                const cleanedJson = jsonData.map(row => {
                  return {
                    No: parseInt(row["No"]),
                    Hadiah: row["Hadiah"],
                    Status: parseInt(row["Status"]),
                    Unit: parseInt(row["Unit"]),
                    isBesar: parseInt(row["isBesar"])
                  };
                });

                // POST JSON to controller
                fetch('/App/SaveJsonHadiah', {
                  method: 'POST',
                  headers: {
                    'Content-Type': 'application/json'
                  },
                  body: JSON.stringify(cleanedJson)
                })
                  .then(response => {
                    if (response.ok) alert("Success!");
                    else alert("Error saving file.");
                  });
              };
              reader.readAsArrayBuffer(fi);

            } else {
              swal("This browser does not support HTML5.", {
                icon: "warning"
              });
              loadPanel.hide();
            }

          }
        }
      } else {
        swal("Pilih File terlebih dahulu", {
          icon: "warning"
        });
        loadPanel.hide();
      }
    })

})

function getHadiah() {
  loadPanel.show()
  console.log(base_url_home)
  $.ajax({
    type: "POST",
    url: base_url + "GetHadiah",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result, status, xhr) {
      console.log("Hadiah");

      console.log(result);

      if (result.status == "1") {
        for (var i = 0; i < result.data.length; i++) {
          Hadiah.push({ id: result.data[i].No, nama: result.data[i].Hadiah, jumlah: result.data[i].Unit, isBesar: result.data[i].isBesar, status: result.data[i].Status })
        }

        ShowDataGrid(Hadiah)
        
      }
      else {
        alert(result.message);
      }

      loadPanel.hide()
    },
    error: function (xhr, status, error) {
      loadPanel.hide()
      console.log("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
    }
  });
}



function ShowDataGrid(data) {
  console.log(data)
  var dataGrid = $("#grid").dxDataGrid({
    dataSource: data,
    remoteOperations: true,
    columnMinWidth: 150,
    columnAutoWidth: true,
    filterRow: {
      visible: true,
      applyFilter: "auto"
    },
    headerFilter: {
      visible: true
    },
    hoverStateEnabled: true,
    groupPanel: {
      visible: true
    },
    grouping: {
      autoExpandAll: false
    },
    scrolling: {
      columnRenderingMode: "virtual"
    },
    columnAutoWidth: true,
    export: {
      enabled: true,
      fileName: "DataHadiahDoorprize",
      allowExportSelectedData: false
    },
    allowColumnReordering: true,
    allowColumnResizing: true,
    showBorders: true,
    wordWrapEnabled: true,
    columns: [
      { dataField: "id", caption: "No", dataType: "number", width : 100 },
      { dataField: "nama", caption: "Hadiah", dataType: "string" },
      { dataField: "status", caption: "Status", dataType: "number"},
      { dataField: "jumlah", caption: "Jumlah Unit", dataType: "number" },
      { dataField: "isBesar", caption: "isBesar", dataType: "number", visible : false }
    ],
    toolbar: {
      items: [
        // ... other toolbar items
        {
          location: 'after', // or 'before', 'center'
          widget: 'dxButton',
          options: {
            icon: 'upload',
            text: 'Upload',
            //elementAttr: { id: 'uploadFilesButton' },
            onClick: function () {
              //console.log('upload')
              $("#modalAddAttachment").modal('show');
            }
          }
        },
        {
          location: 'after', // or 'before', 'center'
          widget: 'dxButton',
          options: {
            icon: 'download',
            text: 'Download Template',
            onClick: function () {
              console.log('download')
              window.location.href = base_url + '/DownloadTemplate';

              
            }
          }
        },
      ]
    },
    onExporting: function (e) {
      var workbook = new ExcelJS.Workbook();
      var worksheet = workbook.addWorksheet('Main sheet');
      var dateNow = new Date().toISOString().split('T')[0]
      dateNow = dateNow.replace('-', '')
      var fileName = "Data Hadiah Doorprize " + dateNow.replace('-', '')
      DevExpress.excelExporter.exportDataGrid
        ({
          worksheet: worksheet,
          component: e.component,
          customizeCell: function (options) {
            options.excelCell.font = { name: 'Arial', size: 12 };
            options.excelCell.alignment = { horizontal: 'left' };
          }
        }).then(function () {
          workbook.xlsx.writeBuffer().then(function (buffer) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), `${fileName}.xlsx`);
          });
        });
    },
    onRowPrepared: function (e) {
      if (e.rowType === "data") {
        const statusText = e.data.STATUS;
        const statusColumnIndex = e.columns.findIndex(col => col.dataField === "Status");

        if (statusColumnIndex !== -1) {
          const $statusCell = $(e.rowElement).find('td').eq(statusColumnIndex);

          if (statusText === '1') {
            $statusCell.css("color", "#40eb15"); // green text
          } else {
            $statusCell.css("color", "#ff0f1f"); // red text
          }
        }
      }
    },
    height: 600,
    showBorders: true,
    paging: {
      pageSize: 20
    },
    pager: {
      visible: true,
      showPageSizeSelector: true,
      allowedPageSizes: [20, 40, 60],
      showInfo: true
    },
    sorting: {
      mode: "multiple"
    },
    filterRow: {
      visible: true,
      applyFilter: "auto"
    },
    headerFilter: { visible: true },
    grouping: { autoExpandAll: false }

  }).dxDataGrid("instance");

  return dataGrid;

}
