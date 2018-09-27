﻿using System.Collections.Generic;

namespace fishfriends.Biz.Database
{
    public static class ConnectionUtils
    {
        public static List<List<string>> ExecuteCommand(IDbConnection dbConnection, string command)
        {
            return dbConnection.ExecuteCommand(command);
        }

        public static List<List<string>> CreateEmptyResultSet(int numCols)
        {
            List<List<string>> resultSet = new List<List<string>>();

            for (var col = 0; col < numCols; col++)
            {
                resultSet.Add(new List<string>());
            }

            return resultSet;
        }


    }
}
