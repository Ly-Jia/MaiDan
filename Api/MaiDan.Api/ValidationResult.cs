namespace MaiDan.Api
{
    public class ValidationResult<T> where T : class
    {
        public ValidationResult(T model)
        {
            Model = model;
            HasError = false;
            ErrorMessage = string.Empty;
        }

        public static implicit operator ValidationResult<T>(T model)
        {
            return new ValidationResult<T>(model);
        }

        public ValidationResult(string errorMessage)
        {
            Model = null;
            HasError = true;
            ErrorMessage = errorMessage;
        }

        public static implicit operator ValidationResult<T>(string errorMessage)
        {
            return new ValidationResult<T>(errorMessage);
        }

        public T Model { get; }
        public bool HasError { get; }
        public string ErrorMessage { get; }
    }
}
