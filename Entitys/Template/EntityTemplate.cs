public abstract class EntityTemplate
{
    private readonly Guid _id;
    private readonly double _width;
    private readonly double _height;
    private readonly double _depth;
    protected double Weight;
    protected DateTime ExpirationDate { get; set; }
    protected DateTime ProductionDate { get; set; }

    protected EntityTemplate(double width, double height, double depth)
    {
        if(!ValidateDimensions(width, height, depth))
            return;
        
        _id = Guid.NewGuid();
        _width = width;
        _height = height;
        _depth = depth;
    }

    public Guid GetId() => _id;
    public double GetWidth() => _width;
    public double GetHeight() => _height;
    public double GetDepth() => _depth;

    protected virtual double SetVolume() => _width * _height * _depth;
    
    private bool ValidateDimensions(double width, double height, double depth)
    {
        bool valid = true;

        try
        {
            if (width <= 0)
            {
                throw new ArgumentException("Ширина должна быть больше 0");
            }
            if (height <= 0)
            {
                throw new ArgumentException("Высота должна быть больше 0");
            }
            if (depth <= 0)
            {
                throw new ArgumentException("Глубина должна быть больше 0");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message); 
            valid = false;
        }

        return valid;
    }
}