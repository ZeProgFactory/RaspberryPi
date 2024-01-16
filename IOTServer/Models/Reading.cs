
public class Reading
{
    public enum Origines { mesure, calculated }

    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public decimal Temp { get; set; }
    public Origines Origine { get; set; } = Origines.mesure;
}
