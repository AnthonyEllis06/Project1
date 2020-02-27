using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataStructures
{
    class Name : IEquatable<Name>, IComparable<Name>,IComparer<Name>
    {
        #region Properites

        public String Prefix { get; set; }
        public String First { get; set; }
        public String Middle { get; set; }
        public String Last { get; set; }
        public String Suffix { get; set; }
        public String Original { get; set; }
        //private Regex pattern;
        #endregion
        #region Constructors        
        /// <summary>
        /// Default null Constructor <see cref="Name"/> class.
        /// </summary>
        public Name()
        {
            Prefix = null;
            First = null;
            Middle = null;
            Last = null;
            Suffix = null;
        }        
        /// <summary>
        /// Copy constructor<see cref="Name"/> class.
        /// </summary>
        /// <param name="NewName">A name object copy</param>
        public Name(Name NewName)
        {
            Prefix=NewName.Prefix;
            First = NewName.First;
            Middle = NewName.Middle;
            Last = NewName.Last;
            Suffix = NewName.Suffix;
            Original = NewName.Original;
        }        
        /// <summary>
        /// Constructor from String object <see cref="Name"/> class.
        /// </summary>
        /// <param name="NameString">String name must be in American Format</param>
        /// 
        public Name(String NameString)
        {
            
            String[] nameParts = Tools.Tokenize(NameString," ");
            Original = NameString;
            Match m;
            String Pattern = @"\b(([Dr].)|([Mr?s?]{2,3}))\.";
            m = new Regex(Pattern).Match(NameString);
            Prefix = m.Value;
            Prefix.Trim();
            NameString = NameString.Remove(m.Index, m.Value.Length);

            Pattern = @"\b(([iIvV]+(?!\.))|([JR]r\.)|([PMJ].[dD]?))\Z";
            m = new Regex(Pattern).Match(NameString);
            Suffix = m.Value;
            Suffix.Trim();
            NameString = NameString.Remove(m.Index, m.Value.Length);

            Pattern = @"(\w+\S?\w+?$)|(\w+\,)";
            m = new Regex(Pattern).Match(NameString);
            Last = m.Value;
            Last.Trim();
            NameString = NameString.Remove(m.Index, m.Value.Length);

            Pattern = @"\b(([\w]+\.?.(?!\,))|((?<=\,\s)\w+))";
            m = new Regex(Pattern).Match(NameString);
            First = m.Value;
            First.Trim();
            NameString = NameString.Remove(m.Index, m.Value.Length);
            Middle = NameString;

        }        
        /// <summary>
        /// Converts the Name to a String based on the format variable passed
        /// </summary>
        /// <param name="format">A NameFormat Choice</param>
        /// <returns>The Full String Representation of the Name based on the Format Choice</returns>
        public string NameToString(NameFormat format)
        {
            String nameToReturn = null;
            String s = " ";
            switch (format)
            {
                case NameFormat.FIRST:
                    nameToReturn = Prefix.Trim()+s+First.Trim()+s+Middle.Trim()+s+Last.Trim()+s+Suffix.Trim();
                    break;
                case NameFormat.LAST:
                    nameToReturn = Prefix.Trim()+s+Last.Trim()+","+s+First.Trim()+s+Middle.Trim()+s+Suffix.Trim();
                    break;
                case NameFormat.ORIGINAL:
                    nameToReturn = Original;
                    break;
            }

            return nameToReturn.Trim();
        }
        public String FirstNameFirst()
        {
            return NameToString(NameFormat.FIRST);
        }
        public String LastNameFirst()
        {
            return NameToString(NameFormat.LAST);
        }
        #region IComparable<Name> implementation
        public int Compare(Name x,Name y)
        {
            if(!x.First.Equals(y.First))
                return x.First.CompareTo(y.First);
            return x.Last.CompareTo(y.Last);
        }

        /// <summary>
        /// Ordering comparer for two Name objects by Last Name
        /// </summary>
        /// <param name="name"> the name to compare this objects name to</param>
        /// <returns>int 0 if equal; >0 if this name is greater;<0 otherwise</returns>
        public int CompareTo(Name name)
        {
            //if last name not equal, base CompareTo on last name only;
            //  else base decision on first name
            if (!Last.Equals(name.Last))
                return Last.CompareTo(name.Last);
            return (First.CompareTo(name.First));
        }

        /// <summary>
        /// Equality comparer for two Name objects
        /// </summary>
        /// <param name="other">  true if first and last names of this name are equal to Other.First and Other.Last, respectively</param>
        /// <returns>Boolean True if Equal and false if not equal</returns>
        public bool Equals(Name other)
        {
            return (First.Equals(other.First) && Last.Equals(other.Last));
        }

        /// <summary>  Override of Object.Equals</summary>
        /// <param name="obj">  object to which this object is compared to.</param>
        /// <returns>Results from the Class Equals method</returns>
        /// <exception cref="ArgumentException">Cannot compare a Name and a obj object type.</exception>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return base.Equals(obj);
            if(!(obj is Name))
                throw new ArgumentException($"Cannot compare a Name and a {obj.GetType()} object.");
            return Equals(obj as Name);
        }
        #endregion
        #endregion
    }
}
