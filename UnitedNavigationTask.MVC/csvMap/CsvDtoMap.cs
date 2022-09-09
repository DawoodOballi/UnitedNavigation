using CsvHelper.Configuration;
using System.Globalization;
using UnitedNavigationTask.MVC.Dtos;

namespace UnitedNavigationTask.MVC.csvMap
{
    public class CsvDtoMap : ClassMap<CsvDto>
    {
        public CsvDtoMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.OrderNumber).Name("Order No", "Order Number", "Order Id"); // Alternate names that could be used as the headers.
            Map(m => m.ConsignmentNumber).Name("Consignment No", "Consignment Number", "Consignment Id"); // Alternate names that could be used as the headers.
            Map(m => m.ParcelCode).Name("Parcel Code", "ParcelCode", "Parcel Id"); // Alternate names that could be used as the headers.
            Map(m => m.ConsigneeName).Name("Consignee Name", "ConsigneeName"); // Alternate names that could be used as the headers.
            Map(m => m.AddressOne).Name("Address 1", "AddressOne", "Address One"); // Alternate names that could be used as the headers.
            Map(m => m.AddressTwo).Name("Address 2", "AddressTwo", "Address Two"); // Alternate names that could be used as the headers.
            Map(m => m.City).Name("City"); // Alternate names that could be used as the headers.
            Map(m => m.CountryCode).Name("Country Code", "CountryCode"); // Alternate names that could be used as the headers.
            Map(m => m.ItemQuantity).Name("Item Quantity", "ItemQuantity"); // Alternate names that could be used as the headers.
            Map(m => m.ItemValue).Name("Item Value", "ItemValue"); // Alternate names that could be used as the headers.
            Map(m => m.ItemWeight).Name("Item Weight", "ItemWeight"); // Alternate names that could be used as the headers.
            Map(m => m.ItemDesciption).Name("Item Description", "ItemDescription"); // Alternate names that could be used as the headers.
            Map(m => m.ItemCurrency).Name("Item Currency", "ItemCurrency"); // Alternate names that could be used as the headers.
        }
    }
}
