using Newtonsoft.Json;

namespace RiftekTemplateUpgrade.Model
{
    public class ScannerSettings
    {
        //user sensor
        public int user_sensor_framerate { get; set; }
        public int user_sensor_maxFramerate { get; set; }

        public int user_sensor_exposureControl { get; set; }
        public int user_sensor_exposure1 { get; set; }
        public int user_sensor_exposure2 { get; set; }
        public int user_sensor_exposure3 { get; set; }
        public bool user_sensor_exposureAdjust { get; set; }
        public int user_sensor_maxExposure { get; set; }


        //user processing 
        public int user_processing_intensityClipping { get; set; }
        public int user_processing_threshold { get; set; }
        public int user_processing_signalWidthMin { get; set; }
        public int user_processing_signalWidthMax { get; set; }
        public int user_processing_profPerSec { get; set; }
        public int user_processing_medianMode { set; get; }
	    public int user_processing_bilateralMode { set; get; }
	    public int user_processing_peakMode { get; set; }



        //user laser
        public bool user_laser_enabled { get; set; }
        public int  user_laser_value { get; set; }

        //user_sensor
        public bool user_sensor_doubleSpeedEnabled { get; set; }
        public int user_sensor_edrType  { get; set; }
        public int user_sensor_edrColumnDivider { get; set; }



    }
}
