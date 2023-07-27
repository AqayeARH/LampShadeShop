namespace _0.Framework.Application.Sms
{
    public interface ISmsService
    {
        Task Send(string number, string message);
        Task SendConfirmOrder(string number, string name, string issueTrackingNo,string orderId);
    }
}