using System;
using System.IO.IsolatedStorage;

namespace Cinemapart.UI.Helpers
{
    public class AppSettingsHelper
    {
        private readonly IsolatedStorageSettings _settings;

        public AppSettingsHelper()
        {
            _settings = IsolatedStorageSettings.ApplicationSettings;
        }

        #region Multiplex Id

        private const string MultiplexIdKey = "MultiplexIdKey";

        public int MultiplexId
        {
            get { return GetValueOrDefault(MultiplexIdKey, 18); }
            set
            {
                if (AddOrUpdateValue(MultiplexIdKey, value))
                    Save();
            }
        }

        #endregion

        #region Date Last Updated

        private const string DateLastUpdatedKey = "DateLastUpdatedKey";

        public DateTime DateLastUpdated
        {
            get { return GetValueOrDefault(DateLastUpdatedKey, DateTime.MinValue); }
            set
            {
                if (AddOrUpdateValue(DateLastUpdatedKey, value))
                    Save();
            }
        }

        #endregion

        #region Helper methods

        private bool AddOrUpdateValue(string key, object value)
        {
            var valueChanged = false;

            if (_settings.Contains(key))
            {
                if (_settings[key] != value)
                {
                    _settings[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                _settings.Add(key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        private T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            if (_settings.Contains(key))
            {
                value = (T)_settings[key];
            }
            else
            {
                value = defaultValue;
            }
            return value;
        }

        private void Save()
        {
            _settings.Save();
        }

        #endregion //Helper methods
    }
}
