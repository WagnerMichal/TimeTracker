﻿namespace TimeTracker.App.Services.Interfaces;

public interface IAlertService
{
    Task DisplayAsync(string title, string message);
}