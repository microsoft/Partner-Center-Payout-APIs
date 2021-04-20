// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PartnerCenterPayoutAPIsSampleCode
{
    class Program
    {
        private static async Task Main()
        {
            // Retrieve an Azure AD access token which will be passed in the Authorization header of the request.
            var userCredentialGenerator = new UserCredentialTokenGenerator();
            var authenticationResult = await userCredentialGenerator.GetToken();

            //***************************************************************************************************************
            // TRANSACTION HISTORY EXPORT APIS
            //***************************************************************************************************************

            // Create a new transaction history export request
            var transactionHistoryResponse = TransactionHistory.CreateRequest(authenticationResult.AccessToken);
            await PrintResponse(transactionHistoryResponse);

            // Example to get request ID from current response:
            // var transactionHistoryRequestId = transactionHistoryResponse.Headers.GetValues("Request-ID").FirstOrDefault();
            var transactionHistoryRequestId = "<add_transactionhistory_request_id_here>";

            // Get the export files for a previously created request's requestId (Guid).
            var transactionHistoryStatusResponse = TransactionHistory.GetRequest(authenticationResult.AccessToken, transactionHistoryRequestId);
            var transactionHistoryResponseValue = await transactionHistoryStatusResponse.Content.ReadAsStringAsync();
            var transactionHistoryValue = JObject.Parse(transactionHistoryResponseValue);
            var transactionHistoryBlobLocation = transactionHistoryValue["value"][0]["blobLocation"].ToString();
            var transactionHistoryRequestStatus = transactionHistoryValue["value"][0]["status"].ToString();
            PrintJson(transactionHistoryResponseValue);

            // Download the export zip file to local machine if the request status is completed and blob location is populated.
            if (transactionHistoryRequestStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(transactionHistoryBlobLocation))
            {
                Utils.DownloadBlob(transactionHistoryBlobLocation);
            }

            //***************************************************************************************************************
            // PAYMENTS EXPORT APIS
            //***************************************************************************************************************

            // Create a new payments export request
            var paymentsResponse = Payments.CreateRequest(authenticationResult.AccessToken);
            await PrintResponse(paymentsResponse);

            // Get the export files for a previously created request's requestId (Guid).
            // Example to get request ID from current response:
            // var paymentsRequestId = paymentsResponse.Headers.GetValues("Request-ID").FirstOrDefault(); ;
            var paymentsRequestId = "<add_payments_request_id_here>";
            var paymentsStatusResponseValue = await Payments.GetRequest(authenticationResult.AccessToken, paymentsRequestId).Content.ReadAsStringAsync();
            var paymentsStatus = JObject.Parse(paymentsStatusResponseValue);

            var paymentsBlobLocation = paymentsStatus["value"][0]["blobLocation"].ToString();
            var paymentsRequestStatus = paymentsStatus["value"][0]["status"].ToString();
            PrintJson(paymentsStatusResponseValue);

            // Download the export zip file to local machine if the request status is completed and blob location is populated.
            if (paymentsRequestStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(paymentsBlobLocation))
            {
                Utils.DownloadBlob(paymentsBlobLocation);
            }

            Console.Read();
        }

        public static async Task PrintResponse(HttpResponseMessage response)
        {
            Console.WriteLine(response);
            var contents = await response.Content.ReadAsStringAsync();
            PrintJson(contents);
            response.Dispose();
        }

        static void PrintJson(string value)
        {
            Console.WriteLine(JsonConvert.SerializeObject(JObject.Parse(value), Formatting.Indented));
        }
    }
}