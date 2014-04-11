using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib
{
    internal class XmlRpcRequest
    {
        private string methodName;
        private List<object> pars;

        internal string MethodName
        {
            get { return methodName; }
        }

        internal object[] Parameters
        {
            get { return pars.ToArray(); }
        }

        internal XmlRpcRequest(string methodName)
        {
            this.methodName = methodName;
            this.pars = new List<object>();
        }

        internal void AddParameter(object parameter)
        {
            this.pars.Add(parameter);
        }

    }
}
