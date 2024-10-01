namespace Human_Benchmark.Messages;

public class StartPeriodMessage : IMessage
{
	public DateTime StartPeriodTime { get; } = DateTime.Now;
}