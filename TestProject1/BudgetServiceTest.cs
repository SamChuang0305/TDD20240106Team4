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
        public void TestNoData()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new Budget { YearMonth = "202401", Amount = 310 },
            });

            var result = _budgetService.Query(new DateTime(2024, 2, 20), new DateTime(2024, 3, 10));
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestStartOverEnd()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new Budget { YearMonth = "202401", Amount = 310 },
            });

            var result = _budgetService.Query(new DateTime(2024, 1, 20), new DateTime(2024, 1, 10));
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestTheSameDay()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new Budget { YearMonth = "202401", Amount = 310 },
            });

            var result = _budgetService.Query(new DateTime(2024, 1, 20), new DateTime(2024, 1, 20));
            Assert.AreEqual(10, result);
        }

        [Test]
        public void TestInSameMonth()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new Budget { YearMonth = "202401", Amount = 310 },
            });

            var result = _budgetService.Query(new DateTime(2024, 1, 20), new DateTime(2024, 1, 25));
            Assert.AreEqual(60, result);
        }

        [Test]
        public void TestInCrossTwoMonth()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new Budget { YearMonth = "202403", Amount = 310 }, new Budget { YearMonth = "202404", Amount = 3000 },
            });

            var result = _budgetService.Query(new DateTime(2024, 3, 29), new DateTime(2024, 4, 5));
            Assert.AreEqual(530, result);
        }

        [Test]
        public void TestFullMonth()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new Budget { YearMonth = "202403", Amount = 310 }, new Budget { YearMonth = "202404", Amount = 3000 },
            });

            var result = _budgetService.Query(new DateTime(2024, 3, 1), new DateTime(2024, 3, 31));
            Assert.AreEqual(310, result);
        }

        [Test]
        public void TestCrossThreeMonth()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new Budget { YearMonth = "202403", Amount = 310 }, new Budget { YearMonth = "202404", Amount = 3000 }
                , new Budget { YearMonth = "202405", Amount = 31000 },
            });

            var result = _budgetService.Query(new DateTime(2024, 3, 29), new DateTime(2024, 5, 5));
            Assert.AreEqual(8030, result);
        }
    }
}
