---
title: ServiceControl Forwarding Log Queues
summary: Details of the ServiceControl audit and error configuration and forwarding behavior 
reviewed: 2020-05-18
---

## Audit and error queues

ServiceControl consumes messages from the audit and error queues and stores these messages locally in its own embedded database. These input queues names are specified at install time.

ServiceControl can also forward these messages to two log queues:

 * Error messages are optionally forwarded to the _error_ log queue (default `error.log`).
 * Audit messages are optionally forwarded to the _audit_ log queue (default `audit.log`).

This behavior can be set through ServiceControl Management.

![](managementutil-queueconfig.png 'width=500')

## Processing failures are not forwarded immediately

Failed imports are not forwarded immediately. These will be stored in ServiceControl as Failed Imports or in the error queue of the ServiceControl instance type. Messages are only forwarded by a ServiceControl instance type if succesfully stored in its datebase.

If it is important that messages are forwarded immediately even if ServiceControl cannot process the messages, meaning prioritizing a custom processor over ServiceControl the solution is to reorder the processing order. Meaning, have a custom processor read from the error and audit queues which forwards it to an error and audit queue that ServiceControl will process.

Forwarding by ServiceControl:

   "error" -> ServiceControl -> "error.log" -> Custom Processor

Forwarding by Custom Processor:

   "error" -> Custom Processor -> "error.log" -> ServiceControl

This will prioritize the custom processor over ServiceControl for audit and error processing


## Error and audit log queues

The log queues retain a copy of the original messages ingested by ServiceControl.
The queues are not directly managed by ServiceControl and are meant as points of external integration.

Note: If external integration is not required, it is strongly recommended to turn forwarding to log queues off. Otherwise, messages will accumulate unprocessed in the forwarding log queue(s) until storage resource are exhausted (message count limits, messages size limit, available disk space) is consumed. When this happens new messages cannot be added to either a queue or none of the queues depending on the transport in use.
