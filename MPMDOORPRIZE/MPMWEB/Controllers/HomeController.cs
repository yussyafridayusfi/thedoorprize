using Microsoft.Office.Interop.Excel;
using MPMLibrary.NET.Mvc.Controllers;
using MPMMODELWEB.Record;
using MPMWEB.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPMWEB.Controllers
{
    public class HomeController : MPMController<ModelWebEx>
    {
        public HomeController()
            : base()
        {
            Model = new ModelWebEx();
        }
        public override ActionResult Index()
        {
            //return View();
            return View("MainMenu");
        }
        public ActionResult Ambil()
        {
            return View("Ambil");
        }
        public ActionResult Export()
        {
            return View("Export");
        }
        public ActionResult GrandPrize()
        {
            return View("GrandPrize");
        }
        public ActionResult Doorprize()
        {
            return View("Doorprize");
        }
        public ActionResult Grand()
        {
            return View("Grand");
        }
        public ActionResult Register()
        {
            return View("Registrasi");
        }
        public ActionResult Register1()
        {
            return View("Register1");
        }
        public ActionResult Register2()
        {
            return View("Register2");
        }
        public ActionResult Register3()
        {
            return View("Register3");
        }
        public ActionResult Register4()
        {
            return View("Register4");
        }
        public ActionResult Register5()
        {
            return View("Register5");
        }
        public ActionResult Register6()
        {
            return View("Register6");
        }
        public ActionResult MainMenu()
        {
            return View();
        }
        public ActionResult RolateDoorprize()
        {
            //return View("Index");
            return View();
        }
        public ActionResult RolateDoorprizeBackup()
        {
            return View("Index");
            //return View();
        }
        public void setStatus(string hadiah)
        {
            //string djson = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiah.json");
            var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
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
            var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
            //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiah.json", output);
            System.IO.File.WriteAllText(json_out, output);
            System.IO.File.WriteAllText(json_out, output);
        }

        public void setStatusGR(string hadiah)
        {
            var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiahGR.json";
            //string djson = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiahGR.json");
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
            var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiahGR.json";
            //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiahGR.json", output);
            System.IO.File.WriteAllText(json_out, output);
        }

        public string getHadihnya(string hadiah)
        {
            var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
            //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiah.json"))
            using (StreamReader r = new StreamReader(json_text))
            {
                string json = r.ReadToEnd();
                List<JsonHadiahRec> items = JsonConvert.DeserializeObject<List<JsonHadiahRec>>(json);

                return items.Where(x => x.No == hadiah).FirstOrDefault().Hadiah;
            }
        }

        [HttpPost]
        public JsonResult getHadiahWin(string npk)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //string djson = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json");
                string djson = System.IO.File.ReadAllText(json_text);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(djson);
                List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(djson);
                string f = jsonObj[0].NPK;
                
                List<JsonRec> getData = new List<JsonRec>();
                for (var row = 0; row < items.Count(); row++)
                {
                    if (jsonObj[row].NPK == npk)
                    {
                        if (jsonObj[row].AMBILHADIAH == " ")
                        {
                            JsonRec Datanya = new JsonRec();
                            Datanya.NPK = jsonObj[row].NPK;
                            Datanya.NAMA = jsonObj[row].NAMA;
                            string datahadiah = jsonObj[row].HADIAH;
                            Datanya.HADIAH = getHadihnya(datahadiah);

                            getData.Add(Datanya);

                            jsonObj[row].AMBILHADIAH = DateTime.Now.ToString();
                        }
                        else
                        {
                            JsonRec Datanya = new JsonRec();
                            Datanya.NPK = jsonObj[row].NPK;
                            Datanya.NAMA = jsonObj[row].NAMA;
                            Datanya.HADIAH = jsonObj[row].AMBILHADIAH;

                            getData.Add(Datanya);
                        }
                        

                        
                    }
                }
                

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json", output);
                System.IO.File.WriteAllText(json_out, output);
                
                return Json(new
                {
                    status = 1,
                    message = "OK",
                    data = getData
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

        [HttpPost]
        public JsonResult setPemanang(List<RecordWeb> Data, string hadiah)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
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
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
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

        [HttpPost]
        public JsonResult setPemanangGR(List<RecordWeb> Data, string hadiah)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
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
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json", output);
                System.IO.File.WriteAllText(json_out, output);
                setStatusGR(hadiah);
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

        [HttpPost]
        public JsonResult GetPemanang(string maxData)
        {
            try
            {
                //var data = Model._queryWeb.getPemanang(Convert.ToInt32(maxData));
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    Random rnd = new Random();

                    //List<RecordWeb> datafinal = (from a in items
                    //                             where a.ABSEN == "1" && a.HADIAH == "0" && (a.KODEWARNA == "doorprize" || a.KODEWARNA == "doorprize, grandprize")
                    //                             select new RecordWeb
                    //                             {
                    //                                 NPK = a.NPK,
                    //                                 NAMA = a.NAMA.Length <= 20 ? a.NAMA : a.NAMA.Substring(0, 20) ,
                    //                                  HADIAH = a.HADIAH,
                    //                                  ISMULIA = a.ISMULIA
                    //                              }).ToList();

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
        public JsonResult GetPemanang_v2(string maxData, string isbesar)
        {
            try
            {
                //var data = Model._queryWeb.getPemanang(Convert.ToInt32(maxData));
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    Random rnd = new Random();

                    //List<RecordWeb> datafinal = (from a in items
                    //                             where a.ABSEN == "1" && a.HADIAH == "0" && (a.KODEWARNA == "doorprize" || a.KODEWARNA == "doorprize, grandprize")
                    //                             select new RecordWeb
                    //                             {
                    //                                 NPK = a.NPK,
                    //                                 NAMA = a.NAMA.Length <= 20 ? a.NAMA : a.NAMA.Substring(0, 20) ,
                    //                                  HADIAH = a.HADIAH,
                    //                                  ISMULIA = a.ISMULIA
                    //                              }).ToList();

                    if (isbesar == "1")
                    {

                        // updated ABSEN 1 UNTUK UNDANGAN DAN ABSEN 2 UNTUK NON UNDANGAN
                        List<RecordWeb> datafinal = (from a in items
                                                     where a.ABSEN != "0" && a.ISMULIA=="1" && a.HADIAH == "0" && (a.KODEWARNA == "doorprize" || a.KODEWARNA == "doorprize, grandprize")
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

                    } else
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
        public JsonResult GetPemanangGR(string maxData)
        {
            try
            {
                //var data = Model._queryWeb.getPemanang(Convert.ToInt32(maxData));
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    Random rnd = new Random();

                    List<RecordWeb> datafinal = (from a in items
                                                 where a.ABSEN == "1" && a.HADIAH == "0" && (a.KODEWARNA == "doorprize, grandprize")
                                                 select new RecordWeb
                                                 {
                                                     NPK = a.NPK,
                                                     NAMA = a.NAMA.Length <= 20 ? a.NAMA : a.NAMA.Substring(0, 20),
                                                     HADIAH = a.HADIAH
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
        public JsonResult GetDataHadiah(string hadiah)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
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
                                            && a.AMBILHADIAH == " "
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
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
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
                                            && a.AMBILHADIAH == " "
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

        public int getJumlahWin(string hadiah)
        {
            var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
            using (StreamReader r = new StreamReader(json_text))
            {
                string json = r.ReadToEnd();
                List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                var data = items.Where(x => x.HADIAH == hadiah && x.AMBILHADIAH == " ").ToList() ;

                return data.Count();
            }
        }
        [HttpPost]
        public JsonResult GetHadiahDoor()
        {
            try
            {
                //var data = Model._queryWeb.listHadiahDoorPrize();
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
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

        public JsonResult GetHadiahDoor_v2()
        {
            try
            {
                //var data = Model._queryWeb.listHadiahDoorPrize();
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
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
        public JsonResult GetHadiah()
        {
            try
            {
                //var data = Model._queryWeb.listHadiahDoorPrize();
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiah.json";
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
        public JsonResult GetHadiahGR()
        {
            try
            {
                //var data = Model._queryWeb.listHadiahDoorPrize();
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\hadiahGR.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\hadiahGR.json"))
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
                                                   Unit = int.Parse(a.Unit)
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

        public void SetHadir(string npk, string nama, string kodewarna)
        {
            try
            {
                string newCompanyMember = "{'NPK':'" + npk + "','NAMA':'" + nama + "','KODEWARNA':'" + kodewarna + "','HADIAH':'0'}";
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register1.json";
                //var json = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register1.json");
                var json = System.IO.File.ReadAllText(json_text);
                var jsonObj = JObject.Parse(json);
                //var experienceArrary = jsonObj.GetValue("experiences") as JArray;
                //var newCompany = JObject.Parse(newCompanyMember);
                //experienceArrary.Add(newCompany);

                //jsonObj["experiences"] = experienceArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
                                       Newtonsoft.Json.Formatting.Indented);
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register1.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register1.json", newJsonResult);
                System.IO.File.WriteAllText(json_out, newJsonResult);
                
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetDataRegister( string npk )
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\DataUndang.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\DataUndang.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<RegisterRec> items = JsonConvert.DeserializeObject<List<RegisterRec>>(json);
                    var data = items.Where(x => x.NPK == npk).Take(1).ToList();
                    foreach(var row in data)
                    {
                        SetHadir(row.NPK, row.NAMA, row.KODEWARNA);
                    }
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
                        data = data
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
        public JsonResult GetData()
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
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
        public JsonResult GetData_all()
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
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
                                                HADIAH = a.HADIAH,
                                                ISMULIA = a.ISMULIA
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

        public JsonResult GetDataDoorMulia()
        {
            try
            {
                //isMulia = 1

                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    List<RecordWeb> data = (from a in items
                                            where a.ABSEN == "1" && a.HADIAH == "0" && (a.KODEWARNA == "doorprize, grandprize" || a.KODEWARNA == "doorprize") && (a.ISMULIA == "1")
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
        public JsonResult GetDataGR()
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);
                    List<RecordWeb> data = (from a in items
                                            where a.ABSEN == "1" && a.HADIAH == "0" && (a.KODEWARNA == "doorprize, grandprize" )
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
        public JsonResult setabsen(string npk)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\DataUndang.json";
                //string djson = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\DataUndang.json");
                string djson = System.IO.File.ReadAllText(json_text);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(djson);
                List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(djson);
                string cekAbsen = "";
                for (var row = 0; row < items.Count(); row++)
                {
                    if (jsonObj[row].NPK == npk)
                    {
                        if (jsonObj[row].ABSEN == "2")
                        {
                            // diabaikan
                        }
                        else if (jsonObj[row].ABSEN == "1")
                        {
                            cekAbsen = npk;
                        }
                        else
                        {
                            jsonObj[row].ABSEN = "1";
                        }

                        //if (jsonObj[row].ABSEN == "1")
                        //{
                        //    cekAbsen = npk;
                        //}
                        //else
                        //{
                        //    jsonObj[row].ABSEN = "1";
                        //}


                    }
                }
                

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\DataUndang.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\DataUndang.json", output);
                System.IO.File.WriteAllText(json_out, output);
                if(cekAbsen != "")
                {
                    return Json(new
                    {
                        status = 0,
                        message = "Sudah Pernah Absen !!",
                        data = ""
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = 1,
                        message = "OK",
                        data = ""
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


        public void SetHadir1(string npk, string nama, string kodewarna)
        {
            try
            {
                string newCompanyMember = "{'NPK':'" + npk + "','NAMA':'" + nama + "','KODEWARNA':'" + kodewarna + "','HADIAH':'0'}";
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register1.json";
                //var json = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register1.json");
                var json = System.IO.File.ReadAllText(json_text);
                //var jsonObj = JObject.Parse(json);
                //var experienceArrary = jsonObj.GetValue("experiences") as JArray;
                //var newCompany = JObject.Parse(newCompanyMember);
                //experienceArrary.Add(newCompany);

                //jsonObj["experiences"] = experienceArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(json,
                                       Newtonsoft.Json.Formatting.Indented);
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register1.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register1.json", newJsonResult);
                System.IO.File.WriteAllText(json_out, newJsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetDataRegister1(string npk)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\DataUndang.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\DataUndang.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<RegisterRec> items = JsonConvert.DeserializeObject<List<RegisterRec>>(json);
                    var data = items.Where(x => x.NPK == npk).Take(1).ToList();
                    //foreach (var row in data)
                    //{
                    //    SetHadir1(row.NPK, row.NAMA, row.KODEWARNA);
                    //}
                    //if (items == null || items.Count().Equals(0))
                    //{
                    //    return Json(new
                    //    {
                    //        status = 0,
                    //        message = "Data Not Found",
                    //        data = new { }
                    //    });
                    //}
                    var msg = "OK";
                    int status_ = 1;

                    if (data == null || data.Count() == 0)
                    {
                        msg = "Data Not Found";
                        status_ = 0;
                    }

                    return Json(new
                    {
                        status = status_,
                        message = msg,
                        data = data
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

        public void SetHadir2(string npk, string nama, string kodewarna)
        {
            try
            {
                string newCompanyMember = "{'NPK':'" + npk + "','NAMA':'" + nama + "','KODEWARNA':'" + kodewarna + "','HADIAH':'0'}";
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register2.json";
                //var json = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register2.json");
                var json = System.IO.File.ReadAllText(json_text);
                var jsonObj = JObject.Parse(json);
                var experienceArrary = jsonObj.GetValue("experiences") as JArray;
                var newCompany = JObject.Parse(newCompanyMember);
                experienceArrary.Add(newCompany);

                jsonObj["experiences"] = experienceArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
                                       Newtonsoft.Json.Formatting.Indented);
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register2.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register2.json", newJsonResult);
                System.IO.File.WriteAllText(json_out, newJsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetDataRegister2(string npk)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\DataUndang.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\DataUndang.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<RegisterRec> items = JsonConvert.DeserializeObject<List<RegisterRec>>(json);
                    var data = items.Where(x => x.NPK == npk).Take(1).ToList();
                    //foreach (var row in data)
                    //{
                    //    SetHadir2(row.NPK, row.NAMA, row.KODEWARNA);
                    //}
                    //if (items == null || items.Count().Equals(0))
                    //{
                    //    return Json(new
                    //    {
                    //        status = 0,
                    //        message = "Data Not Found",
                    //        data = new { }
                    //    });
                    //}

                    var msg = "OK";
                    int status_ = 1;

                    if (data == null || data.Count() == 0)
                    {
                        msg = "Data Not Found";
                        status_ = 0;
                    }

                    return Json(new
                    {
                        status = status_,
                        message = msg,
                        data = data
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

        public void SetHadir3(string npk, string nama, string kodewarna)
        {
            try
            {
                string newCompanyMember = "{'NPK':'" + npk + "','NAMA':'" + nama + "','KODEWARNA':'" + kodewarna + "','HADIAH':'0'}";
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register3.json";
                //var json = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register3.json");
                var json = System.IO.File.ReadAllText(json_text);
                var jsonObj = JObject.Parse(json);
                var experienceArrary = jsonObj.GetValue("experiences") as JArray;
                var newCompany = JObject.Parse(newCompanyMember);
                experienceArrary.Add(newCompany);

                jsonObj["experiences"] = experienceArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
                                       Newtonsoft.Json.Formatting.Indented);
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register3.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register3.json", newJsonResult);
                System.IO.File.WriteAllText(json_out, newJsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetDataRegister3(string npk)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\DataUndang.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\DataUndang.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<RegisterRec> items = JsonConvert.DeserializeObject<List<RegisterRec>>(json);
                    var data = items.Where(x => x.NPK == npk).Take(1).ToList();
                    //foreach (var row in data)
                    //{
                    //    SetHadir3(row.NPK, row.NAMA, row.KODEWARNA);
                    //}
                    //if (items == null || items.Count().Equals(0))
                    //{
                    //    return Json(new
                    //    {
                    //        status = 0,
                    //        message = "Data Not Found",
                    //        data = new { }
                    //    });
                    //}

                    var msg = "OK";
                    int status_ = 1;

                    if (data == null || data.Count() == 0)
                    {
                        msg = "Data Not Found";
                        status_ = 0;
                    }

                    return Json(new
                    {
                        status = status_,
                        message = msg,
                        data = data
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

        public void SetHadir4(string npk, string nama, string kodewarna)
        {
            try
            {
                string newCompanyMember = "{'NPK':'" + npk + "','NAMA':'" + nama + "','KODEWARNA':'" + kodewarna + "','HADIAH':'0'}";
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register4.json";
                //var json = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register4.json");
                var json = System.IO.File.ReadAllText(json_text);
                var jsonObj = JObject.Parse(json);
                var experienceArrary = jsonObj.GetValue("experiences") as JArray;
                var newCompany = JObject.Parse(newCompanyMember);
                experienceArrary.Add(newCompany);

                jsonObj["experiences"] = experienceArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
                                       Newtonsoft.Json.Formatting.Indented);
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register4.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register4.json", newJsonResult);
                System.IO.File.WriteAllText(json_out, newJsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetDataRegister4(string npk)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\DataUndang.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\DataUndang.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<RegisterRec> items = JsonConvert.DeserializeObject<List<RegisterRec>>(json);
                    var data = items.Where(x => x.NPK == npk).Take(1).ToList();
                    //foreach (var row in data)
                    //{
                    //    SetHadir4(row.NPK, row.NAMA, row.KODEWARNA);
                    //}
                    //if (items == null || items.Count().Equals(0))
                    //{
                    //    return Json(new
                    //    {
                    //        status = 0,
                    //        message = "Data Not Found",
                    //        data = new { }
                    //    });
                    //}

                    var msg = "OK";
                    int status_ = 1;

                    if (data == null || data.Count() == 0)
                    {
                        msg = "Data Not Found";
                        status_ = 0;
                    }

                    return Json(new
                    {
                        status = status_,
                        message = msg,
                        data = data
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

        public void SetHadir5(string npk, string nama, string kodewarna)
        {
            try
            {
                string newCompanyMember = "{'NPK':'" + npk + "','NAMA':'" + nama + "','KODEWARNA':'" + kodewarna + "','HADIAH':'0'}";
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register5.json";
                //var json = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register5.json");
                var json = System.IO.File.ReadAllText(json_text);
                var jsonObj = JObject.Parse(json);
                var experienceArrary = jsonObj.GetValue("experiences") as JArray;
                var newCompany = JObject.Parse(newCompanyMember);
                experienceArrary.Add(newCompany);

                jsonObj["experiences"] = experienceArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
                                       Newtonsoft.Json.Formatting.Indented);
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register5.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register5.json", newJsonResult);
                System.IO.File.WriteAllText(json_out, newJsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetDataRegister5(string npk)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\DataUndang.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\DataUndang.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<RegisterRec> items = JsonConvert.DeserializeObject<List<RegisterRec>>(json);
                    var data = items.Where(x => x.NPK == npk).Take(1).ToList();
                    //foreach (var row in data)
                    //{
                    //    SetHadir5(row.NPK, row.NAMA, row.KODEWARNA);
                    //}
                    //if (items == null || items.Count().Equals(0))
                    //{
                    //    return Json(new
                    //    {
                    //        status = 0,
                    //        message = "Data Not Found",
                    //        data = new { }
                    //    });
                    //}

                    var msg = "OK";
                    int status_ = 1;

                    if (data == null || data.Count() == 0)
                    {
                        msg = "Data Not Found";
                        status_ = 0;
                    }

                    return Json(new
                    {
                        status = status_,
                        message = msg,
                        data = data
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

        public void SetHadir6(string npk, string nama, string kodewarna)
        {
            try
            {
                string newCompanyMember = "{'NPK':'" + npk + "','NAMA':'" + nama + "','KODEWARNA':'" + kodewarna + "','HADIAH':'0'}";
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register6.json";
                //var json = System.IO.File.ReadAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register6.json");
                var json = System.IO.File.ReadAllText(json_text);
                var jsonObj = JObject.Parse(json);
                var experienceArrary = jsonObj.GetValue("experiences") as JArray;
                var newCompany = JObject.Parse(newCompanyMember);
                experienceArrary.Add(newCompany);

                jsonObj["experiences"] = experienceArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
                                       Newtonsoft.Json.Formatting.Indented);
                var json_out = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register6.json";
                //System.IO.File.WriteAllText("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register6.json", newJsonResult);
                System.IO.File.WriteAllText(json_out, newJsonResult);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetDataRegister6(string npk)
        {
            try
            {
                var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\DataUndang.json";
                //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\DataUndang.json"))
                using (StreamReader r = new StreamReader(json_text))
                {
                    string json = r.ReadToEnd();
                    List<RegisterRec> items = JsonConvert.DeserializeObject<List<RegisterRec>>(json);
                    var data = items.Where(x => x.NPK == npk).Take(1).ToList();
                    //foreach (var row in data)
                    //{
                    //    SetHadir6(row.NPK, row.NAMA, row.KODEWARNA);
                    //}
                    //if (items == null || items.Count().Equals(0))
                    //{
                    //    return Json(new
                    //    {
                    //        status = 0,
                    //        message = "Data Not Found",
                    //        data = new { }
                    //    });
                    //}

                    var msg = "OK";
                    int status_ = 1;

                    if (data == null || data.Count() == 0)
                    {
                        msg = "Data Not Found";
                        status_ = 0;
                    }

                    return Json(new
                    {
                        status = status_,
                        message = msg,
                        data = data
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
        public ActionResult ExportDataFinal()
        {
            //string reportPath = "D:\\PINDAHAN\\Project\\Panitia Penutupan\\dataregis\\";
            string reportPath = "D:\\TheDoorprize\\dataregis\\";
            string reportName = "DataFinal.xlsb";
            var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
            //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
            using (StreamReader r = new StreamReader(json_text))
            {
                string json = r.ReadToEnd();
                List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);

                Microsoft.Office.Interop.Excel.Application excelApp =
                new Microsoft.Office.Interop.Excel.Application();

                //Create an Excel workbook instance 
                Microsoft.Office.Interop.Excel.Workbook excelWorkBook =
                excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = Convert.ToString("aaaa");
                excelWorkSheet.Columns.AutoFit();

                //for (int i = 1; i < table.Columns.Count + 1; i++)
                //{
                //    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                //}
                excelWorkSheet.Cells[1, 1] = "NPK";
                excelWorkSheet.Cells[1, 2] = "NAMA";
                excelWorkSheet.Cells[1, 3] = "KODEWARNA";
                excelWorkSheet.Cells[1, 4] = "HADIAH";
                excelWorkSheet.Cells[1, 5] = "ABSEN";
                excelWorkSheet.Cells[1, 6] = "AMBILHADIAH";

                //for (int j = 0; j < data.Count(); j++)
                //{
                //    for (int k = 0; k < table.Columns.Count; k++)
                //    {
                //        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                //    }
                //}
                int a = 2;
                foreach (var dt in items)
                {
                    excelWorkSheet.Cells[a, 1] = "'" + dt.NPK;
                    excelWorkSheet.Cells[a, 2] = dt.NAMA;
                    excelWorkSheet.Cells[a, 3] = dt.KODEWARNA;
                    if(dt.HADIAH != "0") { excelWorkSheet.Cells[a, 4] = getHadihnya(dt.HADIAH); } else { excelWorkSheet.Cells[a, 4] = dt.HADIAH; }
                    
                    excelWorkSheet.Cells[a, 5] = dt.ABSEN;
                    excelWorkSheet.Cells[a, 6] = dt.AMBILHADIAH;

                    a++;
                }

                //-- check file directory is present or not/if note create new
                bool exists = System.IO.Directory.Exists(reportPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(reportPath);
                }

                excelWorkBook.SaveAs(reportPath + reportName,
                XlFileFormat.xlExcel12, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                excelWorkBook.Close();
                excelApp.Quit();

                return File(reportPath + reportName, "application/vnd.ms-excel", reportName);

            }


        }
        public ActionResult ExportToExcelLogin(string log)
        {
            //string reportPath = "D:\\PINDAHAN\\Project\\Panitia Penutupan\\dataregis\\";
            string reportPath = "D:\\TheDoorprize\\dataregis\\";
            string reportName = "ReportRegister"+ log + ".xlsb";
            var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\register";
            //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\register" + log+".json"))
            using (StreamReader r = new StreamReader(json_text + log+".json"))
            {
                string json = r.ReadToEnd();
                List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);

                Microsoft.Office.Interop.Excel.Application excelApp =
                new Microsoft.Office.Interop.Excel.Application();

                //Create an Excel workbook instance 
                Microsoft.Office.Interop.Excel.Workbook excelWorkBook =
                excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = Convert.ToString("aaaa");
                excelWorkSheet.Columns.AutoFit();

                //for (int i = 1; i < table.Columns.Count + 1; i++)
                //{
                //    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                //}
                excelWorkSheet.Cells[1, 1] = "NPK";
                excelWorkSheet.Cells[1, 2] = "NAMA";
                excelWorkSheet.Cells[1, 3] = "KODEWARNA";

                //for (int j = 0; j < data.Count(); j++)
                //{
                //    for (int k = 0; k < table.Columns.Count; k++)
                //    {
                //        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                //    }
                //}
                int a = 2;
                foreach (var dt in items)
                {
                    excelWorkSheet.Cells[a, 1] = "'" + dt.NPK;
                    excelWorkSheet.Cells[a, 2] = dt.NAMA;
                    excelWorkSheet.Cells[a, 3] = dt.KODEWARNA;
                    a++;
                }

                //-- check file directory is present or not/if note create new
                bool exists = System.IO.Directory.Exists(reportPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(reportPath);
                }

                excelWorkBook.SaveAs(reportPath + reportName,
                XlFileFormat.xlExcel12, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                excelWorkBook.Close();
                excelApp.Quit();

                return File(reportPath + reportName, "application/vnd.ms-excel", reportName);

            }


        }

        public ActionResult ExportToExcel(string hadiah)
        {
            //string reportPath = "D:\\PINDAHAN\\Project\\Panitia Penutupan\\dataregis\\";
            string reportPath = "D:\\TheDoorprize\\dataregis\\";
            string reportName = getHadihnya(hadiah) + ".xlsb";
            var json_text = @"D:\TheDoorprize\MPMDOORPRIZE\MPMWEB\Content\data.json";
            //using (StreamReader r = new StreamReader("D:\\PINDAHAN\\Project\\Panitia Penutupan\\WEB\\MPMDOORPRIZE\\MPMWEB\\Content\\data.json"))
            using (StreamReader r = new StreamReader(json_text))
            {
                string json = r.ReadToEnd();
                List<JsonRec> items = JsonConvert.DeserializeObject<List<JsonRec>>(json);

                var data = items.Where(x => x.HADIAH == hadiah).ToList();

                Microsoft.Office.Interop.Excel.Application excelApp =
                new Microsoft.Office.Interop.Excel.Application();

                //Create an Excel workbook instance 
                Microsoft.Office.Interop.Excel.Workbook excelWorkBook =
                excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = Convert.ToString("aaaa");
                excelWorkSheet.Columns.AutoFit();

                //for (int i = 1; i < table.Columns.Count + 1; i++)
                //{
                //    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                //}
                excelWorkSheet.Cells[1, 1] = "NPK";
                excelWorkSheet.Cells[1, 2] = "NAMA";
                excelWorkSheet.Cells[1, 3] = "HADIAH";
                excelWorkSheet.Cells[1, 4] = "TANDA TANGAN";

                //for (int j = 0; j < data.Count(); j++)
                //{
                //    for (int k = 0; k < table.Columns.Count; k++)
                //    {
                //        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                //    }
                //}
                int a = 2;
                foreach (var dt in data)
                {
                    excelWorkSheet.Cells[a, 1] = "'"+dt.NPK;
                    excelWorkSheet.Cells[a, 2] = dt.NAMA;
                    excelWorkSheet.Cells[a, 3] = getHadihnya(dt.HADIAH);
                    a++;
                }

                //-- check file directory is present or not/if note create new
                bool exists = System.IO.Directory.Exists(reportPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(reportPath);
                }

                excelWorkBook.SaveAs(reportPath + reportName,
                XlFileFormat.xlExcel12, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                excelWorkBook.Close();
                excelApp.Quit();

                return File(reportPath + reportName, "application/vnd.ms-excel", reportName);

            }

            
        }

    }
}