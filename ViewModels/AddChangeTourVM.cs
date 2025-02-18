using Demo2.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Demo2.ViewModels
{
    internal class AddChangeTourVM:ViewModelBase
    {
        //всееееее типы туров из бд
        List<Models.Type> _typesAll ;
        public List<Models.Type> TypesAll { get => _typesAll; set => this.RaiseAndSetIfChanged(ref _typesAll, value); }
        
        //текущий тур, который подлежит изменению/добавлению/удалению
        Tour _curentTour;
        public Tour CurentTour { get => _curentTour; set => this.RaiseAndSetIfChanged(ref _curentTour, value); }

        //свойство смежной таблицы многое ко многим
        List<ToursType>? _toursType;
        public List<ToursType>? ToursType { get => _toursType; set => this.RaiseAndSetIfChanged(ref _toursType, value); }

        //выпадающий список с исключенными типами туров комбобокс
        public List<Models.Type> Types => MainWindowViewModel.myConnection.Types.ToList().Except(_curentTour.ToursTypes.Select(x => x.Type)).ToList();

        //свойство при выборе какого либо типа тура в выпадающем списке
        Models.Type _selectedType;
        public Models.Type SelectedType
        {
            get => null;
            set
            {
                if (value != null)
                {
                    CurentTour.ToursTypes.Add(new ToursType { Tour = CurentTour, Type = value });
                    ToursType = CurentTour.ToursTypes.ToList();
                    this.RaisePropertyChanged(nameof(Types));
                }
            }
        }

        public async void DeleteType(ToursType deleteType) {
            ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Хотите удалить тип тура?", ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes) { 
                CurentTour.ToursTypes.Remove(deleteType);
                ToursType = CurentTour.ToursTypes.ToList();
                MessageBoxManager.GetMessageBoxStandard("Сообщение", "Тип тура успешно удален", ButtonEnum.Ok).ShowAsync();
            }
            this.RaisePropertyChanged(nameof(Types));
        }

        public AddChangeTourVM(Tour changeTour) {
            TypesAll = MainWindowViewModel.myConnection.Types.ToList();
            ToursType = changeTour.ToursTypes.ToList();
            CurentTour = changeTour;
        }
        public AddChangeTourVM()
        {
            TypesAll = MainWindowViewModel.myConnection.Types.ToList();
            CurentTour = new Tour();
        }

        public void GoBack() {
            MainWindowViewModel.Instance.PageContent = new ShowTours();
        }
        public async void DeleteTour()
        {
            ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Хотите удалить тур?", ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes) {
                List<ToursType> deleteType = MainWindowViewModel.myConnection.ToursTypes.Where(t => t.Tour == CurentTour).ToList();
                MainWindowViewModel.myConnection.ToursTypes.RemoveRange(deleteType);
                MainWindowViewModel.myConnection.Tours.Remove(CurentTour);
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new ShowTours();
                MessageBoxManager.GetMessageBoxStandard("Сообщение", "Тур успешно удален", ButtonEnum.Ok).ShowAsync() ;
            }
        }
        public async void SaveChange() {
            ButtonResult res = await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Хотите сохранить изменения?", ButtonEnum.YesNo).ShowAsync();
            if (res == ButtonResult.Yes)
            {
                if (CurentTour.Id == 0)
                { //add
                    MainWindowViewModel.myConnection.Tours.Add(CurentTour);
                    MainWindowViewModel.myConnection.SaveChanges();
                    MainWindowViewModel.Instance.PageContent = new ShowTours();
                    MessageBoxManager.GetMessageBoxStandard("Сообщение", "Тур успешно добавлен", ButtonEnum.Ok).ShowAsync();
                }
                else
                { //change
                    MainWindowViewModel.myConnection.Update(CurentTour);
                    MainWindowViewModel.myConnection.SaveChanges();
                    MainWindowViewModel.Instance.PageContent = new ShowTours();
                    MessageBoxManager.GetMessageBoxStandard("Сообщение", "Тур успешно изменен", ButtonEnum.Ok).ShowAsync();
                }
            }
            else {
                MainWindowViewModel.Instance.PageContent = new ShowTours();
            }
        }
    }
}
