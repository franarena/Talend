namespace TalendMigration.Core.Exceptions;
public class MigrationException : System.Exception
{
    public MigrationException() { }
    public MigrationException(string message) : base(message) { }
    public MigrationException(string message, System.Exception inner) : base(message, inner) { }
    protected MigrationException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}