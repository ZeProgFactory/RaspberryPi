
using ZPF.SQL;

public class Reading
{
   public enum Origines { mesure, calculated }

   [ZPF.SQL.DB_Attributes.Index(true, SortDirections.desc)]
   public DateTime TimeStamp { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
   public decimal Temp { get; set; }
   public Origines Origine { get; set; } = Origines.mesure;
}
