public static class GenerateDataRandom
{
    private static readonly Random Random = new Random();

    private const int MaxPalletWidth = 100;
    private const int MaxPalletHeight = 100;
    private const int MaxPalletDepth = 100;
    
    public static List<Pallet> GeneratePallets(int palletCount, 
                                              int minBoxes = 1, 
                                              int maxBoxes = 10)
    {
        var pallets = new List<Pallet>();
        
        for (int i = 0; i < palletCount; i++)
        {
            var pallet = GeneratePallet(MaxPalletWidth, MaxPalletHeight, MaxPalletDepth);

            int boxCount = Random.Next(minBoxes, maxBoxes + 1);
            var boxes = GenerateBoxesForPallet(pallet, boxCount);
            
            pallet.SetBoxes(boxes);
            pallets.Add(pallet);
        }
        
        return pallets;
    }

    private static Pallet GeneratePallet(int maxPalletWidth, int maxPalletHeight, int maxPalletDepth)
    {
        var width = RoundTo0Decimals(Random.NextDouble() * maxPalletWidth);
        var height = RoundTo0Decimals(Random.NextDouble() * maxPalletHeight);
        var depth = RoundTo0Decimals(Random.NextDouble() * maxPalletDepth);
        
        var pallet = new Pallet(width, height, depth);
        return pallet;
    }

    private static List<Box> GenerateBoxesForPallet(Pallet pallet, int boxCount)
    {
        var boxes = new List<Box>();
        
        for (int i = 0; i < boxCount; i++)
        {
            double width = GenerateValidDimension(pallet.GetWidth());
            double depth = GenerateValidDimension(pallet.GetDepth());
            double height = GenerateRandomHeight();
            float weight = (float)Math.Round(Random.NextDouble() * 100, 2);
            
            DateTime? expirationDate = null;
            DateTime? productionDate = null;
            
            if (Random.Next(2) == 0)
            {
                expirationDate = GenerateExpirationDate();
            }
            else
            {
                productionDate = GenerateProductionDate();
            }
            
            boxes.Add(new Box(width, height, depth, weight, expirationDate, productionDate));
        }
        
        return boxes;
    }

    private static double GenerateValidDimension(double palletDimension)
    {
        double min = palletDimension * 0.1f;
        double max = palletDimension * 0.95f;
        return RoundTo0Decimals(min + (Random.NextDouble() * (max - min)));
    }

    private static double GenerateRandomHeight()
    {
        return RoundTo0Decimals(Random.NextDouble() * 100);
    }

    private static double RoundTo0Decimals(double value)
    {
        return Math.Round(value, 3);
    }
    
    private static DateTime GenerateExpirationDate()
    {
        return DateTime.Today.AddDays(Random.Next(1, 366));
    }

    private static DateTime GenerateProductionDate()
    {
        return DateTime.Today.AddDays(-Random.Next(1, 101));
    }
}