﻿namespace CleanSheet.Application.Features.Careers.Commands.Create;

public record CreateCareerRequest(
    string ManagerFirstName,
    string ManagerLastName, 
    string InitialTeam)
{
    public CreateCareerCommand ToCommand(long userId) =>
        new(userId, ManagerFirstName, ManagerLastName, InitialTeam);
};