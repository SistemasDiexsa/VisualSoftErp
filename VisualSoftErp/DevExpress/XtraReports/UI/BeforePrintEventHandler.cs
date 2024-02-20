using System;
using System.ComponentModel;

namespace DevExpress.XtraReports.UI
{
    internal class BeforePrintEventHandler
    {
        private Action<object, CancelEventArgs> detail_BeforePrint;

        public BeforePrintEventHandler(Action<object, CancelEventArgs> detail_BeforePrint)
        {
            this.detail_BeforePrint = detail_BeforePrint;
        }
    }
}