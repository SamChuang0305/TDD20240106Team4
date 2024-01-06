using NSubstitute;
using NUnit.Framework;

namespace BudgetProject
{
    public class Tests
    {
        private BudgetService _budgetService;
        private IBudgetRepo _budgetRepo;

        [SetUp]
        public void Setup()
        {
            _budgetRepo = Substitute.For<IBudgetRepo>();
            _budgetService = new BudgetService(_budgetRepo);
        }

        [Test]
        public void TestStartOverEnd()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new Budget { YearMonth = "202401", Amount = 310 },
            });

            var result = _budgetService.Query(new DateTime(2024, 1, 20), new DateTime(2024, 1, 10));
            Assert.Equals(0, result);
        }
    }
}
