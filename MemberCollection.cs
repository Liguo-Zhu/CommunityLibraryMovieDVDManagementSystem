using System.Diagnostics.Metrics;

namespace CommunityLibraryMovieDVDManagementSystem
{
    public class MemberCollection
    {
        //fields
        private int count;
        private Member[] members;
        public Member[] Members { get { return members; } set { members = value; } }
        private List<string> BorrowedMovieTitleSet;

        //constructor
        public MemberCollection(int size)
        {
            members = new Member[size];
            
            count = 0;
            for (int i = 0; i < members.Length; i++)
            {
                var m = new Member("X", "X", "X", "X");
                members[i] = m;
            }
            BorrowedMovieTitleSet = new List<string>();
        }

        //add a member to the system
        public void AddMember(Member person)
        {
            if (count < members.Length)
            {
                foreach (var member in members)
                {
                    if (member.FirstName.Equals("X"))
                    {
                        member.FirstName = person.FirstName;
                        member.LastName = person.LastName;
                        member.PhoneNumber = person.PhoneNumber;
                        member.Password = person.Password;
                        count++;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">> Member collection is full!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //remove a member from the system
        public int RemoveMember(Member person)
        {
            foreach (Member member in members)
            {
                if (member.FirstName!="X" && (member.FirstName).Equals(person.FirstName) && (member.LastName).Equals(person.LastName))
                {
                    if (member.BorrowedDVDs.Count() == 0)
                    {
                        count--;
                        member.FirstName = "X";
                        member.LastName = "X";
                        member.PhoneNumber = "X";
                        member.Password = "X";
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            return 3;
        }

        //search a  member by their name and password
        public bool SearchMemberByNameAndPassword(Member person, int choice) 
        {
            bool isValid = false;
            if(choice == 1) //search staff's name and password
            {
                foreach (Member member in members)
                {
                    if ((member.FirstName).Equals(person.FirstName) && (member.Password).Equals(person.Password))
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Staff successfully logged in.");
                        Console.ForegroundColor = ConsoleColor.White;
                        isValid = true;
                    }
                }
            }
            else //search member's firstname, lastname and password
            {
                foreach (Member member in members)
                {
                    if (person.FirstName != "s" && (member.FirstName).Equals(person.FirstName) && (member.LastName).Equals(person.LastName) && (member.Password).Equals(person.Password))
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Member successfully logged in.");
                        Console.ForegroundColor = ConsoleColor.White;
                        isValid = true;
                    }
                }
            }
            return isValid;
        }

        //search a member by their name only
        public bool SearchMemberByName(Member person) // fistname and lastname
        {
            foreach (Member member in members)
            {
                if (member.FirstName != "X" && (member.FirstName).Equals(person.FirstName) && (member.LastName).Equals(person.LastName))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    return true;
                }
            }
            Console.WriteLine();
            return false;
        }

        //a member borrow a movie's DVD
        public void BorrowAMovieDVD(Member person, string movieTitle) // fistname and lastname
        {
            foreach (Member member in members)
            {
                if (member.FirstName != "X" && (member.FirstName).Equals(person.FirstName) && (member.LastName).Equals(person.LastName))
                {
                    member.BorrowMovie(movieTitle);
                    BorrowedMovieTitleSet.Add(movieTitle);
                }
            }
        }

        //check a member whether can borrow the DVD
        public bool CheckMemberCanBorrowDVD(Member person, string movieTitle)
        {
            bool canBorrow = true;
            foreach (Member member in members)
            {
                if (member.FirstName != "X" && (member.FirstName).Equals(person.FirstName) && (member.LastName).Equals(person.LastName))
                {
                    bool a = member.BorrowedDVDs.Contains(movieTitle);
                    bool b = member.BorrowedDVDs.Count() < 5;
                    canBorrow = !a && b;
                    if (a)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You has borrowed the movie.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (!b)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have already borrowed 5 movies. So, you can no longer borrow..");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            return canBorrow;
        }

        //check a member whether has the DVD
        public bool CheckMemberHasTheDVD(Member person, string movieTitle)
        {
            bool hasTheDVD = false;
            foreach (Member member in members)
            {
                if (member.FirstName != "X" && (member.FirstName).Equals(person.FirstName) && (member.LastName).Equals(person.LastName))
                {
                    hasTheDVD = member.BorrowedDVDs.Contains(movieTitle);
                }
            }
            return hasTheDVD;
        }

        //remove a movie DVD from a member
        public void RemoveMemberBorrowedMovieDVD(Member person, string movieTitle)
        {
            foreach (Member member in members)
            {
                if (member.FirstName != "X" && (member.FirstName).Equals(person.FirstName) && (member.LastName).Equals(person.LastName))
                {
                    Console.WriteLine(">> Member {0} found.", member.FirstName);
                    member.RemoveMovie(movieTitle);
                    BorrowedMovieTitleSet.Remove(movieTitle);
                }
            }
            Console.WriteLine();
        }

        //find a member in the system
        public void FindMember(Member person)
        {
            bool isFound = false;
            foreach (var member in members)
            {
                if ((member.FirstName).Equals(person.FirstName) && (member.LastName).Equals(person.LastName))
                {
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("Phone number: {0}", member.PhoneNumber);
                    isFound = true;
                    break;
                }
            }
            Console.WriteLine();
            if (!isFound)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Member {0}_{1} not found!", person.FirstName, person.LastName);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //find who has rented a particular movie
        public void FindWhoRentParticularMovie(string movieTitle)
        {
            List<string> memberNames = new List<string>();
            foreach (var member in members)
            {
                bool isTrue = member.BorrowedDVDs.Contains(movieTitle);
                if (isTrue)
                {
                    memberNames.Add("name:" + member.FirstName + "_" + member.LastName);
                }
            }
            if(memberNames.Count > 0)
            {
                foreach (var name in memberNames)
                {
                    Console.WriteLine(name);
                }
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No member borrow the movie or the movie is not in the system.");
                Console.ForegroundColor = ConsoleColor.White;
            };
        }

        //list all the movies that borrowed by members
        public void ListBorrowedMovies(Member memberLoginRecord)
        {
            List<string> titleSet = new List<string>();
            foreach (var member in members)
            {
                if (member.FirstName == memberLoginRecord.FirstName && member.LastName == memberLoginRecord.LastName && member.FirstName != "staff")
                {
                    Console.Write("Borrowed movies: ");
                    foreach (var movie in member.BorrowedDVDs)
                    {
                        Console.Write(movie + ", ");
                    }
                    Console.WriteLine();
                }
            }
        }

        //print the information of all the movies in the system
        public void Print()
        {
            Console.WriteLine("----------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Member Collection:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("SerialNo./ UserType: FirstName-LastName/ Phone/ password__DVDs Number");
            Console.WriteLine("----------------------------------------------------------------------");
            for (int i = 0; i < members.Length; i++)
            {
                if (members[i].FirstName =="s")
                {
                    Console.Write("      Staff: ");
                }
                else if (members[i].FirstName == "X")
                {}
                else
                {
                    if (i < 10)
                    {
                        Console.Write($" [{i}] Member:");
                    }
                    else
                    {
                        Console.Write($"[{i}] Member:");
                    }
                    Console.Write($"{members[i].FirstName}-{members[i].LastName}, {members[i].PhoneNumber}, {members[i].Password}__");
                    foreach (var movie in members[i].BorrowedDVDs)
                    {
                        Console.Write(movie + ",");
                    }
                    Console.WriteLine();
                }
            }
            /*if (BorrowedMovieTitleSet.Count() !=0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Borrowed Movie Title Set:");
                foreach (var title in BorrowedMovieTitleSet)
                {
                    Console.Write(title + ", ");
                }
            }
            Console.WriteLine();
            GetTop3RentedMovie();*/
        }
    }//end of MemberCollection class
}
