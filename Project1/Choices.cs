///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project 1
//	File Name:         Choices.cs
//	Description:       The choices for each menu in Program.cs
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Anthony Ellis, ellisah@etsu.edu, East Tennessee State University
//	Created:           Friday, February 21, 2020
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace DataStructures
{

    /// <summary>Enum for choices in main menu</summary>
    enum MainChoice
    {
        OPEN = 1, QUIT
    }

    /// <summary>Enum for choices in the name menu</summary>
    enum NameChoice
    {
        ADD = 1, DELETE, LIST, NAME, QUIT
    }

    /// <summary>enum for decining name format.</summary>
    enum NameFormat
    {
        ORIGINAL = 1, FIRST, LAST, QUIT
    }
}
