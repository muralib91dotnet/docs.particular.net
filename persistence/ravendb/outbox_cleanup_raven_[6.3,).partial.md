It is recommended to disable the cleanup task and rely on the [document expiration](https://ravendb.net/docs/article-page/latest/csharp/server/extensions/expiration) process instead, especially when

- An endpoint is scaled out and instances are competing to run the cleanup task _or_
- An endpoint is running in [multi-tenant mode](/persistence/ravendb/#multi-tenant-support)

The cleanup task can be disabled by specifying a value of `Timeout.InfiniteTimeSpan` for `SetFrequencyToRunDeduplicationDataCleanup`.

snippet: OutboxRavendBFrequencyOfCleanup

Should the cleanup task still be required, it is advised to run the cleanup task on only one NServiceBus endpoint instance per RavenDB database and disable the cleanup task on all other NServiceBus endpoint instances for the most efficient cleanup execution.