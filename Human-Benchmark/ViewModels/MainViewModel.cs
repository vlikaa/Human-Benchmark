using Human_Benchmark.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Human_Benchmark.Messages;

namespace Human_Benchmark.ViewModels;

[INotifyPropertyChanged]
public partial class MainViewModel : BaseViewModel
{
	public MainViewModel(ViewModelFactory factory)
	{
		_viewModel = factory.Create(typeof(StartViewModel));
		
		WeakReferenceMessenger.Default.Register<ChangeViewModelMessage>(this,
			(_, message) =>
			{
				ViewModel = message.ViewModel;
			});
	}
	[ObservableProperty] private BaseViewModel _viewModel;
}