using System.Configuration;

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
    }
}
