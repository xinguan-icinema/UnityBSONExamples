using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace SimpleNetwork {

public class UDPListener 
{
	Thread receiveThread;
	int listenPort;
	UdpClient client;
	Queue<byte[]> messageQueue = new Queue<byte[]>();
	Object queueLock = new Object();
	
	bool stopThread = false;
	
	public UDPListener(int listenPort)
	{
		this.listenPort = listenPort;
		
		this.receiveThread = new Thread(new ThreadStart(this.ListenThread));
		this.receiveThread.IsBackground = true;
		this.receiveThread.Start();
	}

	
	public void Stop() {
		this.client.Close();
		this.stopThread = true;
		this.receiveThread.Join();
	}
	
	void ListenThread()
	{
		this.client = new UdpClient(this.listenPort);
		while (!this.stopThread)
		{
			IPEndPoint anyIp = new IPEndPoint(IPAddress.Any, 0);
			byte[] data = client.Receive(ref anyIp);

			lock(queueLock) {
				this.messageQueue.Enqueue(data);
			}
		}
	}
	
	public byte[] PopMessage()
	{
		byte[] data = null;
		lock(queueLock) {
			try {
				data = this.messageQueue.Dequeue();
			} catch (System.InvalidOperationException e) {
				// Queue was empty
			}
		}
		return data;
	}
}
}

