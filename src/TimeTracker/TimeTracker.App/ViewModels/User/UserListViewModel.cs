﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels;

public partial class UserListViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<UserListModel> Users { get; set; } = null!;

    public UserListViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users = await _userFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync("//users/edit");
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    =>
        await _navigationService.GoToAsync<UserDetailViewModel>(
            new Dictionary<string, object?> { [nameof(UserDetailViewModel.Id)] = id });
  

    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
