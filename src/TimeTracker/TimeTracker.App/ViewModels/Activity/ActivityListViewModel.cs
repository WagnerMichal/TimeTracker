using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.App.Services;
using System.Collections.Generic;

namespace TimeTracker.App.ViewModels;

public partial class ActivityListViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private readonly System.Timers.Timer _timer = new System.Timers.Timer();

    public IEnumerable<ActivityListModel> allActivities { get; set; } = null!;
    public IEnumerable<ActivityListModel> _filteredActivities  = null!;
    private readonly Dictionary<int, string> _userNames = new Dictionary<int, string>();

    private bool _isFiltering;

    public IEnumerable<ActivityListModel> FilteredActivities
    {
        get => _filteredActivities ?? allActivities;
        set => SetProperty(ref _filteredActivities, value);
    }

    private DateTime _currentDateTime = DateTime.Now;
    public DateTime CurrentDateTime
    {
        get => _currentDateTime;
        set => SetProperty(ref _currentDateTime, value);
    }

    private DateTime? _startDateFilter;
    public DateTime? StartDateFilter
    {
        get => _startDateFilter;
        set
        {
            SetProperty(ref _startDateFilter, value);
        }
    }

    private DateTime? _endDateFilter;
    public DateTime? EndDateFilter
    {
        get => _endDateFilter;
        set
        {
            SetProperty(ref _endDateFilter, value);

        }
    }

    private DateTime? _last7endDateFilter;
    public DateTime? Last7EndDateFilter
    {
        get => _last7endDateFilter;
        set
        {
            SetProperty(ref _last7endDateFilter, value);

        }
    }

    private DateTime? _last30endDateFilter;
    public DateTime? Last30EndDateFilter
    {
        get => _last30endDateFilter;
        set
        {
            SetProperty(ref _last30endDateFilter, value);

        }
    }

    private DateTime? _previousMonthendDateFilter;
    public DateTime? PreviousMonthendDateFilter
    {
        get => _previousMonthendDateFilter;
        set
        {
            SetProperty(ref _previousMonthendDateFilter, value);

        }
    }

    private DateTime? _lastYearendDateFilter;
    public DateTime? LastYearendDateFilter
    {
        get => _lastYearendDateFilter;
        set
        {
            SetProperty(ref _lastYearendDateFilter, value);

        }
    }

    private DateTime? _thisMonthendDateFilter;
    public DateTime? ThisMonthendDateFilter
    {
        get => _thisMonthendDateFilter;
        set
        {
            SetProperty(ref _thisMonthendDateFilter, value);

        }
    }

    private DateTime? _thisDayendDateFilter;
    public DateTime? ThisDayendDateFilter
    {
        get => _thisDayendDateFilter;
        set
        {
            SetProperty(ref _thisDayendDateFilter, value);

        }
    }

    private DateTime? _thisYearendDateFilter;
    public DateTime? ThisYearendDateFilter
    {
        get => _thisYearendDateFilter;
        set
        {
            SetProperty(ref _thisYearendDateFilter, value);

        }
    }

    public RelayCommand ClearFiltersCommand { get; private set; }
    public RelayCommand ApplyFiltersCommand { get; private set; }
    public RelayCommand ShowFiltersCommand { get; }
    public RelayCommand Last7DaysCommand { get; }
    public RelayCommand Last30DaysCommand { get; }
    public RelayCommand PreviousMonthCommand { get; }
    public RelayCommand LastYearCommand { get; }
    public RelayCommand ThisMonthCommand { get; }
    public RelayCommand ThisDayCommand { get; }
    public RelayCommand ThisYearCommand { get; }

    public bool IsFiltering
    {
        get => _isFiltering;
        set => SetProperty(ref _isFiltering, value);
    }

    public ActivityListViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService
        )
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;

        _startDateFilter = null;
        _endDateFilter = null;
        _last7endDateFilter = null;
        _last30endDateFilter = null;
        _previousMonthendDateFilter = null;
        _lastYearendDateFilter = null;

        ClearFiltersCommand = new RelayCommand(ClearFilters);
        ApplyFiltersCommand = new RelayCommand(ApplyFilters);
        ShowFiltersCommand = new RelayCommand(ShowFilters);

        Last7DaysCommand = new RelayCommand(Last7Days);
        Last30DaysCommand = new RelayCommand(Last30Days);
        PreviousMonthCommand = new RelayCommand(PreviousMonth);
        LastYearCommand = new RelayCommand(LastYear);
        ThisMonthCommand = new RelayCommand(ThisMonth);
        ThisDayCommand = new RelayCommand(ThisDay);
        ThisYearCommand = new RelayCommand(ThisYear);

        _timer.Interval = 1000; // interval is in milliseconds
        _timer.Elapsed += Timer_Elapsed;
        _timer.Start();
    }

    private void Last7DayFilter()
    {
        var startDate = DateTime.Today.AddDays(-6);
        var endDate = DateTime.Today.AddDays(1).AddSeconds(-1);

        StartDateFilter = startDate;
        Last7EndDateFilter = endDate;

        _filteredActivities = allActivities
            .Where(a => a.Start >= startDate && a.Start <= endDate);

        OnPropertyChanged(nameof(FilteredActivities));

        _isFiltering = false;
    }

    private void Last30DayFilter()
    {
        var startDate = DateTime.Today.AddDays(-29);
        var endDate = DateTime.Today.AddDays(1).AddSeconds(-1);

        StartDateFilter = startDate;
        Last30EndDateFilter = endDate;

        _filteredActivities = allActivities
            .Where(a => a.Start >= startDate && a.Start <= endDate);

        OnPropertyChanged(nameof(FilteredActivities));

        _isFiltering = false;
    }

    private void PreviousMonthFilter()
    {
        var today = DateTime.Today;
        var startDate = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
        var endDate = new DateTime(today.Year, today.Month, 1).AddDays(-1);

        StartDateFilter = startDate;
        PreviousMonthendDateFilter = endDate;

        _filteredActivities = allActivities
            .Where(a => a.Start >= startDate && a.Start <= endDate);

        OnPropertyChanged(nameof(FilteredActivities));

        _isFiltering = false;
    }

    private void ThisMonthFilter()
    {
        var today = DateTime.Today;
        var startDate = new DateTime(today.Year, today.Month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        StartDateFilter = startDate;
        EndDateFilter = endDate;

        _filteredActivities = allActivities
            .Where(a => a.Start >= startDate && a.Start <= endDate);

        OnPropertyChanged(nameof(FilteredActivities));

        _isFiltering = false;
    }

    private void LastYearFilter()
    {
        var today = DateTime.Today;
        var startDate = new DateTime(today.Year - 1, 1, 1);
        var endDate = new DateTime(today.Year - 1, 12, 31);

        StartDateFilter = startDate;
        EndDateFilter = endDate;

        _filteredActivities = allActivities
            .Where(a => a.Start >= startDate && a.Start <= endDate);

        OnPropertyChanged(nameof(FilteredActivities));

        _isFiltering = false;
    }

    private void ThisDayFilter()
    {
        var today = DateTime.Today;
        var startDate = today;
        var endDate = today.AddDays(1).AddSeconds(-1);

        StartDateFilter = startDate;
        EndDateFilter = endDate;

        _filteredActivities = allActivities
            .Where(a => a.Start >= startDate && a.Start <= endDate);

        OnPropertyChanged(nameof(FilteredActivities));

        _isFiltering = false;
    }

    private void ThisYearFilter()
    {
        var today = DateTime.Today;
        var startDate = new DateTime(today.Year, 1, 1);
        var endDate = new DateTime(today.Year, 12, 31);

        StartDateFilter = startDate;
        EndDateFilter = endDate;

        _filteredActivities = allActivities
            .Where(a => a.Start >= startDate && a.Start <= endDate);

        OnPropertyChanged(nameof(FilteredActivities));

        _isFiltering = false;
    }

    private void ApplyFilter()
    {
        if (!_isFiltering)
        {
            return;
        }

        if (StartDateFilter == null && EndDateFilter == null)
        {
            _filteredActivities = allActivities;
        }
        else
        {
            _filteredActivities = allActivities
                .Where(a => StartDateFilter == null || a.Start >= StartDateFilter)
                .Where(a => EndDateFilter == null || a.End <= EndDateFilter);
        }

        OnPropertyChanged(nameof(FilteredActivities));

        _isFiltering = false;
    }

    private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        CurrentDateTime = DateTime.Now;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        allActivities = await _activityFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }

    private void ClearFilters()
    {
        StartDateFilter = null;
        EndDateFilter = null;

        _isFiltering = true;
        ApplyFilter();
    }

    private void ApplyFilters()
    {
        _isFiltering = true;
        ApplyFilter();
    }

    private void ShowFilters()
    {
        IsFiltering = !IsFiltering;
    }

    private void Last7Days()
    {
        Last7DayFilter();
    }

    private void Last30Days()
    {
        Last30DayFilter();
    }

    private void PreviousMonth()
    {
        PreviousMonthFilter();
    }

    private void LastYear()
    {
        LastYearFilter();
    }

    private void ThisMonth()
    {
        ThisMonthFilter();
    }

    private void ThisDay()
    {
        ThisDayFilter();
    }

    private void ThisYear()
    {
        ThisYearFilter();
    }
}