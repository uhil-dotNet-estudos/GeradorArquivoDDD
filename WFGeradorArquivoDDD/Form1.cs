using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeradorArquivoDDD.Dominio;

namespace WFGeradorArquivoDDD
{
    public partial class Form1 : Form
    {

        public List<String> ListClasses { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.txtCaminhoArquivo.Text = Path.GetDirectoryName(Application.ExecutablePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ListaClassesTXT.Text.Trim().ToString().Length > 0)
            {
                ListClasses = ListaClassesTXT.Text.Split(';').ToList();
                try
                {
                    progressBar1.Value = 0;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = (ListClasses.Count*6);
                    progressBar1.Step = 1;
                    this.GerarClasses();
                    MessageBox.Show("Classes Gerada Com Sucesso", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Digite Nome de classes","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
            //progressBar1.Increment(100);

        }

        private void GerarClasses()
        {
            var incress = 1;
            var max = (ListClasses.Count * 6);

         
            foreach (var item in ListClasses)
            {
                StatusLabel1.Text = String.Format("Gerando Classe I{0}{1} ...", item, this.SufixoRepository.Text);
                StatusLabel2.Text = String.Format("{0}%", (progressBar1.Value*100)/max);
                statusStrip1.Refresh();
                
                this.GerarIRepository(item);
              //  progressBar1.Increment(incress);
                progressBar1.PerformStep();
                Task.Delay(1000);



                StatusLabel1.Text = String.Format("Gerando Classe {0}{1} ...", item, this.SufixoRepository.Text);
                StatusLabel2.Text = String.Format("{0}%", (progressBar1.Value * 100) / max);
                statusStrip1.Refresh();
                this.GerarRepository(item);
                //progressBar1.Increment(incress);
                progressBar1.PerformStep();
                Task.Delay(1000);


                StatusLabel1.Text = String.Format("Gerando Classe I{0}{1} ...", item, this.SufixoApplicaion.Text);
                StatusLabel2.Text = String.Format("{0}%", (progressBar1.Value * 100) / max);
                statusStrip1.Refresh();
                this.GerarIApplication(item);
               // progressBar1.Increment(incress);
                progressBar1.PerformStep();
                Task.Delay(1000);


                StatusLabel1.Text = String.Format("Gerando Classe {0}{1} ...", item, this.SufixoApplicaion.Text);
                StatusLabel2.Text = String.Format("{0}%", (progressBar1.Value * 100) / max);
                statusStrip1.Refresh();
                this.GerarApplication(item);
                //progressBar1.Increment(incress);
                progressBar1.PerformStep();
                Task.Delay(1000);


                StatusLabel1.Text = String.Format("Gerando Classe I{0}{1} ...", item, this.SufixoService.Text);
                StatusLabel2.Text = String.Format("{0}%", (progressBar1.Value * 100) / max);
                statusStrip1.Refresh();
                this.GerarIService(item);
                //progressBar1.Increment(incress);
                progressBar1.PerformStep();
                Task.Delay(1000);


                StatusLabel1.Text = String.Format("Gerando Classe I{0}{1} ...", item, this.SufixoService.Text);
                StatusLabel2.Text = String.Format("{0}%", (progressBar1.Value * 100) / max);
                statusStrip1.Refresh();
                this.GerarService(item);
               // progressBar1.Increment(incress);
                progressBar1.PerformStep();
                Task.Delay(1000);


                StatusLabel2.Text = String.Format("{0}%", (progressBar1.Value * 100) / max);
                statusStrip1.Refresh();
            }
        }


        #region  GerarRepository

        private void GerarIRepository(String nomeClasse)
        {
            var conteudo = new StringBuilder();

            conteudo.AppendLine(String.Format("using {0};                              ",this.UsingEntites.Text));
            conteudo.AppendLine("                                                                     ");
            conteudo.AppendLine(String.Format("namespace {0}            ",this.UsingIRepository.Text));
            conteudo.AppendLine("{                                                                    ");
            conteudo.AppendLine(String.Format("    public interface I{0}{1} : I{1}{2}<{0}>   ", nomeClasse, this.SufixoRepository.Text,this.SufixoBase.Text));
            conteudo.AppendLine("    {                                                                ");
            conteudo.AppendLine("    }                                                                ");
            conteudo.AppendLine("}                                                                    ");

            GerarArquivo(String.Format(@"{0}\I{1}{2}.cs", this.CaminhoInterfaceRepositoryTxt.Text,nomeClasse,this.SufixoRepository.Text ), conteudo.ToString());
        }

        #endregion

        #region  GerarRepository

        private void GerarRepository(String nomeClasse)
        {
            var conteudo = new StringBuilder();


            conteudo.AppendLine(String.Format("using {0};                                          ",this.UsingEntites.Text));
            conteudo.AppendLine(String.Format("using {0};                           ",this.UsingIRepository.Text));
            conteudo.AppendLine("                                                                                 ");
            conteudo.AppendLine(String.Format("namespace {0}                               ",this.UsingRepository.Text));
            conteudo.AppendLine("{                                                                                ");
            conteudo.AppendLine(String.Format("   public class {0}{1} : {1}{2}<{0}>, I{0}{1}  ",nomeClasse, this.SufixoRepository.Text,this.SufixoBase.Text));
            conteudo.AppendLine("    {                                                                            ");
            conteudo.AppendLine("                                                                                 ");
            conteudo.AppendLine("    }                                                                            ");
            conteudo.AppendLine("}                                                                                ");


            GerarArquivo(String.Format(@"{0}\{1}{2}.cs", this.CaminhoRepositoryTxt.Text, nomeClasse, this.SufixoRepository.Text), conteudo.ToString());

        }

        #endregion

        #region  GerarIApplication

        private void GerarIApplication(String nomeClasse)
        {
            var conteudo = new StringBuilder();

            conteudo.AppendLine(String.Format("using {0};                                    ",this.UsingEntites.Text));
            conteudo.AppendLine("                                                                           ");
            conteudo.AppendLine(String.Format("namespace {0}                           ",this.UsingIApplication.Text));
            conteudo.AppendLine("{                                                                          ");
            conteudo.AppendLine(String.Format("    public interface I{0}{1} : I{1}{2}<{0}>         ",nomeClasse,this.SufixoApplicaion.Text,this.SufixoBase.Text));
            conteudo.AppendLine("    {                                                                      ");
            conteudo.AppendLine("    }                                                                      ");
            conteudo.AppendLine("}                                                                          ");

            GerarArquivo(String.Format(@"{0}\I{1}{2}.cs", this.CaminhoInterfaceApplicationtxt.Text, nomeClasse, this.SufixoApplicaion.Text), conteudo.ToString());

        }
        #endregion

        #region  GerarApplication
        private void GerarApplication(String nomeClasse)
        {
            var conteudo = new StringBuilder();
            var nomeVariavel = String.Format("{0}{1}", nomeClasse.Substring(0, 1).ToLower(), nomeClasse.Substring(1));
            conteudo.AppendLine(String.Format("using {0};                                            ",this.UsingIApplication.Text));
            conteudo.AppendLine(String.Format("using {0};                                                  ",this.UsingEntites.Text));
            conteudo.AppendLine(String.Format("using {0};                                       ",this.UsingIService.Text));
            conteudo.AppendLine("                                                                                         ");
            conteudo.AppendLine("                                                                                         ");
            conteudo.AppendLine(String.Format("namespace {0}                                                   ",this.UsingApplication.Text));
            conteudo.AppendLine("{                                                                                        ");
            conteudo.AppendLine(String.Format("    public class {0}{1} : {1}{2}<{0}>, I{0}{1}         ",nomeClasse,this.SufixoApplicaion.Text, this.SufixoBase.Text));
            conteudo.AppendLine("    {                                                                                    ");
            conteudo.AppendLine(String.Format("        private readonly I{0}{1} _{2}{1};                                ",nomeClasse,this.SufixoService.Text,nomeVariavel));
            conteudo.AppendLine("                                                                                         ");
            conteudo.AppendLine(String.Format("        public {0}{1}(I{0}{2} {3}{2})                         ",nomeClasse, this.SufixoApplicaion.Text,this.SufixoService.Text,nomeVariavel));
            conteudo.AppendLine(String.Format("            :base({0}{1})                                                        ", nomeVariavel, this.SufixoService.Text));
            conteudo.AppendLine("        {                                                                                ");
            conteudo.AppendLine(String.Format("            _{0}{1} = {0}{1};                                            ", nomeVariavel, this.SufixoService.Text));
            conteudo.AppendLine("        }                                                                                ");
            conteudo.AppendLine("    }                                                                                    ");
            conteudo.AppendLine("}                                                                                        ");

            GerarArquivo(String.Format(@"{0}\{1}{2}.cs", this.CaminhoApplicationTxt.Text, nomeClasse, this.SufixoApplicaion.Text), conteudo.ToString());

        }

        #endregion

        #region GerarIService
        private void GerarIService(String nomeClasse)
        {
            var conteudo = new StringBuilder();

            conteudo.AppendLine(String.Format("using {0};                             ",this.UsingEntites.Text));
            conteudo.AppendLine(("                                                                    "));
            conteudo.AppendLine(String.Format("namespace {0}               ",this.UsingIService.Text));
            conteudo.AppendLine(("{                                                                   "));
            conteudo.AppendLine(String.Format("    public interface I{0}{1} : I{1}{2}<{0}>        ",nomeClasse,this.SufixoService.Text,this.SufixoBase.Text));
            conteudo.AppendLine(("    {                                                               "));
            conteudo.AppendLine(("    }                                                               "));
            conteudo.AppendLine(("}                                                                   "));
            
            GerarArquivo(String.Format(@"{0}\I{1}{2}.cs", this.CaminhoIterfaceServiceTxt.Text, nomeClasse, this.SufixoService.Text), conteudo.ToString());

        }
        #endregion

        #region GerarService
        private void GerarService(String nomeClasse)
        {
            var conteudo = new StringBuilder();
            var nomeVariavel = String.Format("{0}{1}", nomeClasse.Substring(0, 1).ToLower(), nomeClasse.Substring(1));
            conteudo.AppendLine(String.Format("using {0};                                            ", this.UsingEntites.Text));
            conteudo.AppendLine(String.Format("using {0};                             ",this.UsingIRepository.Text));
            conteudo.AppendLine(String.Format("using {0};                                 ",this.UsingIService.Text));
            conteudo.AppendLine("                                                                                   ");
            conteudo.AppendLine(String.Format("namespace {0}                                         ",this.UsingService.Text));
            conteudo.AppendLine("{                                                                                  ");
            conteudo.AppendLine(String.Format("    public class {0}{1} : {1}{2}<{0}>, I{0}{1}            ",nomeClasse,this.SufixoService.Text,this.SufixoBase.Text));
            conteudo.AppendLine("    {                                                                              ");             
            conteudo.AppendLine(String.Format("        private readonly I{0}{1} _{2}{1};                    ",nomeClasse,this.SufixoRepository.Text, nomeVariavel));
            conteudo.AppendLine("                                                                                   ");
            conteudo.AppendLine(String.Format("        public {0}{3}(I{0}{1} {2}{1})                ",nomeClasse,this.SufixoRepository.Text,nomeVariavel,this.SufixoService.Text));
            conteudo.AppendLine(String.Format("            : base({0}{1})                                              ",nomeVariavel,this.SufixoRepository.Text));
            conteudo.AppendLine("        {                                                                          ");
            conteudo.AppendLine(String.Format("            _{0}{1} = {0}{1};                                ",nomeVariavel,this.SufixoRepository.Text));
            conteudo.AppendLine("        }                                                                          ");
            conteudo.AppendLine("                                                                                   ");
            conteudo.AppendLine("    }                                                                              ");
            conteudo.AppendLine("}                                                                                  ");

            GerarArquivo(String.Format(@"{0}\{1}{2}.cs", this.CaminhoServiceTxt.Text, nomeClasse, this.SufixoService.Text), conteudo.ToString());

        }
        #endregion

        private static void GerarArquivo(string fileName, string Conteudo)
        {

            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }

            StreamWriter l_SrLog = new StreamWriter(fileName,false, Encoding.UTF8);
            l_SrLog.WriteLine(Conteudo);
            l_SrLog.Flush();
            l_SrLog.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"Arquivo Xml|*.xml";
            openFileDialog1.FileName = txtCaminhoArquivo.Text;
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(txtCaminhoArquivo.Text);

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtCaminhoArquivo.Text = openFileDialog1.FileName;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SalvarArquivo();
        }

        private void SalvarArquivo()
        {
            var arquivoSave =  CarregarArquivo();

            var conteudoXml = Global.Instance.Serializar(arquivoSave);

            if (!txtCaminhoArquivo.Text.ToLower().EndsWith(".xml")) txtCaminhoArquivo.Text += @"\Configuracao.xml";

            GerarArquivo(txtCaminhoArquivo.Text, conteudoXml);

            MessageBox.Show("Arquivo salvo com sucesso!");

        }

        private Configuracao CarregarArquivo()
        {
            var ArquivoSave = new Configuracao();

            ArquivoSave.DirInterfaceApplication = this.CaminhoInterfaceApplicationtxt.Text;
            ArquivoSave.DirInterfaceRepository = this.CaminhoInterfaceRepositoryTxt.Text;
            ArquivoSave.DirInterfaceService = this.CaminhoIterfaceServiceTxt.Text;
            ArquivoSave.DirRepository = this.CaminhoRepositoryTxt.Text;
            ArquivoSave.DirApplication = this.CaminhoApplicationTxt.Text;
            ArquivoSave.DirService = this.CaminhoServiceTxt.Text;

            ArquivoSave.SufixoApplication = this.SufixoApplicaion.Text;
            ArquivoSave.SufixoRepository = this.SufixoRepository.Text;
            ArquivoSave.SufixoService = this.SufixoService.Text;
            ArquivoSave.SufixoBase = this.SufixoBase.Text;

            ArquivoSave.UsingApplication = this.UsingApplication.Text;
            ArquivoSave.UsingEntities = this.UsingEntites.Text;
            ArquivoSave.UsingInterfaceApplication = this.UsingIApplication.Text;
            ArquivoSave.UsingInterfaceRepository = this.UsingIRepository.Text;
            ArquivoSave.UsingInterfaceService = this.UsingIService.Text;
            ArquivoSave.UsingRepository = this.UsingRepository.Text;
            ArquivoSave.UsingService = this.UsingService.Text;

            ArquivoSave.Entidades = this.ListaClassesTXT.Text;

            return ArquivoSave;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CarregarArquivoXml();
        }

        private void CarregarArquivoXml()
        {
            var ArquivoSave = GetArquivoXML(txtCaminhoArquivo.Text);

            this.CaminhoInterfaceApplicationtxt.Text = ArquivoSave.DirInterfaceApplication;
            this.CaminhoInterfaceRepositoryTxt.Text = ArquivoSave.DirInterfaceRepository;
            this.CaminhoIterfaceServiceTxt.Text = ArquivoSave.DirInterfaceService;
            this.CaminhoRepositoryTxt.Text = ArquivoSave.DirRepository;
            this.CaminhoApplicationTxt.Text = ArquivoSave.DirApplication;
            this.CaminhoServiceTxt.Text = ArquivoSave.DirService;

            this.SufixoApplicaion.Text = ArquivoSave.SufixoApplication;
            this.SufixoRepository.Text = ArquivoSave.SufixoRepository;
            this.SufixoService.Text = ArquivoSave.SufixoService;
            this.SufixoBase.Text = ArquivoSave.SufixoBase;

            this.UsingApplication.Text = ArquivoSave.UsingApplication;
            this.UsingEntites.Text = ArquivoSave.UsingEntities;
            this.UsingIApplication.Text = ArquivoSave.UsingInterfaceApplication;
            this.UsingIRepository.Text = ArquivoSave.UsingInterfaceRepository;
            this.UsingIService.Text = ArquivoSave.UsingInterfaceService;
            this.UsingRepository.Text = ArquivoSave.UsingRepository;
            this.UsingService.Text = ArquivoSave.UsingService;

            this.ListaClassesTXT.Text = ArquivoSave.Entidades;
        }

        private Configuracao GetArquivoXML(string fileName)
        {

            StreamReader sr = new StreamReader(fileName);
            var conteudoxml = sr.ReadToEnd();

            var Config = Global.Instance.FromXml<Configuracao>(conteudoxml);

            sr.Close();

            return Config;
        }
    }
}
