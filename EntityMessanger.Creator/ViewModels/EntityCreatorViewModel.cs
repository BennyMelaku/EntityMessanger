using EntityMessanger.Domain.Models;
using EntityMessanger.Domain.Services;
using System;
using System.Windows;
using Prism.Commands;

namespace EntityMessanger.Creator.ViewModels
{
    public class EntityCreatorViewModel : ViewModelBase
    {
        private readonly IEntityMessangerService _entityMessangerService;
        private string _name;
        private double _xCoordinate;
        private double _yCoordinate;
        private DelegateCommand _sendEntityCommand;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                //If a property of our Name changed, we want to check the CanExecute of the Send Command
                SendEntityCommand.RaiseCanExecuteChanged();
            }
        }
        public double XCoordinate
        {
            get
            {
                return _xCoordinate;
            }
            set
            {
                _xCoordinate = value;
                OnPropertyChanged(nameof(XCoordinate));
                    SendEntityCommand.RaiseCanExecuteChanged();
            }
        }
        public double YCoordinate
        {
            get
            {
                return _yCoordinate;
            }
            set
            {
                _yCoordinate = value;
                OnPropertyChanged(nameof(YCoordinate));
                SendEntityCommand.RaiseCanExecuteChanged();
            }
        }
        public DelegateCommand SendEntityCommand => _sendEntityCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                await _entityMessangerService.SendEntityMessage(new Entity
                {
                    Name = Name,
                    XCoordinate = XCoordinate,
                    YCoordinate = YCoordinate
                });

                (Name, XCoordinate, YCoordinate) = (string.Empty, 0, 0);
                MessageBox.Show("Entity sent successfully!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending entity: {ex.Message}", "Error");
            }
        }, CanSendEntity);

        public EntityCreatorViewModel(IEntityMessangerService entityMessangerService) =>
            _entityMessangerService = entityMessangerService;

        private bool CanSendEntity() =>
            !string.IsNullOrEmpty(Name) 
                && XCoordinate >= 1 
                && XCoordinate <= 850 
                && YCoordinate >= 1 
                && YCoordinate <= 500;
    }
}
