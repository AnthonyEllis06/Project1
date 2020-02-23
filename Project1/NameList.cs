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
        #endregion
        #region Constructor
        public NameList()
        {
            Names = new List<Name>();
        }
        public NameList(String[] StringNameArray)
        {
            int l = StringNameArray.Length;
            for(int i = 0; i < l; i++)
            {
                if(StringNameArray[i].Length>1)
                    Names.Add(new Name(StringNameArray[i]));
            }
        }
        #endregion
        #region Methods
        public void Add(Name NewName)
        {
            Names.Add(NewName);
        }
        public bool Remove(String NameString)
        {
            Name NameToRemove = new Name(NameString);
            return Names.Remove(NameToRemove);
        }

        public List<Name> FindNames(String partialName, NameFormat format = NameFormat.FIRST)
        {
            List<Name> foundNames = new List<Name>();

            return foundNames;
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
