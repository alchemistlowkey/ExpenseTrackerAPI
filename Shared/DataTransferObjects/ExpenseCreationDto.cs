namespace Shared.DataTransferObjects;

public record ExpenseCreationDto : ExpenseManipulationDto
{
    public string CreatedAt { get; init; } = DateTime.UtcNow.ToString("dd-MM-yyyy" + " HH:mm:ss");
}