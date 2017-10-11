namespace MaiDan.Api.DataContract.Ordering
{
    public interface IDataContract<T>
    {
        T ToDomainObject();
    }
}
