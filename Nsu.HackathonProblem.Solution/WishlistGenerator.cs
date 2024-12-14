namespace Nsu.HackathonProblem.Solution;

using Nsu.HackathonProblem.Contracts;

public class WishlistGenerator
    {
        private readonly Random _random;

        public WishlistGenerator()
        {
            _random = new Random();
        }

        public List<Wishlist> GenerateWishlists(List<Employee> employees)
        {
            var wishlists = new List<Wishlist>();

            foreach (var employee in employees)
            {
                var desiredEmployees = employees
                    .Where(e => e.Id != employee.Id)
                    .Select(e => e.Id)
                    .OrderBy(_ => _random.Next())
                    .ToArray();

                wishlists.Add(new Wishlist(employee.Id, desiredEmployees));
            }

            return wishlists;
        }
    }
