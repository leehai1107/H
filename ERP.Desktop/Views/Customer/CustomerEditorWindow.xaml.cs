using System.Windows;
using ERP.Desktop.ViewModels;

namespace ERP.Desktop.Views.Customer;

public partial class CustomerEditorWindow : MahApps.Metro.Controls.MetroWindow
{
    public CustomerEditorWindow(CustomerEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        Title = viewModel.DialogTitle;

        viewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(CustomerEditorViewModel.Result))
            {
                DialogResult = viewModel.Result is not null;
                Close();
            }
        };
    }
}