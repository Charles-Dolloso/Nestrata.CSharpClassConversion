using Nestrata.CSharpClassConversion.Domain.Interfaces;

namespace Nestrata.CSharpClassConversion.Domain.Services
{
    public class SanitizeService : ISanitizeService
    {
        public SanitizeService() { }

        public List<string> InputCleanupForProcess(string input)
        {
            // Remove get; and set;
            input = input.Replace("get;", "").Replace("set;", "");

            // Remove { and }
            input = input.Replace("{", "").Replace("}", "");

            // Split value by public so we are only left with property type and property name
            var newValue = input.Split("public");

            return newValue.Select(x => x.Trim()).ToList();
        }

        public string ToCamelCase(string propertyName)
        {
            // Turn into camel case
            return Char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }

        public string CheckNullableProperty(string propertyType)
        {
            // Check for nullable property
            return (propertyType.Contains("?") ? "?" : "");
        }
    }
}
