using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Windows.Forms;

namespace GameOfAnza_WindowForm_
{
	public class HttpNetwork
	{
		private static HttpNetwork _instance;
		public static HttpNetwork GetInstance()
		{
			if (_instance == null)
			{
				_instance = new HttpNetwork();
			}

			return _instance;
		}

		public enum APICODE
		{
			GET_BUS_ROUTE_LIST,
			GET_ROUTE_INFO,
			GET_STATIONS_BY_ROUTE_LIST,
			GET_STATION_BY_NAME,
			APICODE_NUM
		}

		private struct APIData
		{
			public APICODE apiCode;
			public string apiUrl;
			public string apiParam;
			public string apiServiceKey;

			public string GetRequestLine()
			{
				if (apiUrl == null || apiServiceKey == null)
				{
					return null;
				}

				string requestUrl = apiUrl + "?serviceKey=" + apiServiceKey;
				
				if (apiParam != null)
				{
					requestUrl = requestUrl + "&" + apiParam + "=";
				}

				return requestUrl;
			}
		}

		private string filePath = "C:/Users/NEXT/Desktop/Project/GameOfAnza(WindowForm)/GameOfAnza(WindowForm)/Resources/KeyValues.xml";
		private APIData[] apiData = new APIData[(int)APICODE.APICODE_NUM];

		protected HttpNetwork()
		{
			try
			{
				XmlDocument keyXml = new XmlDocument();
				keyXml.Load(filePath);

				for (int i = 0; i < (int)APICODE.APICODE_NUM; ++i)
				{
					XmlNodeList xmlNodeList = keyXml.SelectNodes("APIKeys/api" + i.ToString());

					foreach (XmlNode xn in xmlNodeList)
					{
						apiData[i].apiCode = (APICODE)i;
						apiData[i].apiUrl = xn["Url"].InnerText;
						apiData[i].apiParam = xn["Param"].InnerText;
						apiData[i].apiServiceKey = xn["ServiceKey"].InnerText;
					}
				}

			}
			catch (ArgumentException ex)
			{
				MessageBox.Show("XML 리딩 문제 발생 \r\n" + ex);
			}
		}

		private string HttpRequest(APICODE code, string param, bool paramEncodeNeeded)
		{
			string requestLine = apiData[(int)code].GetRequestLine();
			if (param != null)
			{
				string addParam;
				if (paramEncodeNeeded)
				{
					addParam = HttpUtility.UrlEncode(param);
				}
				else
				{
					addParam = param;
				}
				requestLine = requestLine + addParam;
			}

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestLine);
			request.Method = "GET";

			try
			{
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					Encoding encode;
					if (response.CharacterSet.ToLower() == "utf-8") { encode = Encoding.UTF8; }
					else { encode = Encoding.Default; }

					Stream stReadData = response.GetResponseStream();
					StreamReader srReadData = new StreamReader(stReadData, encode);

					string strResult = srReadData.ReadToEnd();
					return strResult;
				}
			}
			catch
			{
				return null;
			}
		}
	}
}
