using Human_Benchmark.Services;
using Human_Benchmark.Messages;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Human_Benchmark.ViewModels;

[INotifyPropertyChanged]
public partial class StartViewModel(ViewModelFactory factory) : BaseViewModel
{
	[RelayCommand]
	private void OpenRedView()
	{
		WeakReferenceMessenger.Default
			.Send(new ChangeViewModelMessage(factory.Create(typeof(RedViewModel))));
		
		WeakReferenceMessenger.Default
			.Send(new StartTimerMessage());
	}
}