using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static)
                .Select(item => item.GetValue(null))
                .Cast<T>();
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var type = Parse<T, int>(value, "value", item => item.Id == value);
            return type;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var type =  Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return type;
        }

        private static T Parse<T,K> (K value, string description, Func<T, bool> predicate) where T : Enumeration where K : notnull
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            if (matchingItem == null)
            {
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");
            }
            return matchingItem;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
