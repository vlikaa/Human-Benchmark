namespace Human_Benchmark.Messages;

public class PassIntervalMessage(string intervalString) : IMessage
{
	public string IntervalString { get; } = intervalString;
}