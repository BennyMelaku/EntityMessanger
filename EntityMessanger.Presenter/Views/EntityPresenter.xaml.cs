using EntityMessanger.Presenter.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace EntityMessanger.Presenter.Views
{
    /// <summary>
    /// Interaction logic for EntityPresenter.xaml
    /// </summary>
    public partial class EntityPresenter : UserControl
    {
        public EntityPresenter()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<EntityPresenterViewModel>();
        }
    }
}
