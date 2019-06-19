namespace DDCore.Domain
{
    /// <summary>
    /// Decorator Class to Identify Aggregate Roots.  
    /// Aggregate Roots are supplied a <seealso cref="IRepository{T}" /> 
    /// which can execute <seealso cref="Command{T}"/> and <seealso cref="Specification.LookupSpecification{T}"/>
    /// and <seealso cref="Specification.QuerySpecification{T}"/>
    /// </summary>
    public interface IAggregateRoot
    {
    }
}
