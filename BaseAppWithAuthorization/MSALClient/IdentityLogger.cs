﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.IdentityModel.Abstractions;
using System.Diagnostics;

namespace BaseAppWithAuthorization.MSALClient
{
    /// <summary>
    /// An example of how to use MSAL.NET logging
    /// </summary>
    /// <seealso cref="IIdentityLogger" />
    /// <autogeneratedoc />
    public class IdentityLogger : IIdentityLogger
    {
        private EventLogLevel _minLogLevel = EventLogLevel.LogAlways;

        /// <summary>
        /// Create instance of IIdentityLogger implementer and set a logging level for this instance
        /// </summary>
        /// <param name="minLogLevel">Default: LogAlways</param>
        public IdentityLogger(EventLogLevel minLogLevel = EventLogLevel.LogAlways)
        {
            _minLogLevel = minLogLevel;
        }

        /// <summary>
        /// Checks if log is enabled or not based on the Entry level
        /// </summary>
        /// <param name="eventLogLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(EventLogLevel eventLogLevel)
        {
            return eventLogLevel >= _minLogLevel;
        }

        /// <summary>
        /// Log to console for demo purpose
        /// </summary>
        /// <param name="entry">Log Entry values</param>
        public void Log(LogEntry entry)
        {
            Debug.WriteLine($"MSAL: EventLogLevel: {entry.EventLogLevel}, Message: {entry.Message} ");
        }
    }
}