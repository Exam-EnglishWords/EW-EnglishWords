using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters.Binary;
using RqRsModel;

namespace Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var tcpListener = TcpListener.Create(9001);
            tcpListener.Start();
            var db = new DbInstance();
            Console.WriteLine("Server started");
            while(true)
            {
                using(var acceptor = await tcpListener.AcceptTcpClientAsync())
                {
                    Console.WriteLine("Next Client conected");
                    using (var networkStream = acceptor.GetStream())
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        var request = (Request)bf.Deserialize(networkStream);
                        var response = new Response();
                        if (request.Title == "SET")
                        {
                            db.SetWords(request.RqEWord, request.RqUWord);
                        }
                        else if (request.Title == "DELETE")
                        {
                            db.DeleteWords(request.RqId);
                        }
                        else if (request.Title == "GET")
                        {
                            foreach (var w in db.GetEWords())
                            {
                                response.RqEWords.Add($"ID {w.Id}: {w.EWord}");
                            }
                            foreach (var w in db.GetUWords())
                            {
                                response.RqUWords.Add($"ID {w.Id}: {w.UWord}");
                            }
                        }
                        else if (request.Title == "CHEK_DETAILS")
                        {
                            response.Chek = db.CheckAndRegUser(request.RqLogin, request.RqPassword);
                        }
                        else if (request.Title == "LOGIN")
                        {
                            response.CheckLogOrPass = db.CheckLogAndPass(request.RqLogin, request.RqPassword);
                        }
                        else if (request.Title == "START_LESSON")
                        {
                            var res = db.GetFiveWordsForLesson(request.RqLogin);
                            response.RqEWords = res.Item1;
                            response.RqUWords = res.Item2;
                        }
                        else if (request.Title == "CHECK_ANSWEAR")
                        {
                            var res = db.CheckAnsw(request.RqEWord, request.RqUWord);
                            response.Chek = res;
                        }
                        else if(request.Title == "SET_PROGRESS")
                        {
                            db.SetProgress(request.RqEWords, request.RqLogin);
                        }
                        bf.Serialize(networkStream, response);
                        networkStream.Flush();
                    }
                    acceptor.Dispose();
                }
            }
        }
    }
}
