public class Pallet : EntityTemplate
{
    private List<Box> Boxes { get; set; }

    public Pallet(double width, double height, double depth) : base(width, height, depth)
    {
        Boxes = [];
    }

    public void SetBoxes(List<Box> boxes)
    {
        if (ValidateBoxes(boxes))
        {
            Boxes = boxes;
            SetWeight();
            SetExpirationDate();
            SetVolume();
        }
    }

    public List<Box> GetBoxes()
    {
        return Boxes;
    }

    public double GetWeight() => Weight;
    public double GetVolume() => SetVolume();
    public DateTime GetExpirationDate() => ExpirationDate;
    protected override double SetVolume()
    {
        double palletVolume = GetWidth() * GetHeight() * GetDepth();
        double boxesVolume = Boxes?.Sum(b => b.GetVolume()) ?? 0;
        return palletVolume + boxesVolume;
    }
    
    private double SetWeight()
    {
        Weight = Boxes.Sum(b => b.GetWeight()) + 30;
        return Weight;
    }

    private DateTime? SetExpirationDate()
    {
        if (Boxes.Count != 0)
        {
            ExpirationDate = Boxes!.Min(b => b.GetExpirationDate());
        }
        else
        {
            ExpirationDate = new DateTime();
        }
        
        return ExpirationDate;
    }
    
    private bool ValidateBoxes(List<Box> boxes)
    {
        bool valid = true;
        
        try
        {
            foreach (var box in boxes)
            {
                if (box.GetWidth() > this.GetWidth())
                {
                    throw new ArgumentException("Коробка не должна быть шире паллеты");
                }
                if (box.GetDepth() > this.GetDepth())
                {
                    throw new ArgumentException("Коробка не должна быть глубже паллеты");
                }
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