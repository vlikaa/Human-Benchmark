namespace Human_Benchmark.Messages;

public class PassResultsMessage(double[] results) : IMessage
{
	public double[] Results { get; } = results;
}