namespace Nsu.HackathonProblem.Solution.Utils;

using Nsu.HackathonProblem.Contracts;

public class CsvReader
{
    /// <summary>
    /// Читает CSV файл и возвращает список сотрудников.
    /// </summary>
    /// <param name="filePath">Путь к CSV файлу.</param>
    /// <returns>Список объектов Employee.</returns>
    public List<Employee> ReadEmployees(string filePath)
    {
        try
        {
            var lines = File.ReadAllLines(filePath).Skip(1);

            var employees = lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split(';');
                    return new Employee(
                        int.Parse(parts[0]),
                        parts[1] 
                    );
                })
                .ToList();

            return employees;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ошибка при чтении файла {filePath}: {ex.Message}", ex);
        }
    }
}
