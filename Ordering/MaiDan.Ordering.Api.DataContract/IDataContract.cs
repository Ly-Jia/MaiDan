namespace MaiDan.Ordering.DataContract
{
    public interface IDataContract<T>
    {
        T ToDomainObject();
    }
}
