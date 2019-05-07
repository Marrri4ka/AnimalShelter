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
    private DateTime _date;
    private string _breed;



    public Pet  (string name, string sex, string type, DateTime date, string breed,int id=0) // constructor
    {
      _name = name;
      _sex = sex;
      _type = type;
      _id = id;
      _date = date;
      _breed = breed;
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

    public DateTime GetDate()
    {
      return _date;
    }

    public void SetDate(DateTime newDate)
    {
      _date = newDate;
    }

    public string GetBreed()
    {
      return _breed;
    }

    public void SetBreed(string newBreed)
    {
      _breed = newBreed;
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
        DateTime petDate = rdr.GetDateTime(4);
        string petBreed = rdr.GetString(5);

        Pet newPet = new Pet (petName ,petSex, petType, petDate, petBreed);
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

      cmd.CommandText = @"INSERT INTO animal (name, sex, type, date, breed) VALUES (@PetName, @PetSex, @PetType, @PetDate, @PetBreed);";
      MySqlParameter name = new MySqlParameter();
      MySqlParameter sex = new MySqlParameter();
      MySqlParameter type = new MySqlParameter();
      MySqlParameter date = new MySqlParameter();
      MySqlParameter breed = new MySqlParameter();
      name.ParameterName = "@PetName";
      sex.ParameterName = "@PetSex";
      type.ParameterName = "@PetType";
      date.ParameterName = "@PetDate";
      breed.ParameterName = "@PetBreed";
      name.Value = this._name;
      sex.Value = this._sex;
      type.Value = this._type;
      date.Value = this._date;
      breed.Value = this._breed;

      cmd.Parameters.Add(name);
      cmd.Parameters.Add(sex);
      cmd.Parameters.Add(type);
      cmd.Parameters.Add(date);
      cmd.Parameters.Add(breed);
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
    DateTime petDate = new DateTime();
    string petBreed = "";


    while(rdr.Read())
    {
      petId = rdr.GetInt32(0);
      petName = rdr.GetString(1);
      petSex = rdr.GetString(2);
      petType= rdr.GetString(3);
      petDate = rdr.GetDateTime(4);
      petBreed = rdr.GetString(5);

    }
    Pet foundPet = new Pet (petName,petSex, petType,  petDate,petBreed );

    conn.Close();
    if(conn != null)
    {
      conn.Dispose();
    }
    return foundPet;
    }


    public static List<Pet> SortAscending()
        {
          List<Pet> allPetsAscending = new List<Pet>{};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM animal ORDER by date ASC;";
          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

          while (rdr.Read())
          {
            int petId = rdr.GetInt32(0);
            string petName = rdr.GetString(1);
            string petSex = rdr.GetString(2);
            string petType = rdr.GetString(3);
            DateTime petDate = rdr.GetDateTime(4);
            string petBreed = rdr.GetString(5);
            Pet newPet= new Pet (petName, petSex,petType,petDate,petBreed);
            allPetsAscending.Add(newPet);
          }

          conn.Close();

          if (conn != null)
          {
            conn.Dispose();
          }

          return allPetsAscending ;

        }

        public static List<Pet> SortDescending()
            {
              List<Pet> allPetsDescending = new List<Pet>{};
              MySqlConnection conn = DB.Connection();
              conn.Open();
              MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
              cmd.CommandText = @"SELECT * FROM animal ORDER by date ASC;";
              MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

              while (rdr.Read())
              {
                int petId = rdr.GetInt32(0);
                string petName = rdr.GetString(1);
                string petSex = rdr.GetString(2);
                string petType = rdr.GetString(3);
                DateTime petDate = rdr.GetDateTime(4);
                string petBreed = rdr.GetString(5);
                Pet newPet= new Pet (petName, petSex,petType,petDate,petBreed);
                allPetsDescending.Add(newPet);
              }

              conn.Close();

              if (conn != null)
              {
                conn.Dispose();
              }

              return allPetsDescending ;

            }



            public static List<Pet> FilterType(string userInput)
                  {
                    List<Pet> allPets = new List<Pet>{};
                    MySqlConnection conn = DB.Connection();
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                    cmd.CommandText = @"SELECT * FROM animal WHERE type ='" + userInput + "';";
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

                    while (rdr.Read())
                    {

                      int petId = rdr.GetInt32(0);
                      string petName = rdr.GetString(1);
                      string petSex = rdr.GetString(2);
                      string petType = rdr.GetString(3);
                      DateTime petDate = rdr.GetDateTime(4);
                      string petBreed = rdr.GetString(5);
                      Pet newPet= new Pet (petName,petSex, petType,petDate,petBreed);
                      allPets.Add(newPet);
                    }

                    conn.Close();

                    if (conn != null)
                    {
                      conn.Dispose();
                    }

                    return allPets;

                  }






  }
}
