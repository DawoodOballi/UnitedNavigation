using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using UnitedNavigationTask.MVC.csvMap;
using UnitedNavigationTask.MVC.Dtos;

namespace UnitedNavigationTask.MVC.Handlers
{
    public static class FileHandler
    {
        internal static bool isFileExtensionValid(IFormFile file, out List<string> errors)
        {
            errors = new List<string>();
            if (file == null)
            {
                errors.Add("Please provide a file to upload");
                return false;
            }
            if (!file.FileName.EndsWith(".csv"))
            {
                errors.Add("Please provide a CSV file only");
                return false;
            }
            return true;
        }

        internal static string SaveFile(IFormFile formFile, string v)
        {
            // first I will save the file somewhere in my current directory
            // I will get the current directory
            // then I will add new directories to my current directory just so that I know exactly where my files are being saved.
            if (formFile == null)
                throw new ArgumentNullException("The file was not provided");

            if (string.IsNullOrEmpty(v))
                throw new ArgumentNullException("The relative file path is necessary for validating the file content");

            var dirs = v.Split("/");
            var currentDir = Directory.GetCurrentDirectory();
            foreach (var dir in dirs)
            {
                currentDir = Path.Combine(currentDir, dir);
                DirectoryInfo dirInfo = Directory.CreateDirectory(currentDir);
            }
            currentDir = Path.Combine(currentDir, DateTime.Now.ToString("yyyyMMdd_HHmmss") + "Z" + $"_{formFile.FileName}");
            using (var fs = File.Create(currentDir))
            {
                formFile.CopyTo(fs);
            }
            return currentDir;

        }

        internal static IEnumerable<CsvDto> ValidateFileFields(string filePath, out List<string> allErrors)
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var returnRecords = new List<CsvDto>();
                    allErrors = new List<string>();
                    csv.Context.RegisterClassMap<CsvDtoMap>();
                    //csv.Read();
                    //csv.ReadHeader();
                    //var headers = csv.Context.Reader.HeaderRecord;
                    var records = csv.GetRecords<CsvDto>();
                    var fieldsToPrint = new List<CsvDto>();
                    var rowCounter = 1;
                    try
                    {
                        foreach (var record in records)
                        {
                            var errors = new List<string>();
                            var errorsPerRow = new List<string>();
                            if (!FieldValidator.isOrderNumberValid(record.OrderNumber, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isConsignmentNumberValid(record.ConsignmentNumber, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isParcelCodeValid(record.ParcelCode, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isConsigneeNameValid(record.ConsigneeName, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isAddressValid(record.AddressOne, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isAddressValid(record.AddressTwo, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isCountryCodeValid(record.CountryCode, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isItemQuantityValid(record.ItemQuantity, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isItemValueValid(record.ItemValue, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isItemWeightValid(record.ItemWeight, out errors))
                                errorsPerRow.AddRange(errors);
                            if (!FieldValidator.isItemDescriptionValid(record.ItemDesciption, out errors))
                                errorsPerRow.AddRange(errors);
                            if (errorsPerRow.Count > 0)
                            {
                                errorsPerRow.Insert(0, $"{errorsPerRow.Count} errors found for row {rowCounter}");
                                allErrors.AddRange(errorsPerRow);
                            }
                            else
                            {
                                fieldsToPrint.Add(record);
                            }
                            rowCounter++;
                        }
                    }
                    catch (CsvHelper.MissingFieldException ex)
                    {
                        var regex = new Regex(@"'([0-9])'");
                        var match = regex.Match(ex.Message);
                        var index = match.Groups[1].ToString();
                        var intIndex = Convert.ToInt32(index);
                        allErrors.Add($"Error: Missing column {++intIndex} for row {rowCounter}");
                    }
                    if (allErrors.Count == 0)
                    {
                        returnRecords = fieldsToPrint;
                    }
                    return returnRecords;
                }
            }
        }
    }
}
