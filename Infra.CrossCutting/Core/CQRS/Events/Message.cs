using System.Diagnostics.CodeAnalysis;

namespace Infra.CrossCutting.Core.CQRS.Events
{
    [ExcludeFromCodeCoverage]
    public abstract class Message
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}