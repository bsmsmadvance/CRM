using CRMMobile.Helper;
using IO.Swagger.Model;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace CRMMobile.Converters
{
    public class ContactAddressEngConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var address = value as ContactAddressDTO;
            if (address == null)
                return string.Empty;

            var fullAddress = string.Empty;
        
                if (!string.IsNullOrEmpty(address.HouseNoEN))
                    fullAddress += address.HouseNoEN ;
                if (!string.IsNullOrEmpty(address.VillageEN))
                    fullAddress += "Village/Building " + address.VillageEN + "  ";
                if (!string.IsNullOrEmpty(address.MooEN))
                    fullAddress += "Moo " + address.MooEN + "  ";
                if (!string.IsNullOrEmpty(address.SoiEN))
                    fullAddress += "Soi " + address.SoiEN + "  ";
                if (!string.IsNullOrEmpty(address.RoadTH))
                    fullAddress += address.RoadTH + "Road" + "  ";
                if (address.SubDistrict != null)
                    fullAddress += address.SubDistrict.NameEN + "Sub-District" + "  ";
                if (address.District != null)
                    fullAddress += address.District.NameEN + "District" + "  ";
                if (address.Province != null)
                    fullAddress += address.Province.NameEN + "Province" + "  ";

                fullAddress += address.PostalCode;
            
            return fullAddress;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}