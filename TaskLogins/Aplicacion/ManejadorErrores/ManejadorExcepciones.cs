using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.ManejadorErrores
{
    public class ExcepcionError : Exception
    {
        public HttpStatusCode Codigo { get; }
        public object Errores   { get; }
        public object OK        { get; }
        public ExcepcionError(HttpStatusCode codigo, object Status = null)
        {
            this.Codigo  = codigo;
            this.Errores = Status;
        }
    }


    public class ExcepcionOK : Exception
    {
        public HttpStatusCode response { get; }
        public object OK { get; }
        public ExcepcionOK(HttpStatusCode codigo, object OK = null)
        { 
           this.OK = OK;
        }
    }
}
