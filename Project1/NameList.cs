using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    class NameList
    {
        #region Properties
        private List<Name> Names;
        public int Count { get { return Names.Count; } }
        public NameFormat Format { get; set; } = NameFormat.ORIGINAL;
        #endregion
        #region Constructor
        public NameList()
        {
            Names = new List<Name>();
        }
        public NameList(String[] StringNameArray)
        {
            Names = new List<Name>();
            int l = StringNameArray.Length;
            for(int i = 0; i < l; i++)
            {
                Names.Add(new Name(StringNameArray[i]));
            }
        }
        #endregion
        #region Methods        
        /// <summary>
        /// Adds a Name to the NameList
        /// </summary>
        /// <param name="NewName">The Name object to be added</param>
        public void Add(Name NewName)
        {
            Names.Add(NewName);
        }        
        /// <summary>
        /// Removes the A name Equal to string passed in from the NameList
        /// </summary>
        /// <param name="NameString">The name string.</param>
        /// <returns>Returns boolean true if item removed and boolean false if not removed</returns>
        public bool Remove(String NameString)
        {
            Name NameToRemove = new Name(NameString);
            return Names.Remove(NameToRemove);
        }        
        /// <summary>
        /// Finds all names that are similar to a partial name
        /// </summary>
        /// <param name="partialName">A partial part of a name</param>
        /// <param name="format">The format choice to display name</param>
        /// <returns>List of Names with partial matches in the format of choice or first if no
        /// format is chosen</returns>
        public NameList FindNames(String partialName, NameFormat format = NameFormat.FIRST)
        {
            Name Partial = new Name(partialName);
            NameList foundNames = new NameList();
            foreach (Name name in Names)
        	{
                if(Partial.Prefix == name.Prefix||Partial.First ==name.First||Partial.Middle==name.Middle||Partial.Last==name.Last||Partial.Suffix==name.Suffix )
                    foundNames.Add(name);
	        }
            return foundNames;
        }

        public List<String> SortFirst()
        {
            Names.Sort(new Name());
            List<String> StringNames = new List<String>();
            foreach (Name n in Names)
            {
                StringNames.Add(n.FirstNameFirst());
            }
            return StringNames;
        }
        public List<String> SortLast()
        {
            Names.Sort();
            List<String> StringNames = new List<String>();
            foreach (Name n in Names)
            {
                StringNames.Add(n.LastNameFirst());
            }
            return StringNames;
        }
        public void Display()
        {
            foreach (Name n in Names)
	        {
                Console.WriteLine(n.NameToString(Format));
	        }
        }
        #region Indexers
        public Name this[int index]
        {
            get
            {
                if (index < 0 || index > Count)
                    throw new IndexOutOfRangeException($"Subscript {index} is not between 0 and {Count} and therefor invalid");
                return Names[index];
            }
            set
            {
                if (index < 0 || index > Count)
                    throw new IndexOutOfRangeException($"Subscript {index} is not between 0 and {Count} and therefor invalid");
                Names[index] = value;
            }
        }
        public Name this[String strName]
        {
            get 
            {
                Name name = new Name(strName);
                if (Names.Contains(name))
                    return Names[Names.IndexOf(name)];
                else
                    return null;
            }
        }
        #endregion
        #region Operators
        public static NameList operator+(NameList CurrNameList,Name NewName)
        {
            CurrNameList.Names.Add(NewName);
            return CurrNameList;
        }
        public static NameList operator-(NameList CurrNameList,Name NameToRemove)
        {
            if (CurrNameList.Names.Contains(NameToRemove))
                CurrNameList.Names.Remove(NameToRemove);
            return CurrNameList;
        }
        #endregion
        #endregion
    }
}
