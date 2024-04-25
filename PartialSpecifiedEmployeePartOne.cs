using System;

namespace Lesson61
{
    // internal partial class PartialEmployee
    // must contain same access modifiers

    public class Person {
        public int _Age;
        
        public Person(int Age)
        {
            this._Age = Age;   
        }
    }

    public partial class PartialEmployee : Person
    // must contain partial word in every implementation
    {
        public int _Id;
        public string _Name;

        public PartialEmployee(int Id, string Name, int Age) :
            base(Age)
        {        
            this._Id = Id;
            this._Name = Name;
        }

        public void AnnounceStack()
        {
            Console.WriteLine("I am a wizard.");
        }

        public void SlackOff()
        {
            Console.WriteLine("Yep i am a ui dev, re-adjusting a button.");
        }

        // only partial classes / structs can contain parial methods
        partial void Greet(); 
        
    }
}
