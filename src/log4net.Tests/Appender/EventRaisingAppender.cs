﻿/*
 *
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 *
*/

using log4net.Core;
using System;

namespace log4net.Tests.Appender;

/// <summary>
/// Provides data for the <see cref="EventRaisingAppender.LoggingEventAppended"/> event.
/// </summary>
public class LoggingEventEventArgs(LoggingEvent loggingEvent) : EventArgs
{
  public LoggingEvent LoggingEvent { get; } = loggingEvent ?? throw new ArgumentNullException(nameof(loggingEvent));
}

/// <summary>
/// A log4net appender that raises an event each time a logging event is appended
/// </summary>
/// <remarks>
/// This class is intended to provide a way for test code to inspect logging
/// events as they are generated.
/// </remarks>
public class EventRaisingAppender : log4net.Appender.IAppender
{
  public event EventHandler<LoggingEventEventArgs>? LoggingEventAppended;

  protected void OnLoggingEventAppended(LoggingEventEventArgs e)
    => LoggingEventAppended?.Invoke(this, e);

  public void Close()
  { }

  public void DoAppend(LoggingEvent loggingEvent)
    => OnLoggingEventAppended(new LoggingEventEventArgs(loggingEvent));

  public string Name { get; set; } = string.Empty;
}