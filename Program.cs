namespace LibraryManagementADONorm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            Console.WriteLine("Enter title:");
            string Title = Console.ReadLine();
            Console.WriteLine("Enter author:");
            string Author = Console.ReadLine();
            Console.WriteLine("Enter genre:");
            string Genre = Console.ReadLine();
            Console.WriteLine("Enter borrower:");
            string Borrower = Console.ReadLine();

            Book book = new Book(Title, Author, Genre, Borrower);

            int result = library.AddBook(Title,Author,Genre,Borrower);
            Console.WriteLine(result);
        }
    }
}