using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Request
{
    public class operadoresRequest
    {
        public class operadoresLlenaCajasRequest
        {
            
            public int OperadoresID { get; set; }
        }

        public class operadoresCRUDRequest
        {
            
            public int OperadoresID { get; set; }
            
            public string Rfc { get; set; }
            
            public string Nombre { get; set; }
            
            public string Calle { get; set; }
            
            public string NoExterior { get; set; }
            
            public string NoInterior { get; set; }
            
            public string EstadosID { get; set; }
            
            public string Pais { get; set; }
            
            public string CP { get; set; }
            
            public int MunicipiosID { get; set; }
            
            public int LocalidadesID { get; set; }
            
            public int ColoniasID { get; set; }
            
            public string NumLicencia { get; set; }
            
            public string LicenciaVence { get; set; }
            
            public string Imss { get; set; }
            
            public string FechaIngreso { get; set; }
            
            public string FechaBaja { get; set; }
            
            public string Telefono { get; set; }
            
            public decimal CuotaIspt { get; set; }
            
            public decimal CuotaImss { get; set; }
            
            public decimal Cuotainfonavit { get; set; }
            
            public decimal Sueldo { get; set; }
            
            public decimal PorcentajeSueldo { get; set; }
            
            public string Foto { get; set; }
            
            public int Activo { get; set; }
            
            public int CarrosID { get; set; }
            
            public string Sexo { get; set; }
            
            public int EstadoCivilID { get; set; }
            
            public string Curp { get; set; }
            
            public string FechaNacimiento { get; set; }
            
            public string NumCreditoInfonavit { get; set; }
            
            public string NumRegIdTribOperador { get; set; }
            
            public string ResidenciaFiscalOperador { get; set; }
            
            public int NoSeUsa_clave_operador { get; set; }
            
            public string NoSeUsa_EstadoCivil { get; set; }
            
            public string Referencia { get; set; }
            
            public string EMail { get; set; }

        }
    }
}
