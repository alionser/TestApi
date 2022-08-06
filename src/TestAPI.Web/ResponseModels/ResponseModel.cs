namespace TestAPI.Web.ResponseModels;

public class ResponseModel
{
    public string Error { get; set; }
}

public class ResponseModel<TResult> : ResponseModel
{
    public TResult Result { get; set; }
}