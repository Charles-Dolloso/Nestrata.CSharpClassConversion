using Microsoft.Extensions.DependencyInjection;
using Nestrata.CSharpClassConversion.Domain.Interfaces;
using Nestrata.CSharpClassConversion.Domain.Services;

while (true)
{
    Console.WriteLine();

    var serviceProvider = new ServiceCollection()
        .AddSingleton<ISanitizeService, SanitizeService>()
        .AddSingleton<IClassConversionService, ClassConversionService>()
        .BuildServiceProvider();
    var classConversionService = serviceProvider.GetService<IClassConversionService>();

    Console.WriteLine("Input C# Object Definition as a string for Conversion:");
    var userInput = Console.ReadLine();

    Console.WriteLine();
    Console.WriteLine();

    Console.WriteLine("Converted to Typescript Results:");
    Console.WriteLine();
    Console.WriteLine(classConversionService?.ToTypescriptModel(userInput ?? ""));
    Console.ReadLine();

    while (true)
    {
        Console.Write("Do you want to try again [Y/N]?");
        string answer = Console.ReadLine() ?? "";
        if (answer.ToUpper() == "Y")
            break;
        if (answer.ToUpper() == "N")
            return;
    }
}

