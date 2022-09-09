using System.Text.RegularExpressions;

namespace UnitedNavigationTask.MVC.Handlers
{
    internal static class FieldValidator
    {
        internal static bool isOrderNumberValid(string orderNumber, out List<string> Errors)
        {
            Errors = new List<string>();
            var regex = new Regex(@"^ORD.\d+$");
            if(string.IsNullOrEmpty(orderNumber))
                Errors.Add($"Order Number cannot be empty");
            if (!regex.IsMatch(orderNumber))
                Errors.Add($"Order Number is not valid. It must start with 'ORD' and end with numbers");
            if (Errors.Count > 0)
                return false;
            return true;
        }

        internal static bool isConsignmentNumberValid(string consignmentNumber, out List<string> Errors)
        {
            Errors = new List<string>();
            var regex = new Regex(@"^CON.\d+$");
            if (string.IsNullOrEmpty(consignmentNumber))
                Errors.Add($"Consignment Number cannot be empty");
            if (!regex.IsMatch(consignmentNumber))
                Errors.Add($"Consignment Number is not valid. It must start with 'CON' and end with numbers");
            if (Errors.Count > 0)
                return false;
            return true;
        }

        internal static bool isParcelCodeValid(string parcelCode, out List<string> Errors)
        {
            Errors = new List<string>();
            var regex = new Regex(@"^PARC.\d+$");
            if (string.IsNullOrEmpty(parcelCode))
                Errors.Add($"Parcel Code cannot be empty");
            if (!regex.IsMatch(parcelCode))
                Errors.Add($"Parcel Code is not valid. It must start with 'PARC' and end with numbers");
            if (Errors.Count > 0)
                return false;
            return true;
        }

        internal static bool isConsigneeNameValid(string consigneeName, out List<string> Errors)
        {
            Errors = new List<string>();
            if (string.IsNullOrEmpty(consigneeName))
                Errors.Add($"Consignee Name cannot be empty");
            if (Errors.Count > 0)
                return false;
            return true;
        }

        internal static bool isAddressValid(string address, out List<string> Errors)
        {
            Errors = new List<string>();
            if (string.IsNullOrEmpty(address))
                Errors.Add($"Address One cannot be empty");
            if (Errors.Count > 0)
                return false;
            return true;
        }

        internal static bool isCountryCodeValid(string countryCode, out List<string> Errors)
        {
            Errors = new List<string>();
            if (string.IsNullOrEmpty(countryCode))
                Errors.Add($"Country Code cannot be empty");
            if (Errors.Count > 0)
                return false;
            return true;
        }

        internal static bool isItemQuantityValid(string itemQuantity, out List<string> Errors)
        {
            Errors = new List<string>();
            var regex = new Regex(@"\d+");
            if (!regex.IsMatch(itemQuantity))
                Errors.Add($"Item Quantity can only be a number");
            if (Errors.Count > 0)
                return false;
            return true;
        }

        internal static bool isItemValueValid(string itemValue, out List<string> Errors)
        {
            Errors = new List<string>();
            var regex = new Regex(@"^\d*\.?\d*$");
            if (!regex.IsMatch(itemValue))
                Errors.Add($"Item Value can only be a number");
            if (Errors.Count > 0)
                return false;
            return true;
        }

        internal static bool isItemWeightValid(string itemWeight, out List<string> Errors)
        {
            Errors = new List<string>();
            var regex = new Regex(@"^\d*\.?\d*$");
            if (!regex.IsMatch(itemWeight))
                Errors.Add($"Item Weight can only be a number");
            if (Errors.Count > 0)
                return false;
            return true;
        }

        internal static bool isItemDescriptionValid(string itemDescription, out List<string> Errors)
        {
            Errors = new List<string>();
            if (string.IsNullOrEmpty(itemDescription))
                Errors.Add($"Item Description must be provided");
            if (Errors.Count > 0)
                return false;
            return true;
        }
    }
}
