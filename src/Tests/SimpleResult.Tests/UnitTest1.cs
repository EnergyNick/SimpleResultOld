using FluentAssertions;
using NUnit.Framework;

namespace SimpleResult.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var r = Result.Success();
            var resultWithValue = r.Convert.ToResult(5);
            var resultTypical = resultWithValue.Convert.ToResult();

            var asResult = (Result)resultWithValue;

            var b = asResult.Convert.ToResult("Hi");

            resultWithValue.Convert.ToResult<int>();

            // var readOnlyResult = b.AsReadonly();
            //
            // var hiBro = "Hi Bro!";
            // readOnlyResult.Copy.WithValue(hiBro).Should().Match<ReadOnlyResult<string>>(x => x.Value == hiBro);
            //
            // b.Should().NotBeNull();
            //
            // resultTypical.Should().NotBeNull();
        }
    }
}