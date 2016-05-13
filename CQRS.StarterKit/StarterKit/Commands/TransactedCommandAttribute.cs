using System;

namespace StarterKit.Commands
{
    /// <summary>
    /// Attribute to mark commands that this command handling must be wrapped into a transaction
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TransactedCommandAttribute : Attribute
    {
        // marker attribute for commands to be wrapped into a transaction
        // to be applied on ICommand objects
    }
}