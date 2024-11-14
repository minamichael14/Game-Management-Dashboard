namespace GameZone.ViewModels
{
    public class EditGameFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = String.Empty;

        [Display(Name = "Category")]
        public int CategorieId { get; set; }

        public IEnumerable<SelectListItem> Categories = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;

        public IEnumerable<SelectListItem> Devices = Enumerable.Empty<SelectListItem>();

        public string Description { get; set; } = String.Empty;

        [AllowedExtension(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;

        public string? CurrentCover { get; set; }
    }
}
