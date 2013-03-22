using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ibi.Fe.Router.Controllers
{
    using System.IO;

    using Ibi.Fe.Router.Code;

    using OsmSharp.Routing.Core;
    using OsmSharp.Tools.Math.Geo;

    public class RouteController : Controller
    {
        public ActionResult Index()
        {
            var router = Engine.Instance;

            // resolve both points; find the closest routable road.
            RouterPoint point1 = router.Resolve(VehicleEnum.Car, new GeoCoordinate(53.4866, -2.2447));
            RouterPoint point2 = router.Resolve(VehicleEnum.Car, new GeoCoordinate(53.4732, -2.2540));

            // calculate route.
            var route = router.Calculate(VehicleEnum.Bicycle, point1, point2);

            route.SaveAsGpx(new FileInfo("route.gpx"));

            return null;
        }
    }
}
