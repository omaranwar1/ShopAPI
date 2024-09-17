public class Cart
{
    public int CartId { get; set; }
    public int? UserId { get; set; }

    public List<CartItem> CartItems { get; set; } = new List<CartItem>();

}