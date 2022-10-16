using System.ComponentModel.DataAnnotations;
using YouTube.WebApi.Service.Commons.Helpers;

namespace YouTube.WebApi.Service.Commons.Attributes;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file is not null)
        {
            if (FileSizeHelper.ByteToMb(file.Length) > _maxFileSize)
                return new ValidationResult($"Image must be less than {_maxFileSize} MB");
            else return ValidationResult.Success;
        }
        else return new ValidationResult("The file can not be null!");
    }
}
