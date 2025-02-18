using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Demo2.ViewModels;

namespace Demo2;

public partial class ShowTours : UserControl
{

    public ShowTours()
    {
        InitializeComponent();
        DataContext = new ShowToursVM();
    }
}