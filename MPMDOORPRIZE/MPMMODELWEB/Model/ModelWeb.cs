using MPMLibrary.NET.Lib.Exception;
using MPMMODELWEB.Module;
using MPMMODELWEB.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPMMODELWEB.Model
{
    public class ModelWeb : BaseModel
    {
        public QueryWeb _queryWeb = new QueryWeb();
        public ModelWeb()
            : base()
        {
            _queryWeb.Context = (DBModuleWebDataContext)Context;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public void Update(String NPK, String Hadiah)
        {
            BeginTransaction();
            try
            {
                var items = _queryWeb.ItemUser(NPK);
                items.STATUSPEMENANG = "1";
                items.KODEHADIAH = Hadiah;
                _queryWeb.UpdateUser(items);

                Commit();
            }
            catch (MPMException ex)
            {
                Rollback();
                throw new MPMException(ex.Message);
            }
        }
    }
}
