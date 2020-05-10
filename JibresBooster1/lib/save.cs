using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JibresBooster1.lib
{
    internal class save
    {
        public static readonly string JibresHook = "https://jibres.ir/hook/booster1/";
        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> post(Dictionary<string, string> _vals)
        {
            try
            {
                FormUrlEncodedContent content = new FormUrlEncodedContent(_vals);

                HttpResponseMessage response = await client.PostAsync(JibresHook, content);

                string responseString = await response.Content.ReadAsStringAsync();

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
                string responseString = await client.GetStringAsync(JibresHook);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
