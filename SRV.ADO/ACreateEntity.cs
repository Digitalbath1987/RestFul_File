using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SRV.ADO
{
    public class ACreateEntity
    {


        /// <summary>
        /// CREACIÓN DE ENTIDADES A LISTAS POR MEDIO DE UN READER
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static List<T> CreaEntidades<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {

                    try
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch
                    {
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// CREACIÓN DE ENTIDADES A LISTAS POR MEDIO DE UN DATATABLE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable table)
        {
            try
            {
                List<T> list = new List<T>();
                T obj = default(T);

                foreach (var row in table.AsEnumerable())
                {

                    obj = Activator.CreateInstance<T>();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
