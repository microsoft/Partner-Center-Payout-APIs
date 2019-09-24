// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace PartnerCenterPayoutAPISampleCode
{
    using System;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using PartnerCenterPayoutAPIsSampleCode;
    
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
            PrintResponse(transactionHistoryResponse);

            // Get the export files for a previously created request's requestId.
            var transactionHistoryRequestId = "3c150cae-11ce-412b-a6bd-29a946471841";
            HttpResponseMessage transactionHistoryStatusResponse = TransactionHistory.GetRequest(accessToken, transactionHistoryRequestId);
            string transactionHistoryBlobLocation = JObject.Parse(transactionHistoryStatusResponse.Content.ReadAsStringAsync().Result)["value"][0]["blobLocation"].ToString();
            PrintResponse(transactionHistoryStatusResponse);
            
            // Download the export zip file to local machine.
            Utils.DownloadBlob(transactionHistoryBlobLocation);

            //***************************************************************************************************************
            // PAYMENTS EXPORT APIS
            //***************************************************************************************************************

            // Create a new payments export request
            HttpResponseMessage paymentsResponse = Payments.CreateRequest(accessToken);
            PrintResponse(paymentsResponse);

            // Get the export files for a previously created request's requestId.
            var paymentsRequestId = "a2c409ec-2109-4470-9b69-208c41ed10fb";
            HttpResponseMessage paymentsStatusResponse = Payments.GetRequest(accessToken, paymentsRequestId);
            string paymentsBlobLocation = JObject.Parse(paymentsStatusResponse.Content.ReadAsStringAsync().Result)["value"][0]["blobLocation"].ToString();
            PrintResponse(paymentsStatusResponse);

            // Download the export zip file to local machine.
            Utils.DownloadBlob(paymentsBlobLocation);

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