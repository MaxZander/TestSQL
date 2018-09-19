using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace TestSQL
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public class Users
    {
        public Int32 ID;
        public string UserID;
        public string Psw;

        public Users()
        {
        }
    }
    public partial class MainWindow : Window
    {
        HashSet<Users> h = new HashSet<Users>();
        public MainWindow()
        {
            InitializeComponent();

        }
        private static MySqlConnection cnx = null;

        private static string _ConnectionString;

        public static MySqlConnection GetConnexion()
        {
            _ConnectionString = "Server=localhost; database=dbtest; password=; UID=root; sslmode=none;";

            if (cnx == null)
            {
                cnx = new MySqlConnection();
                cnx.ConnectionString = _ConnectionString;

                cnx.Open();
            }
            return cnx;
        }

        public static void CloseConnexion()
        {
            cnx.Close();
            cnx = null;
        }

        private void dbconnect_Click(object sender, RoutedEventArgs e)
        {
            GetConnexion();
            cmdsql();
            foreach (var item in h)
            {
                if(id.Text == item.UserID && psw.Password == item.Psw)
                {
                    snkbar.Message.Content = "Connexion OK !";
                    snkbar.IsActive = true;
                }
            }
        }
        private void cmdsql()
        {
            MySqlCommand cmd = GetConnexion().CreateCommand();
            cmd.CommandText = "SELECT `ID`, `UserID`, `Psw` FROM `dbusers`";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    
                    h.Add(new Users() { ID = reader.GetInt32(0), UserID = reader.GetString(1), Psw = reader.GetString(2) });
                    //h.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erreur DB \n" + e.Message);
                }

            }
        }
    }
    
}
