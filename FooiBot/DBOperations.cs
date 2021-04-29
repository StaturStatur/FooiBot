using DSharpPlus.CommandsNext;
using FooiBot.Commands;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FooiBot
{
    class DBOperations : RaidLFG
    {

        public static void EinschreibenDi(string[] Anmeldung, CommandContext ctx)
        {
            int results;
            //Connect to DB as Di-User
            var connectionStringDi = @"SERVER = DESKTOP-GBH4PTV\SQLEXPRESS1; DATABASE = FooiDB; USER ID = RaidDi; PASSWORD = 123";
            SqlConnection sqlConnDi = new SqlConnection(connectionStringDi);
            sqlConnDi.Open();
            //add data
            switch (Anmeldung[0])
            {
                case "Exp":
                    SqlCommand cmdExp = new SqlCommand("INSERT INTO RaidDiExp (DateTime, Name, Rolle, Klasse, Level)" +
                        "VALUES('" + DateTime.Now + "','"  //DateTime
                        + ctx.Member.Mention + "','" //Name
                        + Anmeldung[2] + "','" //Rolle
                        + Anmeldung[3] + "','" //Klasse
                        + Anmeldung[0] + "');", sqlConnDi); //Level
                    results = cmdExp.ExecuteNonQuery();
                    sqlConnDi.Close();
                    if(results > 0)
                        Console.WriteLine("Eintragen Erfolgreich (Exp Di)");
                    break;
                case "Lvl2":
                    SqlCommand cmdLvl2 = new SqlCommand("INSERT INTO RaidDiLvl2 (DateTime, Name, Rolle, Klasse, Level)" +
                        "VALUES('" + DateTime.Now + "','"  //DateTime
                        + ctx.Member.Mention + "','" //Name
                        + Anmeldung[2] + "','" //Rolle
                        + Anmeldung[3] + "','" //Klasse
                        + Anmeldung[0] + "');", sqlConnDi); //Level
                    results = cmdLvl2.ExecuteNonQuery();
                    sqlConnDi.Close();
                    if (results > 0)
                        Console.WriteLine("Eintragen Erfolgreich (Lvl2 Di)");
                    break;
                case "Lvl1":
                    SqlCommand cmdLvl1 = new SqlCommand("INSERT INTO RaidDiLvl1 (DateTime, Name, Rolle, Klasse, Level)" +
                        "VALUES('" + DateTime.Now + "','"  //DateTime
                        + ctx.Member.Mention + "','" //Name
                        + Anmeldung[2] + "','" //Rolle
                        + Anmeldung[3] + "','" //Klasse
                        + Anmeldung[0] + "');", sqlConnDi); //Level
                    results = cmdLvl1.ExecuteNonQuery();
                    sqlConnDi.Close();
                    if (results > 0)
                        Console.WriteLine("Eintragen Erfolgreich (Lvl1 Di)");
                    break;
                case "lvl0":
                    SqlCommand cmdLvl0 = new SqlCommand("INSERT INTO RaidDiLvl0 (DateTime, Name, Rolle, Klasse, Level)" +
                        "VALUES('" + DateTime.Now + "','"  //DateTime
                        + ctx.Member.Mention + "','" //Name
                        + Anmeldung[2] + "','" //Rolle
                        + Anmeldung[3] + "','" //Klasse
                        + Anmeldung[0] + "');", sqlConnDi); //Level
                    results = cmdLvl0.ExecuteNonQuery();
                    sqlConnDi.Close();
                    if (results > 0)
                        Console.WriteLine("Eintragen Erfolgreich (Lvl0 Di)");
                    break;
                default:
                    break;
            }
        }

        public static void EinschreibenDo(string[] Anmeldung, CommandContext ctx)
        {
            int results;
            //Connect to DB as Di-User
            var connectionStringDo = @"SERVER = DESKTOP-GBH4PTV\SQLEXPRESS1; DATABASE = FooiDB; USER ID = RaidDo; PASSWORD = 123";
            SqlConnection sqlConnDo = new SqlConnection(connectionStringDo);
            sqlConnDo.Open();
            //add data
            switch (Anmeldung[0])
            {
                case "Exp":
                    SqlCommand cmdExp = new SqlCommand("INSERT INTO RaidDoExp (DateTime, Name, Rolle, Klasse, Level)" +
                        "VALUES('" + DateTime.Now + "','"  //DateTime
                        + ctx.Member.Mention + "','" //Name
                        + Anmeldung[2] + "','" //Rolle
                        + Anmeldung[3] + "','" //Klasse
                        + Anmeldung[0] + "');", sqlConnDo); //Level
                    results = cmdExp.ExecuteNonQuery();
                    sqlConnDo.Close();
                    if (results > 0)
                        Console.WriteLine("Eintragen Erfolgreich (Exp Do)");
                    break;
                case "Lvl2":
                    SqlCommand cmdLvl2 = new SqlCommand("INSERT INTO RaidDoLvl2 (DateTime, Name, Rolle, Klasse, Level)" +
                        "VALUES('" + DateTime.Now + "','"  //DateTime
                        + ctx.Member.Mention + "','" //Name
                        + Anmeldung[2] + "','" //Rolle
                        + Anmeldung[3] + "','" //Klasse
                        + Anmeldung[0] + "');", sqlConnDo); //Level
                    results = cmdLvl2.ExecuteNonQuery();
                    sqlConnDo.Close();
                    if (results > 0)
                        Console.WriteLine("Eintragen Erfolgreich (Lvl2 Do)");
                    break;
                default:
                    break;
            }
        }

        public static void LeererDi()
        {
            int results;
            var connectionStringDi = @"SERVER = DESKTOP-GBH4PTV\SQLEXPRESS1; DATABASE = FooiDB; USER ID = RaidDi; PASSWORD = 123";
            SqlConnection sqlConnDi = new SqlConnection(connectionStringDi);

            //Leeren DiExp
            sqlConnDi.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM RaidDiExp", sqlConnDi);
            results = cmd.ExecuteNonQuery();
            sqlConnDi.Close();
            if (results > 0)
                Console.WriteLine("Leeren Erfolgreich (Exp Di)");
            else
                Console.WriteLine("Leeren nicht Erfolgreich");

            //Leeren DiLvl2
            sqlConnDi.Open();
            cmd.CommandText = "DELETE FROM RaidDiLvl2";
            results = cmd.ExecuteNonQuery();
            sqlConnDi.Close();
            if (results > 0)
                Console.WriteLine("Leeren Erfolgreich (Lvl2 Di)");
            else
                Console.WriteLine("Leeren nicht Erfolgreich");

            //Leeren DiLvl1
            sqlConnDi.Open();
            cmd.CommandText = "DELETE FROM RaidDiLvl1";
            results = cmd.ExecuteNonQuery();
            sqlConnDi.Close();
            if (results > 0)
                Console.WriteLine("Leeren Erfolgreich (Lvl1 Di)");
            else
                Console.WriteLine("Leeren nicht Erfolgreich");

            //Leeren DiLvl0
            sqlConnDi.Open();
            cmd.CommandText = "DELETE FROM RaidDiLvl0";
            results = cmd.ExecuteNonQuery();
            sqlConnDi.Close();
            if (results > 0)
                Console.WriteLine("Leeren Erfolgreich (Lvl0 Di)");
            else
                Console.WriteLine("Leeren nicht Erfolgreich");
        }

        public static void LeererDo()
        {
            int results;
            var connectionStringDo = @"SERVER = DESKTOP-GBH4PTV\SQLEXPRESS1; DATABASE = FooiDB; USER ID = RaidDo; PASSWORD = 123";
            SqlConnection sqlConnDo = new SqlConnection(connectionStringDo);

            //Leeren DoExp
            sqlConnDo.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM RaidDoExp", sqlConnDo);
            results = cmd.ExecuteNonQuery();
            sqlConnDo.Close();
            if (results > 0)
                Console.WriteLine("Leeren Erfolgreich (Exp Do)");
            else
                Console.WriteLine("Leeren nicht Erfolgreich");

            //Leeren DoLvl2
            sqlConnDo.Open();
            cmd.CommandText = "DELETE FROM RaidDoLvl2";
            results = cmd.ExecuteNonQuery();
            sqlConnDo.Close();
            if (results > 0)
                Console.WriteLine("Leeren Erfolgreich (Lvl2 Do)");
            else
                Console.WriteLine("Leeren nicht Erfolgreich");
        }

        public static Tuple<string[,], string[,], string[,], string[,]> AuslesenDi()
        {
            string[,] DataExpDi = new string[3, 30];
            string[,] DataLvl2Di = new string[3, 30];
            string[,] DataLvl1Di = new string[3, 30];
            string[,] DataLvl0Di = new string[3, 30];

            //Connect to DB as Di-User
            var connectionStringDi = @"SERVER = DESKTOP-GBH4PTV\SQLEXPRESS1; DATABASE = FooiDB; USER ID = RaidDi; PASSWORD = 123";
            SqlConnection sqlConnDi = new SqlConnection(connectionStringDi);
            using (sqlConnDi)
            {
                sqlConnDi.Open();
                SqlDataAdapter sqlConn = new SqlDataAdapter("SELECT Klasse, Rolle, Name FROM RaidDiExp", sqlConnDi);
                DataTable sqlData = new DataTable();
                sqlConn.Fill(sqlData);
                int i = 0;

                foreach (DataRow row in sqlData.Rows)
                {
                    DataExpDi[0, i] = row["Name"].ToString();
                    DataExpDi[1, i] = row["Rolle"].ToString();
                    DataExpDi[2, i] = row["Klasse"].ToString();
                    i++;
                }

                sqlConn.SelectCommand.CommandText = "SELECT Klasse, Rolle, Name FROM RaidDiLvl2";
                sqlData.Clear();
                sqlConn.Fill(sqlData);

                foreach (DataRow row in sqlData.Rows)
                {
                    DataLvl2Di[0, i] = row["Name"].ToString();
                    DataLvl2Di[1, i] = row["Rolle"].ToString();
                    DataLvl2Di[2, i] = row["Klasse"].ToString();
                    i++;
                }

                sqlConn.SelectCommand.CommandText = "SELECT Klasse, Rolle, Name FROM RaidDiLvl1";
                sqlData.Clear();
                sqlConn.Fill(sqlData);

                foreach (DataRow row in sqlData.Rows)
                {
                    DataLvl1Di[0, i] = row["Name"].ToString();
                    DataLvl1Di[1, i] = row["Rolle"].ToString();
                    DataLvl1Di[2, i] = row["Klasse"].ToString();
                    i++;
                }

                sqlConn.SelectCommand.CommandText = "SELECT Klasse, Rolle, Name FROM RaidDiLvl0";
                sqlData.Clear();
                sqlConn.Fill(sqlData);

                foreach (DataRow row in sqlData.Rows)
                {
                    DataLvl0Di[0, i] = row["Name"].ToString();
                    DataLvl0Di[1, i] = row["Rolle"].ToString();
                    DataLvl0Di[2, i] = row["Klasse"].ToString();
                    i++;
                }
            }
            return new Tuple<string[,], string[,], string[,], string[,]>(DataExpDi,DataLvl2Di,DataLvl0Di,DataLvl1Di);
        }

        public static Tuple<string[,], string[,]> AuslesenDo()
        {
            string[,] DataExpDo = new string[3, 30];
            string[,] DataLvl2Do = new string[3, 30];

            //Connect to DB as Di-User
            var connectionStringDo = @"SERVER = DESKTOP-GBH4PTV\SQLEXPRESS1; DATABASE = FooiDB; USER ID = RaidDo; PASSWORD = 123";
            SqlConnection sqlConnDo = new SqlConnection(connectionStringDo);
            using (sqlConnDo)
            {
                sqlConnDo.Open();
                SqlDataAdapter sqlConn = new SqlDataAdapter("SELECT Klasse, Rolle, Name FROM RaidDoExp", sqlConnDo);
                DataTable sqlData = new DataTable();
                sqlConn.Fill(sqlData);
                int i = 0;

                foreach (DataRow row in sqlData.Rows)
                {
                    DataExpDo[0, i] = row["Name"].ToString();
                    DataExpDo[1, i] = row["Rolle"].ToString();
                    DataExpDo[2, i] = row["Klasse"].ToString();
                    i++;
                }

                sqlConn.SelectCommand.CommandText = "SELECT Klasse, Rolle, Name FROM RaidDoLvl2";
                sqlData.Clear();
                sqlConn.Fill(sqlData);

                foreach (DataRow row in sqlData.Rows)
                {
                    DataLvl2Do[0, i] = row["Name"].ToString();
                    DataLvl2Do[1, i] = row["Rolle"].ToString();
                    DataLvl2Do[2, i] = row["Klasse"].ToString();
                    i++;
                }
            }
            return new Tuple<string[,], string[,]>(DataExpDo, DataLvl2Do);
        }

        public static void Deleter(string Tag, string Level, CommandContext ctx)
        {
            int results;            

            if (Tag == "Di")
            {
                var connectionStringDi = @"SERVER = DESKTOP-GBH4PTV\SQLEXPRESS1; DATABASE = FooiDB; USER ID = RaidDi; PASSWORD = 123";
                SqlConnection sqlConnDi = new SqlConnection(connectionStringDi);
                SqlCommand cmd = new SqlCommand("", sqlConnDi);
                sqlConnDi.Open();

                switch (Level)
                {
                    case "Exp":
                        cmd.CommandText = "DELETE FROM RaidDiExp WHERE Name = \'" + ctx.Member.Mention + "\';";
                        results = cmd.ExecuteNonQuery();
                        sqlConnDi.Close();
                        if (results > 0)
                            Console.WriteLine("Austragen Erfolgreich (Exp Di)");
                        break;
                    case "Lvl2":
                        cmd.CommandText = "DELETE FROM RaidDiLvl2 WHERE Name = \'" + ctx.Member.Mention + "\';";
                        results = cmd.ExecuteNonQuery();
                        sqlConnDi.Close();
                        if (results > 0)
                            Console.WriteLine("Austragen Erfolgreich (Lvl2 Di)");
                        break;
                    case "Lvl1":
                        cmd.CommandText = "DELETE FROM RaidDiLvl1 WHERE Name = \'" + ctx.Member.Mention + "\';";
                        results = cmd.ExecuteNonQuery();
                        sqlConnDi.Close();
                        if (results > 0)
                            Console.WriteLine("Austragen Erfolgreich (Lvl1 Di)");
                        break;
                    case "Lvl0":
                        cmd.CommandText = "DELETE FROM RaidDiLvl0 WHERE Name = \'" + ctx.Member.Mention + "\';";
                        results = cmd.ExecuteNonQuery();
                        sqlConnDi.Close();
                        if (results > 0)
                            Console.WriteLine("Austragen Erfolgreich (Lvl0 Di)");
                        break;
                    default:
                        break;
                }
            }
            if(Tag == "Do")
            {
                var connectionStringDo = @"SERVER = DESKTOP-GBH4PTV\SQLEXPRESS1; DATABASE = FooiDB; USER ID = RaidDo; PASSWORD = 123";
                SqlConnection sqlConnDo = new SqlConnection(connectionStringDo);
                SqlCommand cmd = new SqlCommand("", sqlConnDo);
                sqlConnDo.Open();

                switch (Level)
                {
                    case "Exp":
                        cmd.CommandText = "DELETE FROM RaidDoExp WHERE Name = \'" + ctx.Member.Mention + "\';";
                        results = cmd.ExecuteNonQuery();
                        sqlConnDo.Close();
                        if (results > 0)
                            Console.WriteLine("Austragen Erfolgreich (Exp Do)");
                        break;
                    case "Lvl2":
                        cmd.CommandText = "DELETE FROM RaidDoLvl2 WHERE Name = \'" + ctx.Member.Mention + "\';";
                        results = cmd.ExecuteNonQuery();
                        sqlConnDo.Close();
                        if (results > 0)
                            Console.WriteLine("Austragen Erfolgreich (Lvl2 Do)");
                        break;
                    default:
                        break;
                }
            }
        }

    }//Class
}//Namespace