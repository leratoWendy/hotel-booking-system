using log4net;
using System.Runtime.CompilerServices;

namespace HotelRoomBookingAPI.Services
{
    /// <summary>
    /// Helper class for obtaining loggers using log4net.
    /// </summary>
    public static class LocalLogHelper
    {
        /// <summary>
        /// Gets a logger for the calling class.
        /// </summary>
        /// <param name="filename">The filename of the calling class (automatically provided).</param>
        /// <returns>An ILog instance for logging.</returns>
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }
    }
}
