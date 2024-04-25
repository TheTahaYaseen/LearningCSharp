using System;

namespace Lesson61
{
    // private partial class PartialEmployee
    // must contain same access modifiers

    // public abstract partial class PartialEmployee
    // even if one part abstract, all parts will be considered abstract

    // public sealed partial class PartialEmployee
    // even if one part sealed, all parts will be considered sealed

    public class PartTimeEmployee
    {
        public int _PartTimeHours;
        
        public PartTimeEmployee(int PartTimeHours) { 
            this._PartTimeHours = PartTimeHours;
        }
    }

    public interface IProgrammer
    {
        void AnnounceStack();
    }

    public interface ILazyMan
    {
        void SlackOff();
    }

    // public partial class PartialEmployee : PartTimeEmployee
    // implementations must not have different base classes, only one will be considered across all
    
    public partial class PartialEmployee : IProgrammer, ILazyMan 
    // must contain partial word in every implementation
    {
        public string GetEmployeeId()
        {
            Greet();
            SayHi();
            return String.Format("Emp{0}{1} Of {2} years old.", this._Id, this._Name, base._Age);
        }

        // implementation optional as signature will be removed if not implemented
        partial void SayHi();

        // cannot have access modifiers nor keywords like abstract, override, new, sealed and likewise
        // partial void SayHi();

        // cannot have implementation and declaration at the same time
        // partial void SayHello() {
        //     Console.WriteLine("Hello.");
        // }

        // must have return type of void
        // partial string ReturnGreet();

        partial void SayHello();

        // signatures shall match between declaration and implementation

        // partial void SayHello(string Hello)
        // {
        //     Console.WriteLine(Hello);
        // }

        partial void SayHello()
        {
            Console.WriteLine("Hello.");
        }

        // must only be implemented once
        
        // partial void SayHello()
        // {
        //     Console.WriteLine("Hello!");
        // }

        // only partial classes / structs can contain parial methods
        partial void Greet()
        {
            Console.WriteLine("Greetings.");
        }

    }
}
