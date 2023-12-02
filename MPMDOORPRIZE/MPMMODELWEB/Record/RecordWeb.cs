using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPMMODELWEB.Record
{
    public class RecordWeb
    {
        public string NPK { get; set; }
        public string NAMA { get; set; }
        public string HADIAH { get; set; }
        public string KODEWARNA { get; set; }
        public string KETERANGAN { get; set; }
        public string ABSEN { get; set; }
        public string AMBILHADIAH { get; set; }
        public string COMPANYOFFICE { get; set; }


    }

    public class JsonRec
    {
        public string NPK { get; set; }
        public string NAMA { get; set; }
        public string HADIAH { get; set; }
        public string KODEWARNA { get; set; }
        public string KETERANGAN { get; set; }
        public string AMBILHADIAH { get; set; }
        public string COMPANYOFFICE { get; set; }        
        public string ABSEN { get; set; }
    }

    public class RegisterRec
    {
        public string NPK { get; set; }
        public string NAMA { get; set; }
        public string HADIAH { get; set; }
        public string KODEWARNA { get; set; }
        public string KETERANGAN { get; set; }
        public string ABSEN { get; set; }
        public string AMBILHADIAH { get; set; }
        public string COMPANYOFFICE { get; set; }
    }


    public class JsonHadiahRec
    {
        public string No { get; set; }
        public string Hadiah { get; set; }
        public string Status { get; set; }
        public string Unit { get; set; }
    }

   

    public class HadiahRecWeb
    {
        public int No { get; set; }
        public string Hadiah { get; set; }
        public string Status { get; set; }
        public int Unit { get; set; }
    }
}
