namespace CommunityLibraryMovieDVDManagementSystem
{
    public class Member
    {
        //fields
        public string FirstName { get; set; }
        public string LastName { get; set; } //Drama, Adventure, Family, Action, Sci-Fi, Comedy, Animated, Thriller, or Other.
        public string PhoneNumber { get; set; } //General (G), Parental Guidance (P, Mature (M15+), or Mature Accompanied (MA15+).
        public string Password { get; set; }
        public List<string> BorrowedDVDs { get; set; }

        //default constructor
        public Member()  {   }

        //constructor
        public Member(string firstName, string lastName, string phoneNumber, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Password = password;
            BorrowedDVDs = new List<string>();
        }

        public void Print()
        {
            Console.Write($"{FirstName}, {LastName}, {PhoneNumber}, {Password}: ");
            //Console.WriteLine($"FirstName: {FirstName}, LastName: {LastName}, PhoneNumber: {PhoneNumber}, Password: {Password}");
            foreach (var movie in BorrowedDVDs)
            {
                Console.Write(movie + ", "); 
            }
            Console.WriteLine();
        }

        //member borrow a movie
        public void BorrowMovie(string movieTitle)
        {
            BorrowedDVDs.Add(movieTitle);
        }

        //member return a movie
        public void RemoveMovie(string movieTitle)
        {
            BorrowedDVDs.Remove(movieTitle);
        }

    }//end of Member class
}
