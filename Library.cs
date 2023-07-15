using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementADONorm

{
    public class Library    {

        private string connectionString;

        public Library()
        {
            connectionString = "Data Source=DESKTOP-41GBJMF; Database=LibraryMangNorm; Integrated Security=true";
        }

        public int AddBook(Book book)
        {
            int bookId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("AddBookss", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@title", SqlDbType.VarChar, 100).Value =title;
                    command.Parameters.Add("@author", SqlDbType.VarChar, 100).Value = author;
                    command.Parameters.Add("@genre", SqlDbType.VarChar, 100).Value = genre;
                    command.Parameters.Add("@borrower", SqlDbType.VarChar, 100).Value = borrower;

                    SqlParameter bookIdParameter = new SqlParameter("@bookId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(bookIdParameter);

                    command.ExecuteNonQuery();

                    if (bookIdParameter.Value != DBNull.Value)
                    {
                        bookId = Convert.ToInt32(bookIdParameter.Value);
                    }
                }
            }

            return bookId;
        }

        public void PrintTables()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("Authors Table:");
                using (SqlCommand command = new SqlCommand("SELECT * FROM Authors", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int authorId = Convert.ToInt32(reader["AuthorId"]);
                            string authorName = reader["AuthorName"].ToString();
                            Console.WriteLine($"AuthorId: {authorId}, AuthorName: {authorName}");
                        }
                    }
                }

                Console.WriteLine("\nGenres Table:");
                using (SqlCommand command = new SqlCommand("SELECT * FROM Genres", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int genreId = Convert.ToInt32(reader["GenreId"]);
                            string genreName = reader["GenreName"].ToString();
                            Console.WriteLine($"GenreId: {genreId}, GenreName: {genreName}");
                        }
                    }
                }

                Console.WriteLine("\nBooks Table:");
                using (SqlCommand command = new SqlCommand("SELECT * FROM Books", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bookId = Convert.ToInt32(reader["BookId"]);
                            string bookTitle = reader["Title"].ToString();
                            int authorId = Convert.ToInt32(reader["AuthorId"]);
                            int genreId = Convert.ToInt32(reader["GenreId"]);
                            Console.WriteLine($"BookId: {bookId}, Title: {bookTitle}, AuthorId: {authorId}, GenreId: {genreId}");
                        }
                    }
                }

                Console.WriteLine("\nBorrowedBooks Table:");
                using (SqlCommand command = new SqlCommand("SELECT * FROM BorrowedBooks", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int borrowedBookId = Convert.ToInt32(reader["BorrowedBookId"]);
                            int bookId = Convert.ToInt32(reader["BookId"]);
                            string borrowerName = reader["BorrowerName"].ToString();
                            Console.WriteLine($"BorrowedBookId: {borrowedBookId}, BookId: {bookId}, BorrowerName: {borrowerName}");
                        }
                    }
                }
            }
        }
    }
}