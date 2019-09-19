// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace PartnerCenterPayoutAPISampleCode
{
    using System;
    using System.Net.Http;

    class Program
    {
        static void Main(string[] args)
        {
            // Retrieve an Azure AD access token which will be passed in the Authorization header of the request.
            string accessToken = UserCredentialTokenGenerator.GetAccessToken().Result;

            // Create a new transaction history export request
            HttpResponseMessage response = TransactionHistory.CreateRequest(accessToken);

            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            response.Dispose();

            //Read the request status polling url from the response's location header
            string transactionHistoryStatusUrl = response.Headers.Location.ToString();

            HttpResponseMessage statusResponse = TransactionHistory.GetRequest(accessToken, transactionHistoryStatusUrl);

            Console.WriteLine(statusResponse);
            Console.WriteLine(statusResponse.Content.ReadAsStringAsync().Result);
            statusResponse.Dispose();

            Console.Read();
        }

    }
}