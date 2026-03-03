namespace Shared.DataTransferObjects;

public record ExpenseCreationDto : ExpenseManipulationDto
{
    public string CreatedAt { get; init; } = DateTime.Now.ToString("dd-MM-yyyy" + " HH:mm:ss");
}