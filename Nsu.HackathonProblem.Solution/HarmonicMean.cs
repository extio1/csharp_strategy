namespace Nsu.HackathonProblem.Solution;

public class HarmonicMean
{
    public static double Calculate(IEnumerable<double> inputSequence) {
        double sumReciprocals = 0.0;
        int c = 0;

        foreach (var num in inputSequence)
        {
            if(num <= 0) {
                throw new ArgumentException($"All elements of inputSequence must be positive, but {num} found");
            }
            sumReciprocals += 1 / num;
            c++;
        }

        return inputSequence.Count() / sumReciprocals;
    }
}
