using Lesson46;
using Lesson72;
using Lesson81;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

using Friday = FemaleAIs.Friday;
using Jarvis = MaleAIs.Jarvis;

// using GovernmentEmployee;

// The Whole Course Is Of 23Hrs 40Mins
// I Have Progressed Through 5Hr 10Mins
// Remaining Is 18Hrs 0Mins Of Tutorials
// Double That Number(Maximum) For Practical Implementation And To Complete This Course In 7 Days (As I Have Been Slacking Off For 2 Days)
// 3Hours & 0Mins Of Practical Implementation Everyday

// Wasted some time in hacker rank and now, 7hrs and 20mins of tutorial left 
// For the original goal like yeah, we are on the original goal sadly

namespace Lesson101
{
    class Program
    {
        static int LabelCount = 0;
        static string LabelText = "";

        static int CountCharacters()
        {
            int Count;
            
            using (StreamReader Reader = new StreamReader(@"C:\Users\TheTahaYaseen\Documents\M.txt"))
            {
                string Content = Reader.ReadToEnd();
                Count = Content.Length;
                Thread.Sleep(5000);
            }

            return Count;
        }

        static void CharacterLabelChange() {
            LabelText = String.Format("{0} characters present in file.", LabelCount);
            Console.WriteLine(LabelText);
        }

    async static void GetFileChars()
        {
            // Task<int> TaskCount = new Task<int>(CountCharacters);
            // TaskCount.Start();


            // A thread shall only manipulates its own properties, therefore, we use actions to type safely, invoke stuff

            Thread ThreadCount = new Thread(() =>
            {
                LabelCount = CountCharacters();
                Action CharacterAction = new Action(CharacterLabelChange);
                CharacterAction();

            });

            ThreadCount.Start();

            string LabelText = "File is being processed.";
            Console.WriteLine(LabelText);
            
            // int LabelCount = await TaskCount;
        }

        static void Main()
        {
            GetFileChars();
            Thread.Sleep(50000);
        }
    }
}

namespace Lesson100
// Func delegate
{
    class Program
    {
        static void Lesson100Main()
        {
            List<Student> Students = Lesson74.Program.Lesson74Main();

            // Func<Type, ResultType>
            // Func<Student, string> ProperStudentRepresentationSelector = Student => Student.StudentId + Student.StudentName + Student.StudentAge + Student.StudentGender[0];
            // Func has several overloaded variations to allow multple parameters and outputs
            
            // IEnumerable<string> StudentRepresentations = Students.Select(ProperStudentRepresentationSelector);

            IEnumerable<string> StudentRepresentations = Students.Select(Student => Student.StudentId + Student.StudentName + Student.StudentAge + Student.StudentGender[0]);

            foreach (string StudentRepresentation in StudentRepresentations)
            {
                Console.WriteLine(StudentRepresentation);
            }
        }
    }
}

namespace Lesson98
// Annonymous Methods And Lambda Expressions
{
    class Program
    {

        // Step 1: Create a method matching signature of Predicate<Student> delegate
        public static bool FindStudent(Student Student)
        {
            return Student.StudentId == 05;
        }
        
        static void Lesson98Main()
        {
            List<Student> Students = Lesson74.Program.Lesson74Main();

            // Step 2: Create an instance of Predicate<Student> delegate and pass the method name as an argument to the constructor
            Predicate<Student> StudentPredicate = new Predicate<Student>(FindStudent);

            // Step 3: Pass delegate instance as an argument for Find() method
            Student Student5 = Students.Find(Student => StudentPredicate(Student));

            Student Student6 = Students.Find(
                delegate (Student Student) { return Student.StudentId == 06; }
            );

            // "=>" is the lambda operation
            // input/arguments(with any name) => your condition/function particularly used with Linq functions returning bool
            Student Student7 = Students.Find(Student => Student.StudentId == 07);
            // Type of parameter can also be defined
            Student7 = Students.Find((Student Student) => Student.StudentId == 07);

            int StudentsOlderThan18 = Students.Count(Student => Student.StudentAge > 18);

            Console.WriteLine("Adult students: {0}.", StudentsOlderThan18);

            // If anonymous method, arguments not being used, parameters are optional but in our case, its needed since we need to have a student to compare to
            // Whereas lambda expressions must have parameter even if not used.

            Console.WriteLine("Student with Id of 05: {0}", Student5.StudentName);
            Console.WriteLine("Student with Id of 06: {0}", Student6.StudentName);
            Console.WriteLine("Student with Id of 07: {0}", Student7.StudentName);
        }
    }
}

namespace Lesson97
{
    class SomeMaths
    {
        int _Number;
        int _Till;

        public SomeMaths(int Number, int Till)
        {
            this._Number = Number;
            this._Till = Till;
        }

        public void GetSumOfDivisible()
        {
            long Sum = 0;
            for (int iteration = 1; iteration <= _Till; iteration++)
            {
                if (iteration % _Number == 0)
                {
                    Sum += iteration;
                }
            }
            Console.WriteLine("Sum of numbers divisible by {0} till {1} is {2}", _Number, _Till, Sum);
        }
    }

    class Program
    {
        static void Lesson97Main()
        {
            Console.WriteLine("The no. of cores present: {0}.", Environment.ProcessorCount);
            const int Till = 50000000;
        
            SomeMaths Calculation1 = new SomeMaths(2, Till);
            SomeMaths Calculation2 = new SomeMaths(3, Till);
            SomeMaths Calculation3 = new SomeMaths(5, Till);
            SomeMaths Calculation4 = new SomeMaths(7, Till);

            Stopwatch SingleThread = Stopwatch.StartNew();

            Calculation1.GetSumOfDivisible();
            Calculation2.GetSumOfDivisible();
            Calculation3.GetSumOfDivisible();
            Calculation4.GetSumOfDivisible();

            SingleThread.Stop();

            Console.WriteLine("It took {0} for it to be done in a single thread", SingleThread.ElapsedMilliseconds);

            Thread Calculation1Thread = new Thread(Calculation1.GetSumOfDivisible); 
            Thread Calculation2Thread = new Thread(Calculation2.GetSumOfDivisible); 
            Thread Calculation3Thread = new Thread(Calculation3.GetSumOfDivisible); 
            Thread Calculation4Thread = new Thread(Calculation4.GetSumOfDivisible);

            Stopwatch MultiThread = Stopwatch.StartNew();

            Calculation1Thread.Start();
            Calculation2Thread.Start();
            Calculation3Thread.Start();
            Calculation4Thread.Start();

            Calculation1Thread.Join();
            Calculation2Thread.Join();
            Calculation3Thread.Join();
            Calculation4Thread.Join();

            MultiThread.Stop();

            Console.WriteLine("It took {0} for it to be done in a multi thread", MultiThread.ElapsedMilliseconds);

        }
    }
}

namespace Lesson95
{

    public class Account
    {
        int _Id;
        double _Balance;

        public Account(int Id, double Balance){
            this._Id = Id;
            this._Balance = Balance;
        }

        public int Id
        {
            get { return this._Id; }
        }

        public void Withdraw(double Amount)
        {
            this._Balance -= Amount;
        }

        public void Deposit(double Amount)
        {
            this._Balance += Amount;
        }
    
    }

    public class AccountManager
    {
        Account _FromAccount;
        Account _ToAccount;
        double _AmountToTransfer;

        public AccountManager(Account FromAccount, Account ToAccount, double AmountToTransfer)
        {
            this._FromAccount = FromAccount;
            this._ToAccount = ToAccount;
            this._AmountToTransfer = AmountToTransfer;
        }

        public void Transfer()
        {

            // Locks in a predefined order nullifies the possibility of a deadlock

            object _AccountLock1 = _ToAccount.Id;
            object _AccountLock2 = _FromAccount.Id;

            if (_FromAccount.Id > _ToAccount.Id)
            {
                _AccountLock1 = _FromAccount.Id;
                _AccountLock2 = _ToAccount.Id;
            }

            lock (_AccountLock1)
            {
                Thread.Sleep(1000);
                lock (_AccountLock2)
                {
                    _FromAccount.Withdraw(_AmountToTransfer);
                    _ToAccount.Withdraw(_AmountToTransfer);
                }
            }
        }
    }

    class Program
    {
        static void Lesson95Main()
        {
            Console.WriteLine("Transaction started.");
            Account AccountA = new Account(1, 10000);
            Account AccountB = new Account(2, 7810);

            AccountManager AccountManagerA = new AccountManager(AccountA, AccountB, 2810);
            Thread TransferThread1 = new Thread(AccountManagerA.Transfer);
            TransferThread1.Name = "TransferThread1";

            AccountManager AccountManagerB = new AccountManager(AccountB, AccountA, 5000);
            Thread TransferThread2 = new Thread(AccountManagerB.Transfer);
            TransferThread2.Name = "TransferThread2";

            TransferThread1.Start();
            TransferThread2.Start();

            TransferThread1.Join();
            TransferThread2.Join();
            Console.WriteLine("Transaction ended.");
        }
    }

}

namespace Lesson93
// Interlocked / Protected multi thread utilized resources 
{

    class Program
    {
        public static int Balance = 0;
        
        static void Lesson93Main()
        {
            // For performance measurement

            // _lock is slower but can used on any resource
            // whereas Interlocked is faster but can only be used on intergral/numerical values for mathematical operations

            Stopwatch Stopwatch = Stopwatch.StartNew();

            Thread OnePlush = new Thread(MillionEarned);
            Thread TwoPlush = new Thread(MillionEarned);
            Thread ThreePlush = new Thread(MillionEarned);

            OnePlush.Start();
            TwoPlush.Start();
            ThreePlush.Start();

            OnePlush.Join();
            TwoPlush.Join();
            ThreePlush.Join();

            Console.WriteLine("The total balance is {0}", Balance);

            Stopwatch.Stop();
            Console.WriteLine("Time taken: {0}", Stopwatch.ElapsedMilliseconds);
        }

        static object _lock = new object();

        public static void MillionEarned()
        {
            for(int iteration = 1; iteration == 1000000; iteration++)
            {
                // Balance++; // Not thread safe
                // Interlocked.Increment(ref Balance); // Thread safe
                // lock (_lock)
                // {
                //     Balance++;
                // } // Another method is locking the resource so none can use it

                // Basically, longer form of lock method
                // Monitor.Enter(_lock);
                // try
                // {
                //     Balance++;
                // }
                // finally
                // {
                //     Monitor.Exit(_lock);
                // }

                bool LockTaken = false;

                Monitor.Enter(_lock, ref LockTaken);
                try
                {
                    Balance++;
                }
                finally
                {
                    if (LockTaken)
                    {
                        Monitor.Exit(_lock);
                    }
                }

            }
        }
    }
}

namespace Lesson92
// Threads (Join, And Is Alive)
{
    class Program
    {
        static void Greet()
        {
            Thread.Sleep(5000);

            Console.WriteLine("Greetings started.");

            Console.WriteLine("Greetings to whomever listening.");
            Console.WriteLine("I hope you are fine wherever you go and live a wonderous life.");

            Console.WriteLine("Greetings given.");
        }

        static void Bless()
        {
            Console.WriteLine("Blessings started.");

            Console.WriteLine("May Allah help you in whatever experience you partake.");
            Console.WriteLine("May he be with you in every part of your journey.");

            Console.WriteLine("Blessings given.");
        }
        static void Lesson91Main()
        {
            Console.WriteLine("Monologue started.");

            Thread GreetingThread = new Thread(Greet);
            GreetingThread.Start();
            GreetingThread.Join(2000);
            // Console.WriteLine("Greetings ended.");

            Thread BlessingThread = new Thread(Bless);
            BlessingThread.Start();
            BlessingThread.Join();
            Console.WriteLine("Blessings ended.");

            for(int iteration = 0; iteration < 10; iteration++)
            {
                if (GreetingThread.IsAlive)
                {
                    Console.WriteLine("Greetings still being processed.");
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine("Greetings ended.");
                    break;
                }
            }

            Console.WriteLine("Monologue ended.");
        }

        // static void Main()
        // {
        //     Console.WriteLine("Monologue started.");
        // 
        //     Thread GreetingThread = new Thread(Greet);
        //     GreetingThread.Start();
        //     // Console.WriteLine("Greetings ended.");
        // 
        //     Thread BlessingThread = new Thread(Bless);
        //     BlessingThread.Start();
        //     // Console.WriteLine("Blessings ended.");
        // 
        //     Console.WriteLine("Monologue ended.");
        // }
    }
}

