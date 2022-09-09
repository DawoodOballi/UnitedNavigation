namespace UnitedNavigationTask.MVC.Dtos
{
    public class ApiDto
    {
        public int id { get; set; } = 0;
        public string orderNumber { get; set; } = "string";
        public string accountRef { get; set; } = "string";
        public string globalDepartmentCode { get; set; } = "string";
        public string name { get; set; } = "string";
        public string address1 { get; set; } = "string";
        public string address2 { get; set; } = "string";
        public string address3 { get; set; } = "string";
        public string address4 { get; set; } = "string";
        public string address5 { get; set; } = "string";
        public string deliveryName { get; set; } = "string";
        public string deliveryAddress1 { get; set; } = "string";
        public string deliveryAddress2 { get; set; } = "string";
        public string deliveryAddress3 { get; set; } = "string";
        public string deliveryAddress4 { get; set; } = "string";
        public string deliveryAddress5 { get; set; } = "string";
        public string customerTelephoneNumber { get; set; } = "string";
        public string contactName { get; set; } = "string";
        public string orderDate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
        public string notes1 { get; set; } = "string";
        public string notes2 { get; set; } = "string";
        public string notes3 { get; set; } = "string";
        public string takenBy { get; set; } = "string";
        public string supplierOrderNumber { get; set; } = "string";
        public string analysis1 { get; set; } = "string";
        public string analysis2 { get; set; } = "string";
        public string analysis3 { get; set; } = "string";
        public bool inSage { get; set; } = false;
        public Purchaseorderitem[] purchaseOrderItems { get; set; }
    }
}

