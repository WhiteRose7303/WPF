using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_WPF.DAL;
using System.Data;

namespace Final_Project_WPF.BL.Grade
{
    class Grade
    {
        private string m_Name;
        private int m_ID;

        public string Name { get => m_Name; set => m_Name = value; }
        public int ID { get => m_ID; set => m_ID = value; }
        public Grade()
        {

        }

        public Grade(DataRow dataRow)
        {
            //מייצרת לקוח מתוך שורת לקוח

            ID = (int)dataRow["ID"];
            Name = dataRow["Level"].ToString();
            
        }
        public void Insert()
        {
            Grade_Dal.Insert(m_Name);
        }

        public bool Update()
        {
            return Grade_Dal.Update(m_Name, m_ID);
        }

        public bool Delete()
        {
            return Grade_Dal.Delete(ID);
        }
        public override string ToString()
        { return $"{Name}"; }

    }
}
