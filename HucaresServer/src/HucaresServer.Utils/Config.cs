using System.Configuration;
using System.IO;

namespace HucaresServer.Utils
{
    public static class Config
    {
        public static string AlprSecretKey {
            get { return ConfigurationManager.AppSettings["AlprSecretKey"]; }
        }

        public static string AlprCountry {
            get {
                return ConfigurationManager.AppSettings["AlprCountry"];
            }
        }
        public static int AlprRecognizeVehicle {
            get {
                return int.Parse(ConfigurationManager.AppSettings["AlprRecognizeVehicle"]);
            }
        }
        public static string AlprState {
            get {
                return ConfigurationManager.AppSettings["AlprState"];
            }
        }
        public static int AlprReturnImage {
            get {
                return int.Parse(ConfigurationManager.AppSettings["AlprReturnImage"]);
            }
        }
        public static int AlprTopn {
            get {
                return int.Parse(ConfigurationManager.AppSettings["AlprTopn"]);
            }
        }
        public static string AlprPrewarp {
            get {
                return ConfigurationManager.AppSettings["AlprPrewarp"];
            }
        }

        public static string TemporaryStorageUrl {
            get {
                return ConfigurationManager.AppSettings["TemporaryStorageUrl"];
            }
        }

        public static string FullTemporaryStoragePath {
            get {
                return Path.Combine(Directory.GetCurrentDirectory(), TemporaryStorageUrl);
            }
        }

        public static string PermStorageUrl {
            get {
                return ConfigurationManager.AppSettings["PermStorageUrl"];
            }
        }

        public static string FullPermStoragePath {
            get {
                return Path.Combine(Directory.GetCurrentDirectory(), PermStorageUrl);
            }
        }
    }
}
