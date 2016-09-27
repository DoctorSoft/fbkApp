using System.Dynamic;
using InputData.InputModels;

namespace FaceBook.Constants
{
    public static class ProxyConstants
    {
        public static ProxyData GetMockProxyData()
        {
            return new ProxyData
            {
                ProxyPort = "34530",
                ProxyType = "http",
                ProxyAddress = "5.101.64.137",
                ProxyUserName = "movses21",
                ProxyPassword = "2V1G10W6Xy"
            };
        }
    }
}
