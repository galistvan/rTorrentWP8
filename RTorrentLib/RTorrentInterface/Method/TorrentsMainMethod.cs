using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Data.Linq;

namespace RTorrentLib.RTorrentInterface.Method
{
    internal class TorrentsMainMethod : TorrentsMethod
    {
        internal TorrentsMainMethod(string url) : base(url) { }

        //TODO rethink this monster
        protected static readonly new object[] parameters = (new object[] { "main" }).Concat(TorrentsMethod.parameters).ToArray(); 

        protected override object[] Parameters
        {
            get { return parameters; }
        }
    }
}
