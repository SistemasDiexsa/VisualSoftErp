using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Response
{
    class domiciliosResponse
    {
    }
    public class domiciliosGridResponse
    {
        
        public int DomicilioID { get; set; }
        
        public string Calle { get; set; }
        
        public string NumeroExterior { get; set; }
        
        public string NumeroInterior { get; set; }
        
        public string Colonia { get; set; }
        
        public string Localidad { get; set; }
        
        public string Referencia { get; set; }
        
        public string Municipio { get; set; }
        
        public string Estado { get; set; }
        
        public string Pais { get; set; }
        
        public string CodigoPostal { get; set; }
        
        public string Cliente { get; set; }
        public string Result { get; set; }
       
        
	
	
	
	
	
	
	
	

	
	
    }  
    
    public class domiciliosLlenaCajasResponse
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
        
        public string NomMun { get; set; }
        
        public string NomLoc { get; set; }
        
        public string NomCol { get; set; }
        
        public string Result { get; set; }

    }
}
