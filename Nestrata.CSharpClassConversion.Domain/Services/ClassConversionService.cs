using Nestrata.CSharpClassConversion.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Nestrata.CSharpClassConversion.Domain.Services
{
    public class ClassConversionService : IClassConversionService
    {
        private readonly ISanitizeService _sanitizeService;

        public ClassConversionService(ISanitizeService sanitizeService)
        {
            _sanitizeService = sanitizeService;
        }

        public string ToTypescriptModel(string input)
        {
            try
            {
                string response = "";

                var cleanInput = _sanitizeService.InputCleanupForProcess(input);

                foreach (var line in cleanInput)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;

                    var extractedLine = line.Split(" ");
                    string propertyType = extractedLine[0];
                    string propertyName = extractedLine[1];

                    if (propertyType == "class") // declare class
                    {
                        if (response.Contains("export interface"))
                            response += "} \r\n\r\n";

                        response += $"export interface {propertyName} {{";
                    }
                    else if (propertyType.Contains("string")) // declare string
                    {
                        response += $"{GetPropertyName(propertyName, propertyType)}: {propertyType};";
                    }
                    else if (propertyType.Contains("int") || propertyType.Contains("long") || propertyType.Contains("short")) // declare number
                    {
                        response += $"{GetPropertyName(propertyName, propertyType)}: number;";
                    }
                    else if (propertyType.Contains("decimal")) // declare decimal
                    {
                        response += $"{GetPropertyName(propertyName, propertyType)}: decimal;";
                    }
                    else if (propertyType.Contains("List")) // declare list
                    {
                        response += $"{GetPropertyName(propertyName, propertyType)}: {propertyType.Replace("List<", "").Replace(">", "")}[];";
                    }

                    response += "\r\n";
                };

                response += "}";
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured: {ex.Message}");
            }
        }

        private string GetPropertyName(string propertyName, string propertyType)
        {
            return $"{_sanitizeService.ToCamelCase(propertyName)}{_sanitizeService.CheckNullableProperty(propertyType)}";
        }
    }
}
