namespace Human_Benchmark.Messages;

public class StopPeriodMessage : IMessage
{
	public DateTime StopPeriodTime { get; } = DateTime.Now;
}