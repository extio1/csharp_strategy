using Nsu.HackathonProblem.Solution;
using Nsu.HackathonProblem.Solution.Utils;

class Program
{
    static void Main()
    {
        var csvReader = new CsvReader();

        string juniorsFilePath = "Juniors20.csv";
        string teamleadsFilePath = "Teamleads20.csv";

        try
        {
            var juniors = csvReader.ReadEmployees(juniorsFilePath);
            var teamleads = csvReader.ReadEmployees(teamleadsFilePath);

            var wishlistGenerator = new WishlistGenerator();

            var scores = new List<double>();
            for(int i = 0; i < 1000; i++){
                var juniorsWishlists = wishlistGenerator.GenerateWishlists(juniors);
                var teamLeadsWishlists = wishlistGenerator.GenerateWishlists(teamleads);

                var strategy = new TeamBuildingStrategy();
                var teams = strategy.BuildTeams(teamleads, juniors, teamLeadsWishlists, juniorsWishlists);
                
                scores.Add(Harmonic.CalculateForTeams(teams, teamLeadsWishlists, juniorsWishlists));
            }

            Console.WriteLine($"Result harmonic score: {scores.Average()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
