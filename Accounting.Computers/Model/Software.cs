using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Computers.Model
{

    public enum Distribution { free, con_free, paid }

    public struct Software
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        private Distribution Distribution_;
        public Distribution Distribution
        {
            get { return Distribution_; }
            set
            {
                Distribution_ = value;
                switch (value)
                {
                    case Distribution.free:
                        this.ExpireDate = DateTime.MaxValue;
                        break;
                    case Distribution.con_free:
                        this.ExpireDate = DateTime.Now.AddDays(30);
                        break;
                    case Distribution.paid:
                        this.ExpireDate = DateTime.Now.AddYears(1);
                        break;
                    default:
                        break;
                }

            }
        }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; private set; }
        public Equipment? Equipment { get; set; }
    }

}