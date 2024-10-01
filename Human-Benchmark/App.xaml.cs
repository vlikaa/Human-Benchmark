﻿using System.Windows;
using Human_Benchmark.Services;
using Human_Benchmark.ViewModels;
using Human_Benchmark.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Human_Benchmark;

public partial class App : Application
{
	private ServiceCollection _serviceCollection = [];
	private static ServiceProvider? _serviceProvider;

	public static ServiceProvider ServiceProvider => _serviceProvider!;

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		_serviceCollection.AddSingleton<MainView>();
		_serviceCollection.AddSingleton<MainViewModel>();
		_serviceCollection.AddSingleton<BaseViewModel, StartViewModel>();
		_serviceCollection.AddSingleton<BaseViewModel, RedViewModel>();
		_serviceCollection.AddSingleton<BaseViewModel, GreenViewModel>();

		_serviceCollection.AddSingleton<ViewModelFactory>();
		
		_serviceProvider = _serviceCollection.BuildServiceProvider();
		
		var view = _serviceProvider.GetRequiredService<MainView>();
		
		view.DataContext = _serviceProvider.GetRequiredService<MainViewModel>();
		
		view.ShowDialog();
	}
}