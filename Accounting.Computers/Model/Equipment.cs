using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Computers.Model
{
    public enum TypeEquipment {printer, monitor, photo, notebook }

    public struct Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public TypeEquipment TypeEquipment { get; set; }
    }
}
