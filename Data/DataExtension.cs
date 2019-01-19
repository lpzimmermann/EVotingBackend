using Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Data
{
    public static class DataExtension
    {
        /// <summary>
        ///     Adds the DBContext to the given serviceCollection
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration">
        ///     The Configuration containing the Config for the Db-Connection
        ///     Database:ConnectionString = Connectionstring
        ///     Database:Name = The name of the Database
        /// </param>
        public static void AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddTransient(s =>
                new MongoClient(configuration["Database:ConnectionString"])
                    .GetDatabase(configuration["Database:Name"]));
            serviceCollection.AddSingleton<DbContext>();
        }

        /// <summary>
        ///     Adds the UserStore to the given serviceCollection
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddPersonStore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(provider => provider.GetService<DbContext>().Persons);
            serviceCollection.AddTransient(provider => provider.GetService<DbContext>().Polls);
            serviceCollection.AddTransient(provider => provider.GetService<DbContext>().Votes);
            serviceCollection.AddTransient<IPersonRepository, PersonRepository>();
            serviceCollection.AddTransient<IPollRepository, PollRepository>();
            serviceCollection.AddTransient<IVoteRepository, VoteRepository>();
        }
    }
}