using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelly.Models;


public class AllDevices
{
   public bool isok { get; set; }
   public Data data { get; set; }
}

public class Data
{
   public Devices_Status devices_status { get; set; }
   public Pending_Notifications pending_notifications { get; set; }
}

public class Devices_Status
{
   public _5432045B561c 5432045b561c { get; set; }
}

public class _5432045B561c
{
   public Ws ws { get; set; }
   public Mqtt mqtt { get; set; }
   public object[] ht_ui { get; set; }
   public Temperature0 temperature0 { get; set; }
   public object[] ble { get; set; }
   public Devicepower0 devicepower0 { get; set; }
   public Sys sys { get; set; }
   public float ts { get; set; }
   public int serial { get; set; }
   public string id { get; set; }
   public Humidity0 humidity0 { get; set; }
   public string _updated { get; set; }
   public string code { get; set; }
   public Wifi wifi { get; set; }
   public Cloud cloud { get; set; }
   public bool _sleeping { get; set; }
   public _Dev_Info _dev_info { get; set; }
}

public class Ws
{
   public bool connected { get; set; }
}

public class Mqtt
{
   public bool connected { get; set; }
}

public class Temperature0
{
   public int id { get; set; }
   public float tC { get; set; }
   public float tF { get; set; }
}

public class Devicepower0
{
   public int id { get; set; }
   public Battery battery { get; set; }
   public External external { get; set; }
}

public class Battery
{
   public float V { get; set; }
   public int percent { get; set; }
}

public class External
{
   public bool present { get; set; }
}

public class Sys
{
   public string mac { get; set; }
   public bool restart_required { get; set; }
   public string time { get; set; }
   public int unixtime { get; set; }
   public int uptime { get; set; }
   public int ram_size { get; set; }
   public int ram_free { get; set; }
   public int fs_size { get; set; }
   public int fs_free { get; set; }
   public int cfg_rev { get; set; }
   public int kvs_rev { get; set; }
   public int webhook_rev { get; set; }
   public object[] available_updates { get; set; }
   public Wakeup_Reason wakeup_reason { get; set; }
   public int wakeup_period { get; set; }
   public int reset_reason { get; set; }
}

public class Wakeup_Reason
{
   public string boot { get; set; }
   public string cause { get; set; }
}

public class Humidity0
{
   public int id { get; set; }
   public float rh { get; set; }
}

public class Wifi
{
   public string sta_ip { get; set; }
   public string status { get; set; }
   public string ssid { get; set; }
   public int rssi { get; set; }
}

public class Cloud
{
   public bool connected { get; set; }
}

public class _Dev_Info
{
   public string id { get; set; }
   public string gen { get; set; }
   public string code { get; set; }
   public bool online { get; set; }
}

public class Pending_Notifications
{
}



