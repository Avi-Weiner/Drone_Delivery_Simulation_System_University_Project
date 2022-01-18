using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace DAL.DalXML
{
    public class XMLTools
    {
        #region SaveLoadWithXMLSerializer
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
            return null;
        }
        public static void SaveIntToXMLSerializer(int nextId, string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(nextId.GetType());
                x.Serialize(file, nextId);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static int LoadIntFromXML(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    int Id = 0;
                    XmlSerializer x = new XmlSerializer(Id.GetType());
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    Id = (int)x.Deserialize(file);
                    file.Close();
                    return Id;
                }
                else
                    return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
            return -1;
        }
        #endregion

        public XElement LoadData(string filePath)
        {
            try
            {
                return XElement.Load(filePath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
                return null;
            }
        }
    }
}
