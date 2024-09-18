using BookStore.Application.Common.Exceptions;

namespace BookStore.Infrastructure.Exceptions;

public class InfrastructureException(string message, object? additionalData = null) : BaseException(message, additionalData);