using BudgetServcie;
using NSubstitute;
using NUnit.Framework;

namespace BudgetService
{
    public class Tests
    {
        private IBudgetRepo _transferService;

        [SetUp]
        public void Setup()
        {
            _transferService = Substitute.For<IBudgetRepo>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}