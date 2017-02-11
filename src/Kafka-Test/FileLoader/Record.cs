using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FileLoader
{
    internal class Record : IEquatable<Record>
    {
        public Record(IEnumerable<string> line)
        {
            Values = line.ToList();
        }

        public int? Identifier
        {
            get
            {
                if (Values.Any() && int.TryParse(Values.First(), out var result))
                {
                    return result;
                }
                return null;
            }
        }

        public IEnumerable<string> Values { get; }

        public bool Equals(Record other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Values.SequenceEqual(other.Values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Record)obj);
        }

        public override int GetHashCode()
        {
            return Values.GetHashCode();
        }

        public override string ToString()
        {
            return Values.Aggregate((v1, v2) => $"{v1}, {v2}");
        }
    }

    internal class RecordTests
    {
        [Test]
        public void TestIdentifierWithValue()
        {
            var input = new[] { "123", "test", "1" };
            var record = new Record(input);

            var id = record.Identifier;

            Assert.That(id.HasValue);
            Assert.That(id.Value, Is.EqualTo(123));
        }

        [Test]
        public void TestIdentifierWithNoList()
        {
            var input = new string[0];
            var record = new Record(input);

            var id = record.Identifier;

            Assert.That(!id.HasValue);
        }

        [Test]
        public void TestIdentifierWithBadList()
        {
            var input = new[] { "NotInt" };
            var record = new Record(input);

            var id = record.Identifier;

            Assert.That(!id.HasValue);
        }

        [Test]
        public void TestValues()
        {
            var input = new[] { "123", "test", "1" };
            var record = new Record(input);

            Assert.That(record.Values, Is.EqualTo(input));
        }
    }
}