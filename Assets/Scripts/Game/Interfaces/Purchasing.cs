namespace SI.Purchasings
{
    public static class Purchasing
    {
        public static bool TryPurchase(Product product, ref float points)
        {
            if (product.Price > points)
                return false;

            points -= product.Price;
            product.Execute();
            return true;
        }
    }
}