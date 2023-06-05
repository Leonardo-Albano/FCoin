namespace FCoin.Business.Interfaces
{
    public interface IValidatorManagement
    {
        Task<bool> ValidateTransaction(int id);
    }
}