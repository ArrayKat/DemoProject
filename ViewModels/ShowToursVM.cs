using Demo2.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2.ViewModels
{
    internal class ShowToursVM:ViewModelBase
    {
        List<Tour> _listPreview;
        public List<Tour> ListPreview { get => _listPreview; set => this.RaiseAndSetIfChanged( ref _listPreview, value); }
        
        Tour _tourChange;
        public Tour TourChange { get => _tourChange; set { this.RaiseAndSetIfChanged(ref _tourChange, value); ChangeTour(); } }

        public void ChangeTour() {
            MainWindowViewModel.Instance.PageContent = new AddChangeTour(TourChange);
        }

        public void AddTour()
        {
            MainWindowViewModel.Instance.PageContent = new AddChangeTour();
        }

        public ShowToursVM() { 
            ListPreview = MainWindowViewModel.myConnection.Tours.Include(x =>x.ToursTypes).ToList();
        }

    }
}
