namespace TestAPI.Web.ResponseModels.Employees;

public sealed class GetEmployeeResultModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }

    public decimal Salary { get; set; }

    public string PhotoUrl { get; set; }
}