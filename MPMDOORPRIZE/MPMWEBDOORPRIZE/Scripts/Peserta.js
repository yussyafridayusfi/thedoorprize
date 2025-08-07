var setMonth, setYear;

$(document).ready(function () {


  $('#btnFilter').on('click', async function () {
    const getAll = await getData()
    //console.log(getAll)
    var arry_data = getAll.data.data.map(item => ({
      ...item,
      TGLUPLOAD: convertDate(item.TGLPROSES),
      STARTPERIOD: convertDate(item.STARTPERIOD),
      ENDPERIOD: convertDate(item.ENDPERIOD)
    }));
    /*console.log(arry_data)*/
    ShowDataGrid(arry_data)

  })

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

            fetch(base_url_home + 'App/UploadTemp', {
              method: 'POST',
              body: formData
            })
              .then(response => {
                if (!response.ok) {
                  swal('Network response was not ok ' + response.statusText, {
                    icon: "error"
                  });
                  loadPanel.hide()
                }

                if (response.status == 0) {
                  swal(response.message, {
                    icon: "error"
                  });
                  loadPanel.hide()
                  return false
                }
                return response.blob();
              })
              .then(blob => {
                swal("Upload data Nosin berhasil!", {
                  icon: "success"
                }).then((result) => {
                  loadPanel.hide();
                  location.reload()

                });

                const url = window.URL.createObjectURL(blob);
                const a = document.createElement('a');
                a.href = url;
                a.download = 'UPLOADNOSINCLAIMAHM.xlsx';
                document.body.appendChild(a);
                a.click();
                a.remove();
                window.URL.revokeObjectURL(url);
                loadPanel.hide();
              })
              .catch(error => {
                swal("Upload data SKPB Unpaid AHM gagal!", {
                  icon: "warning"
                }).then((result) => {
                  loadPanel.hide();
                  location.reload()
                });

                loadPanel.hide();
              });
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


function convertDate(jsonDate) {
  if (!jsonDate) return "";

  let match = jsonDate.match(/\d+/);
  if (!match) return "Invalid Date";

  let timestamp = parseInt(match[0]);
  let date = new Date(timestamp);

  let year = date.getFullYear();
  let month = date.getMonth() + 1;
  let day = date.getDate();

  return `${year}-${String(month).padStart(2, '0')}-${String(day).padStart(2, '0')}`;
}

async function getData() {
  loadPanel.show()
  try {
    const base_url = `${base_url_home}App/`
    var tahunPeriode = $("#gridTahun").val()
    var bulanPeriode = $('#gridbulan').val()
    let url = base_url + 'GetData'

    setMonth = bulanPeriode
    setYear = tahunPeriode

    const result = await axios.post(url, {
      bulan: bulanPeriode,
      tahun: tahunPeriode
    })
    loadPanel.hide()
    //console.log(result)
    return result
  } catch (e) {

  }
}

function ShowDataGrid(data) {

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
      fileName: "DataPesertaDoorprize",
      allowExportSelectedData: false
    },
    allowColumnReordering: true,
    allowColumnResizing: true,
    showBorders: true,
    wordWrapEnabled: true,
    columns: [
      { dataField: "NPK", caption: "Status", dataType: "string" },
      { dataField: "NAMA", caption: "Status", dataType: "string" },
      { dataField: "COMPANYOFFICE", caption: "Status", dataType: "string" },
      { dataField: "KODEWARNA", caption: "Status", dataType: "string" },
      { dataField: "HADIAH", caption: "Status", dataType: "number" },
      { dataField: "ABSEN", caption: "Status", dataType: "number" },
      { dataField: "AMBILHADIAH", caption: "Status", dataType: "number" },
      { dataField: "KETERANGAN", caption: "Status", dataType: "string" },
      { dataField: "ISMULIA", caption: "Status", dataType: "number" },
    ],
    toolbar: {
      items: [
        // ... other toolbar items
        {
          location: 'after', // or 'before', 'center'
          widget: 'dxButton',
          options: {
            icon: 'download',
            text: 'Download Template',
            onClick: function () {

              var wb = XLSX.utils.book_new();
              wb.Props = {
                Title: "Template Upload Data Peserta",
                Subject: "Template Upload Data Peserta",
                Author: "IT",
                CreatedDate: new Date()
              };

              //wb.SheetNames.push("sheet1");
              var header = ['NPK', 'NAMA', 'DEALERCODE', 'ITEMID', 'NOMORMESIN'
                , 'NOMORRANGKA'
              ];
              var ws_data = [];
              ws_data.push(header);
              var ws = XLSX.utils.aoa_to_sheet(ws_data);
              //wb.Sheets["sheet1"] = ws;

              XLSX.utils.book_append_sheet(wb, ws, 'upload')

              var wbout = XLSX.write(wb, { bookType: 'xlsx', type: 'binary' });

              saveAs(new Blob([s2ab(wbout)], { type: "application/octet-stream" }), 'templateuploadnosinclaimahm.xlsx');

            }
          }
        },
        {
          location: 'after',
          widget: 'dxButton',
          options: {
            icon: 'info',
            text: 'Detail',
            //elementAttr: { id: 'uploadFilesButton' },
            onClick: function () {
              const url = `${base_url_home}App/GetDataDetail`;
              axios.post(url, {
              })
                .then(function (response) {
                  //console.log(response);
                  var dataDetail = response.data.data;
                  // console.log(dataDetail)
                  ShowPopUp(dataDetail);
                  //show_data_grid(response.data.data);
                })
                .catch(function (error) {
                  //console.log(error);
                });

            }
          }
        }
      ]
    },
    onExporting: function (e) {
      var workbook = new ExcelJS.Workbook();
      var worksheet = workbook.addWorksheet('Main sheet');
      var dateNow = new Date().toISOString().split('T')[0]
      dateNow = dateNow.replace('-', '')
      var fileName = "Upload Nosin Claim AHM " + dateNow.replace('-', '')
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
        const statusColumnIndex = e.columns.findIndex(col => col.dataField === "STATUS");

        if (statusColumnIndex !== -1) {
          const $statusCell = $(e.rowElement).find('td').eq(statusColumnIndex);

          if (statusText === 'Terkirim') {
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


function s2ab(s) {
  var buf = new ArrayBuffer(s.length);
  var view = new Uint8Array(buf);
  for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF;
  return buf;
}


function ShowPopUp(data) {
  //console.log('showpopup ',data)
  buttonItems = [
    {
      toolbar: 'bottom', location: 'after', widget: 'button',
      options: {
        text: 'OK',
        onClick: function () {
          var e = $("#popupContainer").dxPopup("instance")
          e.element().attr('act', 1);
          e.hide();
        }
      }
    },
    {
      toolbar: 'bottom', location: 'after', widget: 'button', options: {
        text: 'Cancel', onClick: function () {
          var e = $("#popupContainer").dxPopup("instance");
          e.element().attr('act', 0);
          e.hide();
        }
      }
    }
  ];

  var defer = $.Deferred();
  if ($('#popupContainer').length <= 0) {
    $("html").prepend('<div id="popupContainer"></div>');
  }
  $('#popupContainer').dxPopup({
    contentTemplate: $('#content_template'),
    buttons: buttonItems,
    showCloseButton: true,
    width: 1180,
    height: 680,
    dragEnabled: false,
    hideOnOutsideClick: false,
    onShown: function (e) {
      e.element.attr('act', 0);
      $("#gridInPopup").dxDataGrid({
        dataSource: data,
        export: {
          enabled: true,
          //fileName: "DataNosinBelumKirim",
          allowExportSelectedData: false
        },
        toolbar: {
          items: [
            {
              name: "exportButton",
              showText: "always"
            }
          ]
        },
        columns: [
          { dataField: 'JUKLAKNO', caption: 'No Juklak' },
          { dataField: 'DEALERCODE', caption: 'Kode Dealer' },
          { dataField: 'DEALERNAME', caption: 'Nama Dealer' },
          { dataField: 'XTSMAINDEALERCODE', caption: 'Kode Cabang' },
          { dataField: 'ITEMID', caption: 'Item ID' },
          { dataField: 'FRAMENO', caption: 'No Rangka' },
          { dataField: 'ENGINENO', caption: 'No Mesin' }
        ],
        onExporting: function (e) {
          const workbook = new ExcelJS.Workbook();
          const worksheet = workbook.addWorksheet("Data");

          DevExpress.excelExporter.exportDataGrid({
            component: e.component,
            worksheet: worksheet,
            autoFilterEnabled: true
          }).then(function () {
            workbook.xlsx.writeBuffer().then(function (buffer) {
              saveAs(new Blob([buffer], { type: "application/octet-stream" }), "DataNosinBelumKirim.xlsx");
            });
          });

          e.cancel = true; // Cancel built-in export
        },
        sorting: {
          mode: "multiple"
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        columnAutoWidth: true,
        groupPanel: {
          visible: true
        },
        paging: {
          pageSize: 10
        },
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
          mode: "standard" // or "virtual" | "infinite"
        },
        columnAutoWidth: true,
        allowColumnReordering: true,
        allowColumnResizing: true,
        showBorders: true,
        pager: {
          visible: true,
          allowedPageSizes: [5, 10],
          showPageSizeSelector: true,
          showInfo: true,
          showNavigationButtons: true,
        },
        onEditorPreparing: function (e) {
          if (e.parentType === 'dataRow' && e.dataField === 'Position') {
            e.editorOptions.readOnly = isChief(e.value);
          }

          if (e.parentType === "dataRow") {
            e.editorOptions.onKeyDown = function (arg) {
              if (arg.event.keyCode === 13) {
                arg.event.stopPropagation();
              }
            };
          }
        },
        repaintChangesOnly: true,
        onEditorPreparing: function (e) {
          if (e.parentType === "dataRow") {
            /*    console.log(e)*/
            e.editorOptions.onKeyDown = function (arg) {
              if (arg.event.keyCode === 13) {
                arg.event.stopPropagation();
              }
            };
          }

          e.editorOptions.onOpened = function (arg) {
          }
        },

      });

    },
    onHidden: function (e) {
      if (e.element.attr('act') == "1") {
        defer.resolve(true, $("#gridInPopup").dxDataGrid("instance").getSelectedRowsData());
      }
      else {
        defer.resolve(false, []);
      }
    },
    animation: {
      show: { type: "slide", from: { opacity: 1, top: -$(window).height() }, to: { top: 50 } },
      hide: { type: "slide", from: { top: 50 }, to: { top: -$(window).height() } }
    }
  });

  $("#popupContainer").dxPopup("instance").show();
  return defer.promise();
}


function exportGridToExcel(grid) {
  const workbook = new ExcelJS.Workbook();
  const worksheet = workbook.addWorksheet("Export");

  DevExpress.excelExporter.exportDataGrid({
    component: grid,
    worksheet: worksheet,
    autoFilterEnabled: true
  }).then(function () {
    workbook.xlsx.writeBuffer().then(function (buffer) {
      saveAs(new Blob([buffer], { type: "application/octet-stream" }), "DataNosinBelumKirim.xlsx");
    });
  });
}
