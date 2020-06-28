using System.Collections.Generic;

namespace WeatherApp.API.Services.Services
{
    public interface ISignalRConnectionsManager
    {
        void AddConnection(string userID, string connectionID);
        void RemoveConnection(string connectionID);
        HashSet<string> GetConnections(string userID);
    }
}
