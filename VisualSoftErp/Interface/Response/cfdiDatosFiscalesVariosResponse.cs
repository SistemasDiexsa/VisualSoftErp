﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Interface.Response
{
    public class cfdiDatosFiscalesVariosResponse
    {
        public string EmpLugarexpedicion { get; set; }
        public string EmpClaveTransporte { get; set; }
        public string EmpNumPermisoSCT { get; set; }
        public string EmpNombre { get; set; }
        public string EmpRegimenFiscal { get; set; }
        public string EmpRfc { get; set; }
        public string EmpLlaveCSD { get; set; }
        public string ClaveTransporteCarro { get; set; }
        public string NombreAseg { get; set; }
        public string NumPermisoSCTCarro { get; set; }
        public string NumPolizaSeguro { get; set; }
        public string PermSCT { get; set; }
        public int PropietariosID { get; set; }
        public string ConfigVehicular { get; set; }
        public string AnioModeloVM { get; set; }
        public string PlacaVM { get; set; }
        public string Rem1SubTipoRem { get; set; }
        public string Rem1Placa { get; set; }
        public string Rem2SubTipoRem { get; set; }
        public string Rem2Placa { get; set; }
        public string Operfc { get; set; }
        public string NumLicencia { get; set; }
        public string opeNombre { get; set; }
        public string OpeEMail { get; set; }
        public string OpeTel { get; set; }
        public string NumRegIdTribOperador { get; set; }
        public string ResidenciaFiscalOperador { get; set; }
        public string result { get; set; }
        public string ClienteNombre { get; set; }

        public string ClienteRfc { get; set; }
        public string ClienteEmail { get; set; }
        public string RemitenteNombre { get; set; }

        public string RemitenteRfc { get; set; }

        public string DestinatarioNombre { get; set; }

        public string DestinatarioRfc { get; set; }

        public string DomOriPais { get; set; }

        public string DomOriEstado { get; set; }

        public string DomOriMunicipio { get; set; }

        public string DomOriLocalidad { get; set; }

        public string DomOriColonia { get; set; }
        public string DomOriCalle { get; set; }
        public string DomOriNumExt { get; set; }

        public string DomOriNumInt { get; set; }

        public string DomOriRef { get; set; }

        public string DomOriCP { get; set; }

        public string DomDesPais { get; set; }

        public string DomDesEstado { get; set; }

        public string DomDesMunicipio { get; set; }

        public string DomDesLocalidad { get; set; }

        public string DomDesColonia { get; set; }

        public string DomDesCalle { get; set; }
        public string DomDesNumExt { get; set; }

        public string DomDesNumInt { get; set; }

        public string DomDesRef { get; set; }

        public string DomDesCP { get; set; }
        public int SiguienteFolioCfdi { get; set; }
    }
}