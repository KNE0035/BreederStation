using System;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using System.Collections.Generic;
using BreederStationDataLayer.Orm.Dto;
using System.Data.Common;
using DAIS_KNE0035.Orm.SelectCriteria;
using BreederStationDataLayer.Database;

namespace BreederStationDataLayer.Orm.Dao
{
    public class OracleAnimalGateway : AnimalGateway
    {
        public static string TABLE_NAME = "Animal";

        private static string SQL_INSERT = "INSERT INTO Animal (NAME, RACE, FOOD_ID, CAGE_ID, ANIMAL_GROUP_ID, BIRTH_DATE, SEX, ACTIVE)" +
                                           " VALUES (:p_name, :p_race, :p_food_id, :p_cage_id, :p_animal_group_id, :p_birth_date, :p_sex, :p_active)";

        private static string SQL_SELECT = @"SELECT a.id, a.NAME, a.RACE, a.BIRTH_DATE, a.SEX, a.ACTIVE, ag.ID, ag.DESCRIPTION, c.ID, c.LENGTH_M, c.WIDTH_M,
                                                   f.ID, f.name, f.PRICE, f.PROTEINS, f.CARBOHYDRATES, f.FAT, f.FOOD_RUNNING_OUT, f.COMPANY_ID
                                           FROM ANIMAL a
                                           JOIN ANIMAL_GROUP ag ON a.ANIMAL_GROUP_ID = ag.ID
                                           JOIN CAGE c ON a.CAGE_ID = c.ID
                                           JOIN FOOD f ON a.FOOD_ID = f.ID";

        private static string SQL_SELECT_ID = @"SELECT a.id, a.NAME, a.RACE, a.BIRTH_DATE, a.SEX, a.ACTIVE, ag.ID, ag.DESCRIPTION, c.ID, c.LENGTH_M, c.WIDTH_M,
                                                    f.ID, f.name, f.PRICE, f.PROTEINS, f.CARBOHYDRATES, f.FAT, f.FOOD_RUNNING_OUT, f.COMPANY_ID
                                            FROM ANIMAL a
                                            JOIN ANIMAL_GROUP ag ON a.ANIMAL_GROUP_ID = ag.ID
                                            JOIN CAGE c ON a.CAGE_ID = c.ID
                                            JOIN FOOD f ON a.FOOD_ID = f.ID
                                            WHERE a.active = 1 AND a.id =:p_id";


        private static string SQL_UPDATE = "UPDATE ANIMAL SET NAME=:p_name, RACE=:p_race, FOOD_ID=:p_food_id, CAGE_ID=:p_cage_id, ANIMAL_GROUP_ID=:p_animal_group_id, BIRTH_DATE=:p_birth_date, SEX=:p_sex WHERE id=:p_id";

        private static string SQL_DELETE_ID = "UPDATE ANIMAL SET active=:p_active, last_active_date=:p_last_active_date WHERE id=:p_id";

        public OracleAnimalGateway(IDatabaseService db) : base(db)
        {

        }
        protected override void PrepareInsertUpdateCommand(DbCommand command, Animal animal)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_name", OracleDbType.Varchar2).Value = animal.Name;
            oracleCommand.Parameters.Add("p_race", OracleDbType.Varchar2).Value = animal.Race;
            oracleCommand.Parameters.Add("p_food_id", OracleDbType.Int32).Value = animal.Food.Id;
            oracleCommand.Parameters.Add("p_cage_id", OracleDbType.Varchar2).Value = animal.Cage.Id;
            oracleCommand.Parameters.Add("p_animal_group_id", OracleDbType.Varchar2).Value = animal.AnimalGroup.Id;
            oracleCommand.Parameters.Add("p_sex", OracleDbType.Varchar2).Value = animal.Sex.ToString();
            oracleCommand.Parameters.Add("p_active", OracleDbType.Char).Value = animal.Active == true ? 1 : 0;
            oracleCommand.Parameters.Add("p_birth_date", OracleDbType.Date).Value = animal.BirthDate;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = animal.Id;
        }

        protected override void PrepareSelectIdCommand(DbCommand command, int animalId)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = animalId;
        }

        protected override void PrepareDeleteCommand(DbCommand command, int animalId)
        {
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add("p_active", OracleDbType.Char).Value = 0;
            oracleCommand.Parameters.Add("p_last_active_date", OracleDbType.Date).Value = DateTime.Now;
            oracleCommand.Parameters.Add("p_id", OracleDbType.Int32).Value = animalId;
        }

        protected override IList<Animal> Read(DbDataReader reader)
        {
            IList<Animal> animals = new List<Animal>();
            OracleDataReader oracleReader = (OracleDataReader)reader;
            
            while (oracleReader.Read())
            {
                int i = -1;
                Animal animal = new Animal();
                animal.Id = oracleReader.GetInt32(++i);
                animal.Name = oracleReader.GetString(++i);
                animal.Race = oracleReader.GetString(++i);
                animal.BirthDate = oracleReader.GetDateTime(++i);
                animal.Sex = SexEnumUtils.getSexEnum(oracleReader.GetString(++i));
                animal.Active = oracleReader.GetString(++i) == "1" ? true : false;

                animal.AnimalGroup = new AnimalGroup();
                animal.AnimalGroup.Id = oracleReader.GetInt32(++i);
                animal.AnimalGroup.Description = oracleReader.GetString(++i);

                animal.Cage = new Cage();
                animal.Cage.Id = oracleReader.GetInt32(++i);
                animal.Cage.LengthM = oracleReader.GetInt32(++i);
                animal.Cage.WidthM = oracleReader.GetInt32(++i);

                animal.Food = new Food();
                animal.Food.Id = oracleReader.GetInt32(++i);
                animal.Food.Name = oracleReader.GetString(++i);
                animal.Food.Price = oracleReader.GetDouble(++i);
                animal.Food.Proteins = oracleReader.GetInt32(++i);
                animal.Food.Carbohydrates = oracleReader.GetInt32(++i);
                animal.Food.Fat = oracleReader.GetInt32(++i);
                animal.Food.FoodRunningOut = oracleReader.GetString(++i) == "1" ? true : false;

                animal.Food.Company = new Company();
                animal.Food.Company.Id = oracleReader.GetInt32(++i);

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
            OracleCommand oracleCommand = (OracleCommand)command;
            oracleCommand.BindByName = true;
            oracleCommand.CommandText += " WHERE ACTIVE = 1";
            if (criteria.AnimalGroupId != null)
            {
                oracleCommand.CommandText += " AND ag.id=:c_animal_group_id";
                oracleCommand.Parameters.Add("c_animal_group_id", OracleDbType.Int32).Value = criteria.AnimalGroupId;
            }

            if (criteria.CageId != null)
            {
                oracleCommand.CommandText += " AND c.id=:c_cage_id";
                oracleCommand.Parameters.Add("c_cage_id", OracleDbType.Int32).Value = criteria.CageId;
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
