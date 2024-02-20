using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftErp.Response;

namespace VisualSoftErp.Interface.Response
{
    public class operadoresResponse
    {
        public class operadoresCRUDResponse
        {
            
            public int OperadoresID { get; set; }
            
            public string Rfc { get; set; }
            
            public string Nombre { get; set; }
            
            public string Calle { get; set; }
            
            public string NoExterior { get; set; }
            
            public string NoInterior { get; set; }
            
            public int EstadosID { get; set; }
            
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
            
            public string NoSeUsa_clave_operador { get; set; }
            
            public string NoSeUsa_EstadoCivil { get; set; }
            
            public string Referencia { get; set; }
            
            public string EMail { get; set; }

        }

        public class operadoresLlenaCajasResponse
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

        public class operadoresGRIDResponse
        {
            
            public string OperadoresID { get; set; }
            
            public string Rfc { get; set; }
            
            public string Nombre { get; set; }
            
            public string Calle { get; set; }
            
            public string NoExterior { get; set; }
            
            public string NoInterior { get; set; }
            
            public string EstadosID { get; set; }
            
            public string Pais { get; set; }
            
            public string CP { get; set; }
            
            public string MunicipiosID { get; set; }
            
            public string LocalidadesID { get; set; }
            
            public string ColoniasID { get; set; }
            
            public string NumLicencia { get; set; }
            
            public string LicenciaVence { get; set; }
            
            public string Imss { get; set; }
            
            public string FechaIngreso { get; set; }
            
            public string FechaBaja { get; set; }
            
            public string Telefono { get; set; }
            
            public string CuotaIspt { get; set; }
            
            public string CuotaImss { get; set; }
            
            public string Cuotainfonavit { get; set; }
            
            public string Sueldo { get; set; }
            
            public string PorcentajeSueldo { get; set; }
            
            public string Foto { get; set; }
            
            public string Activo { get; set; }
            
            public string CarrosID { get; set; }
            
            public string Sexo { get; set; }
            
            public string EstadoCivilID { get; set; }
            
            public string Curp { get; set; }
            
            public string FechaNacimiento { get; set; }
            
            public string NumCreditoInfonavit { get; set; }
            
            public string NumRegIdTribOperador { get; set; }
            
            public string ResidenciaFiscalOperador { get; set; }
            
            public string NoSeUsa_clave_operador { get; set; }
            
            public string NoSeUsa_EstadoCivil { get; set; }
            
            public string EMail { get; set; }
        }

        public class operadoresCombosListResponse
        {
            
            public List<comboResponse> Tractor { get; set; }
            
            public List<comboResponse> EstadoCivil { get; set; }
        }

        public class operadoresGResponse
        {
            public string OperadoresID { get; set; }
            public string Rfc { get; set; }
            public string Nombre { get; set; }
            public string Tractor { get; set; }
            public string NumLicencia { get; set; }
            public string LicenciaVence { get; set; }
            public string Activo { get; set; }

        }


    }
}
