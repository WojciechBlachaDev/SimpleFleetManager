using Serilog;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;
using System.Windows.Input;

namespace SimpleFleetManager.ViewModels.Main
{
    public class ManualTasksCreatorPageViewModel : BaseViewModel
    {
        #region Variables
        private List<JobStep>? _selectedJobSteps;
        public List<JobStep>? SelectedJobSteps
        {
            get { return _selectedJobSteps; }
            set { if (_selectedJobSteps != value) { _selectedJobSteps = value; OnPropertyChanged(nameof(SelectedJobSteps)); } }
        }
        private IEnumerable<JobStep>? _avaibleJobSteps;
        public IEnumerable<JobStep>? AvaibleJobStep
        {
            get { return _avaibleJobSteps; }
            set { if (_avaibleJobSteps != value) { _avaibleJobSteps = value; OnPropertyChanged(nameof(AvaibleJobStep)); } }
        }
        private IEnumerable<Job>? _avaibleJobs;
        public IEnumerable<Job>? AvaibleJobs
        {
            get { return _avaibleJobs; }
            set { if (_avaibleJobs != value) { _avaibleJobs = value; OnPropertyChanged(nameof(AvaibleJobs)); } }
        }
        private List<JobStep>? _unassignedSteps;
        public List<JobStep>? UnassignedSteps
        {
            get { return _unassignedSteps; }
            set { _unassignedSteps = value; OnPropertyChanged(nameof(UnassignedSteps)); }
        }
        private List<JobStep>? _assignedSteps;
        public List<JobStep>? AssignedSteps
        {
            get { return _assignedSteps; }
            set { _assignedSteps = value; OnPropertyChanged(nameof(AssignedSteps)); }
        }
        private List<Location>? _avaibleLocations;
        public List<Location>? AvaibleLocations
        {
            get { return _avaibleLocations; }
            set { if (_avaibleLocations != value) { _avaibleLocations = value; OnPropertyChanged(nameof(AvaibleLocations)); } }
        }
        private List<string>? _jobStepTypes;
        public List<string>? JobStepTypes
        {
            get { return _jobStepTypes; }
            set { _jobStepTypes = value; OnPropertyChanged(nameof(JobStepTypes)); }
        }
        private List<JobStep>? _currentJobOrderedSteps;
        public List<JobStep>? CurrentJobOrderedSteps
        {
            get { return _currentJobOrderedSteps; }
            set { _currentJobOrderedSteps = value; OnPropertyChanged(nameof(CurrentJobOrderedSteps)); }
        }
        private List<string>? _priorityLevels;
        public List<string>? PriorityLevels
        {
            get { return _priorityLevels; }
            set { _priorityLevels = value; OnPropertyChanged(nameof(PriorityLevels)); }
        }
        private JobStep? _currentJobStep;
        public JobStep? CurrentJobStep
        {
            get { return _currentJobStep; }
            set { _currentJobStep = value; OnPropertyChanged(nameof(CurrentJobStep)); }
        }
        private Job? _currentJob;
        public Job? CurrentJob
        {
            get { return _currentJob; }
            set { if (_currentJob != value) { _currentJob = value; OnPropertyChanged(nameof(CurrentJob)); } }
        }
        private Location? _selectedLocation;
        public Location? SelectedLocation
        {
            get { return _selectedLocation; }
            set { if (_selectedLocation != value) { _selectedLocation = value; OnPropertyChanged(nameof(SelectedLocation)); } }
        }
        private bool _locationComboBoxOppened;
        public bool LocationComboBoxOppened
        {
            get { return _locationComboBoxOppened; }
            set { _locationComboBoxOppened = value; OnPropertyChanged(nameof(LocationComboBoxOppened)); }
        }
        private int _selectedJobStepType;
        public int SelectedJobSteptype
        {
            get { return _selectedJobStepType; }
            set { if (_selectedJobStepType != value) { _selectedJobStepType = value; OnPropertyChanged(nameof(SelectedJobSteptype)); } }
        }
        private int _selectedPriority;
        public int SelectedPriority
        {
            get { return _selectedPriority; }
            set { if (_selectedPriority != value) { _selectedPriority = value; OnPropertyChanged(nameof(SelectedPriority)); } }
        }
        private readonly JobStepDataService _jobStepDataService;
        private readonly JobDataService _jobDataService;
        private readonly LocationDataService _locationDataService;
        #endregion
        #region Constructor
        public ManualTasksCreatorPageViewModel(JobStepDataService jobStepDataService, JobDataService jobDataService, LocationDataService locationDataService)
        {
            CurrentJob = new();
            _jobStepDataService = jobStepDataService;
            _jobDataService = jobDataService;
            _locationDataService = locationDataService;
            LoadAllJobSteps();
            LoadAllJobs();
            LoadAllLocations();
            SortJobStepsByAssign();
            PriorityLevels = ["Very Low", "Low", "Normal", "High", "Very High"];
            JobStepTypes = ["Point to point ride", "Charging", "Get palette from magazine", "Drop palette in magazine", "Get palette from nest", "Leave palette at nest"];
            AddJobStepButtonClick = new RelayCommand(ExecuteAddJobStepButtonClick);
            UpdateJobStepButtonClick = new RelayCommand(ExecuteUpdateJobStepButtonClick);
            DeleteJobStepButtonClick = new RelayCommand(ExecuteDeleteJobStepButtonClick);
            SelectJobStepFromList = new RelayCommand(ExecuteSelectJobStepFromList);
            SelectLocationFromList = new RelayCommand(ExecuteSelectLocationFromList);
            AddJobButtonClick = new RelayCommand(ExecuteAddJobButtonClick);
            UpdateJobButtonClick = new RelayCommand(ExecuteUpdateJobButtonClick);
            DeleteJobButtonClick = new RelayCommand(ExecuteDeleteJobButtonClick);
            SelectJobFromList = new RelayCommand(ExecuteSelectJobFromList);
            AssignJobStep = new RelayCommand(ExecuteAssignJobStep);
            UnassignJobStep = new RelayCommand(ExecuteUnassignJobStep);
        }
        #endregion
        #region Page logic
        private async void LoadAllJobSteps()
        {
            try
            {
                AvaibleJobStep = await _jobStepDataService.GetAll();
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to load all job steps: " + ex.Message);
            }
        }
        private async void LoadAllJobs()
        {
            try
            {
                AvaibleJobs = await _jobDataService.GetAll();
            }
            catch (Exception ex)
            {
                Log.Error("Error occured when tying to load all jobs: " + ex.Message);
            }
        }
        private async void LoadAllLocations()
        {
            try
            {
                AvaibleLocations ??= [];
                IEnumerable<Location>? tmpLocations = await _locationDataService.GetAll();
                foreach (Location location in tmpLocations)
                {
                    if (location.IsActive)
                    {
                        AvaibleLocations.Add(location);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error occured when trying to load all avaible locations: " + ex.Message);
            }
        }
        private void SortJobStepsByAssign()
        {
            try
            {
                if (_avaibleJobSteps != null)
                {
                    AssignedSteps = [];
                    UnassignedSteps = [];
                    List<JobStep>? tmpAssignedSteps = [];
                    List<JobStep>? tmpUnassignedSteps = [];
                    foreach (JobStep step in _avaibleJobSteps)
                    {
                        if (!step.IsAssigned)
                        {
                            tmpUnassignedSteps.Add(step);
                        }
                        else
                        {
                            if (_currentJob != null && step.JobId == _currentJob.Id)
                            {
                                tmpAssignedSteps.Add(step);
                            }
                        }
                    }
                    AssignedSteps = tmpAssignedSteps;
                    UnassignedSteps = tmpUnassignedSteps;
                    AssignedSteps.Reverse();
                    UnassignedSteps.Reverse();
                }
            }
            catch (Exception ex) { Log.Error("Error occured while setting unassigned job steps list: " + ex.Message); }
        }
        private static bool IsJobStepEditable(JobStep step)
        {
            return (!step.IsAssigned);
        }
        private async void RefreshStepsList()
        {
            List<JobStep>? tmpSteps = [];
            if (_currentJob != null && _currentJob.JobSteps != null)
            {
                foreach (int stepId in _currentJob.JobSteps)
                {
                    tmpSteps.Add(await _jobStepDataService.Get(stepId));
                }
            }
            CurrentJobOrderedSteps = tmpSteps;
        }
        #endregion
        #region Buttons executions
        private async void ExecuteAddJobStepButtonClick(object o)
        {
            try
            {
                if (_currentJobStep != null && _selectedLocation != null)
                {
                    JobStep newStep = new()
                    {
                        Name = _currentJobStep.Name,
                        Type = _selectedJobStepType,
                        LocX = _selectedLocation.X,
                        LocY = _selectedLocation.Y,
                        LocR = _selectedLocation.R,
                        LocationId = _selectedLocation.Id,
                    };
                    if (newStep == null) { Log.Warning("New created step is null!"); }
                    else if (newStep.Type < 1 || newStep.Type > 6) { Log.Warning("New created step has wrong type!"); }
                    else
                    {
                        _ = await _jobStepDataService.Create(newStep);
                        LoadAllJobSteps();
                        CurrentJobStep = new();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error catched while creating new job step: " + ex.Message);
            }
        }
        private async void ExecuteUpdateJobStepButtonClick(object o)
        {
            try
            {
                if (_currentJobStep != null && IsJobStepEditable(_currentJobStep))
                {
                    CurrentJobStep = await _jobStepDataService.Update(_currentJobStep.Id, _currentJobStep);
                    LoadAllJobSteps();
                }
            }
            catch (Exception ex) { Log.Error("Error catched while updating job step: " + ex.Message); }
        }
        private async void ExecuteDeleteJobStepButtonClick(object o)
        {
            try
            {
                if (_currentJobStep != null && IsJobStepEditable(_currentJobStep))
                {
                    bool result;
                    result = await _jobStepDataService.Delete(_currentJobStep.Id);
                    if (result) { Log.Debug("Job step delete SUCCESS"); LoadAllJobSteps(); }
                    else { Log.Warning("Job step delete FAILED"); }
                }
            }
            catch (Exception ex) { Log.Error("Error catched while deleting job step: " + ex.Message); }
        }
        private void ExecuteSelectJobStepFromList(object o)
        {
            try
            {
                if (_avaibleJobSteps != null)
                {
                    foreach (JobStep step in _avaibleJobSteps)
                    {
                        if (step.Id == Convert.ToInt32(o)) { CurrentJobStep = step; SelectedJobSteptype = step.Type; break; }
                    }
                }
            }
            catch (Exception ex) { Log.Error("Error catched while selecting job step from list: " + ex.Message); }
        }
        private async void ExecuteSelectLocationFromList(object o)
        {
            try
            {
                SelectedLocation = await _locationDataService.Get(Convert.ToInt32(o));
                if (_selectedLocation != null)
                {
                    CurrentJobStep ??= new();
                    JobStep tmpStep = CurrentJobStep;
                    tmpStep.LocX = _selectedLocation.X;
                    tmpStep.LocY = _selectedLocation.Y;
                    tmpStep.LocR = _selectedLocation.R;
                    CurrentJobStep = tmpStep;
                    LocationComboBoxOppened = false;
                }
            }
            catch (Exception ex) { Log.Error("Error catched while selecting location from list: " + ex.Message); }
        }
        private async void ExecuteAddJobButtonClick(object o)
        {
            try
            {
                if(_currentJob != null)
                {
                    Job newJob = new()
                    {
                        Name = _currentJob.Name,
                        PriorityLevel = _selectedPriority,
                        JobSteps = [],
                        CurrentJobStep = -1
                    };
                    _ = await _jobDataService.Create(newJob);
                    CurrentJob = new();
                    LoadAllJobs();
                }
            }
            catch (Exception ex) { Log.Error("Error occured while trying to add new Job: " + ex.Message); }
        }
        private async void ExecuteUpdateJobButtonClick(object o)
        {
            try
            {
                if (_currentJob != null && CurrentJob != null)
                {
                    CurrentJob.PriorityLevel = _selectedPriority;
                    _ = await _jobDataService.Update(_currentJob.Id, _currentJob);
                    LoadAllJobs();
                }
            }
            catch (Exception ex) { Log.Error("Error occured while trying to update Job: " + ex.Message); }
        }
        private async void ExecuteDeleteJobButtonClick(object o)
        {
            try
            {
                if (_currentJob != null)
                {
                    _ = await _jobDataService.Delete(_currentJob.Id);
                    CurrentJob = new();
                    LoadAllJobs();
                }

            }
            catch (Exception ex) { Log.Error("Error occured while trying to delete Job: " + ex.Message); }
        }
        private async void ExecuteSelectJobFromList(object o)
        {
            try
            {
                List<JobStep>? tmpList = [];
                CurrentJob = await _jobDataService.Get(Convert.ToInt32(o));
                SelectedPriority = CurrentJob.PriorityLevel;
                RefreshStepsList();

            }
            catch (Exception ex) { Log.Error("Error occured while trying to select Job: " + ex.Message); }
        }
        private async void ExecuteAssignJobStep(object o)
        {
            try
            {
                if (_currentJob != null && CurrentJob != null)
                {
                    JobStep step = await _jobStepDataService.Get(Convert.ToInt32(o));
                    step.IsAssigned = true;
                    step.JobId = _currentJob.Id;
                    CurrentJob.JobSteps.Add(step.Id);
                    _ = await _jobStepDataService.Update(step.Id, step);
                    _ = await _jobDataService.Update(_currentJob.Id, _currentJob);
                    LoadAllJobs();
                    LoadAllJobSteps();
                    RefreshStepsList();
                }
            }
            catch (Exception ex) { Log.Error("Errro occured while assigning job step to jobs: " + ex.Message); }
        }
        private async void ExecuteUnassignJobStep(object o)
        {
            try
            {
                if (_currentJob != null && CurrentJob != null)
                {
                    JobStep step = await _jobStepDataService.Get(Convert.ToInt32(o));
                    step.IsAssigned = false;
                    step.JobId = -1;
                    foreach (int jobId in _currentJob.JobSteps)
                    {
                        if (jobId > step.Id)
                        {
                            CurrentJob.JobSteps.RemoveAt(jobId);
                            break;
                        }
                    }
                    _ = await _jobStepDataService.Update(step.Id, step);
                    _ = await _jobDataService.Update(_currentJob.Id, _currentJob);
                    LoadAllJobs();
                    LoadAllJobSteps();
                    RefreshStepsList();
                    SortJobStepsByAssign();
                }
            }
            catch (Exception ex) { Log.Error("Errro occured while unassigning job step to jobs: " + ex.Message); }
        }
        #endregion
        #region ICommand declarations
        public ICommand? AddJobStepButtonClick { get; private set; }
        public ICommand? UpdateJobStepButtonClick { get; private set; }
        public ICommand? DeleteJobStepButtonClick { get; private set; }
        public ICommand? SelectJobStepFromList { get; private set; }
        public ICommand? SelectLocationFromList { get; private set; }
        public ICommand? AddJobButtonClick { get; private set; }
        public ICommand? UpdateJobButtonClick { get; private set; }
        public ICommand? DeleteJobButtonClick { get; private set; }
        public ICommand? SelectJobFromList { get; private set; }
        public ICommand? AssignJobStep { get; private set; }
        public ICommand? UnassignJobStep { get; private set; }
        #endregion
    }
}
