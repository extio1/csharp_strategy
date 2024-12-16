namespace Nsu.HackathonProblem.Solution;

using System.Collections.Generic;
using Nsu.HackathonProblem.Contracts;

public class TeamBuildingStrategy : ITeamBuildingStrategy
{
    // Примитивная жадная стратегия
    public IEnumerable<Team> BuildTeams(
        IEnumerable<Employee> teamleads, IEnumerable<Employee> juniors, 
        IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        var allocatedTeams = new List<Team>();
        var assignedJuniors = new HashSet<int>();
        var assignedTeamleads = new HashSet<int>();

        var potentialMatches = new List<(Employee junior, Employee teamlead, double compatibilityScore)>();

        foreach (var junior in juniors)
        {
            var juniorWishlist = juniorsWishlists.First(w => w.EmployeeId == junior.Id);

            foreach (var teamleadId in juniorWishlist.DesiredEmployees)
            {
                var teamleadWishlist = teamleadsWishlists.First(w => w.EmployeeId == teamleadId);

                potentialMatches.Add(
                    (junior, 
                    teamleads.First(w => w.Id == teamleadId), 
                    CalculateCompatibilityScore(
                        junior.Id, teamleadId,
                        juniorWishlist, 
                        teamleadWishlist
                    )
                    )
                );
            }
        }

        var rankedMatches = potentialMatches.OrderByDescending(p => p.compatibilityScore).ToList();

        foreach (var (junior, teamlead, compatibilityScore) in rankedMatches)
        {
            if (!assignedJuniors.Contains(junior.Id) && !assignedTeamleads.Contains(teamlead.Id))
            {
                allocatedTeams.Add(new Team(teamlead, junior));
                assignedJuniors.Add(junior.Id);
                assignedTeamleads.Add(teamlead.Id);
            }
        }

        return allocatedTeams;
    }

    public static double CalculateCompatibilityScore(
        int juniorId, int teamleadId,
        Wishlist juniorWishlist, Wishlist teamleadWishlist)
    {
        int juniorScore = juniorWishlist.DesiredEmployees.Length - 
                          Array.IndexOf([.. juniorWishlist.DesiredEmployees], teamleadId);
        int teamleadScore = teamleadWishlist.DesiredEmployees.Length - 
                            Array.IndexOf([.. teamleadWishlist.DesiredEmployees], juniorId);
        return 2.0 * juniorScore * teamleadScore / 
               (juniorScore + teamleadScore);
    }
}
