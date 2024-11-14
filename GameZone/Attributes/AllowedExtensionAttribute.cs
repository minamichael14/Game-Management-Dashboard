namespace GameZone.Attributes
{
    public class AllowedExtensionAttribute:ValidationAttribute
    {
        private readonly string _allowedExtension;

        public AllowedExtensionAttribute(string AllowedExtension)
        {
            _allowedExtension = AllowedExtension;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile formFile = value as IFormFile;
            if(formFile != null)
            {
                var extension = Path.GetExtension(formFile.FileName);
                
                bool isAllowed = _allowedExtension.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if(!isAllowed)
                {
                    return new ValidationResult($"Only {_allowedExtension} are allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
