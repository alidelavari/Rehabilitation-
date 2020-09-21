using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class DB : MonoBehaviour
{
	IDbConnection dbcon;

	public void Start()
	{

	}

	public void Open()
	{
		string connection = "URI=file:" + Application.dataPath + "/../../../db/" + "rehabilitation.db";
		//string connection = "URI=file:" + Application.dataPath + "/rehabilitation.db";
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
	}

	public bool IsOpen()
	{
		return dbcon.State == ConnectionState.Open;
	}

	public int[] GetUserData()
	{
		IDbCommand cmd = dbcon.CreateCommand();
		cmd.CommandText = $"SELECT id,session FROM patients WHERE is_online = 1";
		IDataReader reader = cmd.ExecuteReader();
		int[] idSession = new int[2];
		while (reader.Read())
		{
			idSession[0] = int.Parse(reader[0].ToString());
			idSession[1] = int.Parse(reader[1].ToString());
		}
		return idSession;
	}

	public void SaveSuccess(int patient_id, int session, int game_id,
		int target_angle, int yaw_needed, int pitch_needed, int roll_needed)
	{
		IDbCommand cmd = dbcon.CreateCommand();
		cmd.CommandText = $"INSERT INTO datasets (target_angle,yaw_needed,roll_needed,pitch_needed," +
			$"patient_id,game_id,session) VALUES ({target_angle},{yaw_needed},{roll_needed}," +
			$"{pitch_needed},{patient_id}, {game_id},{session})";
		cmd.ExecuteNonQuery();
	}

	public void SaveAngles(int patient_id, int session, int game_id,
		int yaw, int pitch, int roll, int yaw_needed, int pitch_needed, int roll_needed)
	{
		IDbCommand cmd = dbcon.CreateCommand();
		cmd.CommandText = $"INSERT INTO full_dataset (yaw,pitch,roll,yaw_needed,pitch_needed" +
			$",roll_needed,patient_id,game_id,session) VALUES ({yaw},{pitch},{roll}," +
			$"{yaw_needed},{pitch_needed},{roll_needed} , {patient_id}, {game_id},{session})";
		cmd.ExecuteNonQuery();
	}

	public ArrayList LoadLevels(int patient_id)
	{
		IDbCommand cmd = dbcon.CreateCommand();
		cmd.CommandText = $"SELECT level,angle,consistancy,accepted_error,return_policy,current " +
			$"FROM archer_game_levels WHERE patient_id = {patient_id}";
		IDataReader reader = cmd.ExecuteReader();
		ArrayList levelList = new ArrayList();
		while (reader.Read())
		{
			Level level = new Level(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()),
				float.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()), int.Parse(reader[4].ToString()),
				bool.Parse(reader[5].ToString()));
			levelList.Add(level);
		}
		return levelList;
	}

	public void BookMarkLevel(int patientId, int levelId)
	{
		IDbCommand cmd2 = dbcon.CreateCommand();
		cmd2.CommandText = $"UPDATE archer_game_levels SET current = 0 WHERE current = 1";
		cmd2.ExecuteNonQuery();
		IDbCommand cmd1 = dbcon.CreateCommand();
		cmd1.CommandText = $"UPDATE archer_game_levels SET current = 1 WHERE patient_id = {patientId}";
		cmd1.ExecuteNonQuery();
	}

	public void Close()
	{
		dbcon.Close();
	}

	// Update is called once per frame
	public void Update()
	{

	}

	internal void SaveAngles(int v1, int v2, int v3, int currentAngle1, float currentAngle2, float currentAngle3, bool v4, bool v5, bool v6)
	{
		throw new NotImplementedException();
	}
}
