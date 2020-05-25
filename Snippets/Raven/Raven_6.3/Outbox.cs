namespace Raven_6
{
    using System;
    using NServiceBus;

    class Outbox
    {
        Outbox(EndpointConfiguration endpointConfiguration)
        {
            #region OutboxRavendBTimeToKeep
            var outbox = endpointConfiguration.EnableOutbox();
            outbox.SetTimeToKeepDeduplicationData(TimeSpan.FromDays(7));
            #endregion
        }

        void OutboxCleanup(EndpointConfiguration endpointConfiguration)
        {
            #region OutboxRavendBFrequencyOfCleanup
            var outbox = endpointConfiguration.EnableOutbox();
            outbox.SetFrequencyToRunDeduplicationDataCleanup(TimeSpan.FromMinutes(1));
            #endregion
        }
    }
}