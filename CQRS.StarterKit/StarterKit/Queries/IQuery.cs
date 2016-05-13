namespace StarterKit.Queries
{
    /// <summary>
    /// Marker interface to say that this class is a query and shall be handled by IQueryHandler
    /// </summary>
    /// <typeparam name="TResult">
    /// Type of result that query returns. 
    /// Can be a single item (i.e. Person) or multiples (i.e. IEnumrable<Person>)
    /// </typeparam>
    public interface IQuery<out TResult>
    {
    }
}