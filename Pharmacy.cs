using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Kursach
{
    static class Pharmacy
    {
        static string file_name = "D:/database.sqlite";
        static SQLiteConnection connection;
        static SQLiteCommand command = new SQLiteCommand();
        static DataTable table = new DataTable();

        static Pharmacy()
        {
            if (!File.Exists(file_name))
                SQLiteConnection.CreateFile(file_name);

            connection = new SQLiteConnection("Data Source=" + file_name + ";Version=3;");
            connection.Open();
            command.Connection = connection;

            command.CommandText = "CREATE TABLE IF NOT EXISTS Аптека (id INTEGER PRIMARY KEY AUTOINCREMENT, Название TEXT, " +
                "Количество INTEGER, Цена INTEGER, Болезнь TEXT)";
            command.ExecuteNonQuery();

            table.Clear();
            new SQLiteDataAdapter("SELECT * FROM Аптека", connection).Fill(table);
        }

        static public void Create() {
            if (!File.Exists(file_name))
                SQLiteConnection.CreateFile(file_name);

            connection = new SQLiteConnection("Data Source=" + file_name + ";Version=3;");
            connection.Open();
            command.Connection = connection;

            command.CommandText = "CREATE TABLE IF NOT EXISTS Аптека (id INTEGER PRIMARY KEY AUTOINCREMENT, Название TEXT, " +
                "Количество INTEGER, Цена INTEGER, Болезнь TEXT)";
            command.ExecuteNonQuery();
        }

        static public void ViewTable(DataGridView view)
        {
            table.Clear();
            new SQLiteDataAdapter("SELECT * FROM Аптека", connection).Fill(table);
            view.Rows.Clear();

            if (table.Rows.Count == 0)
                return;

            for (int i = 0; i < table.Rows.Count; ++i)
                view.Rows.Add(table.Rows[i].ItemArray);
        }

        static public void Add(string Name, int Count, int Price, string Ill)
        {
            command.CommandText = "INSERT INTO Аптека ('Название', 'Количество', 'Цена', 'Болезнь') values ('" + Name + "', '" + Count + "', '" +
                Price + "', '" + Ill + "')";
            command.ExecuteNonQuery();
        }

        static public void Clear()
        {
            command.CommandText = "DELETE FROM Аптека";
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM sqlite_sequence WHERE name='Аптека'";
            command.ExecuteNonQuery();
        }

        static public void Update(string Name, int Count, int Price, string Ill, string id)
        {
            command.CommandText = "UPDATE Аптека SET ('Название', 'Количество', 'Цена', 'Болезнь') = ('" + Name + "', " + Count + ", " +
                Price + ", '" + Ill + "') WHERE id = " + id;
            command.ExecuteNonQuery();
        }

        static public void Delete(string id)
        {
            command.CommandText = "DELETE FROM Аптека WHERE id = " + id;
            command.ExecuteNonQuery();
        }

        static public bool IsEmpty()
        {
            table.Clear();
            new SQLiteDataAdapter("SELECT * FROM Аптека", connection).Fill(table);
            return table.Rows.Count == 0;
        }

        static public bool InDataBase(string name)
        {
            table.Clear();
            new SQLiteDataAdapter("SELECT * FROM Аптека WHERE Название = '" + name + "'", connection).Fill(table);
            return table.Rows.Count != 0;
        }

        static public int[] Request(string name, string ill, int count)
        {
            int c = 0;
            table.Clear();
            if (ill == "")
                new SQLiteDataAdapter("SELECT * FROM Аптека WHERE Название = '" + name + "'", connection).Fill(table);
            else
                new SQLiteDataAdapter("SELECT * FROM Аптека WHERE (Название, Болезнь) = ('" + name + "', '" + ill + "')", connection).Fill(table);
            foreach (DataRow row in table.Rows)
            {
                if (int.Parse(row.ItemArray[2].ToString()) <= count)
                {
                    command.CommandText = "DELETE FROM Аптека WHERE id = " + row.ItemArray[0].ToString();
                    command.ExecuteNonQuery();
                    count -= int.Parse(row.ItemArray[2].ToString());
                    c += int.Parse(row.ItemArray[2].ToString()) * int.Parse(row.ItemArray[3].ToString());
                }
                else
                {
                    command.CommandText = "UPDATE Аптека SET 'Количество' = '" + (int.Parse(row.ItemArray[2].ToString()) - count) +
                        "' WHERE id = " + row.ItemArray[0].ToString();
                    c += count * int.Parse(row.ItemArray[3].ToString());
                    command.ExecuteNonQuery();
                    count = 0;
                    break;
                }
            }
            return new int[] { count, c };
        }
    }
}