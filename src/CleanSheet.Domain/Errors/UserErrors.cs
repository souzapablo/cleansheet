﻿using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Errors;
public class UserErrors
{
    public static Error EmailAlreadyRegistered = new("EMAIL_ALREADY_REGISTERED", "E-mail is already registered.");
}
