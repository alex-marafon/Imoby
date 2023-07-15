using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Imoby.Entities.Entitie;
public abstract class AbstractDatabaseCollection
  {
    private static Regex validateHexStringRegex = new Regex("^[0-9a-fA-F]+$");
    private string _id = null;

    [BsonRepresentation(BsonType.ObjectId)]
    public string Id
    {
      get
      {
        return this._id;
      }
      set
      {
        if (null != value)
        {
          string idCandidate = validateHexStringRegex.Match(value).ToString();

          if (idCandidate.Length == 24)
          {
            this._id = value;
          }
        }
      }
    }
  }
