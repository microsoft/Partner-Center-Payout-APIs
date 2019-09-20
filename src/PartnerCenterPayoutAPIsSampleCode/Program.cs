// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace PartnerCenterPayoutAPISampleCode
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Net.Http;

    class Program
    {
        static void Main(string[] args)
        {
            // Retrieve an Azure AD access token which will be passed in the Authorization header of the request.
            string accessToken = UserCredentialTokenGenerator.GetAccessToken().Result;

            //***************************************************************************************************************
            // TRANSACTION HISTORY EXPORT APIS
            //***************************************************************************************************************

            // Create a new transaction history export request
            HttpResponseMessage transactionHistoryResponse = TransactionHistory.CreateRequest(accessToken);
            var transactionHistoryResponseObject = transactionHistoryResponse.Content.ReadAsStringAsync().Result;
            PrintResponse(transactionHistoryResponse);

            // Read the requestId from the response object and get the request details.
            var transactionHistoryRequestId = ((JObject.Parse(transactionHistoryResponseObject))["value"][0])["requestId"].ToString();
            HttpResponseMessage transactionHistoryStatusResponse = TransactionHistory.GetRequest(accessToken, transactionHistoryRequestId);
            PrintResponse(transactionHistoryStatusResponse);

            //***************************************************************************************************************
            // PAYMENTS EXPORT APIS
            //***************************************************************************************************************

            // Create a new payments export request
            HttpResponseMessage paymentsResponse = Payments.CreateRequest(accessToken);
            var paymentsResponseObject = paymentsResponse.Content.ReadAsStringAsync().Result;
            PrintResponse(paymentsResponse);

            // Read the requestId from the response object and get the request details.
            var paymentsRequestId = ((JObject.Parse(paymentsResponseObject))["value"][0])["requestId"].ToString();
            HttpResponseMessage statusResponse = Payments.GetRequest(accessToken, paymentsRequestId);
            PrintResponse(statusResponse);

            Console.Read();
        }

        public static void PrintResponse(HttpResponseMessage response)
        {
            Console.WriteLine(response);
            Console.WriteLine(JsonConvert.SerializeObject(JObject.Parse(response.Content.ReadAsStringAsync().Result), Formatting.Indented));
            response.Dispose();
        }
    }
}