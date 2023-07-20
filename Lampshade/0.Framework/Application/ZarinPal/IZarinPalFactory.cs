namespace _0.Framework.Application.ZarinPal
{
	public interface IZarinPalFactory
	{
		string Prefix { get; set; } //if == sandbox -> test , if == www -> real

		PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
			long orderId);

		VerificationResponse CreateVerificationRequest(string authority, string price);
	}
}