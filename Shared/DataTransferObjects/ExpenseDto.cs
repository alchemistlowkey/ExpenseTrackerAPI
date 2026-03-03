namespace Shared.DataTransferObjects;

public record ExpenseDto
{
    public Guid Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string Date { get; init; } = DateTime.Now.ToString("dd-MM-yyyy" + " HH:mm:ss");
    public ExpenseCategoryDto Category { get; init; } = ExpenseCategoryDto.Others;
}
