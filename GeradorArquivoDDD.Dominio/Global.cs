using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeradorArquivoDDD.Dominio
{
    public class Global
    {
        #region Singleton

        //Variaveis Privadas
        private static volatile Global i_instance = null;

        /// <summary>
        /// Singleton
        /// </summary>
        public static Global Instance
        {
            get
            {
                if (i_instance == null)
                {
                    lock (typeof(Global))
                    {
                        if (i_instance == null)
                        {
                            i_instance = new Global();
                        }
                    }
                }
                return i_instance;
            }
        }

        //Inicialização Privada
        private Global()
        {

        }

        #endregion


        /// <summary>
        /// Gera String Xml de um Objeto
        /// </summary>
        /// <param name="a_Objeto"></param>
        /// <returns></returns>
        public String Serializar(object a_Objeto)
        {
            StringWriter l_stringWriter = new StringWriter();
            XmlSerializer l_xmlSerializer = new XmlSerializer(a_Objeto.GetType());

            l_xmlSerializer.Serialize(l_stringWriter, a_Objeto);

            return l_stringWriter.ToString();
        }

        public T FromXml<T>(String xml)
        {
            T returnedXmlClass = default(T);

            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    try
                    {
                        returnedXmlClass =
                            (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException)
                    {
                        // String passed is not XML, simply return defaultXmlClass
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return returnedXmlClass;
        }
    }
}
