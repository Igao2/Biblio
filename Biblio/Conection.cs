using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Biblio
{
    public class Conection
    {
        public SQLiteConnection sQLite = new SQLiteConnection("Data Source = basebiblio.db");

        public void Conectar()
        {
            sQLite.Open();
        }
        public void Desconectar()
        {
            sQLite.Close();
        }
    }
}
