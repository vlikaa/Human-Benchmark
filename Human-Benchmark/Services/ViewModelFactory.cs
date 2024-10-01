using Human_Benchmark.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Human_Benchmark.Services;

public class ViewModelFactory
{
	public BaseViewModel Create(Type type)
	{
		var viewModels = App.ServiceProvider.GetServices(typeof(BaseViewModel)).Cast<BaseViewModel>();
		var viewModel = viewModels.FirstOrDefault(vm => vm.GetType() == type);

		if (viewModel == null)
		{
			throw new ArgumentException($"{type} ViewModel not found.");
		}

		return viewModel;
	}
}