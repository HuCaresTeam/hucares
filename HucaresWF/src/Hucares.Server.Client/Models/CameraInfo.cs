﻿using System;

namespace Hucares.Server.Client.Models
{
    public class CameraInfo : IEquatable<CameraInfo>
    {
        /// <summary>
        /// The primary key of CameraInfo table.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Host url of where footage can be retrieved from the camera.
        /// </summary>
        public string HostUrl { get; set; }

        /// <summary>
        /// Latitude position of geographical location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude position of geographical location. 
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Determines whether this source is a trusted one or not.
        /// Used only as an indentifier for the FrontEnd.
        /// </summary>
        public bool IsTrustedSource { get; set; }

        /// <summary>
        /// Determines whether this source should still be used for parsing images.
        /// When cameras are unneeded they cannot be deleted until all records referencing the camera are deleted.
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        public bool Equals(CameraInfo other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (ReferenceEquals(null, other))
                return false;
            return (Id == other.Id
                    && HostUrl == other.HostUrl
                    && Latitude == other.Latitude
                    && Longitude == other.Longitude
                    && IsTrustedSource == other.IsTrustedSource
                    && IsActive == other.IsActive);
        }
    }
}
