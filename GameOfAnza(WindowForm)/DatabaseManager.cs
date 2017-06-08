using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfAnza_WindowForm_
{
	/*
	 * 싱글톤으로 만들어진 DatabaseManager.
	 */
	class DatabaseManager
	{
		private static DatabaseManager _instance;
		public static DatabaseManager GetInstance()
		{
			if (_instance == null)
			{
				_instance = new DatabaseManager();
			}
			return _instance;
		}

		public class InOutData
		{
			public ObjectId _id { get; set; }
			public string USE_DT { get; set; }
			public string BUS_ROUTE_NO { get; set; }
			public string BUS_ROUTE_NM { get; set; }
			public int STND_BSST_ID { get; set; }
			public int BSST_ARS_NO { get; set; }
			public string BUS_STA_NM { get; set; }
			public int RIDE_PASGR_NUM { get; set; }
			public int ALIGHT_PASGR_NUM { get; set; }
		}


		protected DatabaseManager()
		{
			var credential = MongoCredential.CreateCredential("bus", "next", "dlrmsdnjs93");

			var setting = new MongoClientSettings
			{
				Credentials = new[] { credential },
				Server = new MongoServerAddress("211.249.60.64", 27017)
			};

			var mongoClient = new MongoClient(setting);
			var mongoDatabase = mongoClient.GetDatabase("bus");
			var collection = mongoDatabase.GetCollection<InOutData>("bus_in_out_test");

			var list = collection.Find(x => x.STND_BSST_ID == 105900027).ToList();

			foreach (var st in list)
			{
				Debug.Write(st.USE_DT);
			}


			//var builder = Builders<BsonDocument>.Filter;
			//var filter = builder.Regex() & 
			//var collections = mongoDatabase.GetCollection<BsonDocument>("bus_in_out_test");

			//var connectionStr = "mongodb://next:dlrmsdnjs93@211.249.60.64:27017";
			//MongoClient client = new MongoClient(connectionStr);
			//MongoServer server = client.GetServer();
			//MongoDatabase database = server.GetDatabase("bus");

			//MongoCollection<InOutData> collection = database.GetCollection<InOutData>("bus_in_out_test");

			//IMongoQuery query = Query.EQ("BUS_ROUTE_NO", "동대문01");
			//var results = collection.Find(query).SingleOrDefault();
		}


	}
}
