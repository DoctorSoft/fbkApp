using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace FaceBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CookieContainer cookies = new CookieContainer();
            WebRequestHandler handler = new WebRequestHandler();
            handler.CookieContainer = cookies;
            X509Certificate2 certificate = new X509Certificate2();
            handler.ClientCertificates.Add(certificate);
            var httpClient = new HttpClient();
            //httpClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            httpClient.BaseAddress = new Uri("https://www.facebook.com/");
            httpClient.DefaultRequestHeaders
                .Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            var result = httpClient.GetAsync("https://www.facebook.com/").Result.Content.ReadAsStringAsync().Result;

            var cookieRequest = "https://www.facebook.com/cookie/consent/?pv=1&dpr=1";

            var cookieResult = httpClient.PostAsync(cookieRequest,
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("__a", "1"),
                    new KeyValuePair<string, string>("__be", "-1"),
                    new KeyValuePair<string, string>("__dyn", "7xeXxaER2HwNJ0ZwRAKGzEyay6-C11xG12wAxu13wm8gxZ3ocU9UKaxeUW2y7E4iu3e225ob8aUbo6ucxG48hwv9FovgWmdxu"),
                    new KeyValuePair<string, string>("__pc", "EXP1:DEFAULT"),
                    new KeyValuePair<string, string>("__req", "a"),
                    new KeyValuePair<string, string>("__rev", "2570112"),
                    new KeyValuePair<string, string>("lsd", "AVpl75o4"),
                })).Result.Content.ReadAsStringAsync().Result;

            var userValidation =
                "https://www.facebook.com/ajax/registration/validation/contactpoint_invalid/?contactpoint=GuschinIsaak93%40mail.ru&dpr=1&__user=0&__a=1&__dyn=7xeXxaER2HwNJ0ZwRAKGzEyay6-C11xG12wAxu13wm8gxZ3ocU9UKaxeUW2y7E4iu3e225ob8aUbo6ucxG48hwv9FovgWmdxu&__req=9&__be=-1&__pc=EXP1%3ADEFAULT&__rev=2570112";

            var validationResult = httpClient.GetAsync(userValidation).Result.Content.ReadAsStringAsync().Result;


            var response = httpClient.PostAsync("https://www.facebook.com/ajax/register.php?dpr=1",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("__a", "1"),
                    new KeyValuePair<string, string>("__be", "-1"),
                    new KeyValuePair<string, string>("__dyn", "7xeXxaER2HwNJ0ZwRAKGzEyay6-C11xG12wAxu13wm8gxZ3ocU9UKaxeUW2y7E4iu3e225ob8aUbo6ucxG48hwv9FovgWmdxu"),
                    new KeyValuePair<string, string>("__pc", "EXP1:DEFAULT"),
                    new KeyValuePair<string, string>("__req", "a"),
                    new KeyValuePair<string, string>("__rev", "2570112"),
                    new KeyValuePair<string, string>("__user", "0"),
                    new KeyValuePair<string, string>("ab_test_data", "AfAAAf/AAAfAAAfAAAAAAfAAAAAAAAAAAAAAAAAAAAq/AqVqVADAAH"),
                    new KeyValuePair<string, string>("asked_to_login", "0"),
                    new KeyValuePair<string, string>("birthday_day", "8"),
                    new KeyValuePair<string, string>("birthday_month", "11"),
                    new KeyValuePair<string, string>("birthday_year", "1989"),
                    new KeyValuePair<string, string>("captcha_persist_data", "AZklBlP6qltRYrwvvsz2dh5GF7QS3jw-vDCFrc3RCTVzoBYluDVgSAjfR8KflhmGXzbGm5X6su_KAxsZuPeBk0lnxSFUn2XbvmRya9p3vfIbiu6eXdDBlO1eckLEJPNdY8WFfkvpkInR-yOaesUo9bkxX74JqjM7RuZ2BdPJlYHPldLlu9bmcUpFHrIIc63gyGG0NsGZKRtSy28zwMLEEqiqjvgy_7vTQFW_IFy7dPZpebWcuRI0SL7MMq258PLL66XQOV1jaJBrwcSqak-hGYfHNJtNFWpNQopwt0Pvq74FaVukFi_qFMVKkpReS4HUus28DaeOENcSSge1Nsl0ikrm9pkFe_dRfNq6P0vF4cdZtPeJIjyvQK4sEz_BMM4JRiE"),
                    new KeyValuePair<string, string>("captcha_response", ""),
                    new KeyValuePair<string, string>("captcha_session", "mE92sfLzF5CjPsKEWastuA"),
                    new KeyValuePair<string, string>("contactpoint_label", "email_or_phone"),
                    new KeyValuePair<string, string>("extra_challenge_params", "authp=nonce.tt.time.new_audio_default&psig=yZY0FwKF65KZGt5MCNuS4ynSuzI&nonce=mE92sfLzF5CjPsKEWastuA&tt=Wm10sfwPVnnqZPwEfqRptu6gsok&time=1474099730&new_audio_default=1"),
                    new KeyValuePair<string, string>("firstname", "Isanna"),
                    new KeyValuePair<string, string>("ignore", "captcha"),
                    new KeyValuePair<string, string>("lastname", "Isanna"),
                    new KeyValuePair<string, string>("locale", "ru_RU"),
                    new KeyValuePair<string, string>("lsd", "AVpl75o4"),
                    new KeyValuePair<string, string>("qsstamp", "W1tbNCwzNyw1NCw1Nyw5Miw5NiwxMDIsMTEyLDExNCwxNjIsMTY0LDE2NSwxNzYsMjE0LDIzOSwyNDMsMjYxLDI3NSwyOTYsMzAwLDMxNSwzNDAsMzY1LDM3NSwzODAsMzgzLDM4NCw0MDMsNDMwLDQ0MSw0NDksNDgxLDQ4NCw1MDAsNTAyLDUyMSw1MjMsNTMxLDU0NCw1NDksNTUxLDk5MV1dLCJBWm1WMzZWYk9GTG1sSGdWS2FsbE1ObExqNEhzaWNzYUp4YTZWM2wxR2VMZ3pQMXNkQlZXYjl0TC0xNXFuRHVveF9HWEttY3BWSFlrbngzYkZFVmdiMzBmNUhnbWp6UHZ2UHNpVC1mY1FtUTQxMUlrY1JJbVlRWDZUeHdFUGwxdUFKNUc2UlhHUmQ2SzB3dk41a0g0ZEdGRXJwTWdieXIwZ3l0eDE2SjhEdlNqSm5WOXFRZlhxSV90V3d5b1VuRDJyQV9UemJsWGdoX1liY2VESkwtUUNMeDJGaXBTOVVuSTFPSHFmTXFYbWpuZmRRIl0="),
                    new KeyValuePair<string, string>("recaptcha_type", "password"),
                    new KeyValuePair<string, string>("ignore", "captcha"),
                    new KeyValuePair<string, string>("recaptcha_type", "password"),
                    new KeyValuePair<string, string>("referrer", ""),
                    new KeyValuePair<string, string>("reg_email__", "GuschinIsaak93@mail.ru"),
                    new KeyValuePair<string, string>("reg_email_confirmation__", "GuschinIsaak93@mail.ru"),
                    new KeyValuePair<string, string>("reg_instance", "-LnZV4k5JrHQvMkZcJHmIMla"),
                    new KeyValuePair<string, string>("reg_passwd__", "KxAyMzK9rlV"),
                    new KeyValuePair<string, string>("sex", "1"),
                    new KeyValuePair<string, string>("terms", "on"),
                })).Result.Content.ReadAsStringAsync().Result;
            //var task = httpClient.PostAsXmlAsync<DeviceRequest>("api/SaveData", request);
        }
    }
}
