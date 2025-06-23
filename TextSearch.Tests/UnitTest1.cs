using System;
using System.IO;
using Xunit;
using TextSearch;

namespace TextSearch.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("hello world, hello", "hello", 2)]
        [InlineData("ababab", "aba", 1)]
        [InlineData("aaaa", "aa", 2)]
        [InlineData("test", "", 0)]
        [InlineData("", "test", 0)]
        public void CountOccurrences_WorksCorrectly(string text, string word, int expected)
        {
            Assert.Equal(expected, Program.CountOccurrences(text, word));
        }

        [Fact]
        public void CountOccurrences_NullWord_ReturnsZero()
        {
            Assert.Equal(0, Program.CountOccurrences("abc", null!));
        }

        private int RunMain(string[] args, string stdin, out string stdout, out string stderr)
        {
            var originalOut = Console.Out;
            var originalErr = Console.Error;
            var originalIn = Console.In;

            using var outWriter = new StringWriter();
            using var errWriter = new StringWriter();
            Console.SetOut(outWriter);
            Console.SetError(errWriter);
            Console.SetIn(new StringReader(stdin));

            int code = Program.Main(args);

            stdout = outWriter.ToString().Trim();
            stderr = errWriter.ToString().Trim();

            Console.SetOut(originalOut);
            Console.SetError(originalErr);
            Console.SetIn(originalIn);

            return code;
        }

        [Fact]
        public void Main_NoArguments_Returns1()
        {
            int code = RunMain(Array.Empty<string>(), "some text", out _, out var err);
            Assert.Equal(1, code);
            Assert.Contains("Usage", err);
        }

        [Fact]
        public void Main_EmptyInput_Returns2()
        {
            int code = RunMain(new[] { "-word", "text" }, "", out _, out var err);
            Assert.Equal(2, code);
            Assert.Contains("No input text", err);
        }

        [Fact]
        public void Main_ValidInput_Returns0AndCorrectCount()
        {
            int code = RunMain(new[] { "-word", "foo" }, "foo bar foo", out var output, out var err);
            Assert.Equal(0, code);
            Assert.Equal("2", output);
            Assert.Equal(string.Empty, err);
        }
    }
}
