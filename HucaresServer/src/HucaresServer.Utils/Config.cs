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

        public static string TemporaryStorage {
            get {
                return ConfigurationManager.AppSettings["TemporaryStorage"];
            }
        }

        public static string PermStorage {
            get {
                return ConfigurationManager.AppSettings["PermStorage"];
            }
        }

        public static int HangfireRetry {
            get {
                return int.Parse(ConfigurationManager.AppSettings["HangfireRetry"]);
            }
        }

        public static string HostAddress {
            get {
                return ConfigurationManager.AppSettings["HostAddress"];
            }
        }

        public static int ItemsPerPage {
            get {
                return int.Parse(ConfigurationManager.AppSettings["ItemsPerPage"]);
            }
        }
    }
}
