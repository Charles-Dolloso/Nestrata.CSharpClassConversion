
namespace Nestrata.CSharpClassConversion.Domain.Interfaces
{
    public interface ISanitizeService
    {
        List<string> InputCleanupForProcess(string input);
        string ToCamelCase(string propertyName);
        string CheckNullableProperty(string propertyType);
    }
}
