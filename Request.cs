using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Kursach
{
    static class Request
    {
        static string file_name = "D:/database.sqlite";
        static SQLiteConnection connection;
        static SQLiteCommand command = new SQLiteCommand();
        static DataTable table = new DataTable();

        static Request()
        {
            if (!File.Exists(file_name))
                SQLiteConnection.CreateFile(file_name);

            connection = new SQLiteConnection("Data Source=" + file_name + ";Version=3;");
            connection.Open();
            command.Connection = connection;

            command.CommandText = "CREATE TABLE IF NOT EXISTS Заявки (id INTEGER PRIMARY KEY AUTOINCREMENT, Имя TEXT, " +
                "Название TEXT, Количество INTEGER, Болезнь TEXT)";
            command.ExecuteNonQuery();

            table.Clear();
            new SQLiteDataAdapter("SELECT * FROM Заявки", connection).Fill(table);
        }

        static public void Create() {
            if (!File.Exists(file_name))
                SQLiteConnection.CreateFile(file_name);

            connection = new SQLiteConnection("Data Source=" + file_name + ";Version=3;");
            connection.Open();
            command.Connection = connection;

            command.CommandText = "CREATE TABLE IF NOT EXISTS Заявки (id INTEGER PRIMARY KEY AUTOINCREMENT, Имя TEXT, " +
                "Название TEXT, Количество INTEGER, Болезнь TEXT)";
            command.ExecuteNonQuery();
        }

        static public void Add(string human, string name, string ill, int count)
        {
            command.CommandText = "INSERT INTO Заявки ('Имя', 'Название', 'Количество', 'Болезнь') values " +
                "('" + human + "', '" + name + "', '" + count.ToString() + "', '" + ill + "')";
            command.ExecuteNonQuery();
        }

        static public void Clear()
        {
            command.CommandText = "DELETE FROM Заявки";
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM sqlite_sequence WHERE name='Заявки'";
            command.ExecuteNonQuery();
        }

        static public int[] Update(string name, string ill, int count, int price)
        {
            int c = 0;
            int value = 0;
            table.Clear();
            new SQLiteDataAdapter("SELECT * FROM Заявки WHERE Название = '" + name + "'", connection).Fill(table);
            foreach (DataRow row in table.Rows)
            {
                if (row.ItemArray[4].ToString() != "" && row.ItemArray[4].ToString() != ill)
                    continue;
                if (int.Parse(row.ItemArray[3].ToString()) <= count)
                {
                    ++value;
                    command.CommandText = "DELETE FROM Заявки WHERE id = " + row.ItemArray[0].ToString();
                    command.ExecuteNonQuery();
                    count -= int.Parse(row.ItemArray[3].ToString());
                    c += int.Parse(row.ItemArray[3].ToString()) * price;
                }
                else
                {
                    ++value;
                    command.CommandText = "UPDATE Заявки SET 'Количество' = '" + (int.Parse(row.ItemArray[3].ToString()) - count) +
                        "' WHERE id = " + row.ItemArray[0].ToString();
                    c += count * price;
                    command.ExecuteNonQuery();
                    count = 0;
                    break;
                }
            }
            return new int[] { count, c, value };
        }
        static public void ViewTable(DataGridView view)
        {
            table.Clear();
            new SQLiteDataAdapter("SELECT * FROM Заявки", connection).Fill(table);
            view.Rows.Clear();

            if (table.Rows.Count == 0)
                return;

            for (int i = 0; i < table.Rows.Count; ++i)
                view.Rows.Add(table.Rows[i].ItemArray);
        }
    }
}
