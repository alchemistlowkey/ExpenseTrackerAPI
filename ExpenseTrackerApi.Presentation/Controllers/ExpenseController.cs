using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using Shared.DataTransferObjects;
using Serilog;

namespace ExpenseTrackerApi.Presentation.Controllers
{
    [Route("api/expenses")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(IServiceManager service, ILogger<ExpenseController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all expenses.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>The expenses list</returns>
        /// <response code="200">Returns the list of expenses</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="500">If an error occurs on the server</response>
        [HttpGet(Name = "GetExpenses")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetExpenses()
        {
            Log.Information("Fetching all expenses.");
            var expenses = await _service.ExpenseService.GetAllExpensesAsync(trackChanges: false);
            Log.Information("Successfully retrieved {Count} expense(s).", expenses.Count());
            return Ok(expenses);
        }

        /// <summary>
        /// Retrieves a single expense by its id.
        /// </summary>
        /// <param name="id">The id of the expense to retrieve.</param>
        /// <returns>The requested expense.</returns>
        /// <response code="200">Returns the expense</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="404">If the expense was not found</response>
        /// <response code="500">If an error occurs on the server</response>
        [HttpGet("{id:guid}", Name = "GetExpense")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetExpense(Guid id)
        {
            Log.Information("Fetching expense with id {ExpenseId}.", id);
            var expense = await _service.ExpenseService.GetExpenseAsync(id, trackChanges: false);
            Log.Information("Successfully retrieved expense with id {ExpenseId}.", id);
            return Ok(expense);
        }

        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="expenseCreation">The expense to create.</param>
        /// <returns>The created expense.</returns>
        /// <response code="201">Returns the created expense</response>
        /// <response code="400">If the expense is null</response>
        /// <response code="500">If an error occurs on the server</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized</response> 
        [HttpPost(Name = "CreateExpense")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseCreationDto expenseCreation)
        {
            Log.Information("Creating new expense with amount {Amount} and category {Category}.",
                expenseCreation.Amount, expenseCreation.Category);
            var expense = await _service.ExpenseService.CreateExpenseAsync(expenseCreation);
            Log.Information("Successfully created expense with id {ExpenseId}.", expense.Id);
            return CreatedAtRoute("GetExpense", new { id = expense.Id }, expense);
        }

        /// <summary>
        ///     Updates an existing expense.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expenseUpdate"></param>
        /// <returns></returns>
        /// <response code="204">Returns no content</response>
        /// <response code="400">If the expense is null</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="500">If an error occurs on the server</response>
        [HttpPut("{id:guid}", Name = "UpdateExpense")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateExpense(Guid id, [FromBody] ExpenseUpdationDto expenseUpdate)
        {
            Log.Information("Updating expense with id {ExpenseId}.", id);
            await _service.ExpenseService.UpdateExpenseAsync(id, expenseUpdate, trackChanges: true);
            Log.Information("Successfully updated expense with id {ExpenseId}.", id);
            return NoContent();
        }

        /// <summary>
        ///     Deletes an existing expense.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Returns no content</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="500">If an error occurs on the server</response>
        [HttpDelete("{id:guid}", Name = "DeleteExpense")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            Log.Information("Deleting expense with id {ExpenseId}.", id);
            await _service.ExpenseService.DeleteExpenseAsync(id, trackChanges: false);
            Log.Information("Successfully deleted expense with id {ExpenseId}.", id);
            return NoContent();
        }
    }
}