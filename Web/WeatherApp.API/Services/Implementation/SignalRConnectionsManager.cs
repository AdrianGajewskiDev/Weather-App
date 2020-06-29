using System.Collections.Generic;
using WeatherApp.API.Services.Services;

namespace WeatherApp.API.Services.Implementation
{
    public class SignalRConnectionsManager : ISignalRConnectionsManager
    {
        private static Dictionary<string, HashSet<string>> userMap = new Dictionary<string, HashSet<string>>();


        public void AddConnection(string userID, string connectionID)
        {
            lock (userMap)
            {
                if(!userMap.ContainsKey(userID))
                {
                    userMap[userID] = new HashSet<string>();
                }

                userMap[userID].Add(connectionID);
            }
        }

        public HashSet<string> GetConnections(string userID)
        {
            if(userMap.ContainsKey(userID))
            {
                if (userMap[userID] != null)
                    return userMap[userID];
            }
         

            return null;
        }


        public void RemoveConnection(string connectionID)
        {
            foreach (var userID in userMap.Keys)
            {
                if (userMap[userID].Contains(connectionID))
                {
                    userMap[userID].Remove(connectionID);
                    break;
                }
            }
        }
    }
}
