using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

internal class Dal
{
    
    public static bool ExecuteSql(string sql)
    {
        //מקשר
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename = C: \Users\Hadar CS\source\repos\WPF\Final Project WPF\Database1.mdf; Integrated Security=True");
        //הצבת מחרוזת הקישור במקשר - שימוש בפעולת עזר למציאת מחרוזת זאת
        connection.ConnectionString = GetConnectionString();

        //ההוראה
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = sql;

        //בגלל שיש גישה לקבצים חיצוניים וכן בגלל פתיחת קשר לקובץ חיצוני - "עוטפים" במנגנון טיפול בשגיאות"
        try
        {
            //פתיחת הקשר
            connection.Open();

            //הפעלת הפקודה
            command.ExecuteNonQuery();

            //סגירת הקשר
            connection.Close();

            return true;
        }
        catch (Exception e)
        {
            //משמש רק לצרכי בקרה במקרה של תקלה - חשוב להשאיר כאן נקודת עצירה
            e.ToString();
        }

        return false;
    }

    public static void FillDataSet(DataSet dataSet, string tableName, string orderBy = "")
    {
        //מקשר

        SqlConnection connection = new SqlConnection();
        //הצבת מחרוזת הקישור במקשר
        connection.ConnectionString = GetConnectionString();

        //מבצע פעולה
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        if (orderBy != "")
            command.CommandText = "SELECT * FROM " + tableName + " ORDER BY " + orderBy;
        else
            command.CommandText = "SELECT * FROM " + tableName;

        //מתאם
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = command;
        adapter.Fill(dataSet, tableName);
    }

    private static string GetConnectionString()
    {
        return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Hadar CS\source\repos\WPF\Final Project WPF\Database1.mdf';Integrated Security=True";
        
        //בניית מחרוזת הקישור
        SqlConnectionStringBuilder cString = new SqlConnectionStringBuilder();

        cString.DataSource = @"(localdb)\.";
        cString.AttachDBFilename = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\Database1.mdf";

        return cString.ToString();
        
    }
}