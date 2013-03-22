using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ibi.Fe.Router.Controllers
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Web.Http;

    using GeoJSON.Net.Feature;
    using GeoJSON.Net.Geometry;

    using Ibi.Fe.Router.Code;

    using OsmSharp.Routing.Core;
    using OsmSharp.Tools.Math.Geo;

    [KnownType(typeof(Feature))]
    [KnownType(typeof(LineString))]
    public class RouteController : ApiController
    {
        public Feature Get()
        {
            var router = Engine.Instance;

            // resolve both points; find the closest routable road.
            RouterPoint point1 = router.Resolve(VehicleEnum.Car, new GeoCoordinate(53.4866, -2.2447));
            RouterPoint point2 = router.Resolve(VehicleEnum.Car, new GeoCoordinate(53.4732, -2.2540));

            // calculate route.
            var route = router.Calculate(VehicleEnum.Bicycle, point1, point2);


            var coordinates = route.Entries
                .Select(x => new GeographicPosition(x.Latitude, x.Longitude))
                .ToList();

            var lineString = new LineString(new List<IPosition>(coordinates));

            var feature = new Feature(
                lineString, 
                new Dictionary<string, object>
                    {
                        { "Name", "Test route result." }
                    });

            return feature;
        }
    }
}
