namespace MyMoney.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel viewModel;
        private string viewName;
        private RelayCommand openPurse;
        public MainViewModel()
        {
            ViewName = "Кошелёк";
            ViewModel = new StartViewModel();
        }
        public RelayCommand OpenPurse => openPurse ??
                 (openPurse = new RelayCommand(obj =>
                 {
                     if (ViewName == "Кошелёк")
                     {
                         ViewName = "Главная";
                         ViewModel = new PurseViewModel();
                     }
                     else
                     {
                         ViewName = "Кошелёк";
                         ViewModel = new StartViewModel();
                     }
                 }));
        public BaseViewModel ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; OnPropertyChanged(); }
        }
        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; OnPropertyChanged(); }
        }
    }
}
