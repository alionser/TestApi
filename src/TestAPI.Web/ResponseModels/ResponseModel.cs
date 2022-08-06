namespace TestAPI.Web.ResponseModels;

public class ResponseModel
{
    public string Error { get; set; }
}

public sealed class ResponseModel<TResult> : ResponseModel
{
    public TResult Result { get; set; }
}