using System;
using System.Threading.Tasks;
using MongoDB.Constants;
using MongoDB.Entities;
using MongoDB.Helpers;
using MongoDB.Repositories;

namespace MongoDB
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var context = new BaseRepository(ConnectionConstants.DatabaseName);

            #region Insert

            var client = new Client
            {
                FirstName = "Yan",
                LastName = "Josen",
                Address = new Address
                {
                    StreetAddress = "101 Oak Street",
                    City = "Scranton",
                    State = "PA",
                    ZipCode = "18512"
                }
            };

            await context.InsertAsync(ConnectionConstants.ClientCollection, client);
            #endregion

            #region Get
            var clients = await context.GetAllAsync<Client>(ConnectionConstants.ClientCollection);
            clients.Dump();

            var clientId = new Guid("bacfc3c3-07ce-426d-bcbe-dc646d799fa0");

            var clientResult = await context.GetByIdAsync<Client>(ConnectionConstants.ClientCollection, clientId);
            clientResult.Dump();

            #endregion

            #region Update
            clientResult.DateOfBirth = new DateTime(1982, 10, 31, 0, 0, 0, DateTimeKind.Utc);
            await context.UpsertAsync(ConnectionConstants.ClientCollection, clientId, clientResult);

            #endregion

            #region Delete
            await context.DeleteAsync<Client>(ConnectionConstants.ClientCollection, clientId);

            #endregion

            Console.ReadLine();
        }
    }
}