namespace Lesson91
// Threads
{
    delegate void SumOfNumbersCallback(int Sum);

    class Number
    {
        int _TargetNumber;
        SumOfNumbersCallback _CallbackMethod;

        public Number(int TargetNumber, SumOfNumbersCallback CallbackMethod)
        {
            this._TargetNumber = TargetNumber;
            this._CallbackMethod = CallbackMethod;
        }

        public void PrintNumbers()
        {
            for (int iteration = 0; iteration < _TargetNumber; iteration++)
            {
                Console.WriteLine(iteration);
            }
        }

        public void SumNumbersTill()
        {
            int Sum = 0;
            for (int iteration = 0; iteration < _TargetNumber; iteration++)
            {
                Sum += iteration;
            }

            if (_CallbackMethod != null)
            {
                _CallbackMethod(Sum);
            }
        }
    }

    class Program
    {
        static void PrintSum(int Sum)
        {
            Console.WriteLine("The sum of the numbers is {0}", Sum);
        }

        static void Lesson91Main()
        {
            bool QuitProgram = false;
            do
            {
                Console.WriteLine("Enter to sum the numbers till.");

                string TargetInput = Console.ReadLine();
                TargetInput = TargetInput.ToUpper();

                int TargetNumber = 0;

                if (TargetInput == "QUIT")
                {
                    QuitProgram = true;
                }
                else if (int.TryParse(TargetInput, out TargetNumber))
                {
                    SumOfNumbersCallback CallbackMethod = new SumOfNumbersCallback(PrintSum);
                    Number Target = new Number(TargetNumber, CallbackMethod);
                    Thread TargetThread = new Thread(Target.SumNumbersTill);
                    TargetThread.Start();
                }
                else
                {
                    Console.WriteLine("Invalid number given.");
                }

            } while (!QuitProgram);
        }
    }
}

namespace Lesson87
// Threads
{
    class Number
    {
        int _TargetNumber;

        public Number(int TargetNumber)
        {
            this._TargetNumber = TargetNumber;
        }

        public void PrintNumbers()
        {
            for(int iteration = 0; iteration < _TargetNumber; iteration++)
            {
                Console.WriteLine(iteration);
            }
        }
    }

    class Program
    {
        static void Lesson87Main()
        {
            bool QuitProgram = false;
            do
            {
                Console.WriteLine("Enter to print numbers till.");
             
                string TargetInput = Console.ReadLine();
                TargetInput = TargetInput.ToUpper();            

                int TargetNumber = 0;

                if (TargetInput == "QUIT")
                {
                    QuitProgram = true;
                }
                else if (int.TryParse(TargetInput, out TargetNumber))
                {
                    Number Target = new Number(TargetNumber);
                    Thread NumberPrintingThread = new Thread(Target.PrintNumbers);
                    NumberPrintingThread.Start();
                }
                else
                {
                    Console.WriteLine("Invalid number given.");
                }
                
            } while (!QuitProgram);
        }
    }
}

namespace Lesson86
// Threads 
{
    class Program
    {
        static void Lesson86Main()
        {
            string UserChoice = null;
            do
            {
                Console.WriteLine("Command - \"Chores\" - \"Announce\" - \"Count\" - \"Why\" - \"Leader\" - \"Quit\"");
                UserChoice = Console.ReadLine();
                UserChoice = UserChoice.ToUpper();

                if (UserChoice == "QUIT")
                {
                    Console.WriteLine("Breaking out.");
                }
                else if (UserChoice == "CHORES")
                {
                    Thread ChoresThread = new Thread(DoSomeRandomChores);
                    ChoresThread.Start();
                }
                else if (UserChoice == "ANNOUNCE")
                {
                    Thread AnnounceThread = new Thread(new ThreadStart(AnnounceStack));
                    AnnounceThread.Start();
                }
                else if (UserChoice == "COUNT")
                {
                    int TargetNumber = 100;

                    // Thread CountThread = new Thread(() => Count());

                    // ParameterizedThreadStart ParameterizedThreadStartForCount = new ParameterizedThreadStart(Count);
                    // Thread CountThread = new Thread(ParameterizedThreadStartForCount);

                    Thread CountThread = new Thread(Count);
                    CountThread.Start(TargetNumber);
                }
                else if (UserChoice == "WHY")
                {
                    Thread WhyThread = new Thread(delegate() { Why(); });
                    WhyThread.Start();
                }
                else if (UserChoice == "LEADER")
                {
                    Leader JustifiedLeader = new Leader("Hitler"); 
                    Thread LeaderThread = new Thread(JustifiedLeader.AnnounceLeader);
                    LeaderThread.Start();
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }

            } while (UserChoice != "QUIT");

        }

        static void DoSomeRandomChores()
        {
            Thread.Sleep(7500);
            Console.WriteLine("Random chores done.");   
        }

        static void AnnounceStack()
        {
            Thread.Sleep(1000);
            Console.WriteLine("I am a python/django dev.");
            Thread.Sleep(1000);
            Console.WriteLine("Shifting into c# and .NET");
            Thread.Sleep(1000);
            Console.WriteLine("For the power and control provided.");
        }

        static void Count(object Target)
        {
            int TargetNumber = 0;
            if(int.TryParse(Target.ToString(), out TargetNumber))
            {
                for(int iteration = 0; iteration < TargetNumber; iteration++)
                {
                    Console.WriteLine(iteration);
                }
            }
            else {
                Console.WriteLine("Invalid number.");
            }
        }

        static void Why() {
            Console.WriteLine("Why do you do this to me?");
            Thread.Sleep(1000);
            Console.WriteLine("Why you?");
            Thread.Sleep(1000);
            Console.WriteLine("I know tum meri nahi.");
            Thread.Sleep(1000);
            Console.WriteLine("I do.");
        }

        class Leader
        {
            public string Name { get; set; }

            public Leader (string Name)
            {
                this.Name = Name;
            }

            public void AnnounceLeader()
            {
                Console.WriteLine("Our leader is {0} and for the dear motives we stand.", this.Name);
                for (int iteration = 0; iteration < 3; iteration++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Hail {0}!", this.Name);
                }    
            }
        
        }

    }
}

namespace Lesson85
// Stack Example
{
    class Program
    {
        static void Lesson85Main()
        {
            Dictionary<string, Country> Countries = Country.GetSampleCountries();
            string UserChoice = null;

            Stack<Country> CountriesStack = new Stack<Country>();

            do
            {
                Console.WriteLine("Enter any country code for its information or \"Previous\" for getting previous country's information. \"Quit\" for leaving.");
                UserChoice = Console.ReadLine();
                UserChoice = UserChoice.ToUpper();

                if (UserChoice == "PREVIOUS")
                {
                    Country Country = null;
                    CountriesStack.TryPop(out Country);
                    if (Country != null)
                    {
                        Console.WriteLine("{0}'s capital is {1}", Country.Name, Country.Capital);
                    }
                    else
                    {
                        Console.WriteLine("No previous history.");
                    }
                }
                else if (Countries.ContainsKey(UserChoice))
                {
                    Country Country = Countries[UserChoice];
                    CountriesStack.Push(Country);
                    Console.WriteLine("{0}'s capital is {1}", Country.Name, Country.Capital);
                }
                else if (UserChoice == "QUIT") {
                    Console.Write("Breaking out.");
                }
                else
                {
                    Console.WriteLine("Invalid choice entered.");
                }

            } while (UserChoice != "QUIT");
        }
    }
}

namespace Lesson84
// Queue Example
{
    class Program
    {
        static void Lesson84Main()
        {
            int TotalTokensDistributed = 0;
            int TokenCalledToCounter1;
            int TokenCalledToCounter2;
            int TokenCalledToCounter3;

            bool WaitingInQueue;        

            string UserInput = null;
            Queue<int> TokensQueue = new Queue<int>();

            Console.Write("\n");
            do
            {
                StringBuilder TokensBeforeAnnouncement = new StringBuilder("Tokens In Line Before You: ");

                StringBuilder TokensBefore = new StringBuilder("");
                foreach(int Token in TokensQueue)
                {
                    TokensBefore.Append(String.Format("{0}.", Token));
                }

                if(TokensBefore.Equals("")) {
                    TokensBefore = TokensBefore.Append("None.");
                }

                TokensBeforeAnnouncement.Append(TokensBefore);

                Console.WriteLine(TokensBeforeAnnouncement);
                Console.WriteLine("Enter your command. Quit - Generate - Counter# Cleared");
                UserInput = Console.ReadLine();
                UserInput = UserInput.ToUpper();                

                switch (UserInput){
                    case "GENERATE":
                        TotalTokensDistributed++;
                        int CurrentToken = TotalTokensDistributed;
                        TokensQueue.Enqueue(CurrentToken);
                        Console.WriteLine("Token #{0} Generated.", CurrentToken);
                        break;
                    case "COUNTER#1 CLEARED":
                        WaitingInQueue = TokensQueue.TryDequeue(out TokenCalledToCounter1);
                        if (WaitingInQueue)
                            Console.WriteLine("Counter#1 Cleared. Token #{0}, Please Progress To Counter#1.", TokenCalledToCounter1);
                        break;
                    case "COUNTER#2 CLEARED":
                        WaitingInQueue = TokensQueue.TryDequeue(out TokenCalledToCounter2);
                        if (WaitingInQueue)
                            Console.WriteLine("Counter#2 Cleared. Token #{0}, Please Progress To Counter#2.", TokenCalledToCounter2 );
                        break;
                    case "COUNTER#3 CLEARED":
                        WaitingInQueue = TokensQueue.TryDequeue(out TokenCalledToCounter3);
                        if (WaitingInQueue)
                        {
                            Console.WriteLine("Counter#3 Cleared. Token #{0}, Please Progress To Counter#3.", TokenCalledToCounter3);
                        }
                        else
                        {
                            Console.WriteLine("No one waiting in queue currently.");
                        }
                        break;
                    case "QUIT":
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        break;
                }
                Console.Write("\n");
            } while (UserInput != "QUIT");
        }
    }
}

namespace Lesson83  
// Stack
{
    class Program
    {
        static void Lesson83Main()
        {
            Stack<Student> StudentStack = new Stack<Student>();
            Console.WriteLine("Stack basically follows the rule of \"first in, first out\"");

            List<Student> StudentsList = Lesson74.Program.Lesson74Main();
            foreach(Student Student in StudentsList)
            {
                StudentStack.Push(Student);
                Console.WriteLine("{0}'s details pushed.", Student.StudentName);
            }

            Console.WriteLine("{0} items present in stack", StudentStack.Count);

            Student FirstStudent = StudentStack.Peek();
            Console.WriteLine("Last/top item of the stack, {0} peeked(gotten/recieved) on without getting removed.", FirstStudent.StudentName);

            FirstStudent = StudentStack.Pop();
            Console.WriteLine("Last/top student of the stack, {0} popped(gotten/recieved) after being removed.", FirstStudent.StudentName);

            Student LastStudent = null;

            Console.WriteLine("All students can be accessed similiarly through a foreach loop and in descending order as when we push, we push to the top meaning the start.");
            foreach (Student Student in StudentStack)
            {
                LastStudent = Student;
            }

            Console.WriteLine("Does stack still contain the lastly added / top student {0}? {1}", FirstStudent.StudentName, StudentStack.Contains(FirstStudent) ? "Yes" : "No");
            if (LastStudent != null)
            {
                Console.WriteLine("Does stack contain firstly added / bottom student: {0}? {1}", LastStudent.StudentName, StudentStack.Contains(LastStudent) ? "Yes" : "No");
            }

        }
    }
}

namespace Lesson82
// Queue
{
    class Program
    {
        static void Lesson82Main()
        {
            Queue<Student> StudentQueue = new Queue<Student>();
            Console.WriteLine("Queue basically follows the rule of \"first in, first out.\"");
            
            List<Student> StudentsList = Lesson74.Program.Lesson74Main();
            foreach(Student Student in StudentsList)
            {
                StudentQueue.Enqueue(Student);
                Console.WriteLine("{0}'s details enqueued.", Student.StudentName);
            }

            Console.WriteLine("{0} items present in queue.", StudentQueue.Count);

            Student FirstStudent = StudentQueue.Peek();
            Console.WriteLine("First item of the queue, Student: {0} peeked on(gotten/recieved) without being removed.", FirstStudent.StudentName);

            FirstStudent = StudentQueue.Dequeue();
            Console.WriteLine("First item of the queue, Student: {0} dequeued(gotten/recieved) after being removed.", FirstStudent.StudentName);

            Console.WriteLine("All elements of a queue can be accessed using foreach loop.");
            Student LastStudent = null;
            foreach (Student Student in StudentQueue)
            {
                Console.WriteLine("{0} accessed in queue.", Student.StudentName);
                LastStudent = Student;
            }

            Console.WriteLine("Does queue contain first student: {0}? {1}", FirstStudent.StudentName, StudentQueue.Contains(FirstStudent) ? "Yes" : "No");
            if (LastStudent != null)
            {
                Console.WriteLine("Does queue contain last student: {0}? {1}", LastStudent.StudentName, StudentQueue.Contains(LastStudent) ? "Yes" : "No");
            }

        }
    }
}

