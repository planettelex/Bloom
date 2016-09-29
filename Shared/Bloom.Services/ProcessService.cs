using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Bloom.Services
{
    /// <summary>
    /// Service for system process operations.
    /// </summary>
    /// <seealso cref="Bloom.Services.IProcessService" />
    public class ProcessService : IProcessService
    {
        /// <summary>
        /// Brings a process window to the foreground.
        /// </summary>
        /// <param name="hWnd">The window handle.</param>
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Brings the browser process to the foreground, or starts a new one if one isn't already running.
        /// </summary>
        public void GoToBrowserProcess()
        {
            var browserProcessName = Properties.Settings.Default.BrowserProcessName;
            var runningProcess = Process.GetCurrentProcess();

            if (runningProcess.ProcessName.StartsWith(browserProcessName))
                return;

            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.StartsWith(browserProcessName))
                {
                    SetForegroundWindow(process.MainWindowHandle);
                    return;
                }
            }

            var executablePath = Properties.Settings.Default.BrowserExecutablePath;
            Process.Start(executablePath);
        }

        /// <summary>
        /// Brings the player process to the foreground, or starts a new one if one isn't already running.
        /// </summary>
        public void GoToPlayerProcess()
        {
            var playerProcessName = Properties.Settings.Default.PlayerProcessName;
            var runningProcess = Process.GetCurrentProcess();

            if (runningProcess.ProcessName.StartsWith(playerProcessName))
                return;

            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.StartsWith(playerProcessName))
                {
                    SetForegroundWindow(process.MainWindowHandle);
                    return;
                }
            }

            var executablePath = Properties.Settings.Default.PlayerExecutablePath;
            Process.Start(executablePath);
        }

        /// <summary>
        /// Brings the analytics process to the foreground, or starts a new one if one isn't already running.
        /// </summary>
        public void GoToAnalyticsProcess()
        {
            var analyticsProcessName = Properties.Settings.Default.AnalyticsProcessName;
            var runningProcess = Process.GetCurrentProcess();

            if (runningProcess.ProcessName.StartsWith(analyticsProcessName))
                return;

            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.StartsWith(analyticsProcessName))
                {
                    SetForegroundWindow(process.MainWindowHandle);
                    return;
                }
            }

            var executablePath = Properties.Settings.Default.AnalyticsExecutablePath;
            Process.Start(executablePath);
        }
    }
}
