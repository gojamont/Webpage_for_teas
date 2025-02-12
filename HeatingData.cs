// Note for the csv file
//  [0] is time from,  [1] is time to, [2] is heat demand, [3] is an Electricity price

namespace HeatingOptimizer;
class HeatingData
{
    protected internal static List<Timeframe> WinterTimeFrame = new List<Timeframe>(); // list of timeframes
    protected internal static List<Timeframe> SummerTimeFrame = new List<Timeframe>(); // list of timeframes
    public static void GetData() // function for getting data from csv file
    {
        try
        {
            using (StreamReader sr = new StreamReader("heating_data.csv")) //reads the .csv file
            {
                string? line;
                // skip the first 3 lines
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();

                while ((line = sr.ReadLine()) != null) // if file is not empty, then return the data
                {
                    string[] periods = line.Split(",,"); // period from the csv file
                    string[] winter = periods[0].Split(","); // columns inside of the period in csv for WINTER
                    string[] summer = periods[1].Split(","); // columns inside of the period in csv for SUMMER
                    WinterTimeFrame.Add(new Timeframe(DateTime.Parse(winter[0]), DateTime.Parse(winter[1]), double.Parse(winter[2]), decimal.Parse(winter[3]))); // adds the data to the list
                    SummerTimeFrame.Add(new Timeframe(DateTime.Parse(summer[0]), DateTime.Parse(summer[1]), double.Parse(summer[2]), decimal.Parse(summer[3]))); // adds the data to the list
                }
            }
        }
        catch (Exception e) // if exception appears, then return a message that file could not be read. 
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }
}