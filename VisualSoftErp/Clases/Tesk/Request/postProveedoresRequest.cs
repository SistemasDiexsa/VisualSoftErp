using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases.Tesk.Request
{
    public class postProveedoresRequest
    {
        public string clave { get; set; }
        public string cuentaTipoProveedor { get; set; }
        public int diasDeCredito { get; set; }
        public PDireccion direccion { get; set; }
        public string nombre { get; set; }
        public string rfc { get; set; }
        public int tiempoEntrega { get; set; }
        public string tipoCuenta { get; set; }
        public string tipoOperacionImpuestos { get; set; }
        public string tipoPersona { get; set; }
        public string tipoTerceroCodigo { get; set; }
    }

    public class PDireccion
    {
        public PContacto contacto { get; set; }
        public PDirecciondetalle direccion { get; set; }
        public PLocalidad localidad { get; set; }

    }
    public class PContacto
    {
        public string celular { get; set; }
        public string correo { get; set; }
        public string correoCC { get; set; }
        public string telefono { get; set; }
    }
    public class PDirecciondetalle
    {
        public string calle { get; set; }
        public string ciudad { get; set; }
        public string codigoPostal { get; set; }
        public string colonia { get; set; }
        public string numeroExt { get; set; }
        public string numeroInt { get; set; }
    }
    public class PLocalidad
    {
        public string estadoCodigo { get; set; }
        public string municipioCodigo { get; set; }
        public string paisCodigo { get; set; }
    }
}
