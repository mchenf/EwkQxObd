namespace EwkQxObd.WebApi.Models
{
    public class NetworkCsvUploadViewModel
    {
        public bool FromWebCall { get; set; } = false;
        public IFormFileCollection? Files { get; set; }

        public string SelectedSystem { get; set; } = "Pacific";
    }
}