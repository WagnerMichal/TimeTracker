using CommunityToolkit.Mvvm.Input;
using TimeTracker.App.Messages;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.App.ViewModels;

[QueryProperty(nameof(Project), nameof(Project))]
public partial class UserInProjectEditViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly IUserInProjectFacade _userInProjectFacade;
    private readonly IUserInProjectModelMapper _userInProjectModelMapper;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public ProjectDetailModel? Project { get; set; }
    public ObservableCollection<UserListModel> Users { get; set; } = new();

    public UserListModel? UserSelected { get; set; }

    public UserInProjectDetailModel? UserInProjectNew { get; private set; }

    public UserInProjectEditViewModel(
        IUserFacade userFacade,
        IUserInProjectFacade userInProjectFacade,
        IUserInProjectModelMapper userInProjectModelMapper,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _userInProjectFacade = userInProjectFacade;
        _userInProjectModelMapper = userInProjectModelMapper;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users.Clear();
        var users = await _userFacade.GetAsync();
        foreach (var user in users)
        {
            Users.Add(user);
        }
    }

    [RelayCommand]
    private async Task AddNewUserToProjectAsync()
    {
        if (UserSelected is not null
            && Project is not null)
        {
            try
            {
                UserInProjectNew = UserInProjectDetailModel.Empty;
                UserInProjectNew.UserID = UserSelected.ID;
                UserInProjectNew.ProjectID = Project.ID;
                UserInProjectNew.UserName = UserSelected.Name;
                UserInProjectNew.UserLastName = UserSelected.LastName;

                _userInProjectModelMapper.MapToExistingDetailModel(UserInProjectNew, UserSelected, Project);

                await _userInProjectFacade.SaveAsync(UserInProjectNew, Project.ID);
                Project.Users.Add(_userInProjectModelMapper.MapToListModel(UserInProjectNew));

                messengerService.Send(new ProjectUserAddMessage());
            }

            catch (DbUpdateException) 
            {
                await _alertService.DisplayAsync("Add error", "User already assigned to project");
            }

            _navigationService.SendBackButtonPressed();
        }
    }

    [RelayCommand]
    private async Task UpdateUserAsync(UserInProjectListModel? model)
    {
        if (model is not null
            && Project is not null)
        {
            await _userInProjectFacade.SaveAsync(model, Project.ID);

            messengerService.Send(new ProjectUserEditMessage());
        }
    }

    [RelayCommand]
    private async Task RemoveUserAsync(UserInProjectListModel model)
    {
        if (Project is not null)
        {
            await _userInProjectFacade.DeleteAsync(model.ID);
            Project.Users.Remove(model);

            messengerService.Send(new ProjectUserDeleteMessage());
        }
    }
}