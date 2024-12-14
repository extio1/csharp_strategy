using Nsu.HackathonProblem.Contracts;

namespace Nsu.HackathonProblem.Solution;

public class Harmonic
{
    public static double CalculateForPair(
        int juniorId, int teamleadId,
        Wishlist juniorWishlist, Wishlist teamleadWishlist)
    {
        int juniorScore = juniorWishlist.DesiredEmployees.Length - Array.IndexOf([.. juniorWishlist.DesiredEmployees], teamleadId);
        int teamleadScore = teamleadWishlist.DesiredEmployees.Length - Array.IndexOf([.. teamleadWishlist.DesiredEmployees], juniorId);
        return 2.0 * juniorScore * teamleadScore / (juniorScore + teamleadScore);
    }

    public static double CalculateForTeams(
        IEnumerable<Team> teams,
        IEnumerable<Wishlist> teamleadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        var sum = 0.0;
        foreach (var team in teams)
        {
            var juniorWishlist = juniorsWishlists.First(w => w.EmployeeId == team.Junior.Id);
            var teamleadWishlist = teamleadsWishlists.First(w => w.EmployeeId == team.TeamLead.Id);

            int juniorScore = juniorWishlist.DesiredEmployees.Length - Array.IndexOf([.. juniorWishlist.DesiredEmployees], team.TeamLead.Id);
            int teamleadScore = teamleadWishlist.DesiredEmployees.Length - Array.IndexOf([.. teamleadWishlist.DesiredEmployees], team.Junior.Id);

            sum += 1.0 / juniorScore + 1.0 / teamleadScore;
        }

        return teams.Count() / sum;
    }
}
