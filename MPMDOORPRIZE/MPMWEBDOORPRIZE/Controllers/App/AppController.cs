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

        public ActionResult RolateDoorprize()
        {
            return View("RolateDoorprize");
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

        public ActionResult DownloadTemplatePeserta()
        {
            string relativePath = "~/Scripts/templatepesertadoorprize.xlsx";
            string fullPath = Server.MapPath(relativePath);
            string fileName = Path.GetFileName(fullPath);

            return File(fullPath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPost]
        public JsonResult GetHadiah()
        {
            try
            {
                //var data = Model._queryWeb.listHadiahDoorPrize();
                //var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
                var json_text = jsonPathHadiah;
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiah.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonHadiahRec> items = JsonConvert.DeserializeObject<List<JsonHadiahRec>>(json);
                    List<HadiahRecWeb> data = (from a in items
                                               where a.Status == "0"
                                               select new HadiahRecWeb
                                               {
                                                   No = int.Parse(a.No),
                                                   Hadiah = a.Hadiah,
                                                   Status = a.Status,
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
                        data = data.OrderByDescending(c => c.No).ToList()
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
        public JsonResult GetHadiahGrid()
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
                    var folderPath = Server.MapPath("~/Content/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileName = "hadiah" + ".json";
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

        [HttpPost]
        public JsonResult GetPeserta()
        {
            try
            {
                var json_text = jsonPathDataPeserta;
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    List<RecordWeb> data = (from a in items
                                            select new RecordWeb
                                            {
                                                NPK = a.NPK.Length != 5 ? a.NPK.PadLeft(4, '0') : a.NPK,
                                                NAMA = a.NAMA.Length <= 20 ? a.NAMA : a.NAMA.Substring(0, 20),
                                                HADIAH = a.HADIAH,
                                                //ISMULIA = a.ISMULIA == "1" ? "Mulia" : "Bukan Mulia" 
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
        public async Task<ActionResult> SaveJsonPeserta()
        {
            try
            {
                Request.InputStream.Position = 0;
                using (var reader = new StreamReader(Request.InputStream))
                {
                    var jsonContent = await reader.ReadToEndAsync();

                    // Define file path to save (e.g., Content/Uploads/)
                    var folderPath = Server.MapPath("~/Content/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileName = "data" + ".json";
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


        [HttpPost]
        public JsonResult setPemanang(List<RecordWeb> Data, string hadiah)
        {
            try
            {
                var json_text = jsonPathDataPeserta;
                //var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //string djson = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json");
                string djson = System.IO.File.ReadAllText(json_text);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(djson);
                List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(djson);
                string f = jsonObj[0].NPK;
                foreach (var dr in Data)
                {
                    for (var row = 0; row < items.Count(); row++)
                    {
                        if (jsonObj[row].NPK == dr.NPK)
                        {
                            jsonObj[row].HADIAH = hadiah;
                        }
                    }
                }

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                var json_out = jsonPathDataPeserta;
                //var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json", output);
                System.IO.File.WriteAllText(json_out, output);
                setStatus(hadiah);
                return Json(new
                {
                    status = 1,
                    message = "OK",
                    data = ""
                });
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


        public void setStatus(string hadiah)
        {
            //string djson = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiah.json");
            var json_text = jsonPathDataPeserta;
            //var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
            string djson = System.IO.File.ReadAllText(json_text);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(djson);
            List<JsonHadiahRec> items = JsonConvert.DeserializeObject<List<JsonHadiahRec>>(djson);
            string f = jsonObj[0].NPK;

            for (var row = 0; row < items.Count(); row++)
            {
                if (jsonObj[row].No == hadiah)
                {
                    jsonObj[row].Status = "1";
                }
            }


            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            var json_out = jsonPathDataPeserta;
            //var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
            //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiah.json", output);
            System.IO.File.WriteAllText(json_out, output);
            //System.IO.File.WriteAllText(json_out, output);
        }


        [HttpPost]
        public JsonResult GetPemanang_v2(string maxData, string isbesar)
        {
            try
            {
                //var data = Model._queryWeb.getPemanang(Convert.ToInt32(maxData));
                var json_text = jsonPathDataPeserta;
                //var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    Random rnd = new Random();


                    if (isbesar == "1")
                    {

                        // updated ABSEN 1 UNTUK UNDANGAN DAN ABSEN 2 UNTUK NON UNDANGAN
                        List<RecordWeb> datafinal = (from a in items
                                                     where a.ABSEN != "0" && a.ISMULIA == "1" && a.HADIAH == "0" && (a.KODEWARNA == "doorprize" || a.KODEWARNA == "doorprize, grandprize")
                                                     select new RecordWeb
                                                     {
                                                         NPK = a.NPK,
                                                         NAMA = a.NAMA.Length <= 20 ? a.NAMA : a.NAMA.Substring(0, 20),
                                                         HADIAH = a.HADIAH,
                                                         ISMULIA = a.ISMULIA
                                                     }).ToList();

                        List<RecordWeb> data = datafinal.OrderBy(u => rnd.Next()).Take(Convert.ToInt32(maxData)).ToList();


                        if (data == null || data.Count().Equals(0))
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
                            data = data
                        });

                    }
                    else
                    {
                        // updated ABSEN 1 UNTUK UNDANGAN DAN ABSEN 2 UNTUK NON UNDANGAN
                        List<RecordWeb> datafinal = (from a in items
                                                     where a.ABSEN != "0" && a.HADIAH == "0" && (a.KODEWARNA == "doorprize" || a.KODEWARNA == "doorprize, grandprize")
                                                     select new RecordWeb
                                                     {
                                                         NPK = a.NPK,
                                                         NAMA = a.NAMA.Length <= 20 ? a.NAMA : a.NAMA.Substring(0, 20),
                                                         HADIAH = a.HADIAH,
                                                         ISMULIA = a.ISMULIA
                                                     }).ToList();

                        List<RecordWeb> data = datafinal.OrderBy(u => rnd.Next()).Take(Convert.ToInt32(maxData)).ToList();


                        if (data == null || data.Count().Equals(0))
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
                            data = data
                        });
                    }

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
        public JsonResult GetData()
        {
            try
            {
                var json_text = jsonPathDataPeserta;
                //var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    List<RecordWeb> data = (from a in items
                                            where a.ABSEN != "0" && a.HADIAH == "0" && (a.KODEWARNA == "doorprize, grandprize" || a.KODEWARNA == "doorprize")
                                            select new RecordWeb
                                            {
                                                NPK = a.NPK,
                                                NAMA = a.NAMA.Length <= 20 ? a.NAMA : a.NAMA.Substring(0, 20),
                                                HADIAH = a.HADIAH
                                            }).ToList();
                    if (data == null || data.Count().Equals(0))
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
                        data = data
                    });
                }
                //var data = Model._queryWeb.listDoorPrize();


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
        public JsonResult GetHadiahDoor()
        {
            try
            {
                //var data = Model._queryWeb.listHadiahDoorPrize();
                //var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
                var json_text = jsonPathHadiah;
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiah.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonHadiahRec> items = JsonConvert.DeserializeObject<List<JsonHadiahRec>>(json);
                    List<HadiahRecWeb> data = (from a in items
                                               where a.Status == "1"
                                               select new HadiahRecWeb
                                               {
                                                   No = int.Parse(a.No),
                                                   Hadiah = a.Hadiah,
                                                   Status = a.Status,
                                                   Unit = getJumlahWin(a.No)
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
                        data = data.OrderByDescending(c => c.No).ToList()
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


        public int getJumlahWin(string hadiah)
        {
            var json_text = jsonPathDataPeserta;
            //var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
            using (StreamReader r = new StreamReader(json_text))
            {
                string json = r.ReadToEnd();
                List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                var data = items.Where(x => x.HADIAH == hadiah && x.AMBILHADIAH == "").ToList();

                return data.Count();
            }
        }


        [HttpPost]
        public JsonResult GetDataHadiah(string hadiah)
        {
            try
            {
                //var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                var json_text = jsonPathHadiah;
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    //List<RecordWeb> data = (from a in items
                    //                        where a.ABSEN == "1" && a.HADIAH == hadiah && (a.KODEWARNA == "doorprize, grandprize" || a.KODEWARNA == "doorprize")
                    //                        && a.AMBILHADIAH == " "
                    //                        select new RecordWeb
                    //                        {
                    //                            NPK = a.NPK.Length != 5 ? a.NPK.PadLeft(4, '0') : a.NPK,
                    //                            NAMA = a.NAMA.Length <= 20 ? a.NAMA : a.NAMA.Substring(0, 20),
                    //                            HADIAH = a.HADIAH
                    //                        }).ToList();
                    List<RecordWeb> data = (from a in items
                                            where a.ABSEN != "0" && a.HADIAH == hadiah && (a.KODEWARNA == "doorprize, grandprize" || a.KODEWARNA == "doorprize")
                                            && a.AMBILHADIAH == ""
                                            select new RecordWeb
                                            {
                                                NPK = a.NPK.Length != 5 ? a.NPK.PadLeft(4, '0') : a.NPK,
                                                NAMA = a.NAMA.Length <= 20 ? a.NAMA : a.NAMA.Substring(0, 20),
                                                HADIAH = a.HADIAH
                                            }).ToList();
                    if (data == null || data.Count().Equals(0))
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
                        data = data
                    });
                }
                //var data = Model._queryWeb.listDoorPrize();


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
        public JsonResult GetDataHadiah_all()
        {
            try
            {
                var json_text = jsonPathDataPeserta;
                //var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    //List<RecordWeb> data = (from a in items
                    //                        where a.ABSEN == "1" && (a.KODEWARNA == "doorprize, grandprize" || a.KODEWARNA == "doorprize")
                    //                        && a.AMBILHADIAH == " "
                    //                        && a.HADIAH != "0"
                    //                        select new RecordWeb
                    //                        {
                    //                            NPK = a.NPK.Length != 5 ? a.NPK.PadLeft(4, '0') : a.NPK,
                    //                            NAMA = a.NAMA.Length <= 10 ? a.NAMA : a.NAMA.Substring(0, 10),
                    //                            HADIAH = a.HADIAH
                    //                        }).ToList();
                    List<RecordWeb> data = (from a in items
                                            where a.ABSEN != "0" && (a.KODEWARNA == "doorprize, grandprize" || a.KODEWARNA == "doorprize")
                                            && a.AMBILHADIAH == ""
                                            && a.HADIAH != "0"
                                            select new RecordWeb
                                            {
                                                NPK = a.NPK.Length != 5 ? a.NPK.PadLeft(4, '0') : a.NPK,
                                                NAMA = a.NAMA,
                                                //NAMA = a.NAMA.Length <= 10 ? a.NAMA : a.NAMA.Substring(0, 10),
                                                HADIAH = a.HADIAH
                                            }).ToList();
                    if (data == null || data.Count().Equals(0))
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
                        data = data
                    });
                }
                //var data = Model._queryWeb.listDoorPrize();


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



    }
}