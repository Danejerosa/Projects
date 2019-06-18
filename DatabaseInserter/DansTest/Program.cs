using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DansTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string screwedUpReviews = string.Empty;
            // CHANGE THIS FOR YOUR DATABASE INFO. DANE'S IS PORT 5433 SO IT WAS ADDED
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=Fart;Database=Milestone1DB;Port=5433;");
            conn.Open();

            // YOU CAN HARDCORE FOLDER/FILE NAME HERE. Make sure to use the @ at the start so the \ aren't all fucked up
            // Format: @"C:\Users\User\Desktop\team4\JSONInserter\JSONInserter\yelp_user.sql"
            using (StreamReader sr = new StreamReader(@"C:\Users\User\Desktop\team4\JSONInserter\JSONInserter\yelp_review.SQL"))
            {
                string line;
                int rowsInput = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line[line.Length-1] == ';')
                    {
                        line = line.Substring(0, line.Length - 1);
                        line += " ON CONFLICT do nothing;";
                        screwedUpReviews += line;
                        var cmd = new NpgsqlCommand(screwedUpReviews, conn);
                        cmd.ExecuteNonQuery();
                        Console.Write("Rows Input: " + ++rowsInput + "\r");
                        screwedUpReviews = string.Empty;
                    }
                    else
                    {
                        screwedUpReviews += line;
                    }
                }
            }
            // Close connection
            conn.Close();
            Console.Write("\nALL DONE");
            Console.ReadKey();
        }
    }
}
