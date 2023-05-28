namespace DiscountManagement.Application.Contracts.ColleaguesDiscount;

public class ColleaguesDiscountViewModel
{
    public long Id { get; set; }
    public string CreationDate { get; set; }
    public string ProductName { get; set; }
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
    public bool IsRemoved { get; set; }
}