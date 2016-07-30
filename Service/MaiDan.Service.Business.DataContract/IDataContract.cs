namespace MaiDan.Service.Business.DataContract
{
    public interface IDataContract<T>
    {
        T ToDomainObject();
    }
}
