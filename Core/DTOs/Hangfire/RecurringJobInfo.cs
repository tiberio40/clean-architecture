namespace Core;

public class RecurringJobInfo
{
    public string Id { get; set; }
    public string CronExpression { get; set; }
    public string MethodName { get; set; }

}
