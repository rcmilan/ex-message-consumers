namespace Order.Domain
{
    public abstract class BaseModel<T>
    {
        public T Id { get; set; } = default!;
    }
}
