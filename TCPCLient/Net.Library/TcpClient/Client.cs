﻿using System;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace SomeProject.Library.Client
{
    public class Client
    {
        public TcpClient tcpClient;
        /// <summary>
        ///  Получение сообщени от сервера
        /// </summary>
        /// <param name="stream">Поток данных для доступа по сети</param>
        /// <returns>Результат выполнения операции</returns>
        public OperationResult ReceiveMessageFromServer(NetworkStream stream)
        {
            try
            {
                StringBuilder rcvdMessage = new StringBuilder();
                byte[] data = new byte[256];

                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    rcvdMessage.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    //Console.WriteLine(recievedMessage);
                }
                while (stream.DataAvailable);
                stream.Close();
                tcpClient.Close();
                return new OperationResult(Result.OK, rcvdMessage.ToString());
            }
            catch (Exception e)
            {
                return new OperationResult(Result.Fail, e.ToString());
            }
        }
        /// <summary>
        /// отправляем сообщение на сервер
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public OperationResult SendMessageToServer(string message)
        {
            try
            {
                using (tcpClient = new TcpClient("127.0.0.1", 8080))
                {
                    using (NetworkStream stream = tcpClient.GetStream())
                    {
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        return ReceiveMessageFromServer(stream);
                    }
                }
            }
            catch (Exception e)
            {
                return new OperationResult(Result.Fail, e.Message);
            }
        }
        /// <summary>
        /// отправления файла на сервер
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public OperationResult SendFileToServer(string fileName)
        {
            try
            {
                using (tcpClient = new TcpClient("127.0.0.1", 8080))
                {
                    using (NetworkStream stream = tcpClient.GetStream())
                    {
                        byte[] fname = System.Text.Encoding.UTF8.GetBytes("@file: " + fileName.Split('.')[fileName.Split('.').Length - 1] + "@");
                        stream.Write(fname, 0, fname.Length);
                        using (FileStream fstream = new FileStream(fileName, FileMode.Open))
                        {
                            byte[] buffer = new byte[4096];
                            int len = 0;
                            do
                            {
                                len = fstream.Read(buffer, 0, buffer.Length);
                                stream.Write(buffer, 0, len);
                            } while (len != 0);
                        }
                        return ReceiveMessageFromServer(stream);
                    }
                }
            } catch (Exception e)
            {
                return new OperationResult(Result.Fail, e.Message);
            }
        }
    }
}
