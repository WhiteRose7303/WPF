using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Final_Project_WPF.DAL
{
    class Grade_Dal
    {
        public static DataTable GetDataTable()
        {
            DataTable dataTable = null;
            DataSet dataSet = new DataSet();
            FillDataSet(dataSet);
            dataTable = dataSet.Tables["Grade"];
            return dataTable;
        }
        public static void FillDataSet(DataSet dataSet)
        {
            //ממלאת את אוסף הטבלאות בטבלת הלקוחות
            if (!dataSet.Tables.Contains("Grade"))
            {
                Dal.FillDataSet(dataSet, "Grade", "[Level]");
            }
            //בהמשך יהיו כאן הוראות נוספות הקשורות לקשרי גומלין...
        }

        public static bool Insert(string level)
        {
            //מוסיפה את הלקוח למסד הנתונים
            //בניית הוראת ה-SQL

            string str = "INSERT INTO Grade"
            + "("
            + "[Level]"
            + ")"
            + " VALUES "
            + "("
            + $"N'{level}'"
            + ")";
            //הפעלת פעולת הSQL -תוך שימוש בפעולה המוכנה ExecuteSql במחלקה Dal והחזרה האם הפעולה הצליחה
            return Dal.ExecuteSql(str);
        }

        public static bool Update(string Level, int id)
        {
            //מעדכנת את הלקוח במסד הנתונים

            string str = "UPDATE Grade SET"
            + $" [FirstName] = N'{Level}'"
            + $" WHERE ID = {id}";
            //הפעלת פעולת הSQL -תוך שימוש בפעולה המוכנה ExecuteSql במחלקה Dal והחזרה האם הפעולה הצליחה
            return Dal.ExecuteSql(str);
        }
        public static bool Delete(int id)
        {
            //מוחקת את הלקוח ממסד הנתונים

            string str = $"DELETE FROM Grade WHERE ID = {id}";

            //הפעלת פעולת הSQL -תוך שימוש בפעולה המוכנה ExecuteSql במחלקה Dal והחזרה האם הפעולה הצליחה

            return Dal.ExecuteSql(str);
        }

    }
}
