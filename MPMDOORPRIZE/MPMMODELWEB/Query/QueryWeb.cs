using MPMLibrary.NET.Lib.Db.Objects;
using MPMLibrary.NET.Lib.Exception;
using MPMMODELWEB.Module;
using MPMMODELWEB.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPMMODELWEB.Query
{
    public class QueryWeb : MPMDbObject<DBModuleWebDataContext>
    {
        public QueryWeb(): base()
        {

        }

        public MPM_DOORPRIZE_USER ItemUser(String npk)
        {
            var item = from a in Context.MPM_DOORPRIZE_USERs
                       where a.NPK == npk
                       select a;
            return item.Take(1).FirstOrDefault();
        }

        public void UpdateUser(MPM_DOORPRIZE_USER item)
        {
            try
            {

            }
            catch (MPMException ex)
            {
                throw new MPMException(ex.Message);
            }
        }

        public List<RecordWeb> listDoorPrize()
        {
            try
            {
                var result = from a in Context.MPM_DOORPRIZE_USERs
                             where a.KETENTUANWARNA == "ABU-ABU" || a.KETENTUANWARNA == "ORANGE"
                             select new RecordWeb
                             {
                                 //npk = a.NPK,
                                 //nama = a.NAMA.Substring(0,20).ToLower(),
                                 //warna = a.KETENTUANWARNA
                             };
                return result.ToList();
            }
            catch(MPMException ex)
            {
                throw new MPMException(ex.Message);
            }
        }

        public List<RecordWeb> getPemanang(int maxData)
        {
            try
            {
                Random rnd = new Random();
                var result = listDoorPrize().OrderBy(u => rnd.Next()).Take(maxData);
                return result.ToList();
            }
            catch (MPMException ex)
            {
                throw new MPMException(ex.Message);
            }
        }

        public List<HadiahRecWeb> listHadiahDoorPrize()
        {
            try
            {
                var result = from a in Context.MPM_DOORPRIZEs
                             select new HadiahRecWeb
                             {
                                 //hadiah = a.NAMA,
                                 //jumlah = a.JUMLAH.Value,
                                 //keterangan = a.KETERANGAN
                             };
                return result.ToList();
            }
            catch (MPMException ex)
            {
                throw new MPMException(ex.Message);
            }
        }
    }
}
