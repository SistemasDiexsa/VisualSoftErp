using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    class domiciliosRequest
    {
    }
    public class domiciliosCrudRequest
    {
       
        public int DomicilioID { get; set; }
        
        public string Calle { get; set; }
        
        public string NumeroExterior { get; set; }
        
        public string NumeroInterior { get; set; }
        
        public int Colonia { get; set; }
        
        public int Localidad { get; set; }
        
        public string Referencia { get; set; }
        
        public int Municipio { get; set; }
        
        public string Estado { get; set; }
        
        public string Pais { get; set; }
        
        public string CodigoPostal { get; set; }
        
        public int ClientesID { get; set; }
    }

    public class coloniasComboRequest
    {
        public string CP { get; set; }
    }
}
