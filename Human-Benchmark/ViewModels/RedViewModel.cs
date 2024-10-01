using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Human_Benchmark.Messages;
using Human_Benchmark.Services;

namespace Human_Benchmark.ViewModels;

[INotifyPropertyChanged]
public partial class RedViewModel : BaseViewModel
{
	public RedViewModel(ViewModelFactory factory)
	{
		WeakReferenceMessenger.Default.Register<StartTimerMessage>(this,
			(_, _) =>
			{
				_timer.Interval = _random.Next(1500, 5000);
				_timer.Elapsed += (_, _) =>
				{
					WeakReferenceMessenger.Default
						.Send(new ChangeViewModelMessage(factory.Create(typeof(GreenViewModel))));
				};
				
				_timer.Start();
			});
	}
	
	private readonly Random _random = new();
	private readonly System.Timers.Timer _timer = new()
	{
		AutoReset = false
	};
	
}