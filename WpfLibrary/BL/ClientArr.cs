using WpfLibrary.DAL;
using System.Collections;
using System.Data;


namespace WpfLibrary.BL
{
    public class ClientArr : ArrayList
    {
        public void Fill()
        {
            //להביא מה-DAL טבלה מלאה בכל הלקוחות

            DataTable dataTable = Client_Dal.GetDataTable();

            //להעביר את הערכים מהטבלה לתוך אוסף הלקוחות
            //להעביר כל שורה בטבלה ללקוח

            DataRow dataRow;
            Client curClient;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataRow = dataTable.Rows[i];
                curClient = new Client(dataRow);
                this.Add(curClient);
            }
        }

        public ClientArr Filter(string lastName, string phone)
        {
            ClientArr clientArr = new ClientArr();
            Client client;
            for (int i = 0; i < this.Count; i++)
            {
                //הצבת הלקוח הנוכחי במשתנה עזר - לקוח

                client = (this[i] as Client);
                if
                ((client.FirstName.ToLower().StartsWith(lastName.ToLower()) && (client.Phone.ToString()).Contains(phone)))

                    clientArr.Add(client);
            }
            return clientArr;
        }
    }
}
