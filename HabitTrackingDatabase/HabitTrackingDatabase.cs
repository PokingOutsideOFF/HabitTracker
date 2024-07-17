using System.Data.SQLite;

namespace HabitTrackingDatabase
{
    public class HabitDatabase
    {
        public void InitializeDatabse()
        {
            using (var connection = new SQLiteConnection("Data Source= habits.db"))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS habits(
                       id INTEGER PRIMARY KEY AUTOINCREMENT,
                       habit_name TEXT NOT NULL,
                       quantity TEXT NOT NULL
                    );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ViewHabits()
        {
            Console.WriteLine("YOUR HABITS");
            Console.WriteLine("-----------------------");
            using (var connection = new SQLiteConnection("Data Source= habits.db"))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM habits";
                using(var command = new SQLiteCommand(selectQuery, connection))
                {
                    using(var reader = command.ExecuteReader())
                    { 
                        Console.WriteLine("ID\tHabit\tQuantity");
                        Console.WriteLine("-----------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["id"]}\t{reader["habit_name"]}\t{reader["quantity"]}");
                        }
                    } 

                }
            }
            Console.WriteLine("-----------------------\n");
        }

        public void InsertHabits()
        {
            Console.Write("Enter habit name: ");
            string? habitName = Console.ReadLine();

            Console.Write("Enter quantity: ");
            string? habitQuantity = Console.ReadLine();
            using(var connection = new SQLiteConnection("Data Source= habits.db"))
            {
                connection.Open();

                string insertQuery = "INSERT INTO habits (habit_name, quantity) VALUES(@habit_name, @quantity)";
                
                using(var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@habit_name", habitName);
                    command.Parameters.AddWithValue("@quantity", habitQuantity);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Habit inserted succesfully.\n\n");
        }

        public void DeleteHabits()
        {
           
            int id = 0;
            while (true)
            {
                Console.Write("Enter habit id that is to be deleted: ");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Enter valid integer");
                }
            }

            using (var connection = new SQLiteConnection("Data Source= habits.db"))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM habits WHERE id = @id";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("\nHabit deleted successfully.\n\n");
        }


        public void UpdateHabits()
        {
            int id = 0;
            while (true)
            {
                Console.Write("Enter habit id to be updated: ");
                if(int.TryParse(Console.ReadLine(), out id))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Enter valid integer");
                }
            }


            Console.Write("Enter new quantity: ");
            string? newQuanity = Console.ReadLine();

            using(var connection = new SQLiteConnection("Data Source = habits.db"))
            {
                connection.Open();
                string updateQuery = "UPDATE habits SET quantity = @quantity WHERE id = @id";
                using(var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@quantity", newQuanity);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("\nHabit updated succesfully.\n\n");
        }
    }

}