using EntityMessanger.Domain.Models;
using EntityMessanger.Domain.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace EntityMessanger.Presenter.ViewModels
{
    public class EntityPresenterViewModel : ViewModelBase
    {
        private readonly IEntityMessangerService _entityMessangerService;
        private string _name;
        private double _xCoordinate;
        private double _yCoordinate;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public double XCoordinate
        {
            get { return _xCoordinate; }
            set { _xCoordinate = value; OnPropertyChanged(); }
        }
        public double YCoordinate
        {
            get { return _yCoordinate; }
            set { _yCoordinate = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Entity> Entities { get; } = new ObservableCollection<Entity>();

        public EntityPresenterViewModel(IEntityMessangerService entityMessangerService)
        {
            _entityMessangerService = entityMessangerService;
            _entityMessangerService.EntityMessageReceived += (entity) => 
                Application.Current.Dispatcher.Invoke(() => Entities.Add(entity));
        }
    }
}
