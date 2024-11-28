using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace TPCR
{
    public class CurrencyConverter
    {
        public string GetConversionRates()
        {
            string url = "https://www.cbr-xml-daily.ru/daily_json.js";


            using (WebClient client = new WebClient())
            {
                try
                {
                    string json = client.DownloadString(url);
                    return json;
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }
            }
        }
    }
}
