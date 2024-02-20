using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace VisualSoftErp.Response
{

    public class comboResponse
    {

        public int Clave { get; set; }

        public string Des { get; set; }

        public string ClaveStr { get; set; }
    }

    public class CargaCombosResponse
    {

        public int Clave { get; set; }

        public string Des { get; set; }
    }

    public class StrCargaCombosResponse
    {

        public string Clave { get; set; }

        public string Des { get; set; }
    }
}