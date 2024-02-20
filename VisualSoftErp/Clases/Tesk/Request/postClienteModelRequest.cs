using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeskApiTest.Request
{

    public class postClienteModelRequest
    {
        public string correoCC { get; set; }
        public string cuentaTipoCliente { get; set; }
        public string curp { get; set; }
        public string departamentoCodigo { get; set; }
        public int diasDeCredito { get; set; }
        public Direccion direccion { get; set; }
        public string nombre { get; set; }
        public string primerApellido { get; set; }
        public string rfc { get; set; }
        public string segundoApellido { get; set; }
        public string sucursalCodigo { get; set; }
        public string taxId { get; set; }
        public string tipoPersona { get; set; }

    }
    public class Direccion
    {
        public Contacto contacto { get; set; }
        public Direcciondetalle direccion { get; set; }
        public Localidad localidad { get; set; }

    }
    public class Contacto
    {
        public string celular { get; set; }
        public string correo { get; set; }
        public string correoCC { get; set; }
        public string telefono { get; set; }
    }
    public class Direcciondetalle
    {
        public string calle { get; set; }
        public string ciudad { get; set; }
        public string codigoPostal { get; set; }
        public string colonia { get; set; }
        public string numeroExt { get; set; }
        public string numeroInt { get; set; }
    }
    public class Localidad
    {
        public string estadoCodigo { get; set; }
        public string municipioCodigo { get; set; }
        public string paisCodigo { get; set; }
    }
}
