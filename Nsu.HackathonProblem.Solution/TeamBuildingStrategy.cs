namespace Nsu.HackathonProblem.Solution;

using System.Collections.Generic;
using Nsu.HackathonProblem.Contracts;

public class TeamBuildingStrategy : ITeamBuildingStrategy
{
    // Primitive greedy strategy
    public IEnumerable<Team> BuildTeams(
        IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors, 
        IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        var pairs = new List<Team>();
        var usedJuniors = new HashSet<int>();
        var usedTeamleads = new HashSet<int>();

        var allPossiblePairs = new List<(Employee junior, Employee teamlead, double score)>();

        foreach (var junior in juniors)
        {
            var juniorWishlist = juniorsWishlists.First(w => w.EmployeeId == junior.Id);

            foreach (var teamleadId in juniorWishlist.DesiredEmployees)
            {
                var teamleadWishlist = teamleadsWishlists.First(w => w.EmployeeId == teamleadId);

                allPossiblePairs.Add(
                    (junior, 
                    teamleads.First(w => w.Id == teamleadId), 
                    Harmonic.CalculateForPair(
                        junior.Id, teamleadId,
                        juniorWishlist, 
                        teamleadWishlist
                    )
                    )
                ); 
            }
        }

        var sortedPairs = allPossiblePairs.OrderByDescending(p => p.score).ToList();

        foreach (var (junior, teamlead, score) in sortedPairs)
        {
            if (!usedJuniors.Contains(junior.Id) && !usedTeamleads.Contains(teamlead.Id))
            {
                pairs.Add(new Team(teamlead, junior));
                usedJuniors.Add(junior.Id);
                usedTeamleads.Add(teamlead.Id);
            }
        }

        return pairs;
    }
}
