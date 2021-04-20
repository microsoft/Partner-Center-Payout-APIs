// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace PartnerCenterPayoutAPIsSampleCode
{
    public static class Utils
    {

        public static async void DownloadBlob(string blobLocation)
        {
            CloudBlockBlob blob = new CloudBlockBlob(new Uri(blobLocation));

            // Set the directory path and get the filename from the blobLocation url
            string[] uriSegments = (new Uri(blobLocation)).Segments;
            var directoryPath = Path.GetFullPath($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\..\\Downloads\\PayoutExport\\");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = uriSegments[uriSegments.Length - 1];
            string fileLocation = directoryPath + fileName;
            
            // Download the zip file to the local machine.
            await blob.DownloadToFileAsync(fileLocation, FileMode.Create);

            Console.WriteLine(fileName + " downloaded successfully.");

            //*************************************************************************************************************************
            // Commented Sample code below can be used for doing any further processing of the csv files in the downloaded zip file.
            //*************************************************************************************************************************

            //using (var zipStream = new FileStream(fileLocation, FileMode.Open))
            //{
            //    using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
            //    {
            //        foreach (var entry in archive.Entries)
            //        {
            //            // Process the file here.
            //        }
            //    }
            //}
        }
    }
}
