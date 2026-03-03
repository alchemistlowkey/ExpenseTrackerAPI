namespace Shared.DataTransferObjects;

public abstract record ExpenseManipulationDto
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public ExpenseCategoryDto Category { get; set; } = ExpenseCategoryDto.Others;
    public string UpdatedAt { get; init; } = DateTime.Now.ToString("dd-MM-yyyy" + " HH:mm:ss");
}
