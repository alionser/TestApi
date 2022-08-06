namespace TestAPI.Web.ResponseModels.Departments;

public class GetDepartmentsResultModel
{
    public ICollection<DepartmentsListItem> Departments { get; set; } = new List<DepartmentsListItem>();
}