using Final_Project_WPF.DAL;
using System.Data;
using System.Data.SqlClient;
using Final_Project_WPF.BL;

namespace Final_Project_WPF.BL
{
    public class Client
    {
        private string m_FirstName;
        private string m_LastName;
        private string m_IdentityNumber;
        private string m_Phone;
        private string m_isadmin;
        private string m_pass;
        private string m_aproved;
        private string m_Email;
        private string m_Teacher;
        private Grade m_Group;
        private string m_GroupName;
        private int m_ID;
        private SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename = C: \Users\Hadar CS\source\repos\WPF\Final Project WPF\Database1.mdf; Integrated Security=True");

        public string FirstName { get => m_FirstName; set => m_FirstName = value; }
        public string LastName { get => m_LastName; set => m_LastName = value; }
        public string IdentityNumber { get => m_IdentityNumber; set => m_IdentityNumber = value; }
        public string Phone { get => m_Phone; set => m_Phone = value; }
        public int ID { get => m_ID; set => m_ID = value; }
        public string Isadmin { get => m_isadmin; set => m_isadmin = value; }
        public string Pass { get => m_pass; set => m_pass = value; }
        public string Aproved { get => m_aproved; set => m_aproved = value; }
        public string Email { get => m_Email; set => m_Email = value; }
        public string Teacher { get => m_Teacher; set => m_Teacher = value; }
        internal Grade Group { get => m_Group; set => m_Group = value; }
        public string GroupName { get => m_GroupName; set => m_GroupName = value; }

        public void Insert()
        {
            Client_Dal.Insert(m_FirstName, m_LastName, m_IdentityNumber, m_Phone, m_isadmin, m_pass, m_aproved, m_Email,m_Teacher, m_Group.ID);
        }

        public Client()
        {
        }

        public Client(int id, string aproved, DataRow datarow)
        {
            this.ID = id;
            this.m_aproved = aproved;
            m_Group = new Grade(datarow.GetParentRow("clientgroup"));
        }

        public bool Update()
        {
            return Client_Dal.Update(m_ID, m_FirstName, m_LastName, m_IdentityNumber, m_Phone, m_isadmin, m_pass, m_aproved, m_Email,m_Teacher,m_Group.ID);
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
            IdentityNumber = dataRow["IdentityNumber"].ToString();
            Phone = dataRow["Phone"].ToString();
            Isadmin = dataRow["isadmin"].ToString();
            Pass = dataRow["password"].ToString();
            Aproved = dataRow["aproved"].ToString();
            Email = dataRow["Email"].ToString();
            Teacher = dataRow["Teacher"].ToString();
            Group = new Grade(dataRow.GetParentRow("clientgroup"));
            GroupName = Group.Name;

        }

        public override string ToString()
        { return $"{LastName} {FirstName} {Pass} {Isadmin}"; }
    }
}