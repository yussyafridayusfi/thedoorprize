using MPMLibrary.NET.Mvc.Models;
using MPMMODELWEB.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPMMODELWEB.Model
{
    public class BaseModel : MPMModel
    {
        public BaseModel() : base()
        {
            Context = new DBModuleWebDataContext();


        }
    }
}
