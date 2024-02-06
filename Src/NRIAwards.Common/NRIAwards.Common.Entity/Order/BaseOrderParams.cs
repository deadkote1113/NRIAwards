namespace NRIAwards.Common.Entity.Order;

public class BaseOrderParams
{
    public BaseOrderParams(bool? orderByIdAsc = true)
    {
        OrderByIdAsc = orderByIdAsc;
    }

    public bool? OrderByIdAsc { get; set; }
}
