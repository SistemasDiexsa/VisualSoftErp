using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Response;

namespace VisualSoftErp.Interface.Response
{
    class carrosResponse
    {
    }


    
    public class carrosLlenaCajasResponse
    {
         public int CarrosID { get; set; }
         public int NumeroEconomico { get; set; }
         public int Activo { get; set; }
         public int MarcasID { get; set; }

         public string NumPermisoSCT { get; set; }
         public int AseguradorasID { get; set; }
         public string NumPolizaSeguro { get; set; }
         public string SeguroVence { get; set; }

         public string SeguroInciso { get; set; }
         public string ConfigVehicularID { get; set; }
         public string PlacaVM { get; set; }
         public int AnioModeloVM { get; set; }
         public string ClaveTransporte { get; set; }
         public string PermSCT { get; set; }
         public int PropietariosID { get; set; }
         public string Serie { get; set; }
         public string Rfv { get; set; }
         public string Motor { get; set; }
         public string VmecanicaVence { get; set; }
         public string TcirculacionVence { get; set; }
         public string VhumosVence { get; set; }
    }

    
    public class carrosGRIDResponse
    {
         public string CarrosID { get; set; }
         public string NumeroEconomico { get; set; }
         public string Marca { get; set; }
         public string Modelo { get; set; }
         public string Propietario { get; set; }
        // public string Activo { get; set; }
        // public string MarcasID { get; set; }
        // public string Marca { get; set; }
        // public string NumPermisoSCT { get; set; }
        // public string AseguradorasID { get; set; }
        // public string NombreAseg { get; set; }
        // public string NumPolizaSeguro { get; set; }
        // public string ConfigVehicularID { get; set; }
        // public string PlacaVM { get; set; }
        // public string AnioModeloVM { get; set; }
        // public string ClaveTransporte { get; set; }
        // public string PermSCT { get; set; }
        // public string PropietariosID { get; set; }
        // public string Serie { get; set; }
        // public string Rfv { get; set; }
        // public string Motor { get; set; }
        // public string MotivoBaja { get; set; }
        // public string SeguroInciso { get; set; }
        // public string SeguroVence { get; set; }
        // public string Seguro_imagen { get; set; }
        // public string VmecanicaVence { get; set; }
        // public string TcirculacionImagen { get; set; }
        // public string TcirculacionVence { get; set; }
        // public string VhumosVence { get; set; }
        // public string Vhumos_imagen { get; set; }
        // public string NoSeUsarpropietario { get; set; }
    }

    
    public class carrosCombosListResponse
    {
        
        public List<CargaCombosResponse> Aseguradoras { get; set; }
        
        public List<CargaCombosResponse> Marcas { get; set; }
        
        public List<CargaCombosResponse> Propietarios { get; set; }
        
        public List<StrCargaCombosResponse> c_ConfigAutotransporte { get; set; }

    }
}
