using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ZK_Lymytz.TOOLS
{
    class WS
    {
        static string URl = ":8080/Lymytz_Web/ws/services/grh/";

        public string value()
        {
            try
            {
                Uri address = new Uri("http://api.search.yahoo.com/ContentAnalysisService/V1/termExtraction");

                // Create the web request  
                //HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;  
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return null;
        }

    }
}
