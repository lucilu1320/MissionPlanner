using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;

namespace System.Drawing
{
    /// <summary>
    /// IconConverter is a class that can be used to convert Icon objects from one data type to another.
    /// </summary>
    public class IconConverter : TypeConverter
    {
        /// <summary>
        /// Determines if this converter can convert an object in the given source type to an Icon.
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(byte[]))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Converts the given object to an Icon object.
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is byte[] bytes)
            {
                MemoryStream ms = new MemoryStream(bytes);
                return new Icon(ms);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Determines if this converter can convert an Icon to the given destination type.
        /// </summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(byte[]))
            {
                return true;
            }

            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given Icon object to the given destination type.
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (value is Icon icon)
            {
                if (destinationType == typeof(byte[]))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        icon.Save(ms, SkiaSharp.SKEncodedImageFormat.Png);
                        return ms.ToArray();
                    }
                }

                if (destinationType == typeof(string))
                {
                    return icon.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
