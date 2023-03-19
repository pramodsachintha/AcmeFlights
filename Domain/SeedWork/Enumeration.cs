using System;

namespace Domain.SeedWork
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Id;
        }
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
