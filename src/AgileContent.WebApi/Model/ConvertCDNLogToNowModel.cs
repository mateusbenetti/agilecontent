namespace AgileContent.WebApi.Model
{
    /// <summary>
    /// Input Model For Convert CDN Log File To Now Log File Model.
    /// </summary>
    public class ConvertCDNLogToNowModel
    {
        /// <summary>
        /// Url with CDN Log Content
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Version Now Log File.
        /// </summary>
        public string Version { get; set; }
    }
}
