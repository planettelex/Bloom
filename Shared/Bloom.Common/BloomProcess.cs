using System;

namespace Bloom.Common
{
    /// <summary>
    /// The Bloom process types.
    /// </summary>
    public enum ProcessType
    {
        None,
        Analytics,
        Browser,
        Player
    }

    /// <summary>
    /// Represents a Bloom process.
    /// </summary>
    public class BloomProcess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BloomProcess"/> class.
        /// </summary>
        /// <param name="processType">Type of the process.</param>
        public BloomProcess(ProcessType processType)
        {
            Type = processType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BloomProcess"/> class.
        /// </summary>
        /// <param name="processName">The name of the process.</param>
        public BloomProcess(string processName)
        {
            if (processName.Contains("."))
            {
                var typeName = processName.Split('.')[1];
                Type = (ProcessType) Enum.Parse(typeof(ProcessType), typeName);
            }
            else
                Type = (ProcessType) Enum.Parse(typeof(ProcessType), processName);
        }

        /// <summary>
        /// The namespace.
        /// </summary>
        /// <value>Bloom</value>
        public const string Namespace = "Bloom";

        /// <summary>
        /// Gets the process type.
        /// </summary>
        public ProcessType Type { get; private set; }

        /// <summary>
        /// Gets the process name.
        /// </summary>
        public string Name { get { return Namespace + "." + Type; } }
    }
}