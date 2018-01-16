using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BashIm
{
    class Utils
    {
        private static bool Init = false;
        private static HttpClient _httpClient;

        public static async Task<string> GetPageContent(string Address)
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }

            if (Init == false)
            {
                Init = true;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            }

            var Response = await _httpClient.GetByteArrayAsync(Address);
            var Source = Encoding.GetEncoding(1251).GetString(Response, 0, Response.Length - 1);
            return WebUtility.HtmlDecode(Source);
        }
    }
}