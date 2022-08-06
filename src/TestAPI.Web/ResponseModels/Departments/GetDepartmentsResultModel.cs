namespace TestAPI.Web.ResponseModels.Departments;

public sealed class GetDepartmentsResultModel
{
    public ICollection<DepartmentsListItem> Departments { get; set; } = new List<DepartmentsListItem>();
}