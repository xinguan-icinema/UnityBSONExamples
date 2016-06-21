using UnityEngine;
using System.Collections;

using System.Net;
using System.Net.Sockets;

namespace SimpleNetwork {
	
	
public class UDPSender : SenderInterface
{
	string ip;
	int port;
	UdpClient client;
	
	public UDPSender(string ip, int port)
	{
		this.ip = ip;
		this.port = port;
		this.client = new UdpClient(ip, port);
	}
	
	public void SendBytes(byte[] data)
	{
		try {
			this.client.Send(data, data.Length);
		} catch (System.Exception e) {
			Debug.LogErrorFormat("SendBytes() error: {0}", e.ToString());
		}
	}
}

}
