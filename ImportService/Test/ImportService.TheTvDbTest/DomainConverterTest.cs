using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace ImportService.TheTvDbTest
{
    public class DomainConverterTest
    {
        [Theory]
        [InlineData("Mark Twain", "Mark", "Twain")]
        [InlineData("Adewale Akinnuoye-Agbaje", "Adewale", "Akinnuoye-Agbaje")]
        [InlineData("Terry O'Quinn", "Terry", "O'Quinn")]
        public void ShouldSeparateIntoFirstNameAndLastName(string name, params string[] expectedNames)
        {
            var splitedNames = SeparateName(name);

            Assert.Equal(expectedNames, splitedNames);
        }

        [Theory]
        [InlineData("John Thomas Stockton", "John", "Thomas", "Stockton")]
        [InlineData("L. Scott Caldwell", "L.", "Scott", "Caldwell")]
        [InlineData("M.C. Gainey", "M.", "C.", "Gainey")]
        public void ShouldSeparateIntoFirstNameAndMiddleNameAndLastName(string name, params string[] expectedNames)
        {
            var splitedNames = SeparateName(name);

            Assert.Equal(expectedNames, splitedNames);
        }

        //TODO Implementation of name converter is rewritten here because domain converter has it as private method (maybe move it?)
        private string[] SeparateName(string name)
        {
            var splitedNames = Regex
                .Split(name, @"(?<=[\.\s])")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            return splitedNames;
        }
    }
}
