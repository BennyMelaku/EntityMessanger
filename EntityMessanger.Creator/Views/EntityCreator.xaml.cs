using EntityMessanger.Creator.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace EntityMessanger.Creator.Views
{
    /// <summary>
    /// Interaction logic for EntityCreator.xaml
    /// </summary>
    public partial class EntityCreator : UserControl
    {
        public EntityCreator()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<EntityCreatorViewModel>();
        }
    }
}
