using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using AMICommon2;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using AMICommon;
namespace AMISystemManagement
{
    public class AddToSQL
    {
        //dodavanje nove strukture u SQL bazu podataka
        public void AddNewStructToSQL(AMIAgregatorStruct struktura)
        {
            
            try
            {
                string conectionStr = ConfigurationManager.ConnectionStrings["DBcon"].ConnectionString;

                using (SqlConnection con = new SqlConnection(conectionStr))
                {
                    TimestampWorker ts = new TimestampWorker();
                    List<string> Komande = new List<string>();
                    List<double> mjerenjaLista = new List<double>();

                    foreach (var device in struktura.Structures) // listam device
                    {
                            int agregatorID = struktura.AMIAgregatorCode;
                            int deviceID = device.AMIDeviceCode;
                            DateTime vrijeme2 = ts.UnixTimeStampToDateTime(device.Timestamp);
                            string vrijeme = vrijeme2.ToString("yyyy/MM/dd HH:mm:ss");
                      
                        foreach (var mjerenja in device.Measures) // listam mjerenja
                            {
                                mjerenjaLista.Add(mjerenja.Value);
                            }
                    string komanda = $"Insert into SystemManagerDB values({agregatorID},{deviceID},convert(datetime,'{vrijeme}'),{mjerenjaLista[0]},{mjerenjaLista[1]},{mjerenjaLista[2]},{mjerenjaLista[3]})";
                            Komande.Add(komanda);
                        for (int i = 0; i < 4; i++)
                        {
                            mjerenjaLista.RemoveAt(0);
                        }
                    }

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    foreach (var k in Komande)
                    {
                        cmd.CommandText = k;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
