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

namespace WFGeradorArquivoDDD
{
    public partial class Form1 : Form
    {

        public List<String> ListClasses { get; set; }

        public Form1()
        {
            InitializeComponent();
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

            GerarArquivo(String.Format(@"{0}\I{1}{2}.cs", CaminhoInterfaceRepositoryTxt.Text,nomeClasse,SufixoRepository.Text ), conteudo.ToString());
        }

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
            
            GerarArquivo(String.Format(@"{0}\I{1}{2}.cs", this.CaminhoInterfaceRepositoryTxt.Text, nomeClasse, this.SufixoService.Text), conteudo.ToString());

        }

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

       
    }
}
