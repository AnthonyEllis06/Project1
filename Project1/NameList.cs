//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Program 1 - NameList Class
//	File Name:		NameList.cs
//	Description:	Defines the Namelist Class and its methods to work with a list of names
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Anthony Ellis, ellisah@etsu.edu, East Tennessee State University
//	Created:		Tuesday, Febuary 018, 2020
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    class NameList
    {
        #region Properties

        /// <summary>  List made of name objects</summary>
        private List<Name> Names;

        /// <summary>  the count of items in the list</summary>
        /// <value>  the current count of the instance</value>
        public int Count { get { return Names.Count; } }

        /// <summary>the current name format for the entire list</summary>
        /// <value>  can be set to one to original, last name first, or first name first. by default it is original.</value>
        public NameFormat Format { get; set; } = NameFormat.ORIGINAL;

        /// <summary>  boolean variable to see if list has been changed</summary>
        /// <value>
        ///   <c>if list has been changed it returns true, otherwise false</c>
        /// </value>
        public bool ListChanged { get; private set; }
        #endregion
        #region Constructor

        /// <summary>  The Default constructor for the namelist</summary>
        public NameList()
        {
            Names = new List<Name>();
            ListChanged = false;
        }

        /// <summary>  copy constructor to make a NameList out of a String Array</summary>
        /// <param name="StringNameArray">  a non-empty string array</param>
        public NameList(String[] StringNameArray)
        {
            Names = new List<Name>();
            int l = StringNameArray.Length;
            for(int i = 0; i < l; i++)
            {
                Names.Add(new Name(StringNameArray[i]));
            }
            ListChanged = false;
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
            ListChanged = true;
        }        
        /// <summary>
        /// Removes the A name Equal to string passed in from the NameList
        /// </summary>
        /// <param name="NameString">The name string.</param>
        /// <returns>Returns boolean true if item removed and boolean false if not removed</returns>
        public bool Remove(String NameString)
        {
            Name NameToRemove = new Name(NameString);
            ListChanged = true;
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
            partialName.Trim();
            
            NameList foundNames = new NameList();
            String tempName;
            foreach(Name n in Names)
            {
                tempName = n.NameToString(NameFormat.ORIGINAL);
                if(tempName.Contains(partialName))
                    foundNames = foundNames+n;
            }

            return foundNames;
        }

        /// <summary>  sorts the namelist by first name</summary>
        /// <returns>returns a string List sorted by first name</returns>
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

        /// <summary>  Sorts the list by Last Name</summary>
        /// <returns>returs a string list with all the names sorted by last name</returns>
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

        /// <summary>  Method for displaying NameList contents to console. mostly used for testing purposes</summary>
        public void Display()
        {
            foreach (Name n in Names)
	        {
                Console.WriteLine(n.NameToString(Format));
	        }
        }

        /// <summary>  Converts the NameList to a String Array</summary>
        /// <returns>returns the string array value representation of the NameList</returns>
        public String[] ToArray()
        {
            String[] stringNameArray = new String[Count];
            for (int i = 0; i < Count; i++)
            {
                stringNameArray[i] = Names[i].NameToString(NameFormat.ORIGINAL);
            }
            return stringNameArray;
        }

        /// <summary>  method to set list change to true that is easier to read. also for testing purposes</summary>
        public void ListChange()
        {
            ListChanged = true;
        }
        #region Indexers

        /// <summary>  the Indexer implementation to access names in name List</summary>
        /// <param name="index">  the index used to find the value</param>
        /// <value></value>
        /// <returns>returns name at index if index is within bounds</returns>
        /// <exception cref="IndexOutOfRangeException">  index is not in the range of accessibe values in the NameList</exception>
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
                ListChanged = true;
            }
        }

        /// <summary>implements indexer to get a name from the Name list using a string as index</summary>
        /// <param name="strName">  the string representation of a name</param>
        /// <returns>returns the name if found and null if not found</returns>
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

        /// <summary>  implements the + operator to add names to the list</summary>
        /// <param name="CurrNameList">  the NameList being added to</param>
        /// <param name="NewName"> The name being added to the NameList</param>
        /// <returns>  returns the NameList that was added to</returns>
        public static NameList operator+(NameList CurrNameList,Name NewName)
        {
            CurrNameList.Names.Add(NewName);
            CurrNameList.ListChange();
            return CurrNameList;
        }

        /// <summary>  implements the - operator to remove names from the list</summary>
        /// <param name="CurrNameList">  the NameList to Remove From</param>
        /// <param name="NameToRemove">  the name being removed from NameList</param>
        /// <returns>  the namelist after the name is removed</returns>
        public static NameList operator-(NameList CurrNameList,Name NameToRemove)
        {
            if (CurrNameList.Names.Contains(NameToRemove))
                CurrNameList.Names.Remove(NameToRemove);
            CurrNameList.ListChange();
            return CurrNameList;
        }
        #endregion
        #endregion
    }
}
