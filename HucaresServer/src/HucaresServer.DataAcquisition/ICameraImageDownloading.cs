namespace HucaresServer.DataAcquisition
{
    public interface ICameraImageDownloading
    {    
        /// <summary>
        /// Downloads image from every source available in CameraInfo table. Optionally can be filtered by isTrusted value.
        /// </summary>
        /// <param name="isTrusted"> Optional parameter indicating if the source of image if verified.</param>
        /// <returns> Number of images downloaded.</returns>
        int DownloadImagesFromCameraInfoSources(bool? isTrusted = null);
        
    }
}