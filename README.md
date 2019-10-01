---
page_type: sample
languages:
- csharp
products:
- dotnet
description: "Sample code for using Partner Center Payout APIs"
---

# Sample code for using Partner Center Payout APIs

<!-- 
Guidelines on README format: https://review.docs.microsoft.com/help/onboard/admin/samples/concepts/readme-template?branch=master

Guidance on onboarding samples to docs.microsoft.com/samples: https://review.docs.microsoft.com/help/onboard/admin/samples/process/onboarding?branch=master

Taxonomies for products and languages: https://review.docs.microsoft.com/new-hope/information-architecture/metadata/taxonomies?branch=master
-->

The Partner Center [Payments](https://partner.microsoft.com/dashboard/payouts/reports/incentivepayments) and [Transaction History](https://partner.microsoft.com/dashboard/payouts/reports/transactionhistory) reports have been built to provide the following high level benefits:
- Multi-MPN and/or publisher aggregated view (driven by user’s permission settings) 
- Rich filtering experience 
- Simplified export functionality for multiple MPNs and/or publishers
- Enhanced data transparency on the UI

Building on these capabilities, we are now introducing the ability for partners to use an API to get their Payout data directly. This leverages the capability of the [Export data](https://partner.microsoft.com/dashboard/payouts/reports/incentiveexport) that you might find on the Partner Center UI.

Available APIs - https://apidocs.microsoft.com/services/partnerpayouts
- Transaction history export - [Post](https://apidocs.microsoft.com/services/partnerpayouts#/ExportRequests/transactionhistory) a new request, [Get](https://apidocs.microsoft.com/services/partnerpayouts#/ExportRequests/transactionhistoryAll) an existing request and [Delete](https://apidocs.microsoft.com/services/partnerpayouts#/ExportRequests/transactionhistory2) an existing request.
- Payments export - [Post](https://apidocs.microsoft.com/services/partnerpayouts#/ExportRequests/payments) a new request, [Get](https://apidocs.microsoft.com/services/partnerpayouts#/ExportRequests/paymentsAll) an existing request and [Delete](https://apidocs.microsoft.com/services/partnerpayouts#/ExportRequests/payments2) an existing request.

## Prerequisites
- [Register an application with AAD/Microsoft Identity](https://docs.microsoft.com/en-us/graph/auth-register-app-v2) in the Azure portal. Add appropriate owners and roles to the application.
- Install [Visual Studio](https://visualstudio.microsoft.com/downloads/) for your platform.

## Running the sample
1. Clone or download this sample repository.
2. Open the solution file (..\Partner-Center-Payout-APIs\src\PartnerCenterPayoutAPIsSampleCode\PartnerCenterPayoutAPIsSampleCode.sln) in Visual Studio or your preferred IDE.
3. Add the values for the tenantId, clientId, userName and password keys in the App.config file. TenantId and ClientId values can be found under your Azure Active Directory application registered on the Azure portal while completing the Prerequisite step.
4. Add the necessary requestId you want to retrieve the data for in Program.cs.
5. Add appropriate filter conditions if any in the TransactionHistory.cs file for Transaction history request and/or Payments.cs file for Payments request.
6. Press F5 or run the PartnerCenterPayoutAPIsSampleCode project.

## Important reminders
1. Given the large volume of data, the SLA for the submitted request to be processed is 24 hours. 
2. Once the data is available for download, the delete API can also be used to delete any of the previous transaction history or payments requests. 
3. Requests created through the API or the UI will be visible on the [Export data](https://partner.microsoft.com/dashboard/payouts/reports/incentiveexport) page and can be downloaded or deleted through the API or UI.

## Coming soon
The API is created to address the enhanced partner demand for Payment and Transaction history information access in a programmatic manner. There will continue to be enhancements made to the API with the updated documentation. Additionally, incorporation of the API with the Partner Center SDK will be explored.

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
