
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SnakesLaddersTue
{
    public class Settings : INotifyPropertyChanged
    {
        private string grid_colour1;
        private string grid_colour2;

        public string GRID_COLOUR1
        {
            get => grid_colour1;
            set
            {
                if(value != grid_colour1) {
                    grid_colour1 = value;
                    OnPropertyChanged();
                }
            }
        }

        public string GRID_COLOUR2
        {
            get => grid_colour2;
            set
            {
                if (value != grid_colour2) {
                    grid_colour2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public Settings() {
            GRID_COLOUR1 = "#2B0B98";
            GRID_COLOUR2 = "#2B0B98";
        }

        public void SavetheSettingsFile() {
            string filename = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "settings.json");
            string jsonstring = JsonSerializer.Serialize(this);
            using (StreamWriter writer = new StreamWriter(filename)) {
                writer.Write(jsonstring);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
