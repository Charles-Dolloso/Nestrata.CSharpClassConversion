﻿using Moq;
using Nestrata.CSharpClassConversion.Domain.Interfaces;
using Nestrata.CSharpClassConversion.Domain.Services;
using Xunit;

namespace Nestrata.CSharpClassConversion.Test.Domain.Services
{
    public class ClassConversionServiceTests
    {
        private Mock<ISanitizeService> _sanitizeService;

        public ClassConversionServiceTests()
        {
            // In real life coding we don't need to mock SanitizeService but we will mock it instead to showcase the mocking of response
            // We will create separate test case scenario for SanitizeService in SanitizeServiceTests
            _sanitizeService = new Mock<ISanitizeService>();

            // Service Level Mock
            _sanitizeService.Setup(s => s.CheckNullableProperty("string")).Returns("");
            _sanitizeService.Setup(s => s.CheckNullableProperty("string?")).Returns("?");
            _sanitizeService.Setup(s => s.CheckNullableProperty("int")).Returns("");
            _sanitizeService.Setup(s => s.CheckNullableProperty("int?")).Returns("?");
            _sanitizeService.Setup(s => s.CheckNullableProperty("long")).Returns("");
            _sanitizeService.Setup(s => s.CheckNullableProperty("long?")).Returns("?");
            _sanitizeService.Setup(s => s.CheckNullableProperty("short")).Returns("");
            _sanitizeService.Setup(s => s.CheckNullableProperty("short?")).Returns("?");
            _sanitizeService.Setup(s => s.CheckNullableProperty("decimal")).Returns("");
            _sanitizeService.Setup(s => s.CheckNullableProperty("decimal?")).Returns("?");

            _sanitizeService.Setup(s => s.ToCamelCase("StringTest")).Returns("stringTest");
            _sanitizeService.Setup(s => s.ToCamelCase("IntTest")).Returns("intTest");
            _sanitizeService.Setup(s => s.ToCamelCase("ShortTest")).Returns("shortTest");
            _sanitizeService.Setup(s => s.ToCamelCase("LongTest")).Returns("longTest");
            _sanitizeService.Setup(s => s.ToCamelCase("DecimalTest")).Returns("decimalTest");
        }

        [Fact] // Unit test for service constructor call
        public async Task Service_Constructor_Success()
        {
            //Arrange

            //Act
            var service = new ClassConversionService(_sanitizeService.Object);

            //Assert
            Assert.True(true);
            Assert.NotNull(service);
        }

        #region ToTypescriptModel Method Tests
        [Fact] // Class with common datatypes (string, int, decimal, long, short)
        public void Service_ToTypescriptModel_Success_Common_DataTypes()
        {
            //Arrange
            string input = @"public class TestDto
            {
                public string StringTest { get; set; }
                public int IntTest { get; set; }
                public short ShortTest { get; set; }
                public long LongTest { get; set; }
                public decimal DecimalTest { get; set; }
            }";
            string expectedResult = @"export interface SampleDto {
stringTest: string;
intTest: number;
shortTest: number;
longTest: number;
decimalTest: decimal;
}";
            //Setup
            _sanitizeService.Setup(s => s.InputCleanupForProcess(It.IsAny<string>()))
                .Returns(new List<string>()
            {
                "",
                "class SampleDto",
                "string StringTest",
                "int IntTest",
                "short ShortTest",
                "long LongTest",
                "decimal DecimalTest"
            });

            //Act
            var service = new ClassConversionService(_sanitizeService.Object);
            string actualResult = service.ToTypescriptModel(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact] // Class with common datatypes (string, int, decimal, long, short) that are nullable
        public void Service_ToTypescriptModel_Success_Common_DataTypes_Nullable()
        {
            //Arrange
            string input = @"public class TestDto
            {
                public string? StringTest { get; set; }
                public int? IntTest { get; set; }
                public short? ShortTest { get; set; }
                public long? LongTest { get; set; }
                public decimal? DecimalTest { get; set; }
            }";

            string expectedResult = @"export interface SampleDto {
stringTest?: string;
intTest?: number;
shortTest?: number;
longTest?: number;
decimalTest?: decimal;
}";
            //Setup
            _sanitizeService.Setup(s => s.InputCleanupForProcess(It.IsAny<string>()))
                .Returns(new List<string>()
            {
                "",
                "class SampleDto",
                "string? StringTest",
                "int? IntTest",
                "short? ShortTest",
                "long? LongTest",
                "decimal? DecimalTest"
            });

            //Act
            var service = new ClassConversionService(_sanitizeService.Object);
            string actualResult = service.ToTypescriptModel(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        #endregion
    }
}
