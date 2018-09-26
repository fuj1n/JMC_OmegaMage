using UnityEngine;

public static class LoggerExtensions
{
    /// <summary>
    /// Logs if the condition is met
    /// </summary>
    /// <param name="condition">The condition to be met</param>
    /// <param name="message">The message to log</param>
    /// <param name="level">The log level (info, warning or error)</param>
    public static void ConditionalLog(this MonoBehaviour m, bool condition, string message, string level = "info")
    {
        if (!condition)
#pragma warning disable CS0162 // Unreachable code detected
            return;
#pragma warning restore CS0162 // Unreachable code detected

        switch (level)
        {
            case "info":
                Debug.Log(message);
                break;
            case "warning":
                Debug.LogWarning(message);
                break;
            case "error":
                Debug.LogError(message);
                break;
        }
    }

    /// <summary>
    /// Formatted version of <see cref="ConditionalLog(MonoBehaviour, bool, string, string)"/>
    /// </summary>
    /// <param name="condition">The condition to be met</param>
    /// <param name="message">The formatted message to log</param>
    /// <param name="args">The arguments for the formatter</param>
    public static void ConditionalLog(this MonoBehaviour m, bool condition, string message, params object[] args)
    {
        ConditionalLog(m, condition, string.Format(message, args));
    }

    /// <summary>
    /// Level-aware version of <see cref="ConditionalLog(MonoBehaviour, bool, string, object[])"/>
    /// </summary>
    /// <param name="condition">The condition to be met</param>
    /// <param name="message">The formatted message to log</param>    
    /// <param name="level">The log level (info, warning or error)</param>
    /// <param name="args">The arguments for the formatter</param>
    public static void ConditionalLog(this MonoBehaviour m, bool condition, string message, string level, params object[] args)
    {
        ConditionalLog(m, condition, string.Format(message, args), level);
    }
}
