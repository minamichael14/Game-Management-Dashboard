public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int MaxFileSize)
    {
        _maxFileSize = MaxFileSize;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile formFile)  // IFormFile formFile = value as IFormFile; + catches Nulls
        {
            if (formFile.Length > _maxFileSize)
            {
                return new ValidationResult($"Maximum allowed size is {_maxFileSize} Bytes");
            }
        }
        return ValidationResult.Success;
    }
}