namespace Lesson81
// Using Dicts Instead Of Lists
{
    class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }

        public Country(string Code, string Name, string Capital)
        {
            this.Code = Code;
            this.Name = Name;
            this.Capital = Capital;
        }

        public static Dictionary<string, Country> GetSampleCountries()
        {
            Dictionary<string, Country> Countries = new Dictionary<string, Country>();

            Country Pakistan = new Country("PAK", "Pakistan", "Karachi");
            Countries.Add(Pakistan.Code, Pakistan);

            Country India = new Country("IND", "India", "New Delhi");
            Countries.Add(India.Code, India);

            Country Bangladesh = new Country("BGD", "Bangladesh", "Dhaka");
            Countries.Add(Bangladesh.Code, Bangladesh);

            Country SriLanka = new Country("LKA", "Sri Lanka", "Colombo");
            Countries.Add(SriLanka.Code, SriLanka);

            Country Nepal = new Country("NPL", "Nepal", "Kathmandu");
            Countries.Add(Nepal.Code, Nepal);

            Country China = new Country("CHN", "China", "Beijing");
            Countries.Add(China.Code, China);

            Country Japan = new Country("JPN", "Japan", "Tokyo");
            Countries.Add(Japan.Code, Japan);

            Country SouthKorea = new Country("KOR", "South Korea", "Seoul");
            Countries.Add(SouthKorea.Code, SouthKorea);

            Country Malaysia = new Country("MYS", "Malaysia", "Kuala Lumpur");
            Countries.Add(Malaysia.Code, Malaysia);

            Country Indonesia = new Country("IDN", "Indonesia", "Jakarta");
            Countries.Add(Indonesia.Code, Indonesia);

            return Countries;
        }
    }

    class Program()
    {
        static void Lesson81Main()
        {
            Dictionary<string, Country> Countries = Country.GetSampleCountries();
            bool QuitProgram = false;
            string ContinueProgram = null;

            do
            {
                Console.WriteLine("Enter any country code for its information.");
                string CountryCode = Console.ReadLine();

                if (Countries.ContainsKey(CountryCode))
                {
                    Country Country = Countries[CountryCode];
                    Console.WriteLine("{0}'s capital is {1}", Country.Name, Country.Capital);

                }
                else
                {
                    Console.WriteLine("Invalid country code entered.");
                }

                do
                {
                    Console.WriteLine("Do you want to continue the program, yes or no?");
                    ContinueProgram = Console.ReadLine();
                } while (ContinueProgram.ToLower() != "yes" && ContinueProgram.ToLower() != "no");

                if (ContinueProgram == "no")
                {
                    QuitProgram = true;
                }

            } while (QuitProgram != true);
        }
    }
}

// List Implementation

// namespace Lesson81
// Using Dicts Instead Of Lists
// {
//    class Country
//    {
//        public string Code { get; set; }
//        public string Name { get; set; }
//        public string Capital { get; set; }
//
//        public Country(string Code, string Name, string Capital)
//        {
//            this.Code = Code;
//            this.Name = Name;
//            this.Capital = Capital;
//        }
//
//        public static List<Country> GetSampleCountries()
//        {
//            List<Country> Countries = new List<Country>();
//
//            Country Pakistan = new Country("PAK", "Pakistan", "Karachi");
//            Countries.Add(Pakistan);
//
//            Country India = new Country("IND", "India", "New Delhi");
//            Countries.Add(India);
//
//            Country Bangladesh = new Country("BGD", "Bangladesh", "Dhaka");
//            Countries.Add(Bangladesh);
//
//            Country SriLanka = new Country("LKA", "Sri Lanka", "Colombo");
//            Countries.Add(SriLanka);
//
//            Country Nepal = new Country("NPL", "Nepal", "Kathmandu");
//            Countries.Add(Nepal);
//
//            Country China = new Country("CHN", "China", "Beijing");
//            Countries.Add(China);
//
//            Country Japan = new Country("JPN", "Japan", "Tokyo");
//            Countries.Add(Japan);
//
//            Country SouthKorea = new Country("KOR", "South Korea", "Seoul");
//            Countries.Add(SouthKorea);
//
//            Country Malaysia = new Country("MYS", "Malaysia", "Kuala Lumpur");
//            Countries.Add(Malaysia);
//
//            Country Indonesia = new Country("IDN", "Indonesia", "Jakarta");
//            Countries.Add(Indonesia);
//
//            return Countries;
//        }
//    }
//
//    class Program()
//    {
//        static void Main()
//        {
//            List<Country> Countries = Country.GetSampleCountries();
//            bool QuitProgram = false;
//            string ContinueProgram = null;
//
//            do
//            {
//                Console.WriteLine("Enter any country code for its information.");
//                string CountryCode = Console.ReadLine();
//
//                Country Country = Countries.Find(Country => Country.Code == CountryCode);
//
//                if (Country == null)
//                {
//                    Console.WriteLine("Invalid country code entered.");
//                }
//                else
//                {
//                    Console.WriteLine("{0}'s capital is {1}", Country.Name, Country.Capital);
//                }
//
//                do
//                {
//                    Console.WriteLine("Do you want to continue the program, yes or no?");
//                    ContinueProgram = Console.ReadLine();
//                } while (ContinueProgram.ToLower() != "yes" && ContinueProgram.ToLower() != "no");
//
//                if (ContinueProgram == "no")
//                {
//                    QuitProgram = true;
//                }
//
//            } while (QuitProgram != true);
//        }
//    }
// }

namespace Lesson80
// Lists
{
    class Program()
    {
        static void Lesson80Main()
        {
            List<Student> Students = Lesson74.Program.Lesson74Main();

            Console.WriteLine("Capacity before trimming {0}", Students.Capacity);
            Students.TrimExcess();
            Console.WriteLine("Capacity after trimming {0}", Students.Capacity);

            ReadOnlyCollection<Student> ReadOnlyStudentsList = Students.AsReadOnly();
            Console.WriteLine("Now you cannot modify the read only list");

            bool AreAllMales = Students.TrueForAll(Student => Student.StudentGender == "Male");
            bool AreAllAdults = Students.TrueForAll(Student => Student.StudentAge > 17);

            if (AreAllAdults)
            {
                Console.WriteLine("All Students Are Adult.");
            }
            else
            {
                Console.WriteLine("All Students Are Not Adult.");
            }

            if (AreAllMales)
            {
                Console.WriteLine("All Students Are Male.");
            }
            else
            {
                Console.WriteLine("All Students Are Not Male.");
            }

        }
    }
}

