using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.Solution;

public class Harmonic
{
    public static double CalculateForTeams(
        IEnumerable<Team> teams,
        IEnumerable<Wishlist> teamleadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        var satisfactionIndexes = CountSatisfactionIndexes(teams, teamleadsWishlists, juniorsWishlists);
        var sum = satisfactionIndexes
            .Where(index => index != 0.0)
            .Sum(index => 1.0 / index);

        return satisfactionIndexes.Length / sum;
    }

    private static int[] CountSatisfactionIndexes(
        IEnumerable<Team> teams,
        IEnumerable<Wishlist> teamleadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        var teamCount = teams.Count();
        var indexes = new int[teamCount * 2];

        foreach (var team in teams)
        {
            var juniorWishlist = juniorsWishlists.FirstOrDefault(w => w.EmployeeId == team.Junior.Id);
            var teamleadWishlist = teamleadsWishlists.FirstOrDefault(w => w.EmployeeId == team.TeamLead.Id);

            if (juniorWishlist == null || teamleadWishlist == null)
                continue;

            var teamLeadIndex = GetReversedIndex(juniorWishlist.DesiredEmployees, team.TeamLead.Id, teamCount);
            var juniorIndex = GetReversedIndex(teamleadWishlist.DesiredEmployees, team.Junior.Id, teamCount);

            if (team.Junior.Id - 1 >= 0 && team.Junior.Id - 1 < indexes.Length)
                indexes[team.Junior.Id - 1] = teamLeadIndex;

            if (teamCount + team.TeamLead.Id - 1 >= 0 && teamCount + team.TeamLead.Id - 1 < indexes.Length)
                indexes[teamCount + team.TeamLead.Id - 1] = juniorIndex;
        }

        return indexes;
    }

    private static int GetReversedIndex(int[] desiredEmployees, int id, int maxRank)
    {
        var index = Array.FindIndex(desiredEmployees, employeeId => employeeId == id);
        return index == -1 ? 0 : maxRank - index;
    }
}


