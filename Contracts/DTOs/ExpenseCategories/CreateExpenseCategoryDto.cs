﻿namespace Contracts.DTOs.ExpenseCategories;

public record CreateExpenseCategoryDto
{
    public required string CategoryName { get; init; }
    public required Guid IconId { get; init; }
}