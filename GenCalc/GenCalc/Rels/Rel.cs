using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public abstract class Rel : IEquatable<Rel>
    {
        public abstract RelValue[] Values { get; }

        public abstract Rel[] GetNext(RelValue value);

        public abstract string ToString(Gender? gender);

        public Rel GetDefault(RelValue value) => GetNext(value)[0];

        public bool Contains(Rel given)
        {
            if (given == RelValue.Self) return true;
            List<List<RelValue>> relValues = new();
            foreach (RelValue value in Values)
            {
                if (value == given.Values[0]) relValues.Add(new() { value });
                else if (relValues.Count > 0) relValues[^1].Add(value);
            }
            foreach (List<RelValue> relValueSet in relValues)
            {
                if (relValueSet.Count >= given.Values.Length)
                {
                    bool isValid = true;
                    for (int a = 0; a < given.Values.Length; a++)
                        if (given.Values[a] != relValueSet[a]) isValid = false;
                    if (isValid) return true;
                }
            }
            return false;
        }

        public bool Equals(Rel? other)
        {
            if (other is null) return false;
            if (Values.Length != other.Values.Length) return false;
            for (int a = 0; a < Values.Length; a++) if (Values[a] != other.Values[a]) return false;
            return true;
        }

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => ToString(null);

        public static implicit operator Rel(int value) => (Basic)value;

        public static implicit operator Rel(Rel[] rels)
        {
            Rel result = 0;
            foreach (Rel rel in rels) result += rel;
            return result;
        }

        public static implicit operator Rel(RelValue value) => (Basic)value;

        public static implicit operator Rel(RelValue[] values)
        {
            Rel result = 0;
            foreach (RelValue value in values) result += value;
            return result;
        }

        public static implicit operator Rel(KinshipCount record) => (Kinship)record;

        public static Rel operator +(Rel given) => given;

        public static Rel operator !(Rel given)
        {
            List<RelValue> result = new();
            foreach (var value in given.Values) result.Add(value switch
            {
                RelValue.Parent => RelValue.Child,
                RelValue.Child => RelValue.Parent,
                _ => value
            });
            result.Reverse();
            return result.ToArray();
        }

        public static Rel operator +(Rel left, RelValue right) => left.GetDefault(right);

        public static Rel operator +(Rel left, Rel right)
        {
            Rel result = left;
            foreach (RelValue value in right.Values) result += value;
            return result;
        }

        public static bool operator ==(Rel left, Rel right) => left.Equals(right);

        public static bool operator !=(Rel left, Rel right) => !(left == right);

        // Factory Members

        public static readonly Lineal.Progeny GreatGrandchild = -3;
        public static readonly Lineal.Progeny Grandchild = -2;
        public static readonly Lineal.Progeny Child = -1;
        public static readonly Lineal.Self Self = Lineal.Self.Get;
        public static readonly Nibling GreatGrandnibling = -3;
        public static readonly Nibling Grandnibling = -2;
        public static readonly Nibling Nibling = -1;
        public static readonly Sibling Sibling = Sibling.Full.Get;
        public static readonly Lineal.Progenitor Parent = 1;
        public static readonly Pibling Pibling = 1;
        public static readonly Pibling Grandpibling = 2;
        public static readonly Pibling GreatGrandpibling = 3;
        public static readonly Cousin Cousin = (Cousin)1;
        public static readonly Cousin.Half HalfCousin = (Cousin.Half)1;
        public static readonly Lineal.Progenitor Grandparent = 2;
        public static readonly Lineal.Progenitor GreatGrandparent = 3;

        public static readonly Partner UnknownPartner = Partner.Get(null);
        public static readonly Partner UnmarriedPartner = (Partner)false;
        public static readonly Partner.Spouse Spouse = Partner.Spouse.Get;
        public static readonly InLaw.FromSpouse ParentInLaw = new(Parent);
        public static readonly InLaw.FromSpouse SiblingInLaw = new(Sibling);
        public static readonly InLaw.FromSpouse HalfSiblingInLaw = new(RelValue.HalfSibling);
        public static readonly InLaw.Child ChildInLaw = InLaw.Child.Get;
        public static readonly InLaw.SpouseOfSibling SpouseOfSibling = (InLaw.SpouseOfSibling)false;

        public static Lineal.Progeny NthGreatGrandchild(int genCount) => genCount == 0 ? Grandchild : -Math.Abs(genCount) - 2;

        public static Nibling NthGreatGrandnibling(int genCount, bool hasHalfSibling = false) => genCount == 0 ? Grandnibling : (-Math.Abs(genCount) - 2, hasHalfSibling);

        public static Pibling NthGreatGrandpibling(int genCount, bool hasHalfSibling = false) => genCount == 0 ? Grandpibling : (Math.Abs(genCount) + 2, hasHalfSibling);

        public static Lineal.Progenitor NthGreatGrandparent(int genCount) => genCount == 0 ? Grandparent : Math.Abs(genCount) + 2;

        public abstract class Basic : Rel
        {
            public static Basic Get(RelValue value) => value switch
            {
                RelValue.Self => (Lineal)0,
                RelValue.Parent => (Lineal)1,
                RelValue.Child => (Lineal)(-1),
                RelValue.FullSibling => Sibling.Full.Get,
                RelValue.HalfSibling => Sibling.Half.Get,
                RelValue.UnmarriedPartner => (Partner)false,
                RelValue.Spouse => (Partner)true,
                _ => Partner.Get(null)
            };

            public static Basic Get(int given) => (Kinship)given;

            public static implicit operator Basic(RelValue value) => Get(value);

            public static implicit operator Basic(int given) => Get(given);
        }

        public class SimpleSet : Rel
        {
            private readonly List<Basic> simpleRels = new();

            public SimpleSet(params Rel[] rels)
            {
                foreach (Rel rel in rels)
                {
                    if (rel is SimpleSet srs) simpleRels.AddRange(srs.SimpleRels);
                    else if (rel != RelValue.Self) simpleRels.Add((Basic)rel);
                }
                simpleRels.RemoveAll(simpleRel => simpleRel == (Basic)0);
            }

            public Basic[] SimpleRels => simpleRels.ToArray();

            public override RelValue[] Values
            {
                get
                {
                    List<RelValue> result = new();
                    foreach (Rel rel in SimpleRels)
                        result.AddRange(rel.Values);
                    return result.ToArray();
                }
            }

            public override Rel[] GetNext(RelValue value)
            {
                List<Rel> result = new();
                List<Basic> allSimpleRels = new(SimpleRels);
                Basic last = allSimpleRels[^1];
                allSimpleRels.RemoveAt(allSimpleRels.Count - 1);
                foreach (Rel rel in last.GetNext(value))
                    result.Add(new SimpleSet(new SimpleSet(allSimpleRels.ToArray()), rel));
                return result.ToArray();
            }

            public override string ToString(Gender? gender)
            {
                string result = SimpleRels[^1].ToString(gender);
                for (int a = SimpleRels.Length - 2; a >= 0; a--) result += " of " + SimpleRels[a];
                return result;
            }
        }
    }
}