namespace Codealike.PortableLogic.Communication.ApiModels
{
    using System.Collections.Generic;

    public class Technology
    {
        public List<string> Categories { get; set; }
        
        public string Extension { get; set; }

        public string Name { get; set; }

        public double Percentage { get; set; }

    }
}