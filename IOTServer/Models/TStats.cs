public class TStats
{
    public double Temp { get; set; }
    public double TempMin24 { get; set; }
    public double TempMax24 { get; set; }
    public double TempMin48 { get; set; }
    public double TempMax48 { get; set; }

    public string LastMessage { get; set; } = "";

    // - - -  - - - 

    public override string ToString()
    {
        return $"{Temp} °C";
    }
}
