using Serilog;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
using System.Windows.Input;

namespace SimpleFleetManager.ViewModels.Main
{
    public class LocationsManagerPageViewModel : BaseViewModel
    {
        #region Variables
        private Location? _displayedLocation;
        public Location? DisplayedLocation
        {
            get { return _displayedLocation; }
            set { if (_displayedLocation != value) { _displayedLocation = value; OnPropertyChanged(nameof(DisplayedLocation)); } }
        }
        private Location? _newLocation;
        public Location? NewLocation
        {
            get { return _newLocation; }
            set { if (_newLocation != value) { _newLocation = value; OnPropertyChanged(nameof(NewLocation)); } }
        }
        private Forklift? _selectedForklift;
        public Forklift? SelectedForklift
        {
            get { return _selectedForklift; }
            set { if (_selectedForklift != value) { _selectedForklift = value; OnPropertyChanged(nameof(SelectedForklift)); } }
        }
        private IEnumerable<Location>? _allLocations;
        public IEnumerable<Location>? AllLocations
        {
            get { return _allLocations; }
            set { if (_allLocations != value) { _allLocations = value; OnPropertyChanged(nameof(AllLocations)); } }
        }
        private List<Forklift>? _avaibleForklfits;
        public List<Forklift>? AvaibleForklifts
        {
            get { return _avaibleForklfits; }
            set { if (_avaibleForklfits != value) { _avaibleForklfits = value; OnPropertyChanged(nameof(AvaibleForklifts)); } }
        }
        private bool _locationTypeMenuOpened;
        public bool LocationTypeMenuOpened
        {
            get { return _locationTypeMenuOpened; }
            set { if (_locationTypeMenuOpened != value) { _locationTypeMenuOpened = value; OnPropertyChanged(nameof(LocationTypeMenuOpened));} }
        }
        private bool _selectForkliftOpened;
        public bool SelectForkliftOpened
        {
            get { return _selectForkliftOpened; }
            set { if (_selectForkliftOpened != value) { _selectForkliftOpened = value; OnPropertyChanged(nameof(SelectForkliftOpened)); } }
        }
        private List<string>? _avaibleLocationTypes;
        public List<string>? AvaibleLocationTypes
        {
            get { return _avaibleLocationTypes; }
            set { if (_avaibleLocationTypes != value) { _avaibleLocationTypes = value; OnPropertyChanged(nameof(AvaibleLocationTypes)); } }
        }
        private int _selectedLocationType;
        public int SelectedLocationType
        {
            get { return _selectedLocationType; }
            set { if (_selectedLocationType != value) { _selectedLocationType = value; OnPropertyChanged(nameof(SelectedLocationType)); } }
        }
        private readonly LocationDataService? _locationDataService;
        #endregion
        #region Constructor
        public LocationsManagerPageViewModel(LocationDataService locationDataService, List<Forklift> avaibleForklifts)
        {
            _locationDataService = locationDataService;
            _avaibleForklfits = avaibleForklifts;
            DisplayedLocation = new();
            AvaibleLocationTypes = ["Magazine", "Nest", "StandbyPlace"];
            LoadLocations();
            SelectForkliftFromList = new RelayCommand(ExecuteSelectForkliftFromList);
            AddNewLocationButtonClick = new RelayCommand(ExecuteAddNewLocationButtonClick);
            RemoveLocationButtonClick = new RelayCommand(ExecuteRemoveLocationButtonClick);
            UpdateLocationButtonClick = new RelayCommand(ExecuteUpdateLocationButtonClick);
            GetPositionFromForkliftButtonClick = new RelayCommand(ExecuteGetPositionFromForkliftButtonClick);
            SelectExistingLocation = new RelayCommand(ExecuteSelectExistingLocation);

        }
        #endregion
        #region PageLogic
        private async void LoadLocations()
        {
            if (_locationDataService != null)
            {
                AllLocations = await _locationDataService.GetAll();
            }
        }
        private bool LocationExist(Location location)
        {
            LoadLocations();
            if (_allLocations != null)
            {
                foreach (Location loadedLocation in _allLocations)
                {
                    if (loadedLocation.Id == location.Id)
                    {
                        Log.Error("Location data: Location Id already exists!");
                        return true;
                    }
                    if (loadedLocation.Name == location.Name)
                    {
                        Log.Error("Location data: Location Name already exists!");
                        return true;
                    }
                    bool positionExists = (loadedLocation.X == location.X) && (loadedLocation.Y == location.Y) && (loadedLocation.R == location.R);
                    if (positionExists)
                    {
                        Log.Error("Location data: Location with the same coordinates already exists!:" + loadedLocation.Name);
                        return true;
                    }
                }
                Log.Debug("Location data: Location does not exists!");
                return false;
            }
            return true;
        }
        private static bool LocationDataOk(Location location)
        {
            return !string.IsNullOrEmpty(location.Name);
        }
        #endregion
        #region Buttons executions

