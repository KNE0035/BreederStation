using System;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using System.Collections.Generic;
using BreederStationDataLayer.Orm.Dto;
using System.Data.Common;
using BreederStationDataLayer.Database;
using System.Data.SqlClient;
using System.Data;
using BreederStationDataLayer.Orm.SelectCriteria;

namespace BreederStationDataLayer.Orm.Dao
{
    public class SqlServerAnimalGateway : AnimalGateway
    {
        public static string TABLE_NAME = "Animal";

        private static string SQL_INSERT = "INSERT INTO Animal (NAME, RACE, FOOD_ID, CAGE_ID, ANIMAL_GROUP_ID, BIRTH_DATE, SEX, ACTIVE)" +
                                           " VALUES (@p_name, @p_race, @p_food_id, @p_cage_id, @p_animal_group_id, @p_birth_date, @p_sex, @p_active)";

        private static string SQL_SELECT = "SELECT a.id, a.NAME, a.RACE, a.BIRTH_DATE, a.SEX, a.ACTIVE, ag.ID, ag.DESCRIPTION, c.ID, c.LENGTH_M, c.WIDTH_M," +
                                                   " f.ID, f.name, f.PRICE, f.PROTEINS, f.CARBOHYDRATES, f.FAT, f.FOOD_RUNNING_OUT, f.COMPANY_ID" +
                                           " FROM ANIMAL a" +
                                           " JOIN ANIMAL_GROUP ag ON a.ANIMAL_GROUP_ID = ag.ID" +
                                           " JOIN CAGE c ON a.CAGE_ID = c.ID" +
                                           " JOIN FOOD f ON a.FOOD_ID = f.ID";

        private static string SQL_SELECT_ID = "SELECT a.id, a.NAME, a.RACE, a.BIRTH_DATE, a.SEX, a.ACTIVE, ag.ID, ag.DESCRIPTION, c.ID, c.LENGTH_M, c.WIDTH_M," +
                                                   " f.ID, f.name, f.PRICE, f.PROTEINS, f.CARBOHYDRATES, f.FAT, f.FOOD_RUNNING_OUT, f.COMPANY_ID" +
                                           " FROM ANIMAL a" +
                                           " JOIN ANIMAL_GROUP ag ON a.ANIMAL_GROUP_ID = ag.ID" +
                                           " JOIN CAGE c ON a.CAGE_ID = c.ID" +
                                           " JOIN FOOD f ON a.FOOD_ID = f.ID" +
                                           " WHERE a.active = 1 AND a.id =@p_id";


        private static string SQL_UPDATE = "UPDATE ANIMAL SET NAME=@p_name, RACE=@p_race, FOOD_ID=@p_food_id, CAGE_ID=@p_cage_id, ANIMAL_GROUP_ID=@p_animal_group_id, BIRTH_DATE=@p_birth_date, SEX=@p_sex WHERE id=@p_id";

        private static string SQL_DELETE_ID = "UPDATE ANIMAL SET active=@p_active, last_active_date=@p_last_active_date WHERE id=@p_id";

        public SqlServerAnimalGateway(IDatabaseService db) : base(db)
        {

        }
        protected override void PrepareInsertUpdateCommand(DbCommand command, Animal animal)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_name", SqlDbType.VarChar).Value = animal.Name;
            sqlCommand.Parameters.Add("@p_race", SqlDbType.VarChar).Value = animal.Race;
            sqlCommand.Parameters.Add("@p_food_id", SqlDbType.Int).Value = animal.Food.Id;
            sqlCommand.Parameters.Add("@p_cage_id", SqlDbType.VarChar).Value = animal.Cage.Id;
            sqlCommand.Parameters.Add("@p_animal_group_id", SqlDbType.VarChar).Value = animal.AnimalGroup.Id;
            sqlCommand.Parameters.Add("@p_sex", SqlDbType.VarChar).Value = animal.Sex.ToString();
            sqlCommand.Parameters.Add("@p_active", SqlDbType.Bit).Value = animal.Active;
            sqlCommand.Parameters.Add("@p_birth_date", SqlDbType.Date).Value = animal.BirthDate;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = animal.Id;
        }

        protected override void PrepareSelectIdCommand(DbCommand command, int animalId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = animalId;
        }

        protected override void PrepareDeleteCommand(DbCommand command, int animalId)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.Parameters.Add("@p_active", SqlDbType.Bit).Value = 0;
            sqlCommand.Parameters.Add("@p_last_active_date", SqlDbType.Date).Value = DateTime.Now;
            sqlCommand.Parameters.Add("@p_id", SqlDbType.Int).Value = animalId;
        }

        protected override IList<Animal> Read(DbDataReader reader)
        {
            IList<Animal> animals = new List<Animal>();
            SqlDataReader sqlReader = (SqlDataReader)reader;

            while (sqlReader.Read())
            {
                int i = -1;
                Animal animal = new Animal();
                animal.Id = sqlReader.GetInt32(++i);
                animal.Name = sqlReader.GetString(++i);
                animal.Race = sqlReader.GetString(++i);
                animal.BirthDate = sqlReader.GetDateTime(++i);
                animal.Sex = SexEnumUtils.getSexEnum(sqlReader.GetString(++i));
                animal.Active = sqlReader.GetBoolean(++i);

                animal.AnimalGroup = new AnimalGroup();
                animal.AnimalGroup.Id = sqlReader.GetInt32(++i);
                animal.AnimalGroup.Description = sqlReader.GetString(++i);

                animal.Cage = new Cage();
                animal.Cage.Id = sqlReader.GetInt32(++i);
                animal.Cage.LengthM = sqlReader.GetInt32(++i);
                animal.Cage.WidthM = sqlReader.GetInt32(++i);

                animal.Food = new Food();
                animal.Food.Id = sqlReader.GetInt32(++i);
                animal.Food.Name = sqlReader.GetString(++i);
                animal.Food.Price = (double)sqlReader.GetDecimal(++i);
                animal.Food.Proteins = sqlReader.GetInt32(++i);
                animal.Food.Carbohydrates = sqlReader.GetInt32(++i);
                animal.Food.Fat = sqlReader.GetInt32(++i);
                animal.Food.FoodRunningOut = sqlReader.GetBoolean(++i);

                animal.Food.Company = new Company();
                animal.Food.Company.Id = sqlReader.GetInt32(++i);

                animals.Add(animal);
            }
            return animals;
        }

        internal Animal Select(object p)
        {
            throw new NotImplementedException();
        }

        protected override void applyCriteria(AnimalCriteria criteria, DbCommand command)
        {
            SqlCommand sqlCommand = (SqlCommand)command;
            sqlCommand.CommandText += " WHERE ACTIVE = 1";
            if (criteria.AnimalGroupId != null)
            {
                sqlCommand.CommandText += " AND ag.id=@c_animal_group_id";
                sqlCommand.Parameters.Add("@c_animal_group_id", SqlDbType.Int).Value = criteria.AnimalGroupId;
            }

            if (criteria.CageId != null)
            {
                sqlCommand.CommandText += " AND c.id=@c_cage_id";
                sqlCommand.Parameters.Add("@c_cage_id", SqlDbType.Int).Value = criteria.CageId;
            }
        }

        protected override string GetInsertSql()
        {
            return SQL_INSERT;
        }

        protected override string GetDeleteSql()
        {
            return SQL_DELETE_ID;
        }

        protected override string GetUpdateSql()
        {
            return SQL_UPDATE;
        }

        protected override string GetSelectSql()
        {
            return SQL_SELECT;
        }

        protected override string GetSelectIdSql()
        {
            return SQL_SELECT_ID;
        }
    }
}
