using System;

namespace Domain
{
    public struct PackageOption
    {
        public PackageSize Size { get; set; }
        public PackageProvider Provider { get; set; }

        public override string ToString()
        {
            var sizeName = Enum.GetName(typeof(PackageSize), Size);
            var providerName = Enum.GetName(typeof(PackageProvider), Provider);

            var result = string.Concat(sizeName, " ", providerName);

            return result;
        }
    }
}