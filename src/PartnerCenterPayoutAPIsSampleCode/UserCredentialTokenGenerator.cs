// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace PartnerCenterPayoutAPIsSampleCode
{
    public class UserCredentialTokenGenerator
    {
        private static string TenantId => ConfigurationManager.AppSettings.Get("tenantId");
        private static string ClientId => ConfigurationManager.AppSettings.Get("clientId");
        private static IEnumerable<string> Scopes => new[] {"https://api.partner.microsoft.com/.default"};

        private IPublicClientApplication _publicClientApplication = null;

        public async Task Init()
        {
            if (_publicClientApplication != null)
            {
                return;
            }

            var options = new PublicClientApplicationOptions
            {
                TenantId = TenantId, 
                ClientId = ClientId, 
                AzureCloudInstance = AzureCloudInstance.AzurePublic
            };

            _publicClientApplication = PublicClientApplicationBuilder.CreateWithApplicationOptions(options).Build();
            var builder = _publicClientApplication.AcquireTokenWithDeviceCode(Scopes, DeviceCodeResultCallback);
            var result = await builder.ExecuteAsync();
        }

        /// <summary>
        /// GetToken makes sure that the token is renewed on every fetch.
        /// If a number of authenticated calls will happen within a one minute time span, feel free
        /// to reuse the value returned here. It is also acceptable to call the method when starting
        /// new work. By default, tokens returned from here will be valid for 3600 seconds (1 hour).
        /// </summary>
        /// <returns>
        /// The AuthenticationResult returns the one token used for authen
        /// </returns>
        public async Task<AuthenticationResult> GetToken()
        {
            await Init();

            var account = (await _publicClientApplication.GetAccountsAsync()).FirstOrDefault();
            if (account == null)
            {
                throw new InvalidCredentialException("It appears that the application has failed to authenticate to AAD.");
            }

            var result = await _publicClientApplication.AcquireTokenSilent(Scopes, account).WithForceRefresh(true)
                .ExecuteAsync();

            return result;
        }

        private Task DeviceCodeResultCallback(DeviceCodeResult arg)
        {
            Console.WriteLine($"Please go to {arg.VerificationUrl} and use this Device Code: {arg.UserCode}");
            return Task.Delay(0);
        }
    }
}