using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using RqRsModel;
namespace Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bf = new BinaryFormatter();
            using (var tcpClient = new TcpClient())
            {
                tcpClient.Connect("127.0.0.1", 9001);
                using (var networkStream = tcpClient.GetStream())
                {
                    var request = new Request()
                    {
                        Title = "SET",
                        RqEWord = tbEWord.Text,
                        RqUWord = tbUWord.Text
                    };
                    bf.Serialize(networkStream, request);
                    var response = (Response)bf.Deserialize(networkStream);
                    networkStream.Flush();
                }
            }
        }

        private void dtnDelete_Click(object sender, EventArgs e)
        {
            var bf = new BinaryFormatter();
            using (var tcpClient = new TcpClient())
            {
                tcpClient.Connect("127.0.0.1", 9001);
                using (var networkStrem = tcpClient.GetStream())
                {
                    var request = new Request()
                    {
                        Title = "DELETE",
                        RqId = Convert.ToInt32(tbDelete.Text)
                    };
                    bf.Serialize(networkStrem, request);
                    var response = (Response)bf.Deserialize(networkStrem);
                    networkStrem.Flush();
                }
            }
        }
        public void GetWords()
        {
            lvUA.Items.Clear();
            lvUK.Items.Clear();
            var bf = new BinaryFormatter();
            Response response;
            using (var tcpClient = new TcpClient())
            {
                tcpClient.Connect("127.0.0.1", 9001);
                using (var networkStream = tcpClient.GetStream())
                {

                    var request = new Request()
                    {
                        Title = "GET"
                    };
                    bf.Serialize(networkStream, request);
                    response = (Response)bf.Deserialize(networkStream);
                    networkStream.Flush();
                }
            }
            for (int i = 0; i < response.RqEWords.Count; i++)
            {
                lvUK.Items.Add(new ListViewItem(response.RqEWords[i]));
                lvUA.Items.Add(new ListViewItem(response.RqUWords[i]));
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            GetWords();
        }

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            GetWords();
        }
    }
}
