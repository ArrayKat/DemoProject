using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Demo2.Models;
using Demo2.ViewModels;

namespace Demo2;

public partial class AddChangeTour : UserControl
{
    public AddChangeTour(Tour changeTour)
    {
        InitializeComponent();
        DataContext = new AddChangeTourVM(changeTour);
    }
    public AddChangeTour()
    {
        InitializeComponent();
        DataContext = new AddChangeTourVM();
    }
}