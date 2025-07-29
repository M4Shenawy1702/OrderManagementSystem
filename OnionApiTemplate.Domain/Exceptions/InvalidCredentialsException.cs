﻿namespace OrderManagementSystem.Domain.Exceptions
{
    public class InvalidCredentialsException()
        : BadRequestException(new List<string> { "Invalid Credentials" });
}
