using Serilog;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.ViewModels.Common;

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
        private JobStep? _currentJobStep;
        public JobStep? CurrentJobStep
        {
            get { return _currentJobStep; }
            set { if (_currentJobStep != value) { _currentJobStep = value; OnPropertyChanged(nameof(CurrentJobStep)); } }
        }
        private Job? _currentJob;
        public Job? CurrentJob
        {
            get { return _currentJob; }
            set { if (_currentJob != value) { _currentJob = value; OnPropertyChanged(nameof(CurrentJob)); } }
        }
        private readonly JobStepDataService _jobStepDataService;
        private readonly JobDataService _jobDataService;
        #endregion
        #region Constructor
        public ManualTasksCreatorPageViewModel(JobStepDataService jobStepDataService, JobDataService jobDataService)
        {
            _jobStepDataService = jobStepDataService;
            _jobDataService = jobDataService;
            LoadAllJobSteps();
            LoadAllJobs();
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
        #endregion
    }
}
