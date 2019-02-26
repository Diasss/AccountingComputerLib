using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Computers.Model
{
    public delegate void dSentInfo(Software software);
    public struct ServiceSoftware
    {
        private dException DException;
        private dSuccessful DSuccessful;
        private dSentInfo DSentInfo;
        public string DbName { get; set; }
        public void RegisterDException(dException DException)
        {
            this.DException += DException;
        }
        public void RegisterDSuccessful(dSuccessful DSuccessful)
        {
            this.DSuccessful += DSuccessful;
        }
        public void RegisterDSentInfo(dSentInfo DSentInfo)
        {
            this.DSentInfo += DSentInfo;
        }
        public void EditSoftware(Software software)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(this.DbName))
                    throw new Exception("Строка подключения не должна быть пустой!");

                using (var db = new LiteDatabase(this.DbName))
                {
                    var equipments = db.GetCollection<Software>("Software");
                    equipments.Update(software);
                }

                if (DSuccessful != null)
                {
                    DSuccessful.Invoke("Запись успешно изменена!");
                    if (software.Equipment != null && this.DSentInfo != null)
                    {
                        this.DSentInfo.Invoke(software);
                    }
                }
            }
            catch (Exception ex)
            {
                if (DException != null)
                    DException.Invoke(ex);
            }
        }
        public void DeleteSoftware(int softwareId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.DbName))
                    throw new Exception("Строка подключения не должна быть пустой!");

                using (var db = new LiteDatabase(this.DbName))
                {
                    var equipments = db.GetCollection<Software>("Software");
                    equipments.Delete(softwareId);
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
        public void AddSoftware(Software software)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.DbName))
                    throw new Exception("Строка подключения не должна быть пустой!");

                using (var db = new LiteDatabase(this.DbName))
                {
                    var equipments = db.GetCollection<Software>("Software");
                    equipments.Insert(software);
                }

                if (DSuccessful != null)
                {
                    DSuccessful.Invoke("Запись успешно добавлена!");
                    if(software.Equipment != null && this.DSentInfo != null)
                    {
                        this.DSentInfo.Invoke(software);
                    }
                }
            }
            catch (Exception ex)
            {
                if (DException != null)
                    DException.Invoke(ex);
            }
        }
    }

}
