public static class SecondAnswerGetData
{
    public static List<Pallet>? ShowAnswer(List<Pallet> pallets)
    {
        var threeVolumesPallets = pallets.OrderByDescending(p => p.GetBoxes().Max(b => b.GetExpirationDate()));
        var firstThree = threeVolumesPallets.Take(3);
        var orderedThree = firstThree.OrderBy(x => x.GetVolume()).ToList();
        
        return orderedThree; 
    }
}