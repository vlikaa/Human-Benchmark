using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using Human_Benchmark.Services;
using Human_Benchmark.Messages;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Human_Benchmark.Views;
using MaterialDesignThemes.Wpf;

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

				var resultString = Math.Round(reactionTime, 3) + " ms";		
				
				WeakReferenceMessenger.Default.Send(new PassIntervalMessage(resultString));
				
				if (_correctIterationNumber < IterationCount - 1)
				{
					_reactionTimes[_correctIterationNumber++] = reactionTime;
					
					return;
				}

				var view = new TotalResultView
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};

				view.ShowDialog();
			});
	}

	private DateTime _startTime;
	private const int IterationCount = 0;
	private int _correctIterationNumber;
	private double[] _reactionTimes = new double[5];
	[ObservableProperty] private BaseViewModel _viewModel;
}