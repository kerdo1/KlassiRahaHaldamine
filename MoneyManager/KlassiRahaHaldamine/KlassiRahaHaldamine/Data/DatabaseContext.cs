
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;
using KlassiRahaHaldamine.Models;


namespace KlassiRahaHaldamine.Data
{
    public class DatabaseContext : IAsyncDisposable
    {
        private const string DbName = "KlassiRahaHaldamine.db";
        private static string DbPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);

        private SQLiteAsyncConnection _connection;

        public DatabaseContext()
        {
            _connection = new SQLiteAsyncConnection(DbPath);
        }

        public async Task CreateTableIfNotExistsAsync<T>() where T : class, new()
        {
            await _connection.CreateTableAsync<T>();
        }

        public async Task<bool> AddItemAsync<T>(T item) where T : class, new()
        {
            await CreateTableIfNotExistsAsync<T>();
            return await _connection.InsertAsync(item) > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, new()
        {
            await CreateTableIfNotExistsAsync<T>();
            return await _connection.Table<T>().ToListAsync();
        }
        public async Task<bool> DeleteItemAsync<T>(T item) where T : class, new()
        {
            return await _connection.DeleteAsync(item) > 0;
        }

        public async ValueTask DisposeAsync() => await _connection.CloseAsync();
    }
}
