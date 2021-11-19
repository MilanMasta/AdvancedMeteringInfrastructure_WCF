using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace AMISystemManagement
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            AMISystemManagementServer server = new AMISystemManagementServer();
            Queries queries = new Queries();
    
            //otvaranje i pocetak rada servisa
            server.Open();
            Console.WriteLine("AMISystemManager has been started");

            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"))
                + "AMIAgregator\\bin\\Debug\\Agregators.xml";
            
            //brisanje istorije iz Agregators XML fajla
            //brisanje svih salt podataka jer se ponovo pokrenula aplikacija, kako ne bi ostali zastarjeli podaci
            XDocument document = XDocument.Load(path);
            document.Descendants("Salts").Elements().Remove();
            document.Save(path);

            //brisanje prethodne baze
            queries.DeleteAll(); 

            //MENI
            int input = -1;
            while(input != 0)
            {
                Console.WriteLine();
                Console.WriteLine("[MENI]");
                Console.WriteLine("1. Iscrtavanje izabranog merenja za izabrani uređaj za izabrani datum");
                Console.WriteLine("2. Iscrtavanje sumarnog izabranog merenja za izabrani Agregator za izabrani datum");
                Console.WriteLine("3. Iscrtavanje prosečnog izabranog merenja za izabrani uređaj za izabrani datum");
                Console.WriteLine("4. Iscrtavanje prosečnog izabranog merenja za izabrani Agregator za izabrani datum");
                Console.WriteLine("5. Izlistavanje svih postojećih uređaja u sistemu");
                Console.WriteLine("0. Napusti aplikaciju");
                input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("Unesite datum u formatu YYYY-MM-dd");
                        string datum = Console.ReadLine();
                        Console.WriteLine("Unesi IdDevicea");
                        string IdDevicea = Console.ReadLine();
                        Console.WriteLine("Unesi vrstu mjerenja (Napon,Struja,AktSnaga,ReakSnaga)");
                        string vrsta = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(queries.Komanda1(datum, vrsta, IdDevicea));
                        }catch(Exception e)
                        {
                            Console.WriteLine();
                            Console.WriteLine("NISU UNESENI VALIDNI PODACI.");
                        }                      
                        break;
                    case 2:
                        Console.WriteLine("Unesite datum u formatu YYYY-MM-dd");
                        string datum2 = Console.ReadLine();
                        Console.WriteLine("Unesi IdAgreagatora");
                        string IdAgregatora2 = Console.ReadLine();
                        Console.WriteLine("Unesi vrstu mjerenja (Napon,Struja,AktSnaga,ReakSnaga)");
                        string vrsta2 = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(queries.Komanda2(datum2, vrsta2, IdAgregatora2));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine();
                            Console.WriteLine("NISU UNESENI VALIDNI PODACI.");
                        }                   
                        break;
                    case 3:
                        Console.WriteLine("Unesite datum u formatu YYYY-MM-dd");
                        string datum3 = Console.ReadLine();
                        Console.WriteLine("Unesi IdDevicea");
                        string IdDevicea3 = Console.ReadLine();
                        Console.WriteLine("Unesi vrstu mjerenja (Napon,Struja,AktSnaga,ReakSnaga)");
                        string vrsta3 = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(queries.Komanda3(datum3, vrsta3, IdDevicea3));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine();
                            Console.WriteLine("NISU UNESENI VALIDNI PODACI.");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Unesite datum u formatu YYYY-MM-dd");
                        string datum4 = Console.ReadLine();
                        Console.WriteLine("Unesi IdAgreagatora");
                        string IdAgregatora4 = Console.ReadLine();
                        Console.WriteLine("Unesi vrstu mjerenja (Napon,Struja,AktSnaga,ReakSnaga)");
                        string vrsta4 = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(queries.Komanda4(datum4, vrsta4, IdAgregatora4));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine();
                            Console.WriteLine("NISU UNESENI VALIDNI PODACI.");
                        }
                        break;
                    case 5:
                        try
                        {
                            Console.WriteLine(queries.Komanda5());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine();
                            Console.WriteLine("TRENUTNA GRESKA. POKUSAJTE PONOVO.");
                        }
                        
                        break;
                }
            }

            Console.WriteLine("AMISystemManager is stopping");
            server.Close();

            Console.WriteLine("AMISystemManager has stopped");

        }
    }
}
