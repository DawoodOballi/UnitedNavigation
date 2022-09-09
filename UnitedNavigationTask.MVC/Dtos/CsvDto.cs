using CsvHelper.Configuration.Attributes;

namespace UnitedNavigationTask.MVC.Dtos
{
    public class CsvDto
    {
        [Index(0)]
        public string OrderNumber { get; set; }
        [Index(1)]
        public string ConsignmentNumber { get; set; }
        [Index(2)]
        public string ParcelCode { get; set; }
        [Index(3)]
        public string ConsigneeName { get; set; }
        [Index(4)]
        public string AddressOne { get; set; }
        [Index(5)]
        public string AddressTwo { get; set; }
        [Index(6)]
        public string? City { get; set; }
        [Index(7)]
        public string CountryCode { get; set; }
        [Index(8)]
        public string ItemQuantity { get; set; }
        [Index(9)]
        public string ItemValue { get; set; }
        [Index(10)]
        public string ItemWeight { get; set; }
        [Index(11)]
        public string ItemDesciption { get; set; }
        [Index(12)]
        public string? ItemCurrency { get; set; }
    }
}
