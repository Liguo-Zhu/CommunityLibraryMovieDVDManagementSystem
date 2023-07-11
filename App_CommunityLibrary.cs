using System;

namespace CommunityLibraryMovieDVDManagementSystem
{
    public class App_CommunityLibrary
    {
        
        static void Main(string[] args)
        {
            //---Set storage capacity-------
            int sizeOfMember = 15;//inclue 1 staff, so the maximum members will be 13
            int sizeOfMovie = 11;
            //---Set the number of initial movies and members-------
            int numberOfMember = 10;
            int numberOfMovie = 7;
            //---Set the initial number of DVDs per movie-------
            int dvdNumberOfPerMovie = 5;

            //========================================================================
            //---Create 3 instantiated objects----------------------------------------
            MemberCollection membersCollection = new MemberCollection(sizeOfMember);
            MovieCollection moviesCollection = new MovieCollection(sizeOfMovie);
            Member memberLoginRecord = new Member();
            //---Create a staff account------------------------------------
            var staff = new Member();
            staff.FirstName = "staff";
            staff.LastName = "staff";
            staff.PhoneNumber = "04-1111-6666";
            staff.Password = "today123";
            membersCollection.AddMember(staff);
            //---Add some members------------------------------------------
            for (int i = 1; i < numberOfMember+1; i++)
            {
                string firsName = "f" + i;
                string lastName = "f" + i;
                string phoneNumber = "04-1111-000"+(i - 1);
                string password = "123";
                var member = new Member(firsName, lastName, phoneNumber, password);
                membersCollection.AddMember(member);
            }
            //---Add some movies-------------------------------------------
            for (int i = 1; i < numberOfMovie+1; i++)
            {
                string title = "a" + i;
                string genre = "Sci-Fi";
                string classification = "M15+";
                int duration = 180;
                var movie = new Movie(title, genre,classification,duration);
                movie.DVDsNumber = dvdNumberOfPerMovie-1;
                moviesCollection.InitAddMovie(movie);
                moviesCollection.AddNewMoiveRecords(movie);
            }

            //---Print after initial configuration-------------------------
            Console.WriteLine("================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
            Console.WriteLine(">>> App Initialization...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("================================================");
            membersCollection.Print();
            moviesCollection.Print();
            //-------------------------------------------------------------
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("In each menu, add a new function called 'Print All Info'.");
            Console.WriteLine("It will display the information about current Member Collection and Movie Collection.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine(">>> Complete initialization...");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter any key to enter the main menu >>>");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();
            //---Run the App-----------------------------------------------
            RunApp(membersCollection, moviesCollection, memberLoginRecord);
            //-------------------------------------------------------------
            Console.ReadKey();
        }

        //start the App
        private static void RunApp(MemberCollection membersCollection, MovieCollection moviesCollection, Member memberLoginRecord)
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("================================================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("================================================");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Main Menu");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Select from the following:");
                Console.WriteLine("1. Staff");
                Console.WriteLine("2. Member");
                Console.WriteLine("3. End the program");
                Console.WriteLine("-------------------");
                Console.WriteLine("4. Print All Info");
                Console.WriteLine("-----------------------------");
                Console.Write("Enter your choice ==> ");
                int choice = InputOneNumber(1, 4);
                if (choice == 1)//sub-menu: StaffMenu
                {
                    //Verify user information
                    bool isValidStaff = CheckMemberOrStaffInfo(membersCollection, memberLoginRecord, choice);
                    if (isValidStaff)
                    {
                        StaffMenu(membersCollection, moviesCollection);
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: wrong name or wrong password. Go back to Main Menu.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                    }
                }
                else if (choice == 2)//sub-meun: MemberMenu
                {
                    //Verify user information 
                    bool isValidStaff = CheckMemberOrStaffInfo(membersCollection, memberLoginRecord, choice);
                    if (isValidStaff)
                    {
                        MemberMenu(membersCollection, moviesCollection, memberLoginRecord);
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: wrong name or wrong password. Go back to Main Menu.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                    }
                }
                else if(choice == 4)//Print the system information: members and movies
                {
                    Console.WriteLine();
                    membersCollection.Print();
                    moviesCollection.Print();
                }
                else//Exit the system
                {
                    break;
                }
            }
        }

