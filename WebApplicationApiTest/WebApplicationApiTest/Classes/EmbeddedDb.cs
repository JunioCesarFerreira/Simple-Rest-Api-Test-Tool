using System;
using LiteDB;
using WebApplicationApiTest.Models;

namespace WebApplicationApiTest
{
    public class EmbeddedDb
    {
        private readonly string connectionString;
        private readonly string collectionName = "keyValues";

        public EmbeddedDb()
        {
            connectionString = "Filename=" + Settings.DbFile + ";Password=" + Settings.Pwd;
        }

        public EmbeddedDb(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool Insert(KeyValueModel keyValue)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var col = db.GetCollection<KeyValueModel>(collectionName);
                    col.Insert(keyValue);
                }
                return true;
            }
            catch (Exception E)
            {
                ConLog.Erro(E.Message);
                return false;
            }
        }

        public bool Update(KeyValueModel keyValue)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var col = db.GetCollection<KeyValueModel>(collectionName);
                    col.Update(keyValue);
                }
                return true;
            }
            catch (Exception E)
            {
                ConLog.Erro(E.Message);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var col = db.GetCollection<KeyValueModel>(collectionName);
                    col.Delete(id);
                }
                return true;
            }
            catch (Exception E)
            {
                ConLog.Erro(E.Message);
                return false;
            }
        }

        public KeyValueModel Get(int key)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var col = db.GetCollection<KeyValueModel>(collectionName);
                    return col.FindById(key);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
