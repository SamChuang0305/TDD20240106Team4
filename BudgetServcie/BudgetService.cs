namespace BudgetProject
{
    public class BudgetService
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public decimal Query(DateTime start, DateTime end)
        {
            if (start > end)
            {
                return 0m;
            }

            var allBudget = _budgetRepo
                            .GetAll()
                            .Where(w => w.YearMonth.CompareTo( start.ToString("yyyyMM")) >= 0
                                   && w.YearMonth.CompareTo(end.ToString("yyyyMM")) <= 0);

            var amount = 0m;

            foreach (var budget in allBudget)
            {
                if (end >= budget.MonthEndDay && start <= budget.MonthStartDay)
                {
                    amount += budget.Amount;
                }
                else
                {
                    var tempStart = start > budget.MonthStartDay ? start : budget.MonthStartDay;
                    var tempEnd = end < budget.MonthEndDay ? end : budget.MonthEndDay;
                    var daysInMonth = DateTime.DaysInMonth(tempStart.Year, tempStart.Month);
                    decimal totalDays = (decimal)((tempEnd - tempStart).TotalDays + 1);
                    amount += budget.Amount / (decimal)daysInMonth * totalDays;
                }
            }

            return amount;
        }
    }
}
