using System.Data;

namespace Final_Project_WPF.DAL
{
    public class Client_Dal
    {
        public static bool Insert(string firstName, string lastName, string IdentityNumber, string phone, string isadmin, string pass, string apr, string email)
        {
            //מוסיפה את הלקוח למסד הנתונים
            //בניית הוראת ה-SQL

            string str = "INSERT INTO Clientstab"
            + "("
            + "[FirstName],[LastName],[IdentityNumber], [phone],[isadmin], [password], [aproved], [Email]"
            + ")"
            + " VALUES "
            + "("
            + $"N'{firstName}',N'{lastName}','{IdentityNumber}', '{phone}', N'{isadmin}', N'{pass}', N'{apr}',N'{email}'"
            + ")";
            //הפעלת פעולת הSQL -תוך שימוש בפעולה המוכנה ExecuteSql במחלקה Dal והחזרה האם הפעולה הצליחה
            return Dal.ExecuteSql(str);
        }

        public static bool Update(int id, string firstName, string lastName, string IdentityNumber, string Phone, string isadmin, string pass, string apr, string email)
        {
            //מעדכנת את הלקוח במסד הנתונים

            string str = "UPDATE Clientstab SET"
            + $" [FirstName] = N'{firstName}'"
            + $",[LastName] = N'{lastName}'"
            + $",[Phone] = '{Phone}'"
            + $",[IdentityNumber] = '{IdentityNumber}'"
            + $",[isadmin] = '{isadmin}'"
            + $",[password] = '{pass}'"
            + $",[aproved] = '{apr}'"
            + $",[email] = '{email}'"
            + $" WHERE ID = {id}";
            //הפעלת פעולת הSQL -תוך שימוש בפעולה המוכנה ExecuteSql במחלקה Dal והחזרה האם הפעולה הצליחה
            return Dal.ExecuteSql(str);
        }

        public static DataTable GetDataTable()
        {
            DataTable dataTable = null;
            DataSet dataSet = new DataSet();
            FillDataSet(dataSet);
            dataTable = dataSet.Tables["Clientstab"];
            return dataTable;
        }

        public static bool Delete(int id)
        {
            //מוחקת את הלקוח ממסד הנתונים

            string str = $"DELETE FROM Clientstab WHERE ID = {id}";

            //הפעלת פעולת הSQL -תוך שימוש בפעולה המוכנה ExecuteSql במחלקה Dal והחזרה האם הפעולה הצליחה

            return Dal.ExecuteSql(str);
        }

        public static void FillDataSet(DataSet dataSet)
        {
            //ממלאת את אוסף הטבלאות בטבלת הלקוחות
            Dal.FillDataSet(dataSet, "Clientstab", "[LastName],[FirstName]");
            //בהמשך יהיו כאן הוראות נוספות הקשורות לקשרי גומלין...
        }

        public static void reseed()
        {
            string str = $"DBCC CHECKIDENT (@\"clientstab\", RESEED, 0)";
            Dal.ExecuteSql(str);
        }
    }
}