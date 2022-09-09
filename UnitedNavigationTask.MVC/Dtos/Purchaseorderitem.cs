namespace UnitedNavigationTask.MVC.Dtos
{
    public class Purchaseorderitem
    {
        public int id { get; set; } = 0;
        public int purchaseOrderHeaderId { get; set; } = 0;
        public string stockCode { get; set; } = "string";
        public string description { get; set; } = "string";
        public string nominalCode { get; set; } = "string";
        public int taxCode { get; set; } = 0;
        public int orderQty { get; set; } = 0;
        public float unitPrice { get; set; } = 0;
        public int netAmount { get; set; } = 0;
        public int fullNetAmount { get; set; } = 0;
        public string comment1 { get; set; } = "string";
        public string comment2 { get; set; } = "string";
        public int unitOfSale { get; set; } = 0;
        public int taxRate { get; set; } = 0;
        public int taxAmount { get; set; } = 0;
    }

}