        private async void ExecuteAddNewLocationButtonClick(object o)
        {
            if (_displayedLocation != null && _locationDataService != null)
            {
                Location newLocation = new()
                {
                    Name = _displayedLocation.Name,
                    Description = _displayedLocation.Description,
                    X = _displayedLocation.X,
                    Y = _displayedLocation.Y,
                    R = _displayedLocation.R,
                    IsActive = _displayedLocation.IsActive,
                };
                if (!LocationExist(newLocation))
                {
                    Log.Debug("Adding new location after check");
                    newLocation.Type = _selectedLocationType;
                    await _locationDataService.Create(newLocation);
                    LoadLocations();
                    DisplayedLocation = new();
                }
            }
        }
        private async void ExecuteRemoveLocationButtonClick(object o)
        {
            bool actionResult = false;
            if (_displayedLocation != null && _locationDataService != null)
            {
                actionResult = await _locationDataService.Delete(_displayedLocation.Id);
                LoadLocations();
            }
            Log.Debug("Delete location result == " + actionResult.ToString());
        }
        private async void ExecuteUpdateLocationButtonClick(object o)
        {
            LoadLocations();
            if (_displayedLocation != null && _locationDataService != null && _allLocations != null)
            {
                foreach (Location location in _allLocations)
                {
                    if (location.Id == _displayedLocation.Id && LocationDataOk(_displayedLocation))
                    {
                        _displayedLocation.Type = _selectedLocationType;
                        if (_displayedLocation == await _locationDataService.Update(_displayedLocation.Id, _displayedLocation))
                        {
                            Log.Debug("Update location data succeed");
                            LoadLocations();
                            break;
                        }
                        else
                        {
                            Log.Warning("Error while updating location data!");
                        }
                    }
                }
            }
        }
        private void ExecuteGetPositionFromForkliftButtonClick(object o)
        {
            try
            {
                DisplayedLocation ??= new();
                if (_selectedForklift != null && _selectedForklift != null && _selectedForklift.DataOut != null && _selectedForklift.DataOut.ActualPosition != null)
                {
                    Location newLocation = new()
                    {
                        Name = DisplayedLocation.Name,
                        Description = DisplayedLocation.Description,
                        X = _selectedForklift.DataOut.ActualPosition.X,
                        Y = _selectedForklift.DataOut.ActualPosition.Y,
                        R = _selectedForklift.DataOut.ActualPosition.R,
                        IsActive = DisplayedLocation.IsActive,
                    };
                    DisplayedLocation = newLocation;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while trying to get current forklift position data: " + ex.Message);
            }
        }
        private void ExecuteSelectForkliftFromList(object o)
        {
            try
            {
                if (_avaibleForklfits != null)
                {
                    foreach (Forklift forklift in _avaibleForklfits)
                    {
                        if (forklift.Id == Convert.ToInt32(o))
                        {
                            SelectedForklift = forklift;
                            SelectForkliftOpened = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while trying to select forklift from list: " + ex.Message);
            }
        }
        private async void ExecuteSelectExistingLocation(object o)
        {
            try
            {
                LoadLocations();
                if (_locationDataService != null)
                {
                    DisplayedLocation = await _locationDataService.Get(Convert.ToInt32(o));
                    SelectedLocationType = DisplayedLocation.Type;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while trying to select existing location: " + ex.Message);
            }
        }
        #endregion
        #region ICommand decalrations
        public ICommand? AddNewLocationButtonClick { get; private set; }
        public ICommand? RemoveLocationButtonClick { get; private set; }
        public ICommand? UpdateLocationButtonClick { get; private set; }
        public ICommand? GetPositionFromForkliftButtonClick { get; private set; }
        public ICommand? SelectForkliftFromList { get; private set; }
        public ICommand? SelectExistingLocation {  get; private set; }
        #endregion
    }
}
