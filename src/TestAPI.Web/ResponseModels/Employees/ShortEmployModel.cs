namespace TestAPI.Web.ResponseModels.Employees;

public sealed class ShortEmployModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }

    public decimal Salary { get; set; }
}