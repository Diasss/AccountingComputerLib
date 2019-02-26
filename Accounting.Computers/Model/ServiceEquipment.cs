using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Accounting.Computers.Model
{
    public delegate void dException(Exception ex);
    public delegate void dSuccessful(string message);

    public struct ServiceEquipment
    {
        private dException DException;
        private dSuccessful DSuccessful;
        public string DbName { get; set; }

        public void RegisterDException(dException DException)
        {
            this.DException += DException;
        }
        public void RegisterDSuccessful(dSuccessful DSuccessful)
        {
            this.DSuccessful += DSuccessful;
        }
        public void EditEquipment(Equipment equipment)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(this.DbName))
                    throw new Exception("Строка подключения не должна быть пустой!");

                using (var db = new LiteDatabase(this.DbName))
                {
                    var equipments = db.GetCollection<Equipment>("Equipment");
                    equipments.Update(equipment);
                }

                if (DSuccessful != null)
                    DSuccessful.Invoke("Запись успешно добавлена!");
            }
            catch (Exception ex)
            {
                if (DException != null)
                    DException.Invoke(ex);
            }
        }
        public void DeleteEquipment(int equipmentId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.DbName))
                    throw new Exception("Строка подключения не должна быть пустой!");

                using (var db = new LiteDatabase(this.DbName))
                {
                    var equipments = db.GetCollection<Equipment>("Equipment");
                    equipments.Delete(equipmentId);
                }

                if (DSuccessful != null)
                    DSuccessful.Invoke("Запись удалена!");
            }
            catch (Exception ex)
            {
                if (DException != null)
                    DException.Invoke(ex);
            }
        }
        public void AddEquipment(Equipment equipment)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.DbName))
                    throw new Exception("Строка подключения не должна быть пустой!");

                using (var db = new LiteDatabase(this.DbName))
                {
                    var equipments = db.GetCollection<Equipment>("Equipment");
                    equipments.Insert(equipment);
                }

                if (DSuccessful != null)
                    DSuccessful.Invoke("Запись успешно добавлена!");
            }
            catch (Exception ex)
            {
                if (DException != null)
                    DException.Invoke(ex);
            }
        }
        public List<Equipment> FindEquipmentByName(string Name)
        {
            List<Equipment> ListEquipment = null;

            try
            {
                if (string.IsNullOrEmpty(this.DbName))
                    throw new Exception("Строка подключения не должна быть пустой!");

                using (var db = new LiteDatabase(this.DbName))
                {
                    var equipments = db.GetCollection<Equipment>("Equipment");
                    ListEquipment = equipments.Find(f => f.Name == Name).ToList();
                }

                return ListEquipment;
            }
            catch (Exception exc)
            {
                if (DException != null)
                    DException.Invoke(exc);

                return ListEquipment;
            }

        }
    }
}