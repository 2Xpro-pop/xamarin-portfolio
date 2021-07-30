using System.Diagnostics;
namespace PhotoEffect
{
    public static class DebugingExtension
    {
        public static void DebugLog(this string message)
        {
            Debug.WriteLine(message);
        }
    }
}
