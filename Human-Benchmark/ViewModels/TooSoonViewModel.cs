using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Human_Benchmark.Messages;
using Human_Benchmark.Services;

namespace Human_Benchmark.ViewModels;

public partial class TooSoonViewModel(ViewModelFactory factory) : BaseViewModel
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