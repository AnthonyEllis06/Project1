using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    class Name
    {
        #region Properites

        public String Preffix { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String Suffix { get; set; }
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
            String[] nameArray = Tools.Tokenize(NameString, ",.");
        }
        #endregion
    }
}
