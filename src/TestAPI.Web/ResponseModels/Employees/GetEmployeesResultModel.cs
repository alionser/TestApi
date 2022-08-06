namespace TestAPI.Web.ResponseModels.Employees;

public sealed class GetEmployeesResultModel
{
    public int TotalCount { get; set; }
    public int CountOnPage { get; set; }
    public ICollection<ShortEmployModel> Employees { get; set; } = new List<ShortEmployModel>();
}