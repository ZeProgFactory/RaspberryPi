
using ZPF.SQL;

public class Reading
{
    public enum Origines { mesure, calculated }

    [ZPF.SQL.DB_Attributes.Index(false, SortDirections.desc)]
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public decimal Temp { get; set; }
    public Origines Origine { get; set; } = Origines.mesure;
}
