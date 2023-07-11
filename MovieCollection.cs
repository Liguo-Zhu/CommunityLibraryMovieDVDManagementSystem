namespace CommunityLibraryMovieDVDManagementSystem
{
    public class MovieCollection
    {
        //fields
        private int count; //record the number of movies currently stored in the hashtable
        private int buckets; //size of the hash table
        private Movie[] movies; //array (as a hash talbe)
        private Movie[] rentedMoiveRecords;//record the number of times all movies has been borrowed
        public Movie[] RentedMoiveRecords { get { return rentedMoiveRecords; } } //read only value

        //constructor
        public MovieCollection(int buckets)
        {
            if (buckets > 0)
            {
                this.buckets = buckets;
            }
            count = 0;
            movies = new Movie[buckets];
            rentedMoiveRecords = new Movie[buckets*2];//Recording size is twice that of movie size

            for (int i = 0; i < buckets; i++)//initialization
            {
                movies[i] = new Movie("99999","99999" ,"99999", 99999);
            }

            for(int i = 0; i < buckets*2; i++)//initialization
            {
                rentedMoiveRecords[i] = new Movie("77777", "77777", "77777", 77777);
            }
        }

        //get the hash code of a movie via its title
        private int Hashing(string title)
        {
            int m = buckets;
            int p = 31;
            int hashCode = 0;
            for (int i = 0; i < title.Length; i++)
            {
                hashCode = p * hashCode + title[i];
            }
            //Console.WriteLine($"{(Math.Abs(hashCode) % m)}");
            return (Math.Abs(hashCode) % m);
        }

        //merge method for sorting rented movie records array
        private void Merge(Movie[] movies, int left, int mid, int right)
        {
            Movie[] temp = new Movie[buckets*2];//the size of the temp need to be the same with 'rentedMoiveRecords'
            int i, left_end, num_elements, tmp_pos;
            left_end = (mid - 1);
            tmp_pos = left;
            num_elements = (right - left + 1);
            while ((left <= left_end) && (mid <= right))
            {
                if (movies[left].RentedRecords <= movies[mid].RentedRecords)
                {
                    temp[tmp_pos++] = movies[left++];
                }
                else
                {
                    temp[tmp_pos++] = movies[mid++];
                }
            }
            while (left <= left_end)
            {
                temp[tmp_pos++] = movies[left++];
            }
            while (mid <= right)
            {
                temp[tmp_pos++] = movies[mid++];
            }
            for (i = 0; i < num_elements; i++)
            {
                movies[right] = temp[right];
                right--;
            }
        }

        //merge sort method for sorting rented movie records array
        private void MergeSort(Movie[] movies, int left, int right)
        {
            int mid;
            if (right > left)
            {
                mid = (right + left) / 2;
                MergeSort(movies, left, mid);
                MergeSort(movies, (mid + 1), right);
                Merge(movies, left, (mid + 1), right);
            }
        }

        //get the top3 rental movie records
        public void GetTop3RentedMoiveRecords()
        {
            //sort the records of each movie
            MergeSort(rentedMoiveRecords, 0, rentedMoiveRecords.Length - 1);
            //---------------------------------------------------------------------------------------
            int[] recordArray = new int[rentedMoiveRecords.Length];
            int countRecordNotZero = 0; //count those movies whose borrowing record is not 0.
            //---------------------------------------------------------------------------------------
            foreach (var item in rentedMoiveRecords)
            {
                if (item.Title != "77777" && item.RentedRecords != 0) //if the movie is not empty and its RentedRecords is not 0.
                {
                    recordArray[countRecordNotZero] = item.RentedRecords; // add the movie to recordArray[]
                    countRecordNotZero++;
                }
            }

            Console.WriteLine("Top three(3) most frequently borrowed movie DVDs:");
            Console.WriteLine("-------------------------------------------------");

            if (countRecordNotZero == 0) //No records
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are no movie borrowing records. So there is no top3 records.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else //Has records
            {
                //---------------------------------------------------------------------------------------
                //(1) traverse recordArray[] and one by one copy unique elements of recordArray[] to temp[]. 
                int[] temp = new int[countRecordNotZero];
                int j = 0;
                for (int i = 0; i < countRecordNotZero - 1; i++)//if current element is not equal to next element then store that current element
                {
                    if (recordArray[i] != recordArray[i + 1])
                    {
                        temp[j++] = recordArray[i];
                    }
                }
                temp[j++] = recordArray[countRecordNotZero - 1];
                //---------------------------------------------------------------------------------------
                //(2) descendingUniqueArr[]: store unique values from temp[] in descending order
                int k = 0;
                int[] descendingUniqueArr = new int[j];
                for (int i = j - 1; i >= 0; i--)
                {
                    descendingUniqueArr[k] = temp[i];
                    k++;
                }
                //---------------------------------------------------------------------------------------
                //(3)find the corresponding movies according to the number of times in the descending order array
                if (descendingUniqueArr.Length == 1)
                {
                    Console.WriteLine();
                    Console.Write("No.1: ");
                    foreach (var record in rentedMoiveRecords)
                    {
                        if (record.RentedRecords == descendingUniqueArr[0])
                        {
                            Console.Write(record.Title + "_" + record.RentedRecords + "; ");
                        }
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No No.2 or others.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (descendingUniqueArr.Length == 2)
                {
                    Console.WriteLine();
                    Console.Write("No.1: ");
                    foreach (var record in rentedMoiveRecords)
                    {
                        if (record.RentedRecords == descendingUniqueArr[0])
                        {
                            Console.Write(record.Title + "_" + record.RentedRecords + "; ");
                        }
                    }
                    Console.WriteLine();
                    Console.Write("No.2: ");
                    foreach (var record in rentedMoiveRecords)
                    {
                        if (record.RentedRecords == descendingUniqueArr[1])
                        {
                            Console.Write(record.Title + "_" + record.RentedRecords + "; ");
                        }
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No No.3 or others.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else //descendingUniqueArr.Length >=3
                {
                    Console.WriteLine();
                    Console.Write("No.1: ");
                    foreach (var record in rentedMoiveRecords)
                    {
                        if (record.RentedRecords == descendingUniqueArr[0])
                        {
                            Console.Write(record.Title + "_" + record.RentedRecords + "; ");
                        }
                    }
                    Console.WriteLine();
                    Console.Write("No.2: ");
                    foreach (var record in rentedMoiveRecords)
                    {
                        if (record.RentedRecords == descendingUniqueArr[1])
                        {
                            Console.Write(record.Title + "_" + record.RentedRecords + "; ");
                        }
                    }
                    Console.WriteLine();
                    Console.Write("No.3: ");
                    foreach (var record in rentedMoiveRecords)
                    {
                        if (record.RentedRecords == descendingUniqueArr[2])
                        {
                            Console.Write(record.Title + "_" + record.RentedRecords + "; ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("---------------------------------------------------------------------------");
            }
            Console.WriteLine("---------------------------------------------------------------------------");
        }

        //add a new movie to the rented records
        public void AddNewMoiveRecords(Movie movie)
        {
            if(SeachMoiveInRentedMovieRecords(movie.Title) == -1)
            {
                int index = FindInsertPositionInRentedMovieRecords();
                rentedMoiveRecords[index] = movie;
            }
        }

        //search a movie if it is in the rented movie records
        private int SeachMoiveInRentedMovieRecords(string title)
        {
            int length = rentedMoiveRecords.Length;
            for(int i=0; i <length; i++)
            {
                if(rentedMoiveRecords[i].Title == (title))
                {
                    return i;
                }
            }
            return -1;
        }

        //find a insert position in the rented movie records array
        private int FindInsertPositionInRentedMovieRecords()
        {
            int i = 0;
            int length = rentedMoiveRecords.Length;
            while ((i < length) && (rentedMoiveRecords[i].Title != "77777"))
            {
                i++;
            }
             return i;//find the title, and return its position
        }

        //add a rented record to a movie that has been borrowed
        public void AddRentedMoiveRecords(string title)
        {
            int index = SeachMoiveInRentedMovieRecords(title);
            rentedMoiveRecords[index].AddRentedRecord();
        }
        
        //search a movie whether it is in the system
        public int SearchMovie(Movie movie)
        {
            int bucket = Hashing(movie.Title);
            int i = 0;
            int offset = 0;
            while ((i < buckets) &&
                (movies[(bucket + offset) % buckets].Title != movie.Title) &&
                (movies[(bucket + offset) % buckets].Title != "88888"))
                {
                    i++;
                    offset = i * i; //qudratic probing
                }
            if (movies[(bucket + offset) % buckets].Title == movie.Title)
            {
                return (offset + bucket) % buckets;//find the movie in the collection
            }
            else
            {
                return -1;//not found the movie in the collection
            }
        }

        //find the insertion index
        private int Find_Insertion_Bucket(Movie movie)
        {
            int bucket = Hashing(movie.Title);
            int i = 0;
            int offset = 0;
            while ((i < buckets) &&
                (movies[(bucket + offset) % buckets].Title != "99999") &&
                (movies[(bucket + offset) % buckets].Title != "88888"))
                {
                    i++;
                    offset = i * i;
                }
            return (offset + bucket) % buckets;
        }

        //initial configuration for movie
        public void InitAddMovie(Movie movie)
        {
            if ((count < movies.Length) && (SearchMovie(movie) == -1))
            {
                int bucket = Find_Insertion_Bucket(movie);
                movies[bucket] = movie;
                count++;
                movie.DVDsNumber++;
            }
            else
            {
                Console.WriteLine("The key has already been in the hashtable or the hashtable is full");
            }
        }

        //add a new movie to the system
        public void AddNewMovie(Movie movie)
        {
            int result = SearchMovie(movie);
            if (result == -1)//not found the movie title then check the hashtable whether it is full
            {
                if (count < movies.Length)//the hashtable is not full then add the new movie
                {
                    int bucket = Find_Insertion_Bucket(movie);
                    movies[bucket] = movie;
                    count++;
                    movie.DVDsNumber++;
                    movies[bucket].WhoBorrowedDVD.Add(movie.Title);

                    // add to the record
                    AddNewMoiveRecords(movie);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} has added to the system.", movie.Title);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else//the hashtable is full then cannot add the new movie
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The hashtable is full.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else//has found the movie
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The movie is in the system. You need to use the menu 2:");
                Console.WriteLine("==> 2. Add new DVDs of an existing movie to the system");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //add a exist movie to the system, which means increasing 1 DVD number of the movie
        public void AddExistMovie(Movie movie)
        {
            int bucket = SearchMovie(movie);
            movies[bucket].DVDsNumber++;
            movies[bucket].WhoBorrowedDVD.Add(movie.Title);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("DVD {0} has added (returned) to the system.", movie.Title);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //check DVD number of a movie
        public bool CheckDVDNumberOfMovie(Movie movie)
        {
            int bucket = SearchMovie(movie);
            if (bucket != -1 && movies[bucket].DVDsNumber > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //decrease 1 DVD from a moive
        public void DecreseOneDVDInMovie(Movie movie)
        {
            int bucket = SearchMovie(movie);
            if (movies[bucket].DVDsNumber > 0)
            {
                movies[bucket].DVDsNumber--;
                movies[bucket].WhoBorrowedDVD.Remove(movie.Title);
            }
            if (movies[bucket].DVDsNumber <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The number of DVDs for this movie {movie.Title} is 0.");
                Console.ForegroundColor = ConsoleColor.White;
                movies[bucket] = new Movie("88888", "88888", "88888", 88888);
                count--;
            }
        }

        //remove a exist movie from the system
        public void RemoveExistMovie(Movie movie)
        {
            int bucket = SearchMovie(movie);
            if (movies[bucket].DVDsNumber > 0 )
            {
                movies[bucket].DVDsNumber--;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"A DVD of movie {movie.Title} has been removed.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (movies[bucket].DVDsNumber == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The number of DVDs for this movie {movie.Title} is 0.");
                Console.ForegroundColor = ConsoleColor.White;
                movies[bucket] = new Movie("88888", "88888", "88888", 88888);
                count--;
            }
            Print();
        }

        //browse all movies in the system
        public void BrowseAllMoviesInCollection()
        {
            int j = 1;
            List<string> mList = new List<string>();
            Console.WriteLine("All movies in the system: ");
            Console.WriteLine("------------------------");
            Console.WriteLine("Title---DVDs Number");
            for (int i = 0; i < buckets; i++)
            {
                if ((movies[i].Title != "99999") && (movies[i].Title != "88888"))
                {
                    mList.Add(movies[i].Title + "---" +movies[i].DVDsNumber);
                }
            }
            mList.Sort();
            foreach(var m in mList)
            {
                Console.WriteLine($" {j}) {m}");
                j++;
            }
            Console.WriteLine("------------------------");
        }

        //display information of a moive
        public void PrintInfoOfAMovie(Movie movie)
        {
            Console.WriteLine();
            int bucket = SearchMovie(movie);
            if (bucket != -1)
            {
                Console.WriteLine("Movie Information:");
                Console.WriteLine("------------------");
                Console.WriteLine(" 1) Title: " + movies[bucket].Title);
                Console.WriteLine(" 2) DVD number: " + movies[bucket].DVDsNumber);
                Console.WriteLine(" 3) Genre: " + movies[bucket].Genre);
                Console.WriteLine(" 4) Classification: " + movies[bucket].Classification);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The movie not found!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        // print all the movies in the system
        public void Print()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Movie Collection:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[index] Movie Title___DVDs Number");
            Console.WriteLine("----------------------------------");
            for (int i = 0; i < buckets; i++)
            {
                if ((movies[i].Title == "99999") || (movies[i].Title == "88888"))
                {
                    Console.Write("X, ");
                }
                else
                {
                    Console.Write("[" + i + "]" + movies[i].Title + "_" + movies[i].DVDsNumber + ", ");
                }
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine();
            /*Console.WriteLine("Movie Rented Records:");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i=0; i<rentedMoiveRecords.Length; i++)
            {
                if(rentedMoiveRecords[i].Title !="77777")
                Console.Write(rentedMoiveRecords[i].Title + "-" + rentedMoiveRecords[i].RentedRecords + ", ");
            }
            Console.WriteLine();*/
        }

        //clear all movies
        private void ClearAllMovie()
        {
            count = 0;
            for (int i = 0; i < buckets; i++)
                movies[i] = new Movie("88888", "88888", "88888", 88888);
        }
    }
}

