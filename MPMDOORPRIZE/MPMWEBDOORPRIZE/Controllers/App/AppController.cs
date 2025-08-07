using MPMMODELWEB.Record;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace MPMWEBDOORPRIZE.Controllers.App
{
    public class AppController : Controller
    {
        // get path file location
        public static string basePath = AppDomain.CurrentDomain.BaseDirectory;
        public string jsonPathDataPeserta = Path.Combine(basePath, "Content", "data.json");
        public string jsonPathHadiah = Path.Combine(basePath, "Content", "hadiah.json");

        // GET: App
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Doorprize()
        {
            return View("Doorprize");
        }

        public ActionResult Peserta()
        {
            return View("Peserta");
        }

        public ActionResult Hadiah()
        {
            return View("Hadiah");
        }

        public ActionResult BackgroundUpload()
        {
            return View("BackgroundUpload");
        }

        public ActionResult DownloadTemplate()
        {
            string relativePath = "~/Scripts/templatehadiahdoorprize.xlsx";
            string fullPath = Server.MapPath(relativePath);
            string fileName = Path.GetFileName(fullPath);

            return File(fullPath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        [HttpPost]
        public JsonResult GetHadiah()
        {
            try
            {
                var json_text = jsonPathHadiah;
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonHadiahRec> items = JsonConvert.DeserializeObject<List<JsonHadiahRec>>(json);
                    List<HadiahRecWeb> data = (from a in items
                                               //where a.Status == "0"
                                               select new HadiahRecWeb
                                               {
                                                   No = int.Parse(a.No),
                                                   Hadiah = a.Hadiah,
                                                   Status = (a.Status == "1") ? "Aktif" : "Tidak Aktif",
                                                   Unit = int.Parse(a.Unit),
                                                   isBesar = int.Parse(a.isBesar)
                                               }).ToList();

                    if (items == null || items.Count().Equals(0))
                    {
                        return Json(new
                        {
                            status = 0,
                            message = "Data Not Found",
                            data = new { }
                        });
                    }

                    return Json(new
                    {
                        status = 1,
                        message = "OK",
                        //data = data.OrderByDescending(c => c.No).ToList()
                        data = data.ToList()
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = 0,
                    message = e.Message,
                    data = new { }
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveJsonHadiah()
        {
            try
            {
                Request.InputStream.Position = 0;
                using (var reader = new StreamReader(Request.InputStream))
                {
                    var jsonContent = await reader.ReadToEndAsync();

                    // Define file path to save (e.g., Content/Uploads/)
                    var folderPath = Server.MapPath("~/Content/Uploads/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileName = "hadiah" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".json";
                    var filePath = Path.Combine(folderPath, fileName);

                    System.IO.File.WriteAllText(filePath, jsonContent);

                    return new HttpStatusCodeResult(200);
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Error: " + ex.Message);
            }
        }



    }
}