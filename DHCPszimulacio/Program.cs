using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DHCPszimulacio
{
    class Program
    {
        static List<string> excluded = new List<string>();
        static Dictionary<string, string> dhcp = new Dictionary<string, string>();
        static Dictionary<string, string> reserved = new Dictionary<string, string>();
        static List<string> commands = new List<string>();


        static void BeolvasList(List<string> l, string filename)//excluded.csv beolvasása.
        {
            try
            {
                StreamReader file = new StreamReader(filename);
                try//Ha sikerült beolvasni a fájlt, akkor megpróbálja végigvinni.
                {
                    while (!file.EndOfStream)
                    {
                        l.Add(file.ReadLine());
                    }
                }
                catch (Exception exception)//Ha hiba van, akkor azt kiírja.
                {
                    Console.WriteLine(exception.Message);
                }
                finally//Bezárja a StreamReader-t.
                {
                    file.Close();
                }
            }
            catch (Exception ex)//Ha nem sikerül beolvasni a fájlt, akkor hibát ír ki.
            {
                Console.WriteLine(ex.Message);
            }
        }


        static string CimEggyelNo(string cim)
        {
            //Ha cim = "192.168.10.100", akkor return "192.168.10.101"-et adjon vissza.
            //Szét kell vágni a pont mentén, az utolsót int-té konvertálni és egyet hozzáadni.
            //(255-öt ne lépjük túl.)
            //Aztán össze kell fűzni string-gé.

            string[] adatok = cim.Split('.');
            int okt4 = Convert.ToInt32(adatok[3]);
            if (okt4 < 255)
            {
                okt4++;
            }
            return adatok[0] + "." + adatok[1] + "." + adatok[2] + "." + okt4.ToString();
        }


        static void BeolvasDictionary(Dictionary<string, string> d, string filename)
        {
            try
            {
                StreamReader file = new StreamReader(filename);
                while (!file.EndOfStream)
                {
                    string[] adatok = file.ReadLine().Split(';');
                    d.Add(adatok[0], adatok[1]);
                }
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void Main(string[] args)
        {
            BeolvasList(excluded, "excluded.csv");
            foreach (var ex in excluded)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("\nVége...");
            Console.WriteLine();


            BeolvasList(commands, "test.csv");
            foreach (var c in commands)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine("\nVége...");
            Console.WriteLine();


            BeolvasDictionary(dhcp, "dhcp.csv");
            foreach (var d in dhcp)
            {
                Console.WriteLine(d);
            }
            Console.WriteLine("\nVége...");
            Console.WriteLine();


            BeolvasDictionary(reserved, "reserved.csv");
            foreach (var rs in reserved)
            {
                Console.WriteLine(rs);
            }
            Console.WriteLine("\nVége...");
            Console.WriteLine();


            Console.ReadKey();
        }
    }
}
