namespace PushPopTestTask
{
    public class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public int CompareTo(Person other)
        {
            if (other == null) return 1;
            return Age.CompareTo(other.Age);
        }
        public override string ToString()
        {
            return $"{Name} ({Age})";
        }
    }
}
