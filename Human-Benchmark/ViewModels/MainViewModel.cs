using System.Windows;
using Human_Benchmark.Views;
using Human_Benchmark.Services;
using Human_Benchmark.Messages;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;

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
		
		WeakReferenceMessenger.Default.Register<StartPeriodMessage>(this,
			(_, message) =>
			{
				_startTime = message.StartPeriodTime;
			});
		
		WeakReferenceMessenger.Default.Register<StopPeriodMessage>(this,
			(_, message) =>
			{
				var stopTime = message.StopPeriodTime;

				var reactionTime = (stopTime - _startTime).TotalMilliseconds / 1000;

				var roundResult = Math.Round(reactionTime, 3);		
				
				var resultString = roundResult + " ms";		
				
				WeakReferenceMessenger.Default.Send(new PassIntervalMessage(resultString));
				
				if (_correctIterationNumber < IterationCount - 1)
				{
					_reactionTimes[_correctIterationNumber++] = roundResult;
					
					return;
				}
				
				_reactionTimes[_correctIterationNumber++] = roundResult;

				var view = new TotalResultView
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen,
					DataContext = factory.Create(typeof(TotalResultViewModel))
				};

				WeakReferenceMessenger.Default.Send(new PassResultsMessage(_reactionTimes));
				WeakReferenceMessenger.Default.Send(new ChangeViewModelMessage(factory.Create(typeof(StartViewModel))));
				
				_correctIterationNumber = 0;
				_reactionTimes = new double[5];

				view.ShowDialog();
			});
	}

	private DateTime _startTime;
	private const int IterationCount = 5;
	private int _correctIterationNumber;
	private double[] _reactionTimes = new double[5];
	[ObservableProperty] private BaseViewModel _viewModel;
}