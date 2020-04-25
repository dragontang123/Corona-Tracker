using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using Console = Colorful.Console;

namespace FunProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cutie's Corona Tracker";
            Console.WriteLine(".d88b .d88b. 888b. .d88b. 8b  8    db       88888 888b.    db    .d88b 8  dP 8888 888b. ",Color.Fuchsia);
            Console.WriteLine("8P    8P  Y8 8  .8 8P  Y8 8Ybm8   dPYb        8   8  .8   dPYb   8P    8wdP  8www 8  .8 ", Color.Fuchsia);
            Console.WriteLine("8b    8b  d8 8wwK' 8b  d8 8  \"8  dPwwYb       8   8wwK'  dPwwYb  8b    88Yb  8    8wwK' ", Color.Fuchsia);
            Console.WriteLine("`Y88P `Y88P' 8  Yb `Y88P' 8   8 dP    Yb      8   8  Yb dP    Yb `Y88P 8  Yb 8888 8  Yb ", Color.Fuchsia);
            Console.WriteLine();
            Console.WriteLine("1. View Global Corona Stats", Color.Aquamarine);
            Console.WriteLine("2. View Your Country's Stats",Color.Aquamarine);
            Console.Write(">",Color.Aquamarine);
            //try
            //{
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 1 || option == 2)
                {
                    Console.Clear();
                    if (option == 1)
                    {
                        using (HttpRequest httpRequest = new HttpRequest())
                        {
                            string global = httpRequest.Get("https://corona.lmao.ninja/v2/all?yesterday=false").ToString();
                            GlobalData DeserializedRequest = JsonConvert.DeserializeObject<GlobalData>(global);
                            Console.WriteLine("|-----------|",Color.Orange);
                            Console.WriteLine("|GLOBAL DATA|", Color.Orange);
                            Console.WriteLine("|-----------|", Color.Orange);
                            Console.WriteLine("Total Cases: " + DeserializedRequest.todayCases,Color.Blue);
                            Console.WriteLine("Total Deaths: " + DeserializedRequest.deaths,Color.Red);
                            Console.WriteLine("Total Recovered: " + DeserializedRequest.recovered,Color.Green);
                            Console.WriteLine("Press Enter To Exit");

                        }
                    }
                    else
                    {
                        Console.Write("Country: ", Color.Aquamarine);
                        string country = Console.ReadLine();
                        using (HttpRequest httpRequest = new HttpRequest())
                        {
                            string countrydata = httpRequest.Get("https://corona.lmao.ninja/v2/countries/" + country).ToString();
                            if (countrydata.Contains("\"message\": \"Country not found or doesn't have any cases\""))
                            {
                                Console.WriteLine("[ERROR] Invalid Input", Color.Red);
                                Console.WriteLine("Press Enter To Exit");
                                Console.ReadLine();
                            }
                            else
                            {
                                CountryData countryData = JsonConvert.DeserializeObject<CountryData>(countrydata);
                            if (countryData.country.Contains("USA"))
                            {
                                Console.Write("Do you want to get data about your state?(y/n):", Color.Aquamarine);
                                string stateoption = Console.ReadLine();
                                string[] modes = new string[] { "y", "n" };
                                string _mode = modes.FirstOrDefault<string>(s => stateoption.Contains(s));
                                switch (_mode)
                                {
                                    case "y":
                                        Console.Write("Full state name(e.g. california):", Color.Aquamarine);
                                        string statename = Console.ReadLine();
                                        string statedata = httpRequest.Get("https://corona.lmao.ninja/v2/states/" + statename).ToString();
                                        if (statedata.Contains("\"message\": \"Country not found or doesn't have any cases\""))
                                        {
                                            Console.WriteLine("[ERROR] Invalid Input", Color.Red);
                                            Console.WriteLine("Press Enter To Exit");
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            int statenamelenght = statename.Length;
                                            string result1 = new String('-', statenamelenght + 5);
                                            Console.WriteLine("|"+result1+"|", Color.Orange);
                                            Console.WriteLine("|"+statename.ToUpper()+" DATA|", Color.Orange);
                                            Console.WriteLine("|" + result1 + "|", Color.Orange);
                                            StateData state = JsonConvert.DeserializeObject<StateData>(statedata);
                                            Console.WriteLine("Total Cases: " + state.cases, Color.Blue);
                                            Console.WriteLine("Total Deaths: " + state.deaths, Color.Red);
                                            Console.WriteLine("Today's Cases: " + state.todayCases, Color.Blue);
                                            Console.WriteLine("Today's Deaths: " + state.todayDeaths, Color.Red);
                                            Console.WriteLine("Press Enter To Exit");
                                        }
                                        break;
                                    case "n":
                                        Console.Clear();
                                        int countrynamelenght = country.Length;
                                        string result = new String('-', countrynamelenght+5);
                                        Console.WriteLine("|"+result+"|", Color.Orange);
                                        Console.WriteLine("|" + country.ToUpper() + " DATA|", Color.Orange);
                                        Console.WriteLine("|" + result + "|", Color.Orange);
                                        Console.WriteLine("Total Cases: " + countryData.cases, Color.Blue);
                                        Console.WriteLine("Total Deaths: " + countryData.deaths, Color.Red);
                                        Console.WriteLine("Total Recovered: " + countryData.recovered, Color.Green);
                                        Console.WriteLine("Today's Cases: " + countryData.todayCases, Color.Blue);
                                        Console.WriteLine("Today's Deaths: " + countryData.todayDeaths, Color.Red);
                                        Console.WriteLine("Press Enter To Exit");
                                        break;
                                    default:
                                        Console.WriteLine("[ERROR] Invalid Input", Color.Red);
                                        Console.WriteLine("Press Enter To Exit");
                                        break;
                                }
                            }
                            else
                            {
                                Console.Clear();
                                int countrynamelenght = country.Length;
                                string result = new String('-', countrynamelenght + 5);
                                Console.WriteLine("|" + result + "|", Color.Orange);
                                Console.WriteLine("|" + country.ToUpper() + " DATA|", Color.Orange);
                                Console.WriteLine("|" + result + "|", Color.Orange);
                                Console.WriteLine("Total Cases: " + countryData.cases, Color.Blue);
                                Console.WriteLine("Total Deaths: " + countryData.deaths, Color.Red);
                                Console.WriteLine("Total Recovered: " + countryData.recovered, Color.Green) ;
                                Console.WriteLine("Today's Cases: " + countryData.todayCases, Color.Blue);
                                Console.WriteLine("Today's Deaths: " + countryData.todayDeaths, Color.Red);
                                Console.WriteLine("Press Enter To Exit");
                            }
                            }

                        }
                    }
                }
                else
                {
                    Console.WriteLine("[ERROR] Invalid Input", Color.Red);
                    Console.WriteLine("Press Enter To Exit");
                    Console.ReadLine();
                }
            //}
            //catch
            //{
            //    Console.WriteLine("[ERROR] Invalid Input");
            //    Console.WriteLine("Press Enter To Exit");
            //    Console.ReadLine();
           // }

            Console.ReadLine();
        }
    }

    public class GlobalData
    {
        public long updated { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public long casesPerOneMillion { get; set; }
        public long deathsPerOneMillion { get; set; }
        public int tests { get; set; }
        public long testsPerOneMillion { get; set; }
        public int affectedCountries { get; set; }
    }

    public class CountryData
    {
        public long updated { get; set; }
        public string country { get; set; }
        public Countryinfo countryInfo { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int recovered { get; set; }
        public int active { get; set; }
        public int critical { get; set; }
        public int casesPerOneMillion { get; set; }
        public int deathsPerOneMillion { get; set; }
        public int tests { get; set; }
        public long testsPerOneMillion { get; set; }
        public string continent { get; set; }
    }

    public class Countryinfo
    {
        public int _id { get; set; }
        public string iso2 { get; set; }
        public string iso3 { get; set; }
        public long lat { get; set; }
        public long _long { get; set; }
        public string flag { get; set; }
    }


    public class StateData
    {
        public string state { get; set; }
        public int cases { get; set; }
        public int todayCases { get; set; }
        public int deaths { get; set; }
        public int todayDeaths { get; set; }
        public int active { get; set; }
        public int tests { get; set; }
        public int testsPerOneMillion { get; set; }
    }

}
