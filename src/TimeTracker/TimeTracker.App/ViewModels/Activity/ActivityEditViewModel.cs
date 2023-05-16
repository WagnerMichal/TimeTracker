using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using CommunityToolkit.Mvvm.Input;
using TimeTracker.App.Messages;
using TimeTracker.App.Resources.Texts;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.Common.Enums;

namespace TimeTracker.App.ViewModels;

[QueryProperty(nameof(Activity), nameof(Activity))]
[QueryProperty(nameof(UserId), nameof(UserId))]
public partial class ActivityEditViewModel : ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly IUserInProjectFacade _userInProjectFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public List<Types> Types { get; set; }
    public Guid UserId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public TimeSpan EndTime { get; set; }

    public ObservableCollection<UserInProjectListModel> Projects { get; set; } = new();
    
    public UserInProjectListModel? ProjectSelected { get; set; }

    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;

    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        IUserInProjectFacade userInProjectFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _userInProjectFacade = userInProjectFacade;
        _navigationService = navigationService;

        Types = new List<Types>((Types[])Enum.GetValues(typeof(Types)));
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        StartDate = Activity.Start.Date;
        StartTime = new TimeSpan(Activity.Start.TimeOfDay.Nanoseconds);

        EndDate = Activity.End.Date;
        EndTime = new TimeSpan(Activity.End.TimeOfDay.Nanoseconds);

        if (UserId == Guid.Empty)
        {
            UserId = Activity.UserID;
        }
        
        Projects.Clear();
        var projects = await _userInProjectFacade.GetAsyncByUserId(UserId);
        foreach (var project in projects)
        {
            if (project.ProjectID == Activity.ProjectID)
            {
                ProjectSelected = project;
            }
            Projects.Add(project);
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        DateTime startDateTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hours, StartTime.Minutes, StartTime.Seconds);

        DateTime endDateTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hours, EndTime.Minutes, EndTime.Seconds);

        Activity.UserID = UserId;
        
        Activity.Start = startDateTime;
        Activity.End = endDateTime;

        if (ProjectSelected is not null)
        {
            Activity.ProjectID = ProjectSelected.ProjectID;
        }

        try
        {
            bool result = await _activityFacade.ConflictedActivities(Activity.Start, Activity.End, Activity.UserID);
            if (result)
            {
                await _alertService.DisplayAsync("Save Error", "Conflicting activities.");
                return;
            }

            await _activityFacade.SaveAsync(Activity);
        }
        catch (Exception)
        {
            await _alertService.DisplayAsync("Create Error", "Cannot create");
        }

        messengerService.Send(new ActivityEditMessage { ActivityId = Activity.ID });
        _navigationService.SendBackButtonPressed();
    }
}