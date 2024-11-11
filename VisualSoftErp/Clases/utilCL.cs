using DevExpress.Printing.Exports.RtfExport;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftErp.Clases
{
    public class utilCL
    {
        #region PROPIEDADES
        #endregion

        #region CONSTRUCTOR
        public utilCL() { }
        #endregion

        #region METODOS

        public string fillNumberString(string sender, int maxLength)
        {
            string result = string.Empty;
            try
            {
                if (sender.Length > 0 && maxLength > 0 && maxLength >= sender.Length)
                {
                    result = sender;
                    if(sender.Length < maxLength)
                        result = result.PadLeft(maxLength, '0');       
                }
            }
            catch(Exception ex)
            {
                result = "Error en fillNumberString: " + ex.Message;
            }
            return result;
        }

        #endregion

    }
}
