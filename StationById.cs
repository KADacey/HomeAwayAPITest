using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAwayAPITest
{
    public class StationById
    {
        public Alt_Fuel_Station alt_fuel_station { get; set; }
    }

    public class Alt_Fuel_Station
    {
        public string access_days_time { get; set; }
        public object cards_accepted { get; set; }
        public string date_last_confirmed { get; set; }
        public object expected_date { get; set; }
        public string fuel_type_code { get; set; }
        public int id { get; set; }
        public string groups_with_access_code { get; set; }
        public object open_date { get; set; }
        public object owner_type_code { get; set; }
        public string status_code { get; set; }
        public string station_name { get; set; }
        public string station_phone { get; set; }
        public DateTime updated_at { get; set; }
        public string geocode_status { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string city { get; set; }
        public string intersection_directions { get; set; }
        public object plus4 { get; set; }
        public string state { get; set; }
        public string street_address { get; set; }
        public string zip { get; set; }
        public object bd_blends { get; set; }
        public object e85_blender_pump { get; set; }
        public string[] ev_connector_types { get; set; }
        public object ev_dc_fast_num { get; set; }
        public object ev_level1_evse_num { get; set; }
        public int ev_level2_evse_num { get; set; }
        public string ev_network { get; set; }
        public string ev_network_web { get; set; }
        public object ev_other_evse { get; set; }
        public object hy_status_link { get; set; }
        public object lpg_primary { get; set; }
        public object ng_fill_type_code { get; set; }
        public object ng_psi { get; set; }
        public object ng_vehicle_class { get; set; }
        public Ev_Network_Ids ev_network_ids { get; set; }
    }
}