        //valify a user information
        private static bool CheckMemberOrStaffInfo(MemberCollection personSet, Member memberLoginRecord, int choice)
        {
            var person = new Member();
            bool isValid = false;
            if (choice == 1)//go to the staff menu
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("===== Staff Login =====");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter Username: ");
                person.FirstName = Console.ReadLine();
                Console.Write("Enter Password: ");
                person.Password = Console.ReadLine();
                isValid = personSet.SearchMemberByNameAndPassword(person, choice);
            }
            else//go to the member munu
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("===== Member Login =====");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter FirstName: ");
                person.FirstName = Console.ReadLine();
                Console.Write("Enter LastName: ");
                person.LastName = Console.ReadLine();
                Console.Write("Enter Password: ");
                person.Password = Console.ReadLine();
                isValid = personSet.SearchMemberByNameAndPassword(person, choice);
            }
            if(isValid)
            {
                memberLoginRecord.FirstName = person.FirstName;
                memberLoginRecord.LastName = person.LastName;
            }
            return isValid;
        }
        
        //staff menu
        private static void StaffMenu(MemberCollection membersCollection, MovieCollection moviesCollection)
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine();
                Console.WriteLine("=================================================================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Staff Menu");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("1. Add DVDs of a new movie to the system");
                Console.WriteLine("2. Add new DVDs of an existing movie to the system");
                Console.WriteLine("3. Remove DVDs of an existing movie from the system");
                Console.WriteLine("4. Register a new member to the system");
                Console.WriteLine("5. Remove a registered member from the system");
                Console.WriteLine("6. Find a member's contact phone number, given the member's name");
                Console.WriteLine("7. Find members who are currently renting a particular movie");
                Console.WriteLine("8. Return to main menu");
                Console.WriteLine("-------------------");
                Console.WriteLine("9. Print All Info");
                Console.WriteLine();
                Console.WriteLine("Username: {0}_{1}", membersCollection.Members[0].FirstName, membersCollection.Members[0].LastName);
                Console.Write("Enter your choice ==> ");
                int choice = InputOneNumber(1, 9);
                if(choice == 8)
                {
                    break;
                }
                else
                {
                    switch (choice)
                    {
                        case 1:
                            AddNewDVDs1(moviesCollection); break;
                        case 2:
                            AddExistDVDs2(moviesCollection);  break;
                        case 3:
                            RemoveExistDVDs3(moviesCollection); break;
                        case 4:
                            RegisterNewMember4(membersCollection); break;
                        case 5:
                            RemoveRegisterMember5(membersCollection); break;
                        case 6:
                            FindMemberPhoneNumber6(membersCollection); break;
                        case 7:
                            FindMembersWhoRentSpecificMovie7(membersCollection); break;
                        case 9:
                            membersCollection.Print();
                            moviesCollection.Print();
                            break;
                        default: break;
                    }
                }
            }
        }

        //add a new DVD to the system
        private static void AddNewDVDs1(MovieCollection moviesCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 1. Add DVDs of a new movie to the system:");
            Console.ForegroundColor = ConsoleColor.White;
            var newMovie = new Movie("", "Sci-Fi", "M15+", 90); //each field must be assigned value
            Console.Write("Enter Movie Title: ");
            newMovie.Title = Console.ReadLine();
            moviesCollection.AddNewMovie(newMovie);
        }

        //add a exist DVD to the system
        private static void AddExistDVDs2(MovieCollection moviesCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 2. Add new DVDs of an existing movie to the system:");
            Console.ForegroundColor = ConsoleColor.White;
            var newMovie = new Movie();
            Console.Write("Enter Movie Title: ");
            newMovie.Title = Console.ReadLine();
            int result = moviesCollection.SearchMovie(newMovie);

            if (result == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The movie is not in the system. You need to use the menu 1:");
                Console.WriteLine("==> 1. Add DVDs of a new movie to the system");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The movie is in the system.");
                Console.ForegroundColor = ConsoleColor.White;
                moviesCollection.AddExistMovie(newMovie);

            }
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------");
        }

        //remove a exist DVD from the system
        private static void RemoveExistDVDs3(MovieCollection moviesCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 3. Remove DVDs of an existing movie from the system:");
            Console.ForegroundColor = ConsoleColor.White;
            var newMovie = new Movie();
            Console.Write("Enter Movie Title: ");
            newMovie.Title = Console.ReadLine();
            int result = moviesCollection.SearchMovie(newMovie);
            if (result == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The movie is not in the system.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                moviesCollection.RemoveExistMovie(newMovie);
            }
        }

        //register a new member in the system
        private static void RegisterNewMember4(MemberCollection membersCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 4. Register a new member to the system:");
            Console.ForegroundColor = ConsoleColor.White;
            var newMember = new Member();
            Console.Write("Enter FirstName: ");
            newMember.FirstName = Console.ReadLine();
            Console.Write("Enter LastName: ");
            newMember.LastName = Console.ReadLine();
            Console.Write("Enter PhoneNumber: ");
            newMember.PhoneNumber = Console.ReadLine();
            Console.Write("Enter password: ");
            newMember.Password = Console.ReadLine();
            bool isInTheSystem = membersCollection.SearchMemberByName(newMember);
            if (isInTheSystem)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The name is in the system. Please enter a different name.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                membersCollection.AddMember(newMember);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Registered successfully.");
                Console.ForegroundColor = ConsoleColor.White;
                membersCollection.Print();
            }
        }

        //remove a register member from the system
        private static void RemoveRegisterMember5(MemberCollection membersCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 5. Remove a registered member from the system:");
            Console.ForegroundColor = ConsoleColor.White;
            var member = new Member();
            Console.Write("Enter FirstName: ");
            member.FirstName = Console.ReadLine();
            Console.Write("Enter LastName: ");
            member.LastName = Console.ReadLine();
            int flag = membersCollection.RemoveMember(member);
            if (flag == 1)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">> Member {0} found, and deleted successfully.", member.FirstName);
                Console.ForegroundColor = ConsoleColor.White;
                membersCollection.Print();
            }
            else if (flag == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This member has borrowed DVDs, so you cannot remove the member!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This member not found!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //find a member's phone number
        private static void FindMemberPhoneNumber6(MemberCollection membersCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 6. Find a member's contact phone number, given the member's name:");
            Console.ForegroundColor = ConsoleColor.White;
            var member = new Member();
            Console.Write("Enter FirstName: ");
            member.FirstName = Console.ReadLine();
            Console.Write("Enter LastName: ");
            member.LastName = Console.ReadLine();
            membersCollection.FindMember(member);
        }

        //find members who has rented a specific move in the system
        private static void FindMembersWhoRentSpecificMovie7(MemberCollection membersCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 7. Find members who are currently renting a particular movie:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter the movie tile: ");
            string movieTitle = Console.ReadLine();
            membersCollection.FindWhoRentParticularMovie(movieTitle);
        }

        //member menu
        private static void MemberMenu(MemberCollection membersCollection, MovieCollection moviesCollection, Member memberLoginRecord)
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine();
                Console.WriteLine("==========================================================================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Member Menu");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("1. Browse all the movies");
                Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
                Console.WriteLine("3. Borrow a movie DVD");
                Console.WriteLine("4. Return a movie DVD");
                Console.WriteLine("5. List current borrowing movies");
                Console.WriteLine("6. Display the top 3 movies rented by the members");
                Console.WriteLine("7. Return to main menu");
                Console.WriteLine("-------------------");
                Console.WriteLine("8. Print All Info");
                Console.WriteLine();
                Console.WriteLine("Member Name: {0}_{1}", memberLoginRecord.FirstName, memberLoginRecord.LastName);
                Console.Write("Enter your choice ==> ");
                int choice = InputOneNumber(1, 8);//get the input value
                if (choice == 7)//exit the sub-munu
                {
                    break;
                }
                else
                {
                    switch (choice)
                    {
                        case 1:
                            BrowseAllMovies1(moviesCollection); break;
                        case 2:
                            DisplayInfoOfMovie2(moviesCollection); break;
                        case 3:
                            BorrowAMovieDVD3(membersCollection, moviesCollection, memberLoginRecord); break;
                        case 4:
                            ReturnAMovieDVD4(membersCollection, moviesCollection, memberLoginRecord); break;
                        case 5:
                            ListCurrentBorrowedMovies5(membersCollection, moviesCollection, memberLoginRecord); break;
                        case 6:
                            DisplayTop3RentedMoiveRecords6(moviesCollection); break;
                        case 8:
                            Console.WriteLine();
                            membersCollection.Print();
                            moviesCollection.Print();
                            break;
                        default: break;
                    }
                }
            }
        }

        //browse all movies in the system
        private static void BrowseAllMovies1(MovieCollection moviesCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 1. Browse all the movies:");
            Console.ForegroundColor = ConsoleColor.White;
            moviesCollection.BrowseAllMoviesInCollection();
        }

        //display information of all the movies in the system
        private static void DisplayInfoOfMovie2(MovieCollection moviesCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 2. Display all the information about a movie, given the title of the movie");
            Console.ForegroundColor = ConsoleColor.White;
            var newMovie = new Movie();
            Console.Write("Enter Movie Title: ");
            newMovie.Title = Console.ReadLine();
            moviesCollection.PrintInfoOfAMovie(newMovie);
        }

        //borrow a movie DVD from the system
        private static void BorrowAMovieDVD3(MemberCollection membersCollection, MovieCollection moviesCollection, Member memberLoginRecord)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 3. Borrow a movie DVD:");
            Console.ForegroundColor = ConsoleColor.White;
            var movie = new Movie();
            moviesCollection.BrowseAllMoviesInCollection();
            Console.Write("Enter Movie Title: ");
            movie.Title = Console.ReadLine();
            bool canBorrow = membersCollection.CheckMemberCanBorrowDVD(memberLoginRecord, movie.Title);
            bool hasMovie = moviesCollection.CheckDVDNumberOfMovie(movie);

            if (hasMovie && canBorrow)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have successfully borrowed movie {0}.", movie.Title);
                Console.ForegroundColor = ConsoleColor.White;
                moviesCollection.DecreseOneDVDInMovie(movie);
                membersCollection.BorrowAMovieDVD(memberLoginRecord, movie.Title);
                moviesCollection.AddRentedMoiveRecords(movie.Title);//the movie adds a borrowed record8 
            }
            else if(!hasMovie)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The DVD count is zero. Cannot be borrowed.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //return a movie DVD to the system
        private static void ReturnAMovieDVD4(MemberCollection membersCollection, MovieCollection moviesCollection, Member memberLoginRecord)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 4. Return a movie DVD:");
            Console.ForegroundColor = ConsoleColor.White;
            var movie = new Movie("", "Sci-Fi", "M15+", 90); //each field must be assigned value;
            Console.Write("Enter Movie Title: ");
            movie.Title = Console.ReadLine();
            Console.WriteLine(memberLoginRecord.FirstName + "_" + memberLoginRecord.LastName + ":");
            bool hasTheDVD = membersCollection.CheckMemberHasTheDVD(memberLoginRecord, movie.Title);
            int hasFound = moviesCollection.SearchMovie(movie);

            if (hasTheDVD && hasFound == -1)
            {
                moviesCollection.AddNewMovie(movie);
                membersCollection.RemoveMemberBorrowedMovieDVD(memberLoginRecord, movie.Title);
            }
            else if(hasTheDVD && hasFound != -1)
            {
                moviesCollection.AddExistMovie(movie);
                membersCollection.RemoveMemberBorrowedMovieDVD(memberLoginRecord, movie.Title);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You did not borrow this movie.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //list current movies that have been borrowed
        private static void ListCurrentBorrowedMovies5(MemberCollection membersCollection, MovieCollection moviesCollection, Member memberLoginRecord)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 5. List current borrowing movies:");
            Console.ForegroundColor = ConsoleColor.White;
            membersCollection.ListBorrowedMovies(memberLoginRecord);
        }

        //display the top3 rented movies
        private static void DisplayTop3RentedMoiveRecords6(MovieCollection moviesCollection)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== 6. Display the top 3 movies rented by the members:");
            Console.ForegroundColor = ConsoleColor.White;
            moviesCollection.GetTop3RentedMoiveRecords();
        }
        
        //input a valid number
        private static int InputOneNumber(int min, int max)//min cannot be 0 as if input is a string, the output is 0
        {
            int.TryParse(Console.ReadLine(), out int number);//checking for a valid integer
            while (number < min || number > max)//continuously prompting users for valid number.  
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("    Invalid Enter!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write((max - min) > 1 ? $"--> Reenter the number({min} to {max}): " : $"--> Reenter the number({min} or {max}): ");
                int.TryParse(Console.ReadLine(), out number);
            }
            return number;
        }
    }
}