namespace ERP.Desktop.Views.Inventory;

using ERP.Desktop.ViewModels;

public partial class InventoryEditorWindow : MahApps.Metro.Controls.MetroWindow
{
    public InventoryEditorWindow(InventoryEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        Title = viewModel.DialogTitle;

        viewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(InventoryEditorViewModel.Result))
            {
                DialogResult = viewModel.Result is not null;
                Close();
            }
        };
    }
}