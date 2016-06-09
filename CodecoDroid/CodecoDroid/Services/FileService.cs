using System;
using System.Collections.Generic;
using System.Text;
using PCLStorage;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodecoDroid.Extensions;
using FileAccess = PCLStorage.FileAccess;

namespace CodecoDroid.Services
{
    /// <summary>
    /// Deals directly with the single file that maintains list of all the service IDs in use by the application.
    /// </summary>
    public class FileService
    {
        private readonly IFolder _localStorage = FileSystem.Current.LocalStorage;
        private IFile _serviceIdFile = null;
        private ObservableCollection<string> _storedServiceIds;

        public FileService()
        {
            _storedServiceIds = new ObservableCollection<string>();
        }

        public async Task<ReadOnlyObservableCollection<string>> GetServiceIdsAsync()
        {
            await OpenFile();

            _storedServiceIds = new ObservableCollection<string>();
            List<string> allFileIds = new List<string>();
            using (var fs = await _serviceIdFile.OpenAsync(FileAccess.Read))
            using(StreamReader reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync();
                    allFileIds.Add(line);
                    if (!_storedServiceIds.Contains(line))
                    {
                        _storedServiceIds.AddSorted(line);
                    }
                }
            }

            foreach (var staleId in _storedServiceIds.Except(allFileIds))
            {
                _storedServiceIds.Remove(staleId);
            }

            return new ReadOnlyObservableCollection<string>(_storedServiceIds);
        }

        public async Task AddServiceIdAsync(string newId)
        {
            await OpenFile();

            using (var fs = await _serviceIdFile.OpenAsync(FileAccess.ReadAndWrite))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                await writer.WriteLineAsync(newId);
            }

            _storedServiceIds.AddSorted(newId);
        }

        public async Task RemoveServiceIdAsync(string newId)
        {
            await OpenFile();            

            List<string> linesToSave = new List<string>();

            using (var fs = await _serviceIdFile.OpenAsync(FileAccess.ReadAndWrite))
            using (StreamReader reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    string currLine = await reader.ReadLineAsync();
                    if (currLine != newId)
                    {
                        linesToSave.Add(currLine);
                    }
                }                                
            }
            
            using (var fs = await _serviceIdFile.OpenAsync(FileAccess.ReadAndWrite))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                foreach (var line in linesToSave)
                {
                    await writer.WriteLineAsync(line);
                }
            }
        }

        private async Task OpenFile()
        {
            if(_serviceIdFile == null)
            { 
                _serviceIdFile = await _localStorage.CreateFileAsync(Constants.ServiceIdFileName, CreationCollisionOption.OpenIfExists);
            }
        }        
    }
}
