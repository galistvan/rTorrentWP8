using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib
{
    internal class XmlRpcRequest
    {
        private readonly string _methodName;
        private readonly List<object> _pars;

        internal string MethodName
        {
            get { return _methodName; }
        }

        internal object[] Parameters
        {
            get { return _pars.ToArray(); }
        }

        internal XmlRpcRequest(string methodName)
        {
            this._methodName = methodName;
            this._pars = new List<object>();
        }

        internal void AddParameter(object parameter)
        {
            this._pars.Add(parameter);
        }
    }
}
