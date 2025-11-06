namespace EwkQxObd.WebApi.Models
{
    public class NetworkCsvUploadViewModel
    {
        public bool FromWebCall { get; set; } = true;
        public IFormFileCollection? Files { get; set; }
    }
}