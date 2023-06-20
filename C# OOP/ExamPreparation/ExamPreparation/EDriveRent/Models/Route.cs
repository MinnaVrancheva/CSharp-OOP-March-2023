using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class Route : IRoute
    {
        private string startPoint;
        private string endPoint;
        private double lenght;
        private int routeId;
        private bool isLocked;

        public Route(string startPoint, string endPoint, double lenght, int routeId)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Length = lenght;
            RouteId = routeId;
            isLocked = false;
        }

        public string StartPoint
        {
            get { return startPoint; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.StartPointNull);
                }
                startPoint = value;
            }
        }

        public string EndPoint
        {
            get { return endPoint; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.EndPointNull);
                }
                endPoint = value;
            }
        }

        public double Length
        {
            get { return lenght; }
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(ExceptionMessages.RouteLengthLessThanOne);
                }
                lenght = value;
            }
        }

        public int RouteId { get { return routeId; } private set { routeId = value; } }

        public bool IsLocked { get { return isLocked; } private set { isLocked = value; } }

        public void LockRoute()
        {
            IsLocked = true;
        }
    }
}
