using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HucaresServer.Models
{
    /// <summary>
    /// Responsible for containing data input model structs, which allow only certain data to be passed through to the controller.
    /// </summary>
    public class CameraInfoDataModels
    {
        public struct InsertCameraDataModel
        {
            public string HostUrl { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public bool IsTrustedSource { get; set; }
        }

        public struct UpdateCameraSourceDataModel
        {
            public string HostUrl { get; set; }
            public bool IsTrustedSource { get; set; }
        }

        public struct UpdateCameraActivityDataModel
        {
            public bool IsActive { get; set; }
        }
    }
}