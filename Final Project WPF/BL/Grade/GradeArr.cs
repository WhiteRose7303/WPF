using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Final_Project_WPF.DAL;
using System.Collections;


namespace Final_Project_WPF.BL
{
    class GradeArr : ArrayList
    {
        public void Fill()
        {
            //להביא מה-DAL טבלה מלאה בכל הלקוחות

            DataTable dataTable = Grade_Dal.GetDataTable();

            //להעביר את הערכים מהטבלה לתוך אוסף הלקוחות
            //להעביר כל שורה בטבלה ללקוח

            DataRow dataRow;
            Grade curClient;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataRow = dataTable.Rows[i];
                curClient = new Grade(dataRow);
                this.Add(curClient);
            }
        }
       
    }
}
