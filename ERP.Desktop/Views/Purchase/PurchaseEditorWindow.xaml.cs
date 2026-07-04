using ERP.Desktop.ViewModels;

namespace ERP.Desktop.Views.Purchase;

public partial class PurchaseEditorWindow : MahApps.Metro.Controls.MetroWindow
{
    public PurchaseEditorWindow(PurchaseEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        Title = viewModel.DialogTitle;

        viewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(PurchaseEditorViewModel.Result))
            {
                DialogResult = viewModel.Result is not null;
                Close();
            }
        };
    }
}
