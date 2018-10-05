using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JibresBooster1.lib
{
    class save
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> post(Dictionary<string, string> _vals)
        {            
            try
            {
                var serverURL = "https://jibres.com/hook/booster1/";
                var content = new FormUrlEncodedContent(_vals);

                var response = await client.PostAsync(serverURL, content);

                var responseString = await response.Content.ReadAsStringAsync();

                return true;
            }
            catch
            {
                return false;
            }            
        }



        public static async Task<bool> get(Dictionary<string, string> _vals)
        {
            try
            {
                var serverURL = "https://jibres.com/hook/booster1/";

                var responseString = await client.GetStringAsync(serverURL);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
