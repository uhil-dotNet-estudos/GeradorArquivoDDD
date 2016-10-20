using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace GeradorArquivoDDD.Dominio
{
    public class Configuracao
    {
        public Configuracao()
        {
            
        }

        #region Diretorio

        public string DirInterfaceRepository { get; set; }

        public string DirRepository { get; set; }

        public string DirInterfaceService { get; set; }

        public string DirService { get; set; }

        public string DirInterfaceApplication { get; set; }

        public string DirApplication { get; set; }

        #endregion

        #region Using/NameSpaces

        public string UsingInterfaceRepository { get; set; }

        public string UsingRepository { get; set; }

        public string UsingInterfaceService { get; set; }

        public string UsingService { get; set; }

        public string UsingInterfaceApplication { get; set; }

        public string UsingApplication { get; set; }

        public string UsingEntities { get; set; }

        #endregion

        #region Sufixos

        public string SufixoRepository { get; set; }

        public string SufixoService { get; set; }

        public string SufixoApplication { get; set; }

        public string SufixoBase { get; set; }

        #endregion

        public String Entidades { get; set; }

    }
}
