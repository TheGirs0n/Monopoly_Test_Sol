public class Box : EntityTemplate
{
    public Box(double width, double height, double depth, double weight, DateTime? expirationDate = null, DateTime? productionDate = null) 
        : base(width, height, depth)
    {
        try
        {
            if ((expirationDate.HasValue && productionDate.HasValue)
                || (!expirationDate.HasValue && !productionDate.HasValue))
            {
                throw new ArgumentException("Может быть задан лишь один параметр!");
            }

            if (expirationDate.HasValue)
            {
                ExpirationDate = expirationDate.Value;
            }
            else if (productionDate.HasValue)
            {
                ProductionDate = productionDate.Value;
                ExpirationDate = productionDate.Value.AddDays(100);
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        Weight = weight;
    }
    
    public double GetVolume()
    {
        var volume = GetWidth() * GetHeight() * GetDepth();
        return volume;
    }
    
    public double GetWeight() => Weight;
    
    public DateTime GetExpirationDate() => ExpirationDate;
    public DateTime GetProductionDate() => ProductionDate;
}