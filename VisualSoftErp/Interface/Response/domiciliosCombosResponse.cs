using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
//using VisualSoftErp.Models.Cfdi;
using VisualSoftErp.Response;

namespace VisualSoftErp.Response
{

    public class domiciliosCombosResponse
    {
      
        public ListaDomiciliosPaises ListadePaises { get; set; }
        
        public ListaDomiciliosEstados ListadeEstados { get; set; }
        
        public ListaDomiciliosMunicipios ListadeMunicipios { get; set; }
        
        public ListaDomiciliosLocalidades ListadeLocalidades { get; set; }
        
        public ListaDomiciliosColonias ListadeColonias { get; set; }
        
        public ListaDomiciliosClientes Listadeclientes { get; set; }
    }
   
    public class coloniasComboResponse
    {
        public List<comboResponse> Colonias { get; set; }
        public string Estado { get; set; }
        public int MunicipioID { get; set; }
        public int LocalidadesID { get; set; }
    }

    public class ListaDomiciliosPaises
    {
        public List<comboResponse> Paises { get; set; }
    }
    public class ListaDomiciliosEstados
    {
        public List<comboResponse> Estados { get; set; }
    }
    public class ListaDomiciliosMunicipios
    {
        public List<comboResponse> Municipios { get; set; }
    }
    public class ListaDomiciliosLocalidades
    {
        public List<comboResponse> Localidades { get; set; }
    }    
    public class ListaDomiciliosColonias
    {
        public List<comboResponse> Colonias { get; set; }
    }

    public class ListaDomiciliosClientes
    {
        public List<comboResponse> Clientes { get; set; }
    }
}