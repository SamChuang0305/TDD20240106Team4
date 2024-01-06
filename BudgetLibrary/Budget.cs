namespace BudgetLibrary;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }

    private int _year => int.Parse(YearMonth.Substring(0, 4));
    private int _month => int.Parse(YearMonth.Substring(4, 2));

    public DateTime MonthStartDay => new(_year, _month, 1);
    public DateTime MonthEndDay => new(_year, _month, DateTime.DaysInMonth(_year, _month));
}
