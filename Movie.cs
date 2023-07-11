namespace CommunityLibraryMovieDVDManagementSystem
{
    public class Movie
    {
        //fields
        public string Title { get; set; }
        public string Genre { get; set; } //Drama, Adventure, Family, Action, Sci-Fi, Comedy, Animated, Thriller, or Other.
        public string Classification { get; set; } //General (G), Parental Guidance (PG), Mature (M15+), or Mature Accompanied (MA15+).
        public int Duration { get; set; } //in minutes
        public List<string> WhoBorrowedDVD { get; set; }
        public int DVDsNumber { get; set; }
        private int rentedRecords = 0;
        public int RentedRecords { get { return rentedRecords; } }


        //default constructor
        public Movie() { }
        //constructor
        public Movie(string title, string genre, string classification, int duration)
        {
            Title = title;
            Genre = genre;
            Classification = classification;
            Duration = duration;
            WhoBorrowedDVD = new List<string>();
        }

        //add rented record to the movie
        public void AddRentedRecord()
        {
            rentedRecords++;
        }

        //print information of itself
        public void Print()
        {
            Console.WriteLine($"Title: {Title}\nGenre: {Genre}\nClassification: {Classification}\nDuration: {Duration} minutes");
        }
    }//end of Movie class
}