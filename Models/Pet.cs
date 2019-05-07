using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace AnimalShelter.Models
{
  public class Pet
  {
    private string _name;
    private string _sex;
    private string _type;
    private int _id;



    public Pet  (string name, string sex, string type, int id=0) // constructor
    {
      _name = name;
      _sex = sex;
      _type = type;
      _id = id;
    }


    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }


    public string GetSex()
    {
      return _sex;
    }

    public void SetSex(string newSex)
    {
      _sex= newSex;
    }

    public string GetType()
    {
      return _type;
    }

    public void SetType(string newType)
    {
      _type = newType;
    }






    public static List<Pet> GetAll()
    {
      List<Pet> allPets = new List<Pet>{ };

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM animal;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while (rdr.Read())
      {
        int petId = rdr.GetInt32(0);
        string petName = rdr.GetString(1);
        string petSex = rdr.GetString(2);
        string petType = rdr.GetString(3);

        Pet newPet = new Pet (petName ,petSex, petType);
        allPets.Add(newPet);
      }

      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }

      return allPets;

    }


    //
    // public override bool Equals(System.Object otherItem)
    // {
    //   if (!(otherItem is Item))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Item newItem = (Item) otherItem;
    //
    //     // bool idEquality = (this.GetId() == newItem.GetId());
    //     bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
    //     return (descriptionEquality);
    //   }
    // }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"INSERT INTO items (name, sex, type) VALUES (@PetName, @PetSex, @PetType,);";
      MySqlParameter name = new MySqlParameter();
      MySqlParameter sex = new MySqlParameter();
      MySqlParameter type = new MySqlParameter();
      name.ParameterName = "@PetName";
      sex.ParameterName = "@PetSex";
      type.ParameterName = "@PetType";
      name.Value = this._name;
      sex.Value = this._sex;
      type.Value = this._type;

      cmd.Parameters.Add(name);
      cmd.Parameters.Add(sex);
      cmd.Parameters.Add(type);
      cmd.ExecuteNonQuery();

      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    public int GetId()
    {
      return _id;
    }
    //
    public static Pet Find (int id)
    {

    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM animal WHERE id = @thisId;";
    MySqlParameter thisId = new MySqlParameter();
    thisId.ParameterName = "@thisId";
    thisId.Value = id;
    cmd.Parameters.Add(thisId);
    var rdr = cmd.ExecuteReader() as MySqlDataReader;
    int petId=0;
    string petName ="";
    string petSex ="";
    string petType ="";


    while(rdr.Read())
    {
      petId = rdr.GetInt32(0);
      petName = rdr.GetString(1);
      petSex = rdr.GetString(2);
      petType= rdr.GetString(3);
    }
    Pet foundPet = new Pet (petName,petSex, petType);

    conn.Close();
    if(conn != null)
    {
      conn.Dispose();
    }
    return foundPet;
    }





  }
}
