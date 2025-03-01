﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RecipeManagement.Models;

namespace RecipeManagement.Data
{
    public class RecipeData
    {
        string _connectionString = "Server=tcp:23.102.239.48,1433;Database=FoodRecipe;User Id=sa;Password=ravencardeno_11";

        public List<Recipe> GetRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Name, Description FROM Recipes";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Recipe recipe = new Recipe
                    {
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString()
                    };
                   
                    recipes.Add(recipe);
                }
                connection.Close();
            }

            return recipes;
        }

        public int AddRecipe(Recipe recipe)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Recipes (Name, Description) VALUES (@Name, @Description)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", recipe.Name);
                command.Parameters.AddWithValue("@Description", recipe.Description);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int UpdateRecipe(Recipe recipe)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Recipes SET Description = @Description WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", recipe.Name);
                command.Parameters.AddWithValue("@Description", recipe.Description);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

        public int DeleteRecipe(string recipeName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Recipes WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", recipeName);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected;
            }
        }

    }
}
