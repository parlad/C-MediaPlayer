using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Data.SqlClient;
using System.IO;

namespace text_to_speech
{
    public partial class Form1 : Form
    {
        //form reference

        SpeechSynthesizer ss;
        public Form1()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection("Data Source=NEUPANE; Initial Catalog=femaleVoice;User ID=sa;Password=neupane");
            SqlDataAdapter ad = new SqlDataAdapter("SELECT * id , char form tblCharVoice" , con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            comboBox1.DataSource = dt;


        }
                

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ss = new SpeechSynthesizer();
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;
            connetionString = "Data Source=NEUPANE; Initial Catalog=femaleVoice;User ID=sa;Password=neupane";
            sql = "Select * from tblCharVoice";
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection); dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    dataReader.GetValue(0);
                    //MessageBox.Show(dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2));
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ", ex.ToString());
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //read button click
            ss.Rate = trackBarSpeed.Value; //sets the spped
            ss.Volume = trackBarSpeed.Value; //sets the volumn
            ss.SpeakAsync(txtMessage.Text);

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            // for the pause button click
            ss.Pause();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            // for continue button
            ss.Resume();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            // for record button
            SpeechSynthesizer ss = new SpeechSynthesizer();
            ss.Rate = trackBarSpeed.Value;
            ss.Volume = trackBarVolumn.Value;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "wave File| *.wav";
            sfd.ShowDialog();
            ss.SetOutputToWaveFile(sfd.FileName);
            ss.Speak(txtMessage.Text);
            ss.SetOutputToDefaultAudioDevice();
            MessageBox.Show("Recording Complited...............", "T2S");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //close button
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtMessage.Text = openFileDialog1.FileName;
            }*/

            OpenFileDialog Ofg = new OpenFileDialog();

            try
            {
                Ofg.CheckFileExists = true;
                Ofg.CheckPathExists = true;
                Ofg.DefaultExt = "txt";
                Ofg.DereferenceLinks = true;
                Ofg.Filter = "Text files (*.txt)|*.txt|" +
                    "RTF files (*.rtf)|*.rtf|" +
                                  " + Works 6 and 7(*.wps) | *.wps | " +
                                  "Windows Write (*.wri)|*.wri|" +
                    "WordPerfect document (*.wpd)|*.wpd";
                Ofg.Multiselect = false;
                Ofg.RestoreDirectory = true;
                Ofg.ShowHelp = true;
                Ofg.ShowReadOnly = false;
                Ofg.Title = "Select a file ";
                openFileDialog1.ValidateNames = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(Ofg.OpenFile());
                    txtMessage.Text = sr.ReadToEnd();
                }
            }
            catch
            {
                MessageBox.Show("can not open the file", "Text two speech");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "NotSet":
                    Console.WriteLine( "hello" );
                    break;
                case "Male":
                    ss.SelectVoiceByHints(VoiceGender.Male);
                    break;
                case "Female":
                    ss.SelectVoiceByHints(VoiceGender.Female);
                    break;
                case "Neturl":
                    ss.SelectVoiceByHints(VoiceGender.Neutral);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ss.Volume = trackBarSpeed.Value;

            //ss.SelectVoiceByHints(VoiceGender.Male);
            //ss.SelectVoiceByHints(VoiceGender.Female);

            //ss.Rate = trackBarVolumn.Value;
            //ss.SpeakAsync(txtMessage.Text);
            //axWindowsMediaPlayer1.openPlayer(txtMessage.Text);
            //axWindowsMediaPlayer1.NewStream(txtMessage.Text);
            //axWindowsMediaPlayer1.URL = txtMessage.Text;
            

           

                ss.SelectVoiceByHints(VoiceGender.Male);
                ss.SelectVoiceByHints(VoiceGender.Female);

                axWindowsMediaPlayer1.Ctlcontrols.play();

            

             

        }
    
           
        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           // comboBox1.SelectedIndex = comboBox1.FindStringExact("Sunday");
        }
    }
}
