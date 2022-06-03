namespace DukkantekTask.Service.Models.Dtos
{
    /// <summary>
    /// Dto using for get products count by status
    /// </summary>
    public class ProductsCountSummaryDto
    {
        public int TotalSoldProductsCount { get; set; }
        public int TotalInStockProductsCount { get; set; }
        public int TotalDamagedProductsCount { get; set; }
    }
}
