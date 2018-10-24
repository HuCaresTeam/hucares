using System;
using HucaresServer.Storage.Models;

namespace HucaresServer.Models
{
    /// <summary>
    /// Responsible for containing data input model structs, which allow only certain data to be passed through to the controller.
    /// </summary>
    public class MissingLicensePlateDataModels
    {
        public struct PostPlateRecordDataModel
        {
            public string PlateNumber { get; set; }
            public DateTime SearchStartDateTime { get; set; }
        }
        
        public struct MarkFoundRecordDataModel
        {
            public DateTime EndDateTime { get; set; }
            public LicensePlateFoundStatus Status { get; set; }
        }
    }
}