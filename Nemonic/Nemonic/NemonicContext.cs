using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Windows.Forms;

namespace nemonic
{
    class NemonicContext : ApplicationContext
    {
        static string ReStartPath = NemonicApp.TemporaryPath + @"\restart.json";
        private Dictionary<string, JsonObject> Forms;

        public NemonicContext(NemonicForm form, bool startup = false)
        {
            Forms = new Dictionary<string, JsonObject>();
#if (!DEBUG)
            try
#endif
            {
                if (File.Exists(ReStartPath))
                {
                    string json = File.ReadAllText(ReStartPath);
                    JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                    List<JsonObject> objects = JsonConvert.DeserializeObject<List<JsonObject>>(json, settings);

                    if (startup && objects.Count < 1)
                    {
                        throw new ApplicationException("No need to start program");
                    }

                    foreach (JsonObject obj in objects)
                    {
                        Memo memo = obj as Memo;
                        NemonicForm fm = new NemonicForm(path: memo.path);

                        this.OpenNew(fm);
                        fm.Location = new Point(memo.x, memo.y);

                        this.Forms.Add(memo.path, memo);
                    }
                }
            }
#if (!DEBUG)
            catch (Exception e)
            {
                File.Delete(NemonicContext.ReStartPath);
            }
#endif
                if (Application.OpenForms.Count == 0)
            {
                if (startup)
                {
                    throw new ApplicationException("No need to start program");
                }
                else
                {
                    form.FormClosed += OnFormClosed;
                    form.Show();
                }
            }
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                this.ReadyToRestart();
                this.ExitThread();
            }
        }

        public void OpenNew(NemonicForm form)
        {
            form.FormClosed += OnFormClosed;
            form.Show();
        }

        public void AddFormCondition(string path, Point location)
        {
            Memo memo = new Memo()
            {
                path = path,
                x = location.X,
                y = location.Y
            };

            if (this.Forms.ContainsKey(path))
            {
                this.Forms[path] = memo;
            }
            else
            {
                this.Forms.Add(path, memo);
            }

            this.ReadyToRestart();
        }

        public void ChangeFormCondition(string path, Point location)
        {
            if (this.Forms.ContainsKey(path))
            {
                Memo memo = new Memo()
                {
                    path = path,
                    x = location.X,
                    y = location.Y
                };
                this.Forms[path] = memo;
            }
        }

        public void RemoveFormCondition(string path)
        {
            this.Forms.Remove(path);
            this.ReadyToRestart();
        }

        private void ReadyToRestart()
        {
            List<JsonObject> objects = new List<JsonObject>(this.Forms.Values);
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            File.WriteAllText(ReStartPath, JsonConvert.SerializeObject(objects, Formatting.Indented, settings));
        }
    }

    public class Memo : JsonObject
    {
        public string path;
        public int x;
        public int y;
    }

}
