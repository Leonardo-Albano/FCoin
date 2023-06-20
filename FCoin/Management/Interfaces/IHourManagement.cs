namespace FCoin.Business.Interfaces
{
    public interface IHourManagement
    {
        Task<DateTime> GetHour();
    }
}