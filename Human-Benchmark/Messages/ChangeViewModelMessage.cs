using Human_Benchmark.ViewModels;

namespace Human_Benchmark.Messages;

public class ChangeViewModelMessage(BaseViewModel viewModel) : IMessage
{
	public BaseViewModel ViewModel { get; } = viewModel;
}