using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases
{
    public class diccionarioErroresCL
    {
        #region Propiedades
        public string strmensaje { get; set; }
        public string strmensajePropio { get; set; }
        #endregion
        #region Constructor
        public diccionarioErroresCL()
        {
            strmensaje = string.Empty;
            strmensajePropio = string.Empty;
        }
        #endregion
        #region Métodos
        public string mensajePropio()
        {
            strmensaje = strmensaje.Substring(0, 10);
            switch (strmensaje)
            {
                case "Violation ":
                    strmensajePropio = "La serie + el folio que capturó ya existen";
                    break;
            }

            return strmensajePropio;
        }
        #endregion

    }
}
