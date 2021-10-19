using WpfLibrary.DAL;
using System.Collections;
using System.Data;

namespace WpfLibrary.BL
{
    public class Client
    {
        private string m_FirstName;
        private string m_LastName;
        private string m_ZipCode;
        private string m_Phone;
        private int m_ID;

        public string FirstName { get => m_FirstName; set => m_FirstName = value; }
        public string LastName { get => m_LastName; set => m_LastName = value; }
        public string ZipCode { get => m_ZipCode; set => m_ZipCode = value; }
        public string Phone { get => m_Phone; set => m_Phone = value; }
        public int ID { get => m_ID; set => m_ID = value; }

        public void Insert()
        {
            Client_Dal.Insert(m_FirstName, m_LastName, m_ZipCode, m_Phone);
        }

        public Client()
        {
        }

        public bool Update()
        {
            return Client_Dal.Update(m_ID, m_FirstName, m_LastName, m_ZipCode, m_Phone);
        }

        public bool Delete()
        {
            return Client_Dal.Delete(m_ID);
        }

        public Client(DataRow dataRow)
        {
            //מייצרת לקוח מתוך שורת לקוח

            ID = (int)dataRow["ID"];
            FirstName = dataRow["FirstName"].ToString();
            LastName = dataRow["LastName"].ToString();
            ZipCode = dataRow["ZipCode"].ToString();
            Phone = dataRow["Phone"].ToString();
        }

        public override string ToString()
        { return $"{LastName} {FirstName}"; }
    }

}