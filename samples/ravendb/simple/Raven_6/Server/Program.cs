﻿using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.Expiration;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

class Program
{
    static async Task Main()
    {
        Console.Title = "Samples.RavenDB.Server";

        #region Config

        var endpointConfiguration = new EndpointConfiguration("Samples.RavenDB.Server");
        using (var documentStore = new DocumentStore
        {
            Urls = new [] {"http://localhost:8080" },
            Database = "RavenSimpleSample",
        })
        {
            documentStore.Initialize();

            var persistence = endpointConfiguration.UsePersistence<RavenDBPersistence>();
            // Only required to simplify the sample setup
            persistence.SetDefaultDocumentStore(documentStore);

            #endregion

            var outbox = endpointConfiguration.EnableOutbox();
            outbox.SetTimeToKeepDeduplicationData(TimeSpan.FromMinutes(5));
            // disable local cleanup task
            outbox.SetFrequencyToRunDeduplicationDataCleanup(Timeout.InfiniteTimeSpan);

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.Transactions(TransportTransactionMode.SendsAtomicWithReceive);
            endpointConfiguration.EnableInstallers();

            // create the database
            await documentStore.Maintenance.Server.SendAsync(new CreateDatabaseOperation(new DatabaseRecord(documentStore.Database)));
            // enable the document expiration
            await documentStore.Maintenance.SendAsync(new ConfigureExpirationOperation(new ExpirationConfiguration { Disabled = false, }));

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}