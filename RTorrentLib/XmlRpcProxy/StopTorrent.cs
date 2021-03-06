﻿using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RTorrentLib.XmlRpcProxy
{
    public class StopTorrent : XmlRpcClientProtocol
    {
        public StopTorrent(String serviceUrl)
        {
            this.Url = serviceUrl;
        }
        
        [XmlRpcBegin("d.stop")]
        public IAsyncResult BeginInvoke(AsyncCallback acb, object hash)
        {
            return this.BeginInvoke(MethodBase.GetCurrentMethod(), new object[] { hash }, acb, null);
        }

        [XmlRpcEnd]
        public string EndInvoke(IAsyncResult iasr)
        {
            string ret = base.EndInvoke(iasr).ToString(); //int
            return ret;
        }
    }
}
