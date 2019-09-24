// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace PartnerCenterPayoutAPISampleCode
{
    using System.Configuration;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class UserCredentialTokenGenerator
    {
        public static async Task<string> GetAccessToken()
        {
            // Fetch these settings the App config key values
            string tenantId = ConfigurationManager.AppSettings.Get("tenantId");
            string clientId = ConfigurationManager.AppSettings.Get("clientId");
            string userName = ConfigurationManager.AppSettings.Get("userName");
            string password = ConfigurationManager.AppSettings.Get("password");
            string scope = "https://api.partner.microsoft-int.com";

            string tokenEndpointFormat = "https://login.microsoftonline.com/{0}/oauth2/token";
            string tokenEndpoint = string.Format(tokenEndpointFormat, tenantId);

            dynamic result;
            using (HttpClient client = new HttpClient())
            {
                string tokenUrl = tokenEndpoint;
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, tokenUrl))
                {
                    string content = string.Format("grant_type=password&username={0}&password={1}&client_id={2}&resource={3}", userName, password, clientId, scope);

                    request.Content = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");

                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject(responseContent);
                    }
                }
            }

            return result.access_token;
        }
    }
}