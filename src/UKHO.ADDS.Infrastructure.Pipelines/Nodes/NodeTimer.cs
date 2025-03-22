using System.Diagnostics;
using Serilog;
using Serilog.Events;

namespace UKHO.ADDS.Infrastructure.Pipelines.Nodes
{
    /// <summary>
    ///     Used to time nodes and write to the log.
    /// </summary>
    internal class NodeTimer
    {
        private Stopwatch _stopwatch;

        public string StartTimer(dynamic instance, string methodName)
        {
            _stopwatch = Stopwatch.StartNew();
            return $"Starting stopwatch for methodName {methodName} of class {instance.GetType().FullName}";
        }

        public string StopTimer(dynamic instance, string methodName)
        {
            if (_stopwatch != null && _stopwatch.IsRunning)
            {
                _stopwatch.Stop();
                var elapsed = _stopwatch.Elapsed.TotalMilliseconds;
                return $"Stopping stopwatch for methodName {methodName} of class {instance.GetType().FullName}. Elapsed ms: {elapsed}";
            }

            return $"Call to stop occurred, but stopwatch not started. Class {instance.GetType().FullName}, Method {methodName}";
        }

        public void LogStart(dynamic node, string methodName)
        {
            if (Log.IsEnabled(LogEventLevel.Debug))
            {
                string message = StartTimer(node, methodName);
                Log.Debug(message);
            }
        }

        public void LogStop(dynamic node, string methodName)
        {
            if (Log.IsEnabled(LogEventLevel.Debug))
            {
                string message = StopTimer(node, methodName);
                Log.Debug(message);
            }
        }
    }
}
