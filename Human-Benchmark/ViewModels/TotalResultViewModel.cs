using System.Windows;
using Human_Benchmark.Messages;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Human_Benchmark.ViewModels;

[INotifyPropertyChanged]
public partial class TotalResultViewModel : BaseViewModel
{
	public TotalResultViewModel()
	{
		WeakReferenceMessenger.Default.Register<PassResultsMessage>(this,
			(_, message) =>
			{
				TotalResults = message.Results;

				AvgResult = Math.Round(TotalResults.Average(), 3);
			});
	}
	
	[RelayCommand]
	private void Close()
	{
		Application.Current.Windows
			.OfType<Window>()
			.SingleOrDefault(x => x.IsActive)?
			.Close();
	}
	
	[ObservableProperty] private double[] _totalResults = new double[5];
	[ObservableProperty] private double _avgResult;
}