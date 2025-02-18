using Avalonia.Controls;
using Demo2.Models;
using ReactiveUI;

namespace Demo2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance;
        public MainWindowViewModel() { Instance = this; }

        UserControl _pageContent = new ShowTours();

        public UserControl PageContent { get => _pageContent; set => this.RaiseAndSetIfChanged(ref _pageContent, value); }

        public static ToursContext myConnection = new ToursContext();
    }
}
