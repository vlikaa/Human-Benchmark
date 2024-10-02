using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Human_Benchmark.Messages;
using Human_Benchmark.Services;

namespace Human_Benchmark.ViewModels;

[INotifyPropertyChanged]
public partial class GreenViewModel(ViewModelFactory factory) : BaseViewModel
{
	[RelayCommand]
	private void StopTimer()
	{
		WeakReferenceMessenger.Default.Send(new StopPeriodMessage());
		
		if (++_lap >= 5)
		{
			_lap = 0;
			
			return;
		}

		WeakReferenceMessenger.Default.Send(new ChangeViewModelMessage(factory.Create(typeof(ResultViewModel))));
	}

	private int _lap;
}