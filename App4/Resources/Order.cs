    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;
    using Android.Widget;
    using Newtonsoft.Json;

public class Order2
{
    public object orderId { get; set; }
    public string orderStatusCode { get; set; }
    public string orderStatusDescription { get; set; }
    public int serviceProviderId { get; set; }
    public DateTime orderOpeningDate { get; set; }
    public string orderSchedulingDate { get; set; }
    public string orderSchedulingPeriod { get; set; }
    public object orderSettlementDate { get; set; }
    public object orderCancellationDate { get; set; }
    }

    public class Order
    {
    public Order2 order { get; set; }
    }

    public class RootObject
    {
    public List<Order> orders { get; set; }
    }