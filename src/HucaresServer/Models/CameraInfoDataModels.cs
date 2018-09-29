using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HucaresServer.Models
{
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