namespace RiftekTemplateUpgrade.Model
{
    public class Template
    {
        public int Number { get; set; }
        
        public ScannerSettings ScannerSettings { get; set; }


        public Template() { }
        public Template(int number, ScannerSettings scannerSettings)
        {
            Number = number;
            ScannerSettings = scannerSettings;
        }


    }
}
