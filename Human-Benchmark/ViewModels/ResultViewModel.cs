using Human_Benchmark.Messages;
using Human_Benchmark.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Human_Benchmark.ViewModels;

[INotifyPropertyChanged]
public partial class ResultViewModel : BaseViewModel
{
	private readonly ViewModelFactory _factory;
	
	public ResultViewModel(ViewModelFactory factory)
	{
		_factory = factory;
		
		WeakReferenceMessenger.Default.Register<PassIntervalMessage>(this,
			(_, message) =>
			{
				_intervalString = message.IntervalString;
			});
	}
	
	[RelayCommand]
	private void OpenRedView()
	{
		WeakReferenceMessenger.Default
			.Send(new ChangeViewModelMessage(_factory.Create(typeof(RedViewModel))));
		
		WeakReferenceMessenger.Default
			.Send(new StartTimerMessage());
	}
	
	[ObservableProperty] private string _intervalString = string.Empty;
}