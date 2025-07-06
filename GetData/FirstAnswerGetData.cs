public static class FirstAnswerGetData
{
    public static List<IGrouping<DateTime,Pallet>>? ShowAnswer(List<Pallet> pallets)
    {
        var groupedPalletsWeight = pallets.GroupBy(x => x.GetExpirationDate());
        var orderedPallets = groupedPalletsWeight.OrderBy(x => x.Key);
        var orderedWeight = orderedPallets.ThenBy(x => x.Select(weight => weight.GetWeight())).ToList();
        
        return orderedWeight; 
    }
}