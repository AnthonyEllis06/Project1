using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataStructures
{
    class Name : IEquatable<Name>, IComparable<Name>
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
        public Name()
        {

        }
        public Name(Name NewName)
        {

        }
        public Name(String NameString)
        {
            
            String[] nameParts = Tools.Tokenize(NameString," ");
            Original = NameString;
            Match m;
            String Pattern = @"((\A[Dr\.]+|(M?r?s?)\.))";
            m = new Regex(Pattern).Match(NameString);
            Prefix = m.Value;
            NameString = NameString.Remove(m.Index, m.Value.Length);

            Pattern = @"(\b[iIvV]+\z(?!\.))|([JS]r\.)";
            m = new Regex(Pattern).Match(NameString);
            Suffix = m.Value;
            NameString = NameString.Remove(m.Index, m.Value.Length);

            Pattern = @"(\w+\S?\w+?$)|(\w+\,)";
            m = new Regex(Pattern).Match(NameString);
            Last = m.Value;
            NameString = NameString.Remove(m.Index, m.Value.Length);

            Console.WriteLine(Original);
            Console.WriteLine(Prefix+Last+Suffix);
            //Regex pattern = new Regex(@"(?<prefix>((\b\A[Dr\.]+|(M?r?s?)\.\b)))|(?<suffix>(\b[iIvV]+\z(?!\.))|([JS]r\.))|(?<first>((\b\A[\w]+\.?.(?!\,)\b)|((?<=\,\s)\w+\b\.?)))|(?<last>((\w+\S\w+$)|(\w+\,)))|(?<middle>\b\w+\.?\,?)");
            // MatchCollection matchCollection = pattern.Matches(NameString);
            //foreach (Match m in matchCollection)
            //{
            //if (m.Groups["first"] != null && m.Success)
            //First = m.Groups["first"].ToString();
            //else if (m.Groups["last"] != null && m.Success)
            //Last = m.Groups["last"].Value;
            //}

            //Console.WriteLine(Original);
            //Console.WriteLine("first: "+First+"Last "+Last+matchCollection.Count);
            
        }

        #region IComparable<Name> implementation
        /// <summary>
        /// Ordering comparer for two Name objects
        /// </summary>
        /// <param name="name">  "name"&gt;the name to come this objects name to</param>
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
        /// <returns></returns>
        public bool Equals(Name other)
        {
            return (First.Equals(other.First) && Last.Equals(other.Last));
        }

        /// <summary>  Overide of Object.Equals</summary>
        /// <param name="obj">  object to which this object is compared to.</param>
        /// <returns></returns>
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
