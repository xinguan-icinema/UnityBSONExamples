using UnityEngine;
using System.Collections;

using System.Net;
using System.Net.Sockets;

namespace SimpleNetwork {
	
	
public class UDPSender : SenderInterface
{
	UdpClient client;
	
	public UDPSender(string ip, int port)
	{
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
