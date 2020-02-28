//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Program 1 - NameList Class
//	File Name:		Name.cs
//	Description:	Defines the name class used to structure and create a name
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Anthony Ellis, ellisah@etsu.edu, East Tennessee State University
//	Created:		Tuesday, Febuary 018, 2020
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataStructures
{

    /// <summary>
    /// The Name Class is a class that can seperate a name into its various parts
    /// and can be compared by last name or first name.
    /// </summary>
    /// <seealso cref="System.IEquatable{DataStructures.Name}" />
    /// <seealso cref="System.IComparable{DataStructures.Name}" />
    /// <seealso cref="System.Collections.Generic.IComparer{DataStructures.Name}" />
    class Name : IEquatable<Name>, IComparable<Name>,IComparer<Name>
    {
        #region Properites

        /// <summary>  property for the Name prefix.</summary>
        /// <value>  String representation of the prefix</value>
        public String Prefix { get; set; }

        /// <summary>  property for the names first name</summary>
        /// <value>String representation of the first name</value>
        public String First { get; set; }

        /// <summary>  property for the names middle name</summary>
        /// <value>  string representation for the middle name</value>
        public String Middle { get; set; }

        /// <summary>property for the names last name</summary>
        /// <value>string representation for the names last name</value>
        public String Last { get; set; }

        /// <summary>  property for the names suffix</summary>
        /// <value>  string representation of the suffix</value>
        public String Suffix { get; set; }

        /// <summary>  property for the names originial passed in value in the string constructor</summary>
        /// <value>  the string representation of the original name passed in</value>
        public String Original { get; set; }
        #endregion
        #region Constructors        
        /// <summary>
        /// Default constructor sets all props to null
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
        /// Copy constructor to copy one name to another.
        /// </summary>
        /// <param name="NewName">A name object to Copy</param>
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
        /// Name Constructor from String.
        /// </summary>
        /// <param name="NameParts">String name must be in American Format</param>
        public Name(String NameParts)
        {

            if (NameParts.Length <= 1)
                return;
            List<String> nameParts = new List<String>(Tools.Tokenize(NameParts," "));
            Original = NameParts;
            Match m;
            #region Prefix
            String Pattern = @"\b(([Dr].)|([Mr?s?]{2,3}))\.";
            m = new Regex(Pattern).Match(nameParts.First<String>());
            if (m.Success)
            {
                Prefix = nameParts.First<String>();
                nameParts.Remove(nameParts[0]);
            }
            if (nameParts.Count == 0)
                return;
            #endregion
            #region Suffix
            Pattern = @"\b(([iIvV]+(?!\.))|([JR]r\.)|([PMJ].[dD]?))\Z";
            m = new Regex(Pattern).Match(nameParts.Last<String>());
            if (m.Success)
            {
                Suffix = nameParts.Last<String>();
                nameParts.Remove(nameParts.Last<String>());
            }
            if (Suffix != null)
                Suffix = ", " + Suffix;
            if (nameParts.Count == 0)
                return;
            #endregion
            #region Last Name
            Pattern = @"\w+\,";
            Regex lastRegex = new Regex(Pattern);
            Last = null;
            foreach(String namePart in nameParts)
            {
                m = lastRegex.Match(namePart);
                if (m.Success && Last == null)
                    Last = namePart;
            }
            if (Last == null)
                Last = nameParts.Last<String>();
            nameParts.Remove(Last);
            if (Last.Contains(",")&& Last!=null)
                Last = Last.Substring(0, Last.Length - 1);
            if (nameParts.Count == 0)
                return;
            #endregion
            #region First Name
            Pattern = @"\b(([\w]+\.?.(?!\,))|((?<=\,\s)\w+))";
            Regex firstRegex = new Regex(Pattern);
            First = null;
            foreach (String namePart in nameParts)
            {
                m = firstRegex.Match(namePart);
                if (m.Success && First == null)
                    First = namePart;
            }
            nameParts.Remove(First);
            if (First.Contains(",")&&First!=null)
                Last = First.Substring(0, Last.Length - 1);
            #endregion
            #region Middle Name
            Middle = null;
            nameParts.ForEach(part => Middle = Middle + part.Trim());
            if(Middle!= null)
                Middle = Middle+ " ";
            #endregion

        }
        
        #endregion
        #region Methods
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
                    nameToReturn = Prefix + s + First + s + Middle + Last + Suffix;
                    break;
                case NameFormat.LAST:
                        nameToReturn = Prefix + s + Last +","+ s + First + s + Middle + Suffix;
                    break;
                case NameFormat.ORIGINAL:
                    nameToReturn = Original;
                    break;
            }

            return nameToReturn.Trim();
        }
        /// <summary> 
        /// Used to call NameToString in the First Name First Format
        /// </summary>
        /// <returns>Returns a String Representation of the name in the First name first format</returns>
        public String FirstNameFirst()
        {
            return NameToString(NameFormat.FIRST);
        }

        /// <summary> 
        /// Used to call NameToString in the Last Name First Format
        /// </summary>
        /// <returns>>Returns a String Representation of the name in the Last name first format</returns>
        public String LastNameFirst()
        {
            return NameToString(NameFormat.LAST);
        }
        #region IComparable<Name> implementation
        /// TODO fix compare to how it is described in the project file
        /// <summary>  Compares two names by first name, if first names are equal then Compares by Last name</summary>
        /// <param name="One">  The first name to Compare</param>
        /// <param name="Two">  The second name to Compare</param>
        /// <returns>returns int value representing how the names compare. value&lt;0 means one is comes before two, value = 0 names are the same, value &gt;0 name one comes after name two</returns>
        public int Compare(Name One,Name Two)
        {
            if(!One.First.Equals(Two.First))
                return One.First.CompareTo(Two.First);
            return One.Last.CompareTo(Two.Last);
        }

        /// <summary>
        /// Ordering comparer for two Name objects by Last Name
        /// </summary>
        /// <param name="name"> the name to compare this objects name to.</param>
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
