namespace NRIAwards.DAL.Context.Model;
public class Characteristic : PostgresModel
{
    public string Name { get; set; }
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}
