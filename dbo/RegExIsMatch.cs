using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{

    [SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    public static SqlString RegexEscape(string Input)
    {
        return Regex.Escape(Input);
    }

    [SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    public static SqlBoolean RegexIsMatch (string Input, string Pattern)
    {
        return Regex.IsMatch(Input, Pattern);
    }
    
    [SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillMatches", TableDefinition = "[Index] int, [Value] nvarchar(4000)")]
    public static IEnumerable RegexMatch(string Input, string Pattern)
    {
        List<Match> GroupCollection = new List<Match>();
        Match m = Regex.Match(Input, Pattern);
        while (m.Success)
        {
            GroupCollection.Add(m);
            m = m.NextMatch();
        }
        return GroupCollection;
    }

    public static void FillMatches(object Group, out SqlInt32 Index, out SqlString Value)
    {
        Match rm = (Match)Group;
        Index = rm.Index;
        Value = rm.Value;
    }

    [SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    public static SqlString RegexReplace(string Input, string Pattern, string Replacement)
    {
        return Regex.Replace(Input, Pattern, Replacement);
    }

    [SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillSplit", TableDefinition = "[Value] nvarchar(max)")]
    public static IEnumerable RegexSplit(string Input, string Pattern)
    {
        List<string> GroupCollection = new List<string>();
        string[] substrings = Regex.Split(Input, Pattern);
        foreach (string match in substrings)
        {
            GroupCollection.Add(match);
        }
        return GroupCollection;
    }

    public static void FillSplit(object Group, out SqlString Value)
    {
        Value = (String)Group;
    }



    //[SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    //public static SqlString RegexMatchGroupAt(Input string, string Pattern, string groupName, int captureIndex)
    //{
    //    return Regex.M
    //}

    //[SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    //public static SqlString RegexMatchGroupIndexAt(string Input, string Pattern, string groupIndex, int captureIndex)
    //{

    //}

    //[SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    //public static SqlInt32 RegexMatchGroupCaptureCount(string Input, string Pattern, string groupName)
    //{

    //}

    //[SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    //public static SqlInt32 RegexMatchGroupIndexCaptureCount(string Input, string Pattern, string groupIndex)
    //{
    //    return Regex.RegexMatchGroupIndexCaptureCount(Input, Pattern, groupIndex);
    //}

    //[SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    //public static IEnumerable RegexMatchGroupCaptures(string Input, string Pattern, string groupName)
    //{

    //}

    //[SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    //public static IEnumerable RegexMatchGroupIndexCaptures(string Input, string Pattern, string groupIndex)
    //{

    //}


}
