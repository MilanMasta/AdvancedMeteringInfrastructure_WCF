using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISystemManagement
{
    // Iscrtavanje izabranog merenja za izabrani uređaj za izabrani datum - Komanda1
    // Iscrtavanje sumarnog izabranog merenja za izabrani Agregator za izabrani datum - Komanda2
    // Iscrtavanje prosečnog merenja za izabrani uređaj za izabrani datum - Komanda3
    // Iscrtavanje prosečnog izabranog merenja za izabrani Agregator za izabrani datum - Komanda4
    // Izlistavanje svih postojećih uređaja u sistemu - Komanda5
    [ExcludeFromCodeCoverage]
    class Queries
    {
        public string Komanda1(string dat,string tipMj,string IdDev)
        {
            
            int idDevicea = int.Parse(IdDev);
            string tipMjerenja = tipMj;

            string komanda =$@"Select IdDevicea, VremeIDatum, {tipMjerenja} from SystemManagerDB
                                where IdDevicea = {idDevicea} and VremeIDatum = '{dat}'";


            string tabela = String.Format("\n{0,0}{1,15}{2,15}\n------------------------------------\n", "IdDevicea", "Datum", "TipMjerenja");
            string conectionStr = ConfigurationManager.ConnectionStrings["DBcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = komanda;
                SqlDataReader da = cmd.ExecuteReader();

                while (da.Read())
                {
                    string idD = da.GetValue(0).ToString();
                    string datum = da.GetValue(1).ToString().Substring(0,9);
                    string value = da.GetValue(2).ToString();

                    string podaci = String.Format("{0,0}{1,15}{2,10}\n", idD, datum, value);
                    tabela = tabela + podaci;
                }
            }
            return tabela;
        }


        public string Komanda2(string dat, string tipMj, string IdAgr)
        {

            int idAgregatora = int.Parse(IdAgr);
            string tipMjerenja = tipMj;
            string komanda = $@"Select SUM({tipMjerenja}) {tipMjerenja} from SystemManagerDB
                                where VremeIDatum = '{dat}' and  IdAgregata = {idAgregatora}
                                group by IdAgregata";


            string conectionStr = ConfigurationManager.ConnectionStrings["DBcon"].ConnectionString;
            string ret = "";
            using (SqlConnection con = new SqlConnection(conectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = komanda;
                SqlDataReader da = cmd.ExecuteReader();

                while (da.Read())
                {
                    string value = da.GetValue(0).ToString();
                    ret = $"\nSuma {tipMjerenja} za datum {dat} i Agregator sa ID-om {idAgregatora} je {value}\n";
                }
            }
            return ret;
        }
        public string Komanda3(string dat, string tipMj, string IdDev)
        {

            int idDevicea = int.Parse(IdDev);
            string tipMjerenja = tipMj;
            string komanda = $@"Select AVG({tipMjerenja}) {tipMjerenja} from SystemManagerDB
                                where VremeIDatum = '{dat}' and  IdDevicea = {idDevicea}
                                group by IdDevicea";


            string conectionStr = ConfigurationManager.ConnectionStrings["DBcon"].ConnectionString;
            string ret = "";
            using (SqlConnection con = new SqlConnection(conectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = komanda;
                SqlDataReader da = cmd.ExecuteReader();

                while (da.Read())
                {
                    string value = da.GetValue(0).ToString();

                    ret = $"\nProsjecna {tipMjerenja} za datum {dat} i uredaj sa ID-om {IdDev} je {value}\n";
                }
            }
            return ret;
        }

        public string Komanda4(string dat, string tipMj, string IdAgr)
        {

            int idAgregatora = int.Parse(IdAgr);
            string tipMjerenja = tipMj;
            string komanda = $@"Select AVG({tipMjerenja}) {tipMjerenja} from SystemManagerDB
                                where VremeIDatum = '{dat}' and  IdAgregata = {idAgregatora}
                                group by IdAgregata";


            string conectionStr = ConfigurationManager.ConnectionStrings["DBcon"].ConnectionString;
            string ret = "";
            using (SqlConnection con = new SqlConnection(conectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = komanda;
                SqlDataReader da = cmd.ExecuteReader();

                while (da.Read())
                {
                    string value = da.GetValue(0).ToString();

                    ret = $"\nProsjecna {tipMjerenja} za datum {dat} i Agregator sa ID-om {idAgregatora} je {value}\n";
                }
            }
            return ret;
        }

        public string Komanda5()
        {
            string komanda = "select distinct IdDevicea from SystemManagerDB";


            string tabela = "\nIdDevice-a:\n---------------";
            string conectionStr = ConfigurationManager.ConnectionStrings["DBcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conectionStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = komanda;
                SqlDataReader da = cmd.ExecuteReader();

                while (da.Read())
                {
                    string idD = da.GetValue(0).ToString();

                    string podaci = $"\n{idD}";
                    tabela = tabela + podaci;
                }
            }
            return tabela;
        }
        public void DeleteAll()
        {
            string conectionStr = ConfigurationManager.ConnectionStrings["DBcon"].ConnectionString;
            string comand = "delete from SystemManagerDB";
            using (SqlConnection con = new SqlConnection(conectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                //BACACE EXC JER NEMA OTVOREN LOKALNI SQL SERVER
                con.Open();
                cmd.CommandText = comand;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
