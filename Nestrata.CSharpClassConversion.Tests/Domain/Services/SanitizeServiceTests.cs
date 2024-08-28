using Moq;
using Nestrata.CSharpClassConversion.Domain.Services;

namespace Nestrata.CSharpClassConversion.Test.Domain.Services
{
    public class SanitizeServiceTests
    {
        public SanitizeServiceTests()
        {
        }

        #region InputCleanupForProcess Method Tests
        [Fact] // Class with common datatypes (string, int, decimal, long, short)
        public void Service_InputCleanupForProcess_Success_Common_DataTypes()
        {
            //Arrange
            string input = @"public class PersonDto
            {
                public string StringTest { get; set; }
                public int IntTest { get; set; }
                public long LongTest { get; set; }
                public decimal DecimalTest { get; set; }
            }";
            List<string> expectedResult = new List<string>()
            {
                "",
                "class PersonDto",
                "string StringTest",
                "int IntTest",
                "long LongTest",
                "decimal DecimalTest"
            };

            //Act
            var service = new SanitizeService();
            List<string> actualResult = service.InputCleanupForProcess(input);

            //Assert
            Assert.True(expectedResult.Count() == actualResult.Count());
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Service_InputCleanupForProcess_Success_DataType_With_List_Model()
        {
            //Arrange
            string input = @"public class TestDto
            {
                public string StringTest { get; set; }
                public int IntTest { get; set; }
                public long LongTest { get; set; }
                public decimal DecimalTest { get; set; }
                public List<Test> Tests { get; set; }

                public class Test
                {
                    public string StringTest { get; set; }
                    public int IntTest { get; set; }
                    public long LongTest { get; set; }
                    public decimal DecimalTest { get; set; }
                }
            }";
            List<string> expectedResult = new List<string>()
            {
                "",
                "class TestDto",
                "string StringTest",
                "int IntTest",
                "long LongTest",
                "decimal DecimalTest",
                "List<Test> Tests",
                "class Test",
                "string StringTest",
                "int IntTest",
                "long LongTest",
                "decimal DecimalTest"
            };

            //Act
            var service = new SanitizeService();
            List<string> actualResult = service.InputCleanupForProcess(input);

            //Assert
            Assert.True(expectedResult.Count() == actualResult.Count());
            Assert.Equal(expectedResult, actualResult);
        }
        #endregion

        #region ToCamelCase Method Tests
        [Theory]
        [InlineData("StringTest", "stringTest")]
        [InlineData("IntTest", "intTest")]
        [InlineData("ShortTest", "shortTest")]
        [InlineData("LongTest", "longTest")]
        [InlineData("DecimalTest", "decimalTest")]
        public void Service_ToCamelCase_Success(string propertyName, string expectedResult)
        {
            //Act
            var service = new SanitizeService();
            string actualResult = service.ToCamelCase(propertyName);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        #endregion

        #region CheckNullableProperty Method Tests
        [Theory]
        [InlineData("string?", "?")]
        [InlineData("int?", "?")]
        [InlineData("short?", "?")]
        [InlineData("long?", "?")]
        [InlineData("decimal?", "?")]
        [InlineData("List<Test>?", "?")]
        [InlineData("string", "")]
        [InlineData("int", "")]
        [InlineData("short", "")]
        [InlineData("long", "")]
        [InlineData("decimal", "")]
        [InlineData("List<Test>", "")]
        public void Service_CheckNullableProperty_Success(string propertyType, string expectedResult)
        {
            //Act
            var service = new SanitizeService();
            string actualResult = service.CheckNullableProperty(propertyType);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
        #endregion
    }
}