namespace Lesson78
// Complex Sorting
{
    class Program
    {
        public class SortByName : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                return x.StudentName.CompareTo(y.StudentName);
            }

        }

        static void Lessom79Main()
        {
            // for a quick list // create your own
            List<Student> Students = Lesson74.Program.Lesson74Main();

            Console.WriteLine("Before Sorting");
            foreach (Student Student in Students)
            {
                Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            Students.Sort();
            Console.WriteLine("After Sorting");
            foreach (Student Student in Students)
            {
                Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            Students.Reverse();
            Console.WriteLine("After Sorting Descending");
            foreach (Student Student in Students)
            {
                Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            SortByName SortByName = new SortByName();
            Students.Sort(SortByName);
            Console.WriteLine("After Sorting By Name");
            foreach (Student Student in Students)
            {
                Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            Comparison<Student> StudentComparer = new Comparison<Student>(Student.CompareById);
            Students.Sort(StudentComparer);

            // Basically the same as making the delegate pointer through more lines
            Students.Sort(delegate (Student x, Student y) { return x.StudentId.CompareTo(y.StudentId); });

            // Basically the same as making the delegate 
            Students.Sort((Student1, Student2) => Student1.StudentId.CompareTo(Student2.StudentId));

            Console.WriteLine("After Sorting By Id");
            foreach (Student Student in Students)
            {
                Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

        }
    }

}

namespace Lesson77
// Sorting
{
    class Program
    {
        static void Lesson77Main()
        {
            List<int> Numbers = new List<int>() { 2, 53, 23, 4532, 21, 2, 24, 34, 23, 291, 27, 1, 10 };

            Console.WriteLine("Unsorted Numbers");
            foreach (int Number in Numbers)
            {
                Console.WriteLine(Number);
            }

            Numbers.Sort();
            Console.WriteLine("Sorted Numbers");
            foreach (int Number in Numbers)
            {
                Console.WriteLine(Number);
            }

            Numbers.Reverse();
            Console.WriteLine("Sorted Descending Numbers");
            foreach (int Number in Numbers)
            {
                Console.WriteLine(Number);
            }

            List<string> Alphabets = new List<string>() { "T", "N", "H", "F", "A", "K", "A" };

            Console.WriteLine("Unsorted Alphabets");
            foreach (string Alphabet in Alphabets)
            {
                Console.WriteLine(Alphabet);
            }

            Alphabets.Sort();
            Console.WriteLine("Sorted Alphabets");
            foreach (string Alphabet in Alphabets)
            {
                Console.WriteLine(Alphabet);
            }

            Alphabets.Reverse();
            Console.WriteLine("Sorted Descending Alphabets");
            foreach (string Alphabet in Alphabets)
            {
                Console.WriteLine(Alphabet);
            }



        }
    }
}

namespace Lesson74
// Lists
{
    class GoodStudent : Student
    {
        public GoodStudent(int _StudentId, string _StudentName, int _StudentAge, string _StudentGender)
            : base(_StudentId, _StudentName, _StudentAge, _StudentGender)
        {

        }
    }

    // uncomment console statements when applying
    class Program
    {
        public static List<Student> Lesson74Main()
        {
            GoodStudent Taha = new GoodStudent(01, "Taha Yaseen", 15, "Male");
            Student Talha = new Student(02, "M. Talha", 20, "Male");
            Student Hamza = new Student(03, "Hamza Malik", 22, "Male");
            Student Sarah = new Student(04, "Sarah Cadbury", 21, "Female");

            // Length defined as argument
            List<Student> Students = new List<Student>(100);
            Students.Add(Talha);
            Students.Add(Hamza);

            foreach (Student Student in Students)
            {
                // Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            // Can still be added due to inheriting
            Students.Add(Taha);

            foreach (Student Student in Students)
            {
                // Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            // Since ladies first
            Students.Insert(0, Sarah);

            int TotalStudents = Students.Count;
            // Console.WriteLine("Total Students: {0}", TotalStudents);

            foreach (Student Student in Students)
            {
                // Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            Student Radhika = Students.Find(Student => Student.StudentName == "Sarah Cadbury");
            List<Student> MaleStudents = Students.FindAll(Student => Student.StudentGender == "Male");

            if (Radhika != null)
            {
                // Console.WriteLine("If Radhika Found, Her Full Name: {0}", Radhika.StudentName ?? "Not Found");
            }
            foreach (Student MaleStudent in MaleStudents)
            {
                // Console.WriteLine("Male Student: {0}", MaleStudent.StudentName);
            }

            Student LastMaleStudent = Students.FindLast(Student => Student.StudentGender == "Male");
            // Console.WriteLine("Last Male Student: {0}", LastMaleStudent.StudentName);

            if (Students.Contains(Taha))
            {
                // Console.WriteLine("Taha Is Indeed A Student");
            }

            if (Students.Exists(Student => Student.StudentGender == "Female"))
            {
                // Console.WriteLine("There Are Female Students Present In Class");
            }

            int IndexOfFirstMaleStudent = Students.FindIndex(Student => Student.StudentGender == "Male");
            int IndexOfLastMaleStudent = Students.FindLastIndex(Student => Student.StudentGender == "Male");
            // Console.WriteLine("First Male Student Index: {0}", IndexOfFirstMaleStudent);
            // Console.WriteLine("Last Male Student Index: {0}", IndexOfLastMaleStudent);

            // Conversion
            Student[] StudentsArray = Students.ToArray();

            Student[] ManualStudentsArray = new Student[4];
            ManualStudentsArray[0] = Taha;
            ManualStudentsArray[1] = Sarah;
            ManualStudentsArray[2] = Talha;
            ManualStudentsArray[3] = Hamza;

            // Conversion
            List<Student> ManualStudentList = ManualStudentsArray.ToList();

            List<Student> MoreStudents = new List<Student>();
            Student Uzair = new GoodStudent(05, "Sardar Uzair", 19, "Male");
            Student Ahmed = new Student(06, "Ahmed Raza", 15, "Male");

            MoreStudents.Add(Uzair);
            MoreStudents.Add(Ahmed);

            Students.AddRange(MoreStudents);

            // Console.WriteLine("More Students");
            foreach (Student Student in Students)
            {
                // Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            List<Student> OldStudents = Students.GetRange(0, 4);

            // Console.WriteLine("Old Students");
            foreach (Student Student in OldStudents)
            {
                // Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }


            List<Student> MoreMoreStudents = new List<Student>();
            Student Tony = new GoodStudent(07, "Tony Stark", 51, "Male");
            Student Pepper = new Student(08, "Pepper Potts", 43, "Female");

            MoreMoreStudents.Add(Tony);
            MoreMoreStudents.Add(Pepper);

            Students.InsertRange(0, MoreMoreStudents);

            // Console.WriteLine("More More Students");
            foreach (Student Student in Students)
            {
                // Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            Students.RemoveAt(4);

            // Console.WriteLine("5th Student Removed");

            foreach (Student Student in Students)
            {
                // Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            // Console.WriteLine("Female Students Removed");

            Students.RemoveAll(Student => Student.StudentGender == "Female");
            foreach (Student Student in Students)
            {
                // Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            // Console.WriteLine("Last 2 Students Removed");
            // also uncomment this // Students.RemoveRange(Students.Count-2, 2);
            foreach (Student Student in Students)
            {
                // Console.WriteLine("Id: {0}; Name: {1}; Age: {2}; Gender {3}", Student.StudentId, Student.StudentName, Student.StudentAge, Student.StudentGender);
            }

            return Students;
        }
    }
}

namespace Lesson72
{
    // Dictionaries
    class Student : IComparable<Student>
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
        public string StudentGender { get; set; }

        public Student(int _StudentId, string _StudentName, int _StudentAge, string _StudentGender)
        {
            this.StudentId = _StudentId;
            this.StudentName = _StudentName;
            this.StudentAge = _StudentAge;
            this.StudentGender = _StudentGender;
        }

        public static int CompareById(Student x, Student y)
        {
            return x.StudentId.CompareTo(y.StudentId);
        }

        public int CompareTo(Student other)
        {
            // if(this.StudentAge > other.StudentAge)
            // {
            //     return 1;
            // }
            // else if (this.StudentAge < other.StudentAge)
            // {
            //     return -1;
            // }
            // else
            // {
            //     return 0;
            // }
            return this.StudentAge.CompareTo(other.StudentAge);
        }
    }

    class Program
    {
        static void Lesson72Main()
        {

            Student Taha = new Student(01, "Taha Yaseen", 15, "Male");
            Student Talha = new Student(02, "M. Talha", 20, "Male");
            Student Hamza = new Student(03, "Hamza Malik", 22, "Male");
            Student Sarah = new Student(04, "Sarah Cadbury", 21, "Female");

            Dictionary<string, Student> Students = new Dictionary<string, Student>();
            Students.Add("Taha", Taha);
            Students.Add("Talha", Talha);
            Students.Add("Hamza", Hamza);
            Students.Add("Sarah", Sarah);

            if (Students.ContainsKey("Taha"))
            {
                Console.WriteLine(Students["Taha"].StudentName);
                Students["Taha"].StudentName = "The Taha Yaseen";
                Console.WriteLine(Students["Taha"].StudentName);
            }

            foreach (KeyValuePair<string, Student> Student in Students)
            {
                Console.WriteLine(Student.Value.StudentId);
            }

            List<string> StudentsKey = Students.Keys.ToList();
            List<Student> StudentsValue = Students.Values.ToList();

            foreach (string StudentKey in StudentsKey)
            {
                Console.WriteLine(StudentKey);
            }

            foreach (Student Student in StudentsValue)
            {
                Console.WriteLine(Student.StudentName);
                Console.WriteLine(Student.StudentAge);
                Console.WriteLine(Student.StudentGender);
            }

            Student TheTaha;
            Student DotR;

            if (Students.TryGetValue("Taha", out TheTaha))
            {
                Console.WriteLine("TheTaha Assigned");
            }
            else
            {
                Console.WriteLine("TheTaha Not Assigned");
            }

            if (Students.TryGetValue("DotR", out DotR))
            {
                Console.WriteLine("DotR Assigned");
            }
            else
            {
                Console.WriteLine("DotR Not Assigned");
            }

            int TotalStudents = Students.Count;
            int TotalMaleStudents = Students.Count(Student => Student.Value.StudentGender == "Male");

            Console.WriteLine("Total Students: {0}", TotalStudents);
            Console.WriteLine("Total Male Students: {0}", TotalMaleStudents);

            Students.Remove("Hamza");
            Console.WriteLine("Hamza Removed");

            TotalMaleStudents = Students.Count(Student => Student.Value.StudentGender == "Male");
            Console.WriteLine("Total Male Students: {0}", TotalMaleStudents);

            Students.Clear();
            Console.WriteLine("Students Cleared");
            TotalStudents = Students.Count;
            Console.WriteLine("Total Students: {0}", TotalStudents);

            List<Student> NewStudents = new List<Student>();

            NewStudents.Add(Taha);
            NewStudents.Add(Talha);
            NewStudents.Add(Hamza);
            NewStudents.Add(Sarah);

            Dictionary<int, Student> NewStudentsDict = NewStudents.ToDictionary(Student => Student.StudentId, Student => Student);
            Console.WriteLine("NewStudents Converted To Dictionary Functioning Same As Before");
        }
    }
}

namespace Lesson71
// Snippets
{
    class Program
    {
        static void Lesson71Main()
        {
            for (int k = 0; k < 1; k++)
            {

            }
        }
    }
}

namespace Lesson66
// Optional Parameters 
{
    class Calculator
    {
        // Params array must be the last parameter
        public static void ParamsArraySum(int FirstNumber, int SecondNumber, params object[] RestOfNumbers)
        {
            int Sum = FirstNumber + SecondNumber;
            if (RestOfNumbers != null)
            {
                foreach (int RestOfNumber in RestOfNumbers)
                {
                    Sum += RestOfNumber;
                }
            }
            Console.WriteLine("Sum: {0}", Sum);
        }

        public static void MethodOverLoadingSum(int FirstNumber, int SecondNumber)
        {
            MethodOverLoadingSum(FirstNumber, SecondNumber, null);
        }

        public static void MethodOverLoadingSum(int FirstNumber, int SecondNumber, int[] RestOfNumbers)
        {
            int Sum = FirstNumber + SecondNumber;
            if (RestOfNumbers != null)
            {
                foreach (int RestOfNumber in RestOfNumbers)
                {
                    Sum += RestOfNumber;
                }
            }
            Console.WriteLine("Sum: {0}", Sum);
        }

        // (Must appear as last parameters)
        public static void DefaultParameterSum(int FirstNumber = 34, int SecondNumber = 35, int[] RestOfNumbers = null)
        {
            int Sum = FirstNumber + SecondNumber;
            if (RestOfNumbers != null)
            {
                foreach (int RestOfNumber in RestOfNumbers)
                {
                    Sum += RestOfNumber;
                }
            }
            Console.WriteLine("Sum: {0}", Sum);
        }

        public static void OptionalAttributeSum(int FirstNumber, int SecondNumber, [Optional] int[] RestOfNumbers)
        {
            int Sum = FirstNumber + SecondNumber;
            if (RestOfNumbers != null)
            {
                foreach (int RestOfNumber in RestOfNumbers)
                {
                    Sum += RestOfNumber;
                }
            }
            Console.WriteLine("Sum: {0}", Sum);
        }

    }

    class Program
    {
        static void Lesson66Main()
        {
        }
    }
}

namespace Lesson65
// Indexers
{
    // rename "RealStudent" to "Student" when reapplying, changed due to alot of contradictions
    class RealStudent
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
        public string StudentGender { get; set; }

        public RealStudent(int _StudentId, string _StudentName, int _StudentAge, string _StudentGender)
        {
            this.StudentId = _StudentId;
            this.StudentName = _StudentName;
            this.StudentAge = _StudentAge;
            this.StudentGender = _StudentGender;
        }
    }

    class Class
    {
        private List<Student> Students;
        public Class()
        {
            Students = new List<Student>();
            Student Taha = new Student(01, "Taha Yaseen", 15, "Male");
            Student Talha = new Student(02, "M. Talha", 20, "Male");
            Student Hamza = new Student(03, "Hamza Malik", 22, "Male");
            Student Sarah = new Student(04, "Sarah Cadbury", 21, "Female");
            Students.Add(Taha);
            Students.Add(Talha);
            Students.Add(Hamza);
            Students.Add(Sarah);
        }

        public string this[int StudentId]
        {
            get
            {
                return Students.FirstOrDefault(Student => Student.StudentId == StudentId).StudentName;
            }
            set
            {
                Students.FirstOrDefault(Student => Student.StudentId == StudentId).StudentName = value;
            }
        }

        // public string this[string StudentName]
        // {
        //     get
        //     {
        //         return Students.FirstOrDefault(Student => Student.StudentName == StudentName).StudentName;
        //     }
        //     set
        //     {
        //         Students.FirstOrDefault(Student => Student.StudentName == StudentName).StudentName = value;
        //     }
        // }

        public string this[string StudentGender]
        {
            get
            {
                return Students.Count(Student => Student.StudentGender == StudentGender).ToString();
            }
            set
            {
                foreach (Student Student in Students)
                {
                    if (Student.StudentGender == StudentGender)
                    {
                        Student.StudentGender = value;
                    }
                }
            }
        }

    }

    class Program
    {
        static void Lesson65Main()
        {
            Class Class = new Class();

            String Taha = Class[1];
            Console.WriteLine(Taha);

            Class[1] = "The Taha Yaseen";
            Taha = Class[1];
            Console.WriteLine(Taha);

            // String Hamza = Class["Hamza Malik"];
            // Console.WriteLine(Hamza);

            // Class["Hamza Malik"] = "Malik Sahab";
            // Hamza = Class["Malik Sahab"];
            // Console.WriteLine(Hamza);

            String CountOfMales = Class["Male"];
            Console.WriteLine("Count Of Male Students: {0}", CountOfMales);

            Class["Male"] = "Man";
            CountOfMales = Class["Male"];
            Console.WriteLine("Count Of Male Students: {0}", CountOfMales);
            string CountOfMen = Class["Man"];
            Console.WriteLine("Count Of Men: {0}", CountOfMen);
        }
    }
}

namespace Lesson62
// Partial classes divided into 2 other files
{

}

namespace HackerRank
// Project Euler
{

    class Solution
    {

        static void HackerRankMain(String[] args)
        {
            int t = Convert.ToInt32(Console.ReadLine());
            if (t >= 1 && t <= Math.Pow(10, 5))
            {
                for (int a0 = 0; a0 < t; a0++)
                {
                    int n = Convert.ToInt32(Console.ReadLine());
                    if (n >= 1 && n <= Math.Pow(10, 5))
                    {
                        long Sum = SumOfMultiplesOf3And5Below(n);
                        Console.WriteLine(Sum);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
        }

        public static long SumOfMultiplesOf3And5Below(int Number)
        {
            long Sum = 0;
            for (int Iteration = Number - 1; Iteration > 0; Iteration--)
            {
                if (Iteration % 3 == 0 || Iteration % 5 == 0)
                {
                    Sum += Iteration;
                }
            }

            for (int Jteration = Number - 1; Jteration > 0; Jteration--)
            {
                if (Jteration % 15 == 0)
                {
                    Sum -= Jteration;
                }
            }

            return Sum;
        }
    }
}

namespace Lesson61
{
    class Program()
    {
        static void Lsson61Main()
        {
            PartialEmployee Taha = new PartialEmployee(01, "Taha Yaseen", 15);
            string EmployeeId = Taha.GetEmployeeId();
            Console.WriteLine("Employee Id: {0}", EmployeeId);
        }
    }
}

namespace Lesson60
{
    class Program
    {
        static void Lesson60Main()
        {

            string[] SentenceComponents = new string[] { "I", " am", " Ironman!" };

            string StringSentence = "";
            foreach (string SentenceComponent in SentenceComponents)
            {
                StringSentence += SentenceComponent;
            }
            // Due to string being immutable, everytime this loop runs a new instance in heap is created, creating alot of garbage
            Console.WriteLine(StringSentence);

            StringBuilder StringBuilderSentence = new StringBuilder("");
            foreach (string SentenceComponent in SentenceComponents)
            {
                StringBuilderSentence.Append(SentenceComponent);
            }
            // This is much efficient since string builder is mutable so the same instance is changed everytime
            Console.WriteLine(StringBuilderSentence);

        }
    }
}

namespace Lesson59
{
    class Program
    {
        static void Lesson59Main()
        {

            Lesson58.Employee? Taha = new Lesson58.Employee(01, "Taha Yaseen");

            string TahaConvertStringRep = Convert.ToString(Taha);
            string TahaStringRep = Taha.ToString();

            Console.WriteLine("Convert Rep: '{0}'", TahaConvertStringRep);
            Console.WriteLine("Rep: '{0}'", TahaStringRep);

            // Standard .ToString() Gives Null Exception Whereas Convert.ToString() Converts To Empty String

            Taha = null;

            TahaConvertStringRep = Convert.ToString(Taha);
            TahaStringRep = Taha.ToString();

            Console.WriteLine("Convert Rep: '{0}'", TahaConvertStringRep);
            Console.WriteLine("Rep: '{0}'", TahaStringRep);

        }
    }
}

namespace Lesson58
{

    public class Employee
    {
        public int _Id;
        public string _Name;

        public Employee(int Id, string Name)
        {
            this._Id = Id;
            this._Name = Name;
        }

        public override string ToString()
        {
            return String.Format("{0} : {1}", this._Id, this._Name);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (!(obj is Employee))
            {
                return false;
            }
            return this._Id == ((Employee)obj)._Id && this._Name == ((Employee)obj)._Name;
        }

        public override int GetHashCode()
        {
            return this._Id.GetHashCode() ^ this._Name.GetHashCode();
        }
    }

    public class Program
    {
        static void Lesson58Main()
        {
            Employee Taha = new Employee(01, "Taha Yaseen");
            Employee SecondTaha = new Employee(01, "Taha Yaseen");
            Console.WriteLine(Taha.ToString());
            Console.WriteLine(Taha.Equals(SecondTaha));
        }
    }

}

namespace Lesson57
// Overriding ToString() Method
{
    public class Employee
    {
        public int _Id;
        public string _Name;

        public Employee(int Id, string Name)
        {
            this._Id = Id;
            this._Name = Name;
        }

        public override string ToString()
        {
            return String.Format("{0} : {1}", this._Id, this._Name);
        }
    }

    public class Program
    {
        static void Lesson57Main()
        {
            Employee Taha = new Employee(01, "Taha Yaseen");
            Console.WriteLine(Taha.ToString());
        }
    }

}

namespace Lesson56
// Generics
{
    class Calculator<ArgumentType>
    {

        // Converting Value Types To Refrence Type Is Boxing
        // Object is a class

        public static bool AreEqual(ArgumentType FirstValue, ArgumentType SecondValue)
        {
            return FirstValue.Equals(SecondValue);
        }

        // public static bool AreEqual<ArgumentType>(ArgumentType FirstValue, ArgumentType SecondValue)
        // {
        //     return FirstValue.Equals(SecondValue);
        // }

        // public static bool AreEqual(object FirstValue, object SecondValue)
        // {
        //     return FirstValue == SecondValue;
        // }

        // public static bool AreEqual(int FirstValue, int SecondValue)
        // {
        //     return FirstValue == SecondValue;
        // }

    }

    class Program
    {
        static void Lesson56Main()
        {
            string FirstValue = "Taha";
            string SecondValue = "Taha";
            // bool IsFirstAndSecondValueEqual = Calculator.AreEqual<string>(FirstValue, SecondValue);
            bool IsFirstAndSecondValueEqual = Calculator<string>.AreEqual(FirstValue, SecondValue);

            Console.WriteLine("Generics make your code type independent! Generally used in System.Collections.");
            Console.WriteLine("Are both values equal? {0}", IsFirstAndSecondValueEqual);

        }
    }
}

namespace Lesson55
// Late Binding
{
    class Program
    {
        static void Lesson55Main()
        {
            Assembly CurrentExectingAssembly = Assembly.GetExecutingAssembly();
            Type EmployeeType = CurrentExectingAssembly.GetType("Lesson53.Employee");

            object EmployeeInstance = Activator.CreateInstance(EmployeeType);
            MethodInfo GetEmployeeIdMethod = EmployeeType.GetMethod("GetEmployeeId");

            int FirstParameter = 1;
            string SecondParameter = "Taha";
            object[] Parameters = [FirstParameter, SecondParameter];

            string EmployeeId = (string)GetEmployeeIdMethod.Invoke(EmployeeInstance, Parameters);
            Console.WriteLine("Employee Id: {0}", EmployeeId);
        }
    }
}

namespace Lesson54
// Reflection Example
{
    class Program
    {
        static void Lesson54Main()
        {

            Console.WriteLine("Enter your type's name.");
            string TypeName = Console.ReadLine();
            Type Target = Type.GetType(TypeName);
            Console.WriteLine("");

            if (Target == null)
            {
                Console.WriteLine("Please make sure your type exists.");
            }

            else
            {
                Console.WriteLine("Full Name: {0}", Target.FullName);
                Console.WriteLine("Name: {0}", Target.Name);
                Console.WriteLine("Namespace: {0}", Target.Namespace);

                Console.WriteLine("Properties");
                PropertyInfo[] Properties = Target.GetProperties();
                foreach (PropertyInfo Property in Properties)
                {
                    Console.WriteLine("{0} {1}", Property.PropertyType.Name, Property.Name);
                }

                Console.WriteLine("Methods");
                MethodInfo[] Methods = Target.GetMethods();
                foreach (MethodInfo Method in Methods)
                {
                    Console.WriteLine("{0} {1}", Method.ReturnType.Name, Method.Name);
                }

                Console.WriteLine("Constructors");
                ConstructorInfo[] Constructors = Target.GetConstructors();
                foreach (ConstructorInfo Constructor in Constructors)
                {
                    Console.WriteLine("{0}", Constructor.ToString());
                }
            }

        }
    }
}

namespace Lesson53
// Reflection
{
    class Employee
    {
        public int _Id { get; set; }
        public string _Name { get; set; }

        public Employee() { }
        public Employee(int Id, string Name)
        {
            this._Id = Id;
            this._Name = Name;
        }

        public void PrintId()
        {
            Console.WriteLine(this._Id);
        }
        public void PrintName()
        {
            Console.WriteLine(this._Name);
        }
        public string GetEmployeeId(int _Id, String _Name)
        {
            return String.Format("Emp{0}{1}", _Id, _Name);
        }

    }

    class Program
    {
        static void Lesson53Main()
        {

            // Type Target = Type.GetType("Lesson53.Employee");
            // Type Target = typeof(Employee);

            Employee Taha = new Employee();
            Type Target = Taha.GetType();

            Console.WriteLine("Full Name: {0}", Target.FullName);
            Console.WriteLine("Name: {0}", Target.Name);
            Console.WriteLine("Namespace: {0}", Target.Namespace);

            PropertyInfo[] Properties = Target.GetProperties();
            foreach (PropertyInfo Property in Properties)
            {
                Console.WriteLine("{0} {1}", Property.PropertyType, Property.Name);
            }

            MethodInfo[] Methods = Target.GetMethods();
            foreach (MethodInfo Method in Methods)
            {
                Console.WriteLine("{0} {1}", Method.ReturnType, Method.Name);
            }

            ConstructorInfo[] Constructors = Target.GetConstructors();
            foreach (ConstructorInfo Constructor in Constructors)
            {
                Console.WriteLine("{0}", Constructor.ToString());
            }

        }
    }
}

namespace Lesson52
// Attributes : Obsolete
{
    class Calculator
    {
        [Obsolete("Use Add(List<int> Numbers) Method")]
        // [ObsoleteAttribute("Use Add(List<int> Numbers) Method")] // Adding "Attribute" Is Optional
        public static int Add(int FirstNumber, int SecondNumber)
        {
            return FirstNumber + SecondNumber;
        }

        public static int Add(List<int> Numbers)
        {
            int Sum = 0;
            foreach (int Number in Numbers)
            {
                Sum += Number;
            }

            return Sum;
        }
    }

    class Program
    {
        static void Lesson52Main()
        {
            List<int> Numbers = new List<int>() { 1, 2 };
            int Result = Calculator.Add(Numbers);
            Console.WriteLine("Result: {0}", Result);
        }
    }
}

namespace Lesson49
// Access Modifiers
{

    public class Employee
    {
        private int _Id;
        public string Name
        {
            get; set;
        }
        protected long _Salary;
    }

    class CorporateEmployee : Employee
    {
        public void WhisperSalary()
        {
            CorporateEmployee Rafay = new CorporateEmployee();
            Rafay._Salary = 52000; // Can be assigned to here since its in derivement containment or containment
            Console.WriteLine("Salary Of Rafay: {0}", Rafay._Salary);
        }
    }

    public class GovernmentEmployee : Employee
    {
        internal int _Id;
        protected internal int _UpperIncome;
    }


    class Program()
    {
        static void Lesson49Main()
        {
            Employee Taha = new Employee();
            // Taha._Id = 01; // Cannot be assigned since can be only changed within the containment of the class
            Taha.Name = "Taha Yaseen"; // Can be easily manipulated from anywhere
            CorporateEmployee Rafay = new CorporateEmployee();
            // Rafay._Salary = 52000; // Cannot be assigned here

            GovernmentEmployee Sarah = new GovernmentEmployee();
            Sarah._Id = 1; // Overhere you can access id due to it being internal (using it in its own assembly)
            Console.WriteLine("Sarah's Id: {0}", Sarah._Id);

            Sarah._UpperIncome = 52000; // Can be assigned in derived or original class and also, particularly in internal document both even combined act as individuals granting both possibilities
            Console.WriteLine("Sarah's Upper Income: {0}", Sarah._UpperIncome);
        }
    }
}

namespace Lesson48
// Regions & Types vs TypeMembers
{
    class Program()
    {
        static void Lesson48Main()
        {
            // #region RegionName can be used to create regions inside your code
            // #endregion to end any region 
        }
    }
}

#region EnumsLessons
namespace Lesson47
// Enums
{
    public enum Season
    {
        Summer,
        Winter,
        Autumn,
        Spring
    }

    class Program
    {
        static void Lesson47Main()
        {
            string[] SeasonNames = Enum.GetNames(typeof(Season));
            int[] SeasonValues = (int[])Enum.GetValues(typeof(Season));
            for (int iteration = 0; iteration < SeasonNames.Length; iteration++)
            {
                Console.WriteLine("{0}: {1}", SeasonValues[iteration], SeasonNames[iteration]);
            }
        }
    }
}

namespace Lesson46
{
    public enum Gender
    {
        Male, Female, Unknown
    }

    public class Person
    {
        public string _Name;
        public Gender _Gender;

        public Person(string Name, Gender Gender)
        {
            this._Name = Name;
            this._Gender = Gender;
        }
    }

    class Program
    {
        static void Lesson46Main()
        {
            Person Taha = new Person("Taha Yaseen", Gender.Male);
            Person Sarah = new Person("Sarah Cadbury", Gender.Female);
            Person AbdulRehman = new Person("Abdul Rehman", Gender.Unknown);
            List<Person> Persons = new List<Person> { Taha, Sarah, AbdulRehman };

            foreach (Person Person in Persons)
            {
                string PersonGender;
                switch (Person._Gender)
                {
                    case Gender.Male:
                        PersonGender = "Male";
                        break;
                    case Gender.Female:
                        PersonGender = "Female";
                        break;
                    default:
                        PersonGender = "Unknown";
                        break;
                }
                Console.WriteLine("Gender Of {0} is {1}", Person._Name, PersonGender);
            }
        }
    }
}
#endregion

namespace Lesson45
// Why Enums?
{
    public class Person
    {
        public string _Name;
        public bool? _Gender;

        public Person(string Name, bool? Gender)
        {
            this._Name = Name;
            this._Gender = Gender;
        }
    }

    class Program
    {
        static void Lesson45Main()
        {
            Person Taha = new Person("Taha Yaseen", true);
            Person Sarah = new Person("Sarah Cadbury", false);
            Person AbdulRehman = new Person("Abdul Rehman", null);
            List<Person> Persons = new List<Person> { Taha, Sarah, AbdulRehman };

            foreach (Person Person in Persons)
            {
                string Gender;
                switch (Person._Gender)
                {
                    case true:
                        Gender = "Male";
                        break;
                    case false:
                        Gender = "Female";
                        break;
                    case null:
                        Gender = "Unknown";
                        break;
                }
                Console.WriteLine("Gender Of {0} is {1}", Person._Name, Gender);
            }
        }
    }
}

namespace Lesson43
// Exception Handling Abuse
{
    class Program
    {
        static void Lesson43Main()
        {
            try
            {

                Console.WriteLine("Enter your numerator.");

                int Numerator;
                bool IsNumeratorValid = int.TryParse(Console.ReadLine(), out Numerator);

                if (!IsNumeratorValid)
                {
                    throw new Exception(String.Format("Numerator shall be a valid number between {0} till {1}.", Int32.MinValue, Int32.MaxValue));
                }

                Console.WriteLine("Enter your dominator.");
                int Denominator;
                bool IsDenominatorValid = int.TryParse(Console.ReadLine(), out Denominator);

                if (!IsDenominatorValid)
                {
                    throw new Exception(String.Format("Denominator shall be a valid number between {0} till {1}", Int32.MinValue, Int32.MaxValue));
                }
                else if (Denominator == 0)
                {
                    throw new Exception("Denominator should not be zero.");
                }

                int Result = Numerator / Denominator;
                Console.WriteLine("Result: {0}", Result);
            }

            // This is exception handling abuse
            // catch (FormatException)
            // {
            //     Console.WriteLine("Please enter numbers.");
            // }
            // catch (OverflowException)
            // {
            //     Console.WriteLine("Please enter numbers in the range {0} till {1}.", Int32.MinValue, Int32.MaxValue);
            // }
            // catch (DivideByZeroException)
            // {
            //     Console.WriteLine("Denominator cannot be zero.");
            // }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
            }
        }
    }
}

namespace Lesson42
// Custom Exceptions
{
    class Program
    {
        class UserAlreadyLoggedInException : Exception
        {
            public UserAlreadyLoggedInException() : base() { }
            public UserAlreadyLoggedInException(string Message) : base(Message) { }
            public UserAlreadyLoggedInException(string Message, Exception InnerException) : base(Message, InnerException) { }
            public UserAlreadyLoggedInException(SerializationInfo Info, StreamingContext Context) : base(Info, Context) { }
        }

        static void Lesson42Main()
        {
            // throw new UserAlreadyLoggedInException();
            // throw new UserAlreadyLoggedInException("Unsure of creating duplicate session");
            try
            {
                throw new UserAlreadyLoggedInException("Unsure of creating duplicate session", new FileNotFoundException());
            }
            catch (Exception Error)
            {
                Console.WriteLine("An error occured, {0}.", Error.Message);
            }

        }
    }
}

namespace Lesson41
// Inner Exception
{
    class Program
    {
        static void Lesson41Main()
        {
            try
            {
                try
                {
                    Console.WriteLine("Enter your first number.");
                    int FirstNumber = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter your second number.");
                    int SecondNumber = Convert.ToInt32(Console.ReadLine());

                    int Result = FirstNumber / SecondNumber;
                    Console.WriteLine("Result: {0}", Result);
                }
                catch (Exception Error)
                {
                    string ErrorLogPath = @"C:\Users\TheTahaYaseen\Documents\F#.txt";

                    if (File.Exists(ErrorLogPath))
                    {
                        StreamWriter ErrorLogWriter = new StreamWriter(ErrorLogPath);

                        string ErrorType = Error.GetType().Name;
                        string ErrorMessage = Error.Message;
                        string ErrorLog = String.Format("{0}: {1}.", ErrorType, ErrorMessage);

                        ErrorLogWriter.Write(ErrorLog);
                        ErrorLogWriter.Close();

                        Console.WriteLine("An error occured, please try again later.");
                    }

                    else
                    {
                        throw new FileNotFoundException(String.Format("{0} does not exist; ", ErrorLogPath), Error);
                    }

                }
            }
            catch (Exception Error)
            {
                Console.WriteLine("Current Exception: {0}; Inner Exception: {1};", Error.GetType().Name, (Error.InnerException?.GetType().Name ?? "None"));
            }
        }
    }
}

namespace Lesson40
// ExceptionHandling
{
    class Program
    {
        static void Lesson40Main()
        {
            StreamReader StreamReader = null;
            try
            {
                StreamReader = new StreamReader(@"C:\Users\TheTahaYaseen\Documents\C#.txt");
                string TextInReader = StreamReader.ReadToEnd();

                Console.WriteLine("Text in Reader: {0}", TextInReader);
            }
            catch (FileNotFoundException FileNotFoundError)
            {
                Console.WriteLine("Please make sure that {0} exists.", FileNotFoundError.FileName);
                Console.WriteLine("ExceptionMessage: {0}\nFile Not Found: {1}\nExceptionStackTrace: {2}", FileNotFoundError.Message, FileNotFoundError.FileName, FileNotFoundError.StackTrace);
            }
            catch (Exception GeneralError)
            {
                Console.WriteLine("Exception Message: {0}", GeneralError.Message);
            }
            finally
            {
                if (StreamReader != null)
                {
                    StreamReader.Close();
                    Console.WriteLine("StreamReader closed.");
                }
            }
        }
    }
}

namespace Lesson39
// Multicast Delegates
{
    class Program
    {
        public static void Announce(out string Output)
        {
            // Console.WriteLine("Announce");
            // return "Announce";
            Output = "Announce";
        }

        public static void Narrate(out string Output)
        {
            // Console.WriteLine("Narrate");
            // return "Narrate";
            Output = "Narrate";
        }

        public static void Whisper(out string Output)
        {
            // Console.WriteLine("Whisper");
            // return "Whisper";
            Output = "Whisper";
        }

        // public delegate void SayingDelegate();
        // public delegate string SayingDelegate();
        public delegate void SayingDelegate(out string Output);

        static void Lesson39Main()
        {
            SayingDelegate SayingDelegate, AnnouncementDelegate, NarrationDelegate, WhisperDelegate;

            AnnouncementDelegate = new SayingDelegate(Announce);
            NarrationDelegate = new SayingDelegate(Narrate);
            WhisperDelegate = new SayingDelegate(Whisper);

            SayingDelegate = AnnouncementDelegate + NarrationDelegate + WhisperDelegate - AnnouncementDelegate;

            // SayingDelegate = AnnouncementDelegate;
            // SayingDelegate += NarrationDelegate;
            // SayingDelegate += WhisperDelegate;
            // SayingDelegate -= AnnouncementDelegate;

            // Last method's return value will be returned
            // string ReturnOfDelegate = SayingDelegate();
            // Console.WriteLine("ReturnOfDelegate: {0}", ReturnOfDelegate);

            // Last method's output value will be assigned
            string OutputOfDelegate;
            SayingDelegate(out OutputOfDelegate);
            Console.WriteLine("OutputOfDelegate: {0}", OutputOfDelegate);

        }
    }
}

namespace Lesson37And38
// Delegates Usage
{

    class PromotionCriterias
    {
        public static bool Experience(Employee Employee)
        {
            if (Employee._Experience >= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class StarkIndustries
    {

        public static void ListEmployeesToPromote()
        {
            List<Employee> _Employees = new List<Employee>();
            Employee Tony = new Employee(1, "Tony", 13, 255000000);
            Employee Howard = new Employee(2, "Howard", 31, 1000000);
            Employee Pepper = new Employee(3, "Pepper", 4, 100000);
            _Employees.Add(Tony);
            _Employees.Add(Howard);
            _Employees.Add(Pepper);

            MatchesPromotionCriteria MatchesPromotionCriteria = new MatchesPromotionCriteria(PromotionCriterias.Experience);
            // Using Lambda Functions
            // MatchesPromotionCriteria MatchesPromotionCriteria = new MatchesPromotionCriteria(Employee => Employee._Experience >= 5);
            Employee.ListEmployeesToPromote(_Employees, MatchesPromotionCriteria);
        }

    }

    delegate bool MatchesPromotionCriteria(Employee Employee);

    class Employee
    {
        public int _Id;
        public string _Name;
        public int _Experience;
        public int _Salary;

        public Employee(int Id, string Name, int Experience, int Salary) { this._Id = Id; this._Name = Name; this._Experience = Experience; this._Salary = Salary; }

        public static void ListEmployeesToPromote(List<Employee> Employees, MatchesPromotionCriteria MatchesPromotionCriteria)
        {
            foreach (Employee Employee in Employees)
            {
                if (MatchesPromotionCriteria(Employee))
                {
                    Console.WriteLine("{0} needs to be promoted.", Employee._Name);
                }
            }
        }

    }

    class Program
    {
        static void Lesson37And38Main()
        {
            StarkIndustries StarkIndustries = new StarkIndustries();
            StarkIndustries.ListEmployeesToPromote();
        }
    }
}

namespace Lesson36
// Delegates
{
    class Program
    {

        public delegate void PushMessage(string Message);

        public static void Announce(string Message)
        {
            Console.WriteLine(Message);
        }

        static void Lesson36Main()
        {
            PushMessage DoAnnouncement = new PushMessage(Announce);
            DoAnnouncement("I am learning c#.");
        }
    }
}

namespace Lesson35
{

    interface IJunior
    {
        void CopyPaste();
    }

    interface ISenior
    {
        void Refactor();
    }

    public class Junior : IJunior
    {
        public void CopyPaste()
        {
            Console.WriteLine("CopyPaste.");
        }
    }

    public class Senior : ISenior
    {
        public void Refactor()
        {
            Console.WriteLine("Refactor.");
        }
    }

    public class Programmer : IJunior, ISenior
    {

        Junior Junior = new Junior();
        Senior Senior = new Senior();

        public void CopyPaste()
        {
            Junior.CopyPaste();
        }

        public void Refactor()
        {
            Senior.Refactor();
        }
    }

    class Program
    {
        static void Lesson35Main()
        {
            Programmer Programmer = new Programmer();
            Programmer.CopyPaste();
            Programmer.Refactor();
        }
    }
}

namespace Lesson33
// Abstract Classes vs Interfaces & Why no multiple class inheritance allowed?
{

    // Diamond problem
    class A
    {
        public virtual void Greet()
        {
            Console.WriteLine("Hi!");
        }
    }
    class B : A
    {
        public override void Greet()
        {
            Console.WriteLine("Hey!");
        }
    }

    class C : A
    {
        public override void Greet()
        {
            Console.WriteLine("Ola!");
        }
    }

    // class D : B, C
    // {
    // If it does not implement any method what shall D.Greet() do and thats why multi class inheritance is not allowed
    // }

    public interface ICoder
    {

    }

    public interface IProgrammer : ICoder // : Coder (only inherits from interfaces)
    {

        // cannot have fields
        // private string FavoriteLanguage;

        void Introduction();

        // cannot have access modifiers
        void Greet()
        {
            Console.WriteLine("Hi");
        }
    }

    public abstract class Programmer : ICoder
    {
        // can have fields
        private string FavoriteLanguage;

        public abstract void Introduction();

        // can have access modifiers
        public void Greet()
        {
            Console.WriteLine("Hi");
        }
    }

    // inherits from abstract classes and interfaces but only class at a time but can from multiple interfaces at a time
    public abstract class SoftwareEngineer : Programmer { }

    public abstract class CopyPaster : ICoder, IProgrammer
    {
        public abstract void Introduction();
    }

    class Program
    {
        static void Lesson33Main()
        {
        }
    }
}

namespace Lesson32
// Abstract Classes Cannot Be Instancized And Have Some Missing Implementation; Can Be Inherited From
{
    public abstract class Programmer
    {
        public abstract void AnnounceStack();
    }

    public class DjangoDev : Programmer
    {
        public override void AnnounceStack()
        {
            Console.WriteLine("I use django for backend apis and flutter for the main ui.");
        }
    }
    class Program
    {
        static void Lesson32Main()
        {
            DjangoDev Taha = new DjangoDev();
            Taha.AnnounceStack();
        }
    }
}

namespace Lesson30And31
// Interfaces
{
    interface IJunior
    {
        void Copy();
        void Paste();
    }

    interface ISenior : IJunior
    {
        void Refactor();
    }

    interface IWebDev : ISenior
    {
        void Program();
    }

    interface IAppDev : ISenior
    {
        void Program();
    }

    // Multi Interface Inheritance Allowed But Not Multiple Classes Inheritance
    class SeniorWebDev : IWebDev, IAppDev
    {
        public void Copy()
        {
            Console.WriteLine("Copy");
        }

        public void Paste()
        {
            Console.WriteLine("Paste");
        }

        public void Refactor()
        {
            Console.WriteLine("Refactor");
        }

        public void Program()
        {
            Console.WriteLine("Web Dev");
        }

        void IAppDev.Program()
        {
            Console.WriteLine("App Dev");
        }

    }


    class Program
    {
        static void Lesson30And31fMain()
        {
            SeniorWebDev Dev = new SeniorWebDev();
            Dev.Copy();
            Dev.Paste();
            Dev.Refactor();
            Dev.Program();
            ((IAppDev)Dev).Program();
        }
    }
}

namespace Lessonn29
// Structs Vs Classes
{
    class Program
    {
        sealed class ReferenceClass : Program
        // Sealed means it cannot be inherited from but still it can inherit even if sealed
        {
            public string _PlaceholderValue;

            public ReferenceClass(string PlaceholderValue)
            {
                this._PlaceholderValue = PlaceholderValue;
            }

            // Can have parameter less constructor
            public ReferenceClass() { }

            // Can have destructor
            ~ReferenceClass() { }

        }
        struct ReferenceStruct
        {
            public string _PlaceholderValue;

            public ReferenceStruct(string PlaceholderValue)
            {
                this._PlaceholderValue = PlaceholderValue;
            }

            // Cannot have parameter less constructor
            // Cannot have destructor

            // Sealed by default, meaning it cannot be inherited from anyways and it can only inherit from interfaces

        }

        static void Lesson29Main()
        {
            // Struct Is A Value Type - Meaning It Directly Stores The Value In The Stack
            // Class Is A Refrence Type - Whereas Is A Refrence To Values Stored In Heap (Later To Be Cleared By The Garbage Collector) And Refrence Stored In Stack

            // Any value type is destroyed after its scope is lost mmediately from the stack whereas for any refrence type, only the refrence is destroyed from stack
            // And, the values from the hea are destroyed sometime after by the garbage collector
            // Also, that since classes are refrences to values, if you copy class, then refrence is copied and if you alter any value from within the two, both will be altered
            // Whereas value types such as integers, when copied and one is altered, only the one being messed with will change

            int FirstValue = 1;
            int SecondValue = FirstValue;
            SecondValue++;

            Console.WriteLine("First Value: {0}, Second Value: {1}", FirstValue, SecondValue);

            ReferenceClass FirstReferenceClass = new ReferenceClass("Class Placeholder 1");
            ReferenceClass SecondReferenceClass = FirstReferenceClass;
            SecondReferenceClass._PlaceholderValue = "Class Placeholder 2";

            Console.WriteLine("First Reference Class: {0}, Second Reference Class: {1}", FirstReferenceClass._PlaceholderValue, SecondReferenceClass._PlaceholderValue);

            ReferenceStruct FirstReferenceStruct = new ReferenceStruct("Struct Placeholder 1");
            ReferenceStruct SecondReferenceStruct = FirstReferenceStruct;
            SecondReferenceStruct._PlaceholderValue = "Struct Placeholder 2";

            Console.WriteLine("First Reference Struct: {0}, Second Reference Struct: {1}", FirstReferenceStruct._PlaceholderValue, SecondReferenceStruct._PlaceholderValue);

        }
    }

}

namespace Lesson28
// Structs
{
    public struct Employee
    {
        private int _Id;
        private string _Name;

        public int Id
        {
            set
            {
                if (int.IsNegative(value))
                {
                    throw new Exception("Employee Id Cannot Be Negative.");
                }
                else
                {
                    this._Id = value;
                }
            }
            get
            {
                return this._Id;
            }
        }

        public string Name
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Employee Name Cannot Be Null.");
                }
                else
                {
                    this._Name = value;
                }
            }

            get
            {
                return string.IsNullOrEmpty(this._Name) ? "No Name Assigned." : this._Name;
            }
        }

        public Employee(int Id, string Name)
        {
            this._Id = Id;
            this._Name = Name;
        }

        public void PrintDetails()
        {
            Console.WriteLine("Employee's id is {0} and his/her name is {1}.", this._Id, this._Name);
        }
    }

    class Program
    {
        static void Lesson28Main()
        {
            Employee Taha = new Employee(1, "Taha Yaseen");
            Taha.PrintDetails();

            Employee Rafay = new Employee();
            Rafay.Id = 2;
            Rafay.Name = "Rafay Yaseen";
            Rafay.PrintDetails();

            Employee Talha = new Employee
            {
                Id = 3,
                Name = "Talha Anjum"
            };
            Talha.PrintDetails();

        }
    }

}

namespace Lesson26And27
// Why Properties And Properties
{
    public class Employee
    {
        private int _Id;
        private string _Name;
        static private int _BaseSalary = 52000;

        public void SetId(int Id)
        {
            if (int.IsNegative(Id))
            {
                throw new Exception("Employee Id Cannot Be Negative.");
            }
            else
            {
                this._Id = Id;
            }
        }

        public int GetId()
        {
            return this._Id;
        }

        public void SetName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Employee Name Cannot Be Null.");
            }
            else
            {
                this._Name = Name;
            }
        }

        public string GetName()
        {
            return string.IsNullOrEmpty(this._Name) ? "No Name Assigned." : this._Name;
        }

        public int GetBaseSalary()
        {
            return _BaseSalary;
        }

    }

    public class CSharpEmployee
    {
        private int _Id;
        private string _Name;
        static private int _BaseSalary = 52000;

        public string Framework
        {
            get;
            set;
        }

        public int Id
        {
            set
            {
                if (int.IsNegative(value))
                {
                    throw new Exception("Employee Id Cannot Be Negative.");
                }
                else
                {
                    this._Id = value;
                }
            }

            get
            {
                return this._Id;
            }

        }

        public string Name
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Employee Name Cannot Be Null.");
                }
                else
                {
                    this._Name = value;
                }
            }

            get
            {
                return string.IsNullOrEmpty(this._Name) ? "No Name Assigned." : this._Name;
            }
        }

        public int BaseSalary
        {
            get
            {
                return _BaseSalary;
            }
        }

    }

    class Program
    {
        public static void Lesson26And27Main()
        {
            Employee Taha = new Employee();
            Taha.SetId(1);
            Taha.SetName("Taha Yaseen");
            int BaseSalaryOfTaha = Taha.GetBaseSalary();
            int IdOfTaha = Taha.GetId();
            string NameOfTaha = Taha.GetName();
            Console.WriteLine("Id: {0}, Name: {1} & Base Salary: {2}.", IdOfTaha, NameOfTaha, BaseSalaryOfTaha);

            CSharpEmployee Rafay = new CSharpEmployee();
            Rafay.Id = 2;
            Rafay.Name = "Rafay Yaseen";
            Rafay.Framework = "ASP .NET";
            int BaseSalary = Rafay.BaseSalary;
            int Id = Rafay.Id;
            string Name = Rafay.Name;
            Console.WriteLine("Id: {0}, Name: {1} & Base Salary: {2}.", Id, Name, BaseSalary);

        }
    }
}

namespace Lesson25
// Method Overloading
{
    public class Program
    {
        public static void Lesson25Main()
        {

        }

        // Method overloading is when you have many different methods/functions with the same name
        // but different signature (meaning its name and type, kind and number of forma parameters excluding return type and params array)

        static int Add(int FirstNumber, int SecondNumber)
        {
            return FirstNumber + SecondNumber;
        }

        static float Add(float FirstNumber, float SecondNumber)
        {
            return FirstNumber + SecondNumber;
        }

        static float Add(int FirstNumber, float SecondNumber)
        {
            return FirstNumber + SecondNumber;
        }

        static int Add(int FirstNumber, int SecondNumber, int ThirdNumber)
        {
            return FirstNumber + SecondNumber + ThirdNumber;
        }

        static int Add(int FirstNumber, int SecondNumber, out int ThirdNumber)
        {
            ThirdNumber = 0;
            return FirstNumber + SecondNumber;
        }

        // All These Have Different Kind, Types And Number Of Parameters

    }
}

namespace Lesson21Till24
// Inheritance (Inheriting Properties And Methods From Similar Class Reducing Redundancy),
// Method Hiding,
// PolyMorphism (Performing any action with base class' reference variable value by overriding base class' method)
// And Difference Between Method Overiding And Hiding; When Overriden, the new method will be called whereas When Hidden using new keyword, the base class method will still be called (if reference variable is of type base class.
{
    public class Employee // For TheTahaYaseen Industries
    {
        public string _FullName;
        public string _Email;

        public Employee(string FullName, string Email)
        {
            this._FullName = FullName;
            this._Email = Email;
        }

        // Virtual tells the derived class taht you can override it if willing, otherwise it will stay same
        public virtual void PrintDetails()
        {
            Console.WriteLine("Employee's name is {0} and his/her email is {1}!", this._FullName, this._Email);
        }
    }

    public class FullTimeEmployee : Employee
    {
        public float _YearlySalary;

        public FullTimeEmployee(string FullName, string Email, float YearlySalary) : base(FullName, Email)
        {
            this._YearlySalary = YearlySalary;
        }

        public new void PrintDetails()
        {
            Console.WriteLine("Full Time Employee's name is {0} and his/her email is {1}!", this._FullName, this._Email);
        }
    }
    public class PartTimeEmployee : Employee
    {
        public float _HourlyRate;

        public PartTimeEmployee(string FullName, string Email, float HourlyRate) : base(FullName, Email)
        {
            this._HourlyRate = HourlyRate;
        }

        // Override tells to override base class' method
        public override void PrintDetails()
        {
            Console.WriteLine("Part Time Employee's name is {0} and his/her email is {1}!", this._FullName, this._Email);
        }
    }

    class Program
    {
        public static void Lesson21Till24Main()
        {

            FullTimeEmployee Taha = new FullTimeEmployee("Taha Yaseen", "thetahayaseen@gmail.com", 50000);
            PartTimeEmployee Rafay = new PartTimeEmployee("Rafay Yaseen", "rarti@gmail.com", 5);
            Employee Umar = new Employee("Umar Usman", "umarusman@gmail.com");

            Employee[] Employees = new Employee[] { Taha, Rafay, Umar };

            foreach (Employee Employee in Employees)
            {
                Employee.PrintDetails();
            }

        }
    }
}

namespace Lesson20
// Static And Instance Class Members
{

    class Circle
    {
        static float _Pi;
        int _Radius;

        // Static Instructor Called Before Anything
        static Circle()
        {
            Circle._Pi = 3.142f;
        }

        // Instance Constructor Called After Static Methods & Variables
        public Circle(int Radius)
        {
            this._Radius = Radius;
        }

        public static float GetFloatValue()
        {
            return _Pi;
        }

        public float CalculateArea()
        {
            return _Pi * _Radius * _Radius;
        }

    }

    class Program
    {
        public static void Lesson20Main()
        {

            Circle DiagramA = new Circle(10);
            float DiagramAArea = DiagramA.CalculateArea();
            Console.WriteLine("Area Of Diagram A: {0}", DiagramAArea);

            float ValueOfFloat = Circle.GetFloatValue();
            Console.WriteLine("Value Of Float: {0}", ValueOfFloat);
        }
    }
}

namespace Lesson19
// Classes 
{
    class VideoDuration
    {
        int _Hours;
        int _Minutes;
        int _Seconds;

        public VideoDuration(int Hours, int Minutes, int Seconds)
        {
            this._Hours = Hours;
            this._Minutes = Minutes;
            this._Seconds = Seconds;
        }

        public int[] GetDurationComponents()
        {
            int[] DurationComponents = new int[] { this._Hours, this._Minutes, this._Seconds };
            return DurationComponents;
        }

        public string GetStringDuration()
        {
            string Duration = String.Format("{0}:{1}:{2}", this._Hours, this._Minutes, this._Seconds);
            return Duration;
        }

        ~VideoDuration()
        {

        }

    }

    class Lesson
    {
        string _Topic;
        VideoDuration? _Duration;

        public Lesson() : this(null, null) { }

        public Lesson(string Topic, VideoDuration? Duration)
        {
            this._Topic = Topic;
            this._Duration = Duration;
        }

        public string[] GetDetails()
        {
            string[] Details = new string[] { this._Topic, this._Duration?.GetStringDuration() ?? null };
            return Details;
        }

        ~Lesson()
        {

        }
    }

    class Program
    {
        public static void Lesson19Main()
        {
            VideoDuration Lesson19Duration = new VideoDuration(0, 19, 52);
            Lesson Lesson19 = new Lesson("Classes", Lesson19Duration);

            string[] Lesson19Details = Lesson19.GetDetails();
            Console.WriteLine("Topic: \"{0}\" with Duration of \"{1}\".", Lesson19Details[0], Lesson19Details[1]);

            Lesson Lesson20 = new Lesson();

            string[] Lesson20Details = Lesson20.GetDetails();
            Console.WriteLine("Topic: \"{0}\" with Duration of \"{1}\".", Lesson20Details[0], Lesson20Details[1]);
        }
    }
}

namespace Lesson18
// Namespaces
{
    class Program
    {
        public static void Lesson18Main()
        {
            Jarvis.Narrate("I am Jarvis");
            Friday.Narrate("I am Friday");
        }
    }

}

namespace Lesson17
// Method Parameters & Arguments
{
    class Program
    {
        public static void Lesson17Main()
        {
            int MyIncomeInUSD = 1000;

            // Value Parameter
            AssignMultipliedBy10(MyIncomeInUSD);
            Console.WriteLine(MyIncomeInUSD.ToString());

            // Refrence Parameter
            ReallyAssignMultipliedBy10(ref MyIncomeInUSD);
            Console.WriteLine(MyIncomeInUSD.ToString());

            // Output Parameter
            int MyIncomeMultipliedBy10;
            int MyIncomeDividedBy10;

            DivideAndMultiplyBy10(MyIncomeInUSD, out MyIncomeDividedBy10, out MyIncomeMultipliedBy10);
            Console.WriteLine("Income Multiplied By 10: {0}; Income Divided By 10: {1};", MyIncomeMultipliedBy10, MyIncomeDividedBy10);

            // Params Array Has To Be The Last Parameter And Only One Params List Allowed Of Any Data Type
            GreetAll("Taha", "Musk", "Stark", "Ferguson");
            GreetAll();
        }

        public static void GreetAll(params string[] Names)
        {
            if (Names.Length != 0)
            {
                foreach (string Name in Names)
                {
                    Console.WriteLine("Hey, {0}! How are you? Fine, I hope so.", Name);
                }
            }
            else
            {
                Console.WriteLine("Hey, To whomever hearing! How are you? Fine, I hope so.");
            }
        }

        public static void AssignMultipliedBy10(int value)
        {
            value = value * 10;
        }

        public static void ReallyAssignMultipliedBy10(ref int value)
        {
            value = value * 10;
        }

        public static void DivideAndMultiplyBy10(int value, out int DividedBy10, out int MultipliedBy10)
        {
            MultipliedBy10 = value * 10;
            DividedBy10 = value / 10;
        }
    }
}

namespace Lesson16
// Methods
{
    class Program
    {
        public static void Lesson16Main()
        {
            Program.PrintEvenNumbersTill(30);
            Program program = new Program();
            int CheckDigitOf192 = program.CreateCheckDigit(192);
            Console.WriteLine(CheckDigitOf192);
        }

        public int CreateCheckDigit(int Value)
        {
            int RemainderBy11 = Value / 11;
            int CheckDigit = 11 - RemainderBy11;
            return CheckDigit;
        }

        public static void PrintEvenNumbersTill(int Range)
        {
            for (int iteration = 0; iteration < Range; iteration++)
            {
                if (iteration % 2 == 0)
                {
                    Console.WriteLine("Even Number: {0}", iteration);
                }
            }
        }
    }
}

namespace Lesson15
// For And For Each Loop
{
    class Program
    {
        static void Lesson15Main()
        {
            int[] FavouriteNumbers = [7, 212, 970, 4133];

            Console.WriteLine(" ");
            Console.WriteLine("While loop");
            Console.WriteLine(" ");
            int iteration = 0;
            while (iteration < FavouriteNumbers.Length)
            {
                Console.WriteLine("{0}: {1}", iteration, FavouriteNumbers[iteration]);
                iteration++;
            }

            Console.WriteLine(" ");
            Console.WriteLine("For loop");
            Console.WriteLine(" ");
            for (int jteration = 0; jteration < FavouriteNumbers.Length; jteration++)
            {
                Console.WriteLine("{0}: {1}", jteration, FavouriteNumbers[jteration]);
            }

            Console.WriteLine(" ");
            Console.WriteLine("For each loop");
            Console.WriteLine(" ");
            foreach (int FavouriteNumber in FavouriteNumbers)
            {
                Console.WriteLine("{0}", FavouriteNumber);
            }

            Console.WriteLine(" ");
            Console.WriteLine("Break inside loops");
            Console.WriteLine(" ");
            for (int BreakNumber = 0; BreakNumber < 100; BreakNumber++)
            {
                if (BreakNumber % 42 == 0 && BreakNumber != 0)
                {
                    break;
                }
                Console.WriteLine(BreakNumber);
            }

            Console.WriteLine(" ");
            Console.WriteLine("Continue inside loops");
            Console.WriteLine(" ");
            for (int ContinueNumber = 0; ContinueNumber < 100; ContinueNumber++)
            {
                if (ContinueNumber % 3 == 1)
                {
                    continue;
                }
                Console.WriteLine(ContinueNumber);
            }

        }
    }
}

namespace Lesson14
// Do While Loop
{
    class Program
    {
        static void Lesson14Main()
        {
            int? UserDecisionToContinue = null;
            do
            {
                Console.WriteLine("Enter your age.");

                string UserAgeInput = Console.ReadLine();
                int UserAge = int.Parse(UserAgeInput);

                while (UserAge < 18)
                {
                    UserAge++;
                    Console.WriteLine("1 year of wait and now, you are {0} years old.", UserAge);
                }
                Console.WriteLine("You are eligible to drive.");

                do
                {
                    Console.WriteLine("Do you want to play this game again? 0:No and 1:Yes");

                    string UserDecisionToContinueInput = Console.ReadLine();
                    UserDecisionToContinue = int.Parse(UserDecisionToContinueInput);
                }
                while (UserDecisionToContinue != 0 && UserDecisionToContinue != 1);
            } while (UserDecisionToContinue == 1);


        }
    }
}

namespace Lesson13
// While Loop
{
    class Program
    {
        static void Lesson13Main()
        {
            Console.WriteLine("Enter your age.");

            string UserAgeInput = Console.ReadLine();
            int UserAge = int.Parse(UserAgeInput);

            while (UserAge < 18)
            {
                UserAge++;
                Console.WriteLine("1 year of wait and now, you are {0} years old.", UserAge);
            }
            Console.WriteLine("You are eligible to drive.");
        }
    }
}

namespace Lesson11And12
// Switch
{
    class Program
    {
        static void Lesson11And12Main()
        {
        ChoicesGiven:
            Console.WriteLine("Please enter your choice. 0:Ferguson, 1:McGregor, 2:Nurmagomedov, 3:Silva, 4:Romero, 5:Andesya and 6:None");

            string UserChoiceInput = Console.ReadLine();
            int UserChoice = int.Parse(UserChoiceInput);

            switch (UserChoice)
            {
                case 0:
                    Console.WriteLine("Indeed, he is the GOAT of this sport.");
                    break;
                case 1:
                case 2:
                    Console.WriteLine("Indeed, he is great but he himself admitted prime ferguson is better.");
                    break;
                case 3:
                case 4:
                case 5:
                    Console.WriteLine("You are a kid who doesn't know jack shit about anything.");
                    break;
                case 6:
                    Console.WriteLine("Better start watching UFC.");
                    break;

                default: Console.WriteLine("Your choice ({0}) is invalid.", UserChoice); goto ChoicesGiven;
            }

        DecisionGiven:
            Console.WriteLine("Do you want to know my opinion on other fighters? 0:No and 1:Yes");

            string UserDecisionToContinueInput = Console.ReadLine();
            int UserDecisionToContinue = int.Parse(UserDecisionToContinueInput);

            switch (UserDecisionToContinue)
            {
                case 0:
                    break;
                case 1:
                    goto ChoicesGiven;
                default:
                    Console.WriteLine("Invalid choice {0}.", UserDecisionToContinue); goto DecisionGiven;
            }

        }
    }
}

namespace Lesson10
// If
{
    class Program
    {
        static void Lesson10Main()
        {
            Console.WriteLine("Please enter your age.");

            string AgeInput = Console.ReadLine();
            int Age = int.Parse(AgeInput);

            if (Age < 13 || Age > 65)
            {
                Console.WriteLine("Do not even think about driving.");
            }
            else if (Age > 12 && Age < 18)
            {
                Console.WriteLine("Learn to drive but not on the roads.");
            }
            else
            {
                Console.WriteLine("You are eligible to drive!");
            }


        }
    }
}

namespace Lesson9
// Comments
{

    class Program
    {
        static void Lesson9Main()
        {

            Console.WriteLine("Comments");

            // Single Line Comment

            /* 
                Multi Line Comment
            */

            Comment? Message = null;
        }
    }

    /// <summary>
    /// XML Document Comment Used To Document Class Descriptions Or Implementation Descriptions Which Come When You Hover Over Upon Call
    /// </summary>
    class Comment
    {

    }

}

namespace Lesson8
// Arrays
{

    class Program
    {
        static void Lesson8Main()
        {
            int[] FavouriteNumbers = new int[1];
            FavouriteNumbers[0] = 7;

            Console.WriteLine(FavouriteNumbers[0]);
        }
    }
}

namespace Lesson7
// Nullable Types
{

    class Program
    {
        static void Lesson7Main()
        {
            // Implicit Conversions Are When No Loss Of Data Happens Or When Exceptions Cannot Be Thrown When Converting
            int MyAge = 15;
            float MyPreciseAge = MyAge;

            // Explicit Conversions Are When Loss Of Data Happens Or When Exceptions Can Be Thrown When Converting
            float HisPreciseAge = 11.7f;
            int HisAge = (int)HisPreciseAge;
            HisAge = Convert.ToInt32(HisPreciseAge); // Convert Class Throws An Exception When The Value Overflows Due To Having A Larger Size

            string HerAgeString = "17";
            int HerAge = int.Parse(HerAgeString);

            string ItsAgeString = "32B";
            int ItsAge = 0;
            bool IsItsAgeSuccessfullyConverted = int.TryParse(ItsAgeString, out ItsAge);

            if (IsItsAgeSuccessfullyConverted)
            {
                Console.WriteLine("Successfull Conversion!");
            }
            else
            {
                Console.WriteLine("Unsuccessfull Conversion!");
            }

        }
    }
}

namespace Lesson6
// Nullable Types
{

    class Program
    {
        static void Lesson6Main()
        {
            // Reference Types Such As Strings Can Be Null
            string Username = "thetahayaseen";
            string Nickname = null;
            // Value Types Such As Integers Cannot Be Null
            int Pin = 221208;
            int? FavouriteNumber = null;

            Console.WriteLine("Username: {0}; Pin: {1};", Username, Pin);
            Console.WriteLine("Nickname: {0}; Favourite Number: {1};", (Nickname != null ? Nickname : "Not Given"), (FavouriteNumber != null ? FavouriteNumber : "Not Given"));

            int? SeatsAvailaible = null;
            int SeatsForSale;

            if (SeatsAvailaible == null)
            {
                SeatsForSale = 0;
            }
            else
            {
                SeatsForSale = SeatsAvailaible.Value;
                SeatsForSale = (int)SeatsAvailaible;
            }

            int? TicketsForSale = null;
            int TicketsAvailaible = TicketsForSale ?? 0;

        }
    }
}

namespace Lesson5
// Common Operators
{

    class Program
    {
        static void Lesson5Main()
        {
            int AssignmentMarks = 0;

            AssignmentMarks = AssignmentMarks + 100;

            AssignmentMarks = AssignmentMarks - 20;

            AssignmentMarks = AssignmentMarks * 9;

            AssignmentMarks = AssignmentMarks / 11;

            Console.WriteLine("Assignment Marks Retrieved: {0}", AssignmentMarks);

            int AssignmentMarksRemainderByTwo = AssignmentMarks % 2;

            bool AreMarksOfAnOddCount = AssignmentMarks != 0;
            Console.WriteLine("Are Assignment Marks Odd: {0}", AreMarksOfAnOddCount);

            bool AreMarksOfAnEvenCount = AssignmentMarks == 0;
            Console.WriteLine("Are Assignment Marks Even: {0}", AreMarksOfAnEvenCount);

            bool AreMarksGreaterThan50 = AssignmentMarks > 50;
            Console.WriteLine("Are Assignment Marks Greater Than 50: {0}", AreMarksGreaterThan50);

            bool AreMarksLessThan75 = AssignmentMarks < 75;
            Console.WriteLine("Are Assignment Marks Less Than 75: {0}", AreMarksLessThan75);

            if (AreMarksOfAnOddCount && AreMarksGreaterThan50)
            {
                Console.WriteLine("I Somehow Like That You Have An Odd Number As Your Marks!");
            }

            if (AreMarksOfAnEvenCount || AreMarksLessThan75)
            {
                Console.WriteLine("Well! You Are Pathetic With An Even Number Or Having Less Than 75 Marks!");
            }

            string IsNumberEvenOrOdd = AreMarksOfAnOddCount ? "Odd" : "Even";
            Console.WriteLine("Number Is {0}", IsNumberEvenOrOdd);
        }
    }
}

namespace Lesson4
// String Type
{

    class Program
    {
        static void Lesson4Main()
        {
            string Introduction = "\"Taha Yaseen\"\nC# And .NET Dev";
            string ProjectsRepo = @"Projects In Directory: thetahayaseen\thenewengineeringera\.NET\";
            Console.WriteLine(Introduction);
            Console.WriteLine(ProjectsRepo);

        }
    }
}

namespace Lesson3
// Built In Types
{

    class Program
    {
        static void Lesson3Main()
        {
            bool PractisingCSharpForGameDev = false;
            bool PractisingCSharpForDotNet = true;

            Console.WriteLine("Hey! What's Your Age By The Way?");
            string AgeInput = Console.ReadLine();
            byte Age = byte.Parse(AgeInput);

            Console.WriteLine("If You Are A CS Major, You Should Know To Use Byte For Storing An Age In C#!");
            Console.WriteLine("Byte's Max Value: {0}; Byte's Min Value: {1};", byte.MaxValue, byte.MinValue);

            double ValueOfPi = 3.14159265359;
            float ShorterValueOfPi = 3.142f;
        }
    }

}

namespace Lesson2
// Reading And Writing To A Console
{

    class Program
    {
        static void Lesson2Main()
        {
            Console.WriteLine("Please Enter Your Name?");
            string Name = Console.ReadLine();
            Console.WriteLine("Hi! " + Name + ", How Are You?");
            string Reply = Console.ReadLine();
            Console.WriteLine("You Said {0}? Thats Nice!", Reply);
        }
    }

}

namespace Lesson1
// Introduction
{
    namespace Heroes
    {
        public static class Ironman
        {
            public static void MakeAWittyJoke()
            {
                Console.WriteLine("Don't Take From My Pile, Steve!");
            }
        }
    }

    class Program
    {

        static void AddALaugh()
        {
            Console.WriteLine("Hahahahaha! Man You Cannot Stay Serious!");
        }

        static void Lesson1Main()
        {
            Console.WriteLine("Pursuing .NET As Suggested By Sir Murtuza!");
            Heroes.Ironman.MakeAWittyJoke();
            AddALaugh();
        }
    }
}