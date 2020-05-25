namespace Raven_5
{
    using System;
    using NServiceBus;

    class Outbox
    {
        Outbox(EndpointConfiguration endpointConfiguration)
        {
            #region OutboxRavendBTimeToKeep
            endpointConfiguration.SetTimeToKeepDeduplicationData(TimeSpan.FromDays(7));
            #endregion

            #region OutboxRavendBFrequencyOfCleanup
            endpointConfiguration.SetFrequencyToRunDeduplicationDataCleanup(TimeSpan.FromMinutes(1));
            #endregion
        }
    }
}