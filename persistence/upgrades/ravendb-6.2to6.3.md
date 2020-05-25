---
title: RavenDB Persistence Upgrade from 6.2 to 6.3
summary: Instructions on how to upgrade NServiceBus.RavenDB 6.2 to 6.3
component: Raven
related:
 - nservicebus/upgrades/7to8
reviewed: 2020-05-25
isUpgradeGuide: true
upgradeGuideCoreVersions:
 - 7
---

Starting with NServiceBus.RavenDB version 6.3.0, outbox related settings on `EndpointConfiguration` are obsolete and using it will produce the following message:

> 'RavenDBOutboxExtensions.SetTimeToKeepDeduplicationData(EndpointConfiguration, TimeSpan)' is obsolete: 'Use `SetTimeToKeepDeduplicationData` available on the `OutboxSettings` instead. Will be removed in version 7.0.0.'

and

> 'RavenDBOutboxExtensions.SetFrequencyToRunDeduplicationDataCleanup(EndpointConfiguration, TimeSpan)' is obsolete: 'Use `SetFrequencyToRunDeduplicationDataCleanup` available on the `OutboxSettings` instead. Will be removed in version 7.0.0.'

To migrate outbox settings API use:

snippet: OutboxSettingsUpgrade

NOTE: It is recommended to disable the cleanup task and rely on the [document expiration](https://ravendb.net/docs/article-page/latest/csharp/server/extensions/expiration) process instead. For more information on how to disable the cleanup task refer to the [outbox cleanup guidance](/persistence/ravendb/outbox.md?version=raven_6.3#deduplication-record-lifespan).